using System;
using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.Docker;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities;
using Serilog;
using TextCopy;
using static AzTasks;
using static Nuke.Common.Tools.Docker.DockerTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
// ReSharper disable UnusedMember.Local

[GitHubActions(
    "continuous",
    GitHubActionsImage.UbuntuLatest,
    On = new[] { GitHubActionsTrigger.Push },
    InvokedTargets = new[] { nameof(Publish) },
    ImportSecrets = new[] { nameof(ApiToken) })]
[CheckBuildProjectConfigurations]
partial class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode
    public static int Main() => Execute<Build>();

    [GitRepository] readonly GitRepository Repository;

    [PathExecutable] readonly Tool Az;

    [Parameter] readonly string Name;
    [Parameter] readonly string Location;
    [Parameter] readonly string ResourceGroup;

    AbsolutePath TemplateFile => BuildProjectDirectory / "create-swa.json";

    // nuke create --name azure-swa-demo --resource-group DEMOS
    Target Create => _ => _
        .Requires(() => Name)
        .Executes(() =>
        {
            AzDeploymentGroupCreate(_ => _
                .SetResourceGroup(ResourceGroup)
                .SetTemplateFile(TemplateFile)
                .AddParameter("name", Name)
                .AddParameter("location", Location));

            SaveParameter(() => Name);
        });

    [Parameter] readonly string User;
    [Parameter] readonly AzAuthenticationProvider Provider;
    [Parameter] readonly string[] Roles;
    [Parameter] readonly int InvitationExpiration = 1;

    // nuke invite --user matkoch --roles admin
    Target Invite => _ => _
        .Requires(() => User)
        .Requires(() => Provider)
        .Requires(() => Roles)
        .Executes(() =>
        {
            // az staticwebapp users invite --name ...
            var invitationLink = AzStaticWebAppUsersInvite(_ => _
                .SetName(Name)
                .SetAuthenticationProvider(Provider)
                .SetRoles(Roles)
                .SetUserDetails(User)
                .SetDomain(GetDomain())
                .SetInvitationExpirationInHours(InvitationExpiration)).Result;

            ClipboardService.SetText(invitationLink);
        });

    Target Test => _ => _
        .Executes(() =>
        {
            // dotnet test SwaApi.Tests.csproj --logger trx;LogFileName=SwaApi.Tests.trx    --results-directory ... --configuration Release
            // dotnet test SwaApp.Tests.csproj --logger trx;LogFileName=SwaApp.Tests.trx    --results-directory ... --configuration Release
            // dotnet test Something.Tests.csproj --logger trx;LogFileName=SwaApp.Tests.trx    --results-directory ... --configuration Release
            DotNetTest(_ => _
                .ResetVerbosity()
                .SetResultsDirectory(RootDirectory / "output" / "test-results")
                .SetConfiguration(Configuration.Release)
                .CombineWith(Solution.GetProjects("*.Tests"), (_, v) => _
                    .SetProjectFile(v)
                    .AddLoggers($"trx;LogFileName={v.Name}.trx")));
        });

    [Solution(GenerateProjects = true)] readonly Solution Solution;
    [Parameter] [Secret] readonly string ApiToken;

    // nuke deploy
    Target Publish => _ => _
        .Requires(() => ApiToken != null || IsLocalBuild)
        .DependsOn(Test)
        .Executes(() =>
        {
            // docker run --workdir /deploy
            //            --platform linux/amd64 <image>
            //            ...
            //     /bin/staticsites/StaticSitesClient
            //          run
            //          --app src/SwaApp
            //          --api src/SwaApi
            //          --outputLocation wwwroot
            //          --apiToken ***
            //          --deploymentaction upload
            DockerRun(_ => _
                .SetImage("mcr.microsoft.com/appsvc/staticappsclient:stable")
                .SetWorkdir("/deploy")
                .AddVolume($"{RootDirectory}:/deploy")
                .SetPlatform("linux/amd64")
                .AddArgs((StaticSitesClientSettings _) => _
                    .SetDeploymentAction("upload")
                    .SetApiToken(ApiToken ?? GetApiToken())
                    .SetApiLocation(RootDirectory.GetUnixRelativePathTo(Solution.SwaApi.Directory))
                    .SetAppLocation(RootDirectory.GetUnixRelativePathTo(Solution.SwaApp.Directory))
                    .SetOutputLocation("wwwroot")));
        });

    [Parameter] string CustomApexDomain;
    string CustomWwwDomain => $"www.{CustomApexDomain}";

    Target CustomDomain => _ => _
        .Requires(() => CustomApexDomain)
        .Executes(() =>
        {
            Az("staticwebapp hostname set " +
               $"--name {Name} " +
               $"--resource-group {ResourceGroup} " +
               $"--hostname {CustomWwwDomain}");

            var validationToken = Az("staticwebapp hostname show " +
                            $"--name {Name} " +
                            $"--resource-group {ResourceGroup} " +
                            $"--hostname {CustomApexDomain} " +
                            "--query validationToken")
                    .StdToText();
            Log.Information("Set a TXT record {ValidationToken} and hit [Enter] to continue...", validationToken);
            Console.ReadKey();

            Az("staticwebapp hostname set " +
               $"--name {Name} " +
               $"--resource-group {ResourceGroup} " +
               $"--hostname {CustomApexDomain} " +
               "--validation-method dns-txt-token");
        });

    string GetApiToken()
    {
        var result = Az($"staticwebapp secrets list --name {Name}", logOutput: false).StdOutputToJson();
        return result.GetPropertyValue("properties").GetPropertyStringValue("apiKey");
    }

    string GetDomain()
    {
        var result = Az($"staticwebapp show --name {Name}", logOutput: false).StdOutputToJson();
        return result.GetPropertyStringValue("defaultHostname");
    }
}
