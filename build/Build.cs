using System;
using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.Npm;
using Nuke.Common.Utilities;
using Nuke.Common.Utilities.Collections;
using Serilog;
using TextCopy;
using static Nuke.Common.IO.FileSystemTasks;

// ReSharper disable UnusedMember.Local

[GitHubActions(
    "continuous",
    GitHubActionsImage.UbuntuLatest,
    On = new[] { GitHubActionsTrigger.Push },
    InvokedTargets = new[] { nameof(Publish) },
    ImportSecrets = new[] { nameof(ApiToken), "SyncfusionLicenseKey" })]
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
            AzTasks.AzDeploymentGroupCreate(_ => _
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
            var invitationLink = AzTasks.AzStaticWebAppUsersInvite(_ => _
                .SetName(Name)
                .SetAuthenticationProvider(Provider)
                .SetRoles(Roles)
                .SetUserDetails(User)
                .SetDomain(GetDomain())
                .SetInvitationExpirationInHours(InvitationExpiration)).Result;

            ClipboardService.SetText(invitationLink);
        });

    AbsolutePath SourceDirectory => RootDirectory / "src";

    Target Clean => _ => _
        .Before(Compile, Test)
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories("*/bin", "*/obj").ForEach(DeleteDirectory);
        });

    [Solution(GenerateProjects = true)] readonly Solution Solution;
    [Parameter] Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    Target Compile => _ => _
        .Executes(() =>
        {
            // dotnet publish src/SwaApp -c [configuration]
            DotNetTasks.DotNetPublish(_ => _
                .SetProject(Solution.SwaApp)
                .SetConfiguration(Configuration));

            DotNetTasks.DotNetPublish(_ => _
                .SetProject(Solution.SwaApi)
                .SetConfiguration(Configuration));
        });

    Target Test => _ => _
        .After(Compile)
        .Executes(() =>
        {
            // dotnet test SwaApi.Tests.csproj    --logger trx;LogFileName=SwaApi.Tests.trx --results-directory ... --configuration Release
            // dotnet test SwaApp.Tests.csproj    --logger trx;LogFileName=SwaApp.Tests.trx --results-directory ... --configuration Release
            // dotnet test Something.Tests.csproj --logger trx;LogFileName=SwaApp.Tests.trx --results-directory ... --configuration Release
            DotNetTasks.DotNetTest(_ => _
                .ResetVerbosity()
                .SetResultsDirectory(RootDirectory / "output" / "test-results")
                .SetConfiguration(Configuration)
                .CombineWith(Solution.GetProjects("*.Tests"), (_, v) => _
                    .SetProjectFile(v)
                    .AddLoggers($"trx;LogFileName={v.Name}.trx")));
        });

    [Parameter] [Secret] readonly string ApiToken;

    string Environment => Repository.IsOnMainOrMasterBranch() ? "production" : Repository.Branch;

    // nuke deploy
    Target Publish => _ => _
        .Requires(() => ApiToken != null || IsLocalBuild)
        .DependsOn(Compile)
        .DependsOn(Test)
        .Executes(() =>
        {
            NpmTasks.NpmInstall(_ => _
                .SetPackages("@azure/static-web-apps-cli")
                .EnableGlobal());

            StaticWebAppsTasks.StaticWebAppsDeploy(_ => _
                .SetConfigLocation(RootDirectory / "src")
                .SetAppLocation(Solution.SwaApp.Directory)
                .SetOutputLocation(Solution.SwaApp.Directory / "bin" / Configuration / "net6.0" / "publish" / "wwwroot")
                .SetApiLocation(Solution.SwaApi.Directory / "bin" / Configuration / "net6.0" / "publish")
                .SetEnvironment(Environment)
                .SetDeploymentToken(ApiToken ?? GetApiToken()));
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
