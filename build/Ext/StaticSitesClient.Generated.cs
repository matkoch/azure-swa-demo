
using JetBrains.Annotations;
using Newtonsoft.Json;
using Nuke.Common;
using Nuke.Common.Tooling;
using Nuke.Common.Tools;
using Nuke.Common.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;


/// <summary>
///   <p>For more details, visit the <a href="">official website</a>.</p>
/// </summary>
[PublicAPI]
[ExcludeFromCodeCoverage]
public partial class StaticSitesClientTasks
{
    /// <summary>
    ///   Path to the StaticSitesClient executable.
    /// </summary>
    public static string StaticSitesClientPath =>
        ToolPathResolver.TryGetEnvironmentExecutable("STATICSITESCLIENT_EXE") ??
        GetToolPath();
    public static Action<OutputType, string> StaticSitesClientLogger { get; set; } = ProcessTasks.DefaultLogger;
    public static Action<ToolSettings, IProcess> StaticSitesClientExitHandler { get; set; } = ProcessTasks.DefaultExitHandler;
    /// <summary>
    ///   <p>For more details, visit the <a href="">official website</a>.</p>
    /// </summary>
    public static IReadOnlyCollection<Output> StaticSitesClient(ArgumentStringHandler arguments, string workingDirectory = null, IReadOnlyDictionary<string, string> environmentVariables = null, int? timeout = null, bool? logOutput = null, bool? logInvocation = null, Action<OutputType, string> logger = null, Action<IProcess> exitHandler = null)
    {
        using var process = ProcessTasks.StartProcess(StaticSitesClientPath, arguments, workingDirectory, environmentVariables, timeout, logOutput, logInvocation, logger ?? StaticSitesClientLogger);
        (exitHandler ?? (p => StaticSitesClientExitHandler.Invoke(null, p))).Invoke(process.AssertWaitForExit());
        return process.Output;
    }
    /// <summary>
    ///   <p>For more details, visit the <a href="">official website</a>.</p>
    /// </summary>
    /// <remarks>
    ///   <p>This is a <a href="http://www.nuke.build/docs/authoring-builds/cli-tools.html#fluent-apis">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p>
    ///   <ul>
    ///     <li><c>--api</c> via <see cref="StaticSitesClientSettings.ApiLocation"/></li>
    ///     <li><c>--apiBuildCommand</c> via <see cref="StaticSitesClientSettings.ApiBuildCommand"/></li>
    ///     <li><c>--apiToken</c> via <see cref="StaticSitesClientSettings.ApiToken"/></li>
    ///     <li><c>--app</c> via <see cref="StaticSitesClientSettings.AppLocation"/></li>
    ///     <li><c>--appArtifactLocation</c> via <see cref="StaticSitesClientSettings.AppArtifactLocation"/></li>
    ///     <li><c>--appBuildCommand</c> via <see cref="StaticSitesClientSettings.AppBuildCommand"/></li>
    ///     <li><c>--branch</c> via <see cref="StaticSitesClientSettings.Branch"/></li>
    ///     <li><c>--buildTimeoutInMinutes</c> via <see cref="StaticSitesClientSettings.BuildTimeoutInMinutes"/></li>
    ///     <li><c>--configFileLocation</c> via <see cref="StaticSitesClientSettings.ConfigFileLocation"/></li>
    ///     <li><c>--deploymentaction</c> via <see cref="StaticSitesClientSettings.DeploymentAction"/></li>
    ///     <li><c>--deploymentProvider</c> via <see cref="StaticSitesClientSettings.DeploymentProvider"/></li>
    ///     <li><c>--environmentName</c> via <see cref="StaticSitesClientSettings.EnvironmentName"/></li>
    ///     <li><c>--event</c> via <see cref="StaticSitesClientSettings.EventJsonPath"/></li>
    ///     <li><c>--headBranch</c> via <see cref="StaticSitesClientSettings.HeadBranch"/></li>
    ///     <li><c>--oryxEnabled</c> via <see cref="StaticSitesClientSettings.OryxEnabled"/></li>
    ///     <li><c>--outputLocation</c> via <see cref="StaticSitesClientSettings.OutputLocation"/></li>
    ///     <li><c>--pullRequestTitle</c> via <see cref="StaticSitesClientSettings.PullRequestTitle"/></li>
    ///     <li><c>--repositoryUrl</c> via <see cref="StaticSitesClientSettings.RepositoryUrl"/></li>
    ///     <li><c>--repoToken</c> via <see cref="StaticSitesClientSettings.RepoToken"/></li>
    ///     <li><c>--routesLocation</c> via <see cref="StaticSitesClientSettings.RoutesLocation"/></li>
    ///     <li><c>--skipAppBuild</c> via <see cref="StaticSitesClientSettings.SkipAppBuild"/></li>
    ///     <li><c>--verbose</c> via <see cref="StaticSitesClientSettings.Verbose"/></li>
    ///   </ul>
    /// </remarks>
    public static IReadOnlyCollection<Output> StaticSitesClient(StaticSitesClientSettings toolSettings = null)
    {
        toolSettings = toolSettings ?? new StaticSitesClientSettings();
        using var process = ProcessTasks.StartProcess(toolSettings);
        toolSettings.ProcessExitHandler.Invoke(toolSettings, process.AssertWaitForExit());
        return process.Output;
    }
    /// <summary>
    ///   <p>For more details, visit the <a href="">official website</a>.</p>
    /// </summary>
    /// <remarks>
    ///   <p>This is a <a href="http://www.nuke.build/docs/authoring-builds/cli-tools.html#fluent-apis">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p>
    ///   <ul>
    ///     <li><c>--api</c> via <see cref="StaticSitesClientSettings.ApiLocation"/></li>
    ///     <li><c>--apiBuildCommand</c> via <see cref="StaticSitesClientSettings.ApiBuildCommand"/></li>
    ///     <li><c>--apiToken</c> via <see cref="StaticSitesClientSettings.ApiToken"/></li>
    ///     <li><c>--app</c> via <see cref="StaticSitesClientSettings.AppLocation"/></li>
    ///     <li><c>--appArtifactLocation</c> via <see cref="StaticSitesClientSettings.AppArtifactLocation"/></li>
    ///     <li><c>--appBuildCommand</c> via <see cref="StaticSitesClientSettings.AppBuildCommand"/></li>
    ///     <li><c>--branch</c> via <see cref="StaticSitesClientSettings.Branch"/></li>
    ///     <li><c>--buildTimeoutInMinutes</c> via <see cref="StaticSitesClientSettings.BuildTimeoutInMinutes"/></li>
    ///     <li><c>--configFileLocation</c> via <see cref="StaticSitesClientSettings.ConfigFileLocation"/></li>
    ///     <li><c>--deploymentaction</c> via <see cref="StaticSitesClientSettings.DeploymentAction"/></li>
    ///     <li><c>--deploymentProvider</c> via <see cref="StaticSitesClientSettings.DeploymentProvider"/></li>
    ///     <li><c>--environmentName</c> via <see cref="StaticSitesClientSettings.EnvironmentName"/></li>
    ///     <li><c>--event</c> via <see cref="StaticSitesClientSettings.EventJsonPath"/></li>
    ///     <li><c>--headBranch</c> via <see cref="StaticSitesClientSettings.HeadBranch"/></li>
    ///     <li><c>--oryxEnabled</c> via <see cref="StaticSitesClientSettings.OryxEnabled"/></li>
    ///     <li><c>--outputLocation</c> via <see cref="StaticSitesClientSettings.OutputLocation"/></li>
    ///     <li><c>--pullRequestTitle</c> via <see cref="StaticSitesClientSettings.PullRequestTitle"/></li>
    ///     <li><c>--repositoryUrl</c> via <see cref="StaticSitesClientSettings.RepositoryUrl"/></li>
    ///     <li><c>--repoToken</c> via <see cref="StaticSitesClientSettings.RepoToken"/></li>
    ///     <li><c>--routesLocation</c> via <see cref="StaticSitesClientSettings.RoutesLocation"/></li>
    ///     <li><c>--skipAppBuild</c> via <see cref="StaticSitesClientSettings.SkipAppBuild"/></li>
    ///     <li><c>--verbose</c> via <see cref="StaticSitesClientSettings.Verbose"/></li>
    ///   </ul>
    /// </remarks>
    public static IReadOnlyCollection<Output> StaticSitesClient(Configure<StaticSitesClientSettings> configurator)
    {
        return StaticSitesClient(configurator(new StaticSitesClientSettings()));
    }
    /// <summary>
    ///   <p>For more details, visit the <a href="">official website</a>.</p>
    /// </summary>
    /// <remarks>
    ///   <p>This is a <a href="http://www.nuke.build/docs/authoring-builds/cli-tools.html#fluent-apis">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p>
    ///   <ul>
    ///     <li><c>--api</c> via <see cref="StaticSitesClientSettings.ApiLocation"/></li>
    ///     <li><c>--apiBuildCommand</c> via <see cref="StaticSitesClientSettings.ApiBuildCommand"/></li>
    ///     <li><c>--apiToken</c> via <see cref="StaticSitesClientSettings.ApiToken"/></li>
    ///     <li><c>--app</c> via <see cref="StaticSitesClientSettings.AppLocation"/></li>
    ///     <li><c>--appArtifactLocation</c> via <see cref="StaticSitesClientSettings.AppArtifactLocation"/></li>
    ///     <li><c>--appBuildCommand</c> via <see cref="StaticSitesClientSettings.AppBuildCommand"/></li>
    ///     <li><c>--branch</c> via <see cref="StaticSitesClientSettings.Branch"/></li>
    ///     <li><c>--buildTimeoutInMinutes</c> via <see cref="StaticSitesClientSettings.BuildTimeoutInMinutes"/></li>
    ///     <li><c>--configFileLocation</c> via <see cref="StaticSitesClientSettings.ConfigFileLocation"/></li>
    ///     <li><c>--deploymentaction</c> via <see cref="StaticSitesClientSettings.DeploymentAction"/></li>
    ///     <li><c>--deploymentProvider</c> via <see cref="StaticSitesClientSettings.DeploymentProvider"/></li>
    ///     <li><c>--environmentName</c> via <see cref="StaticSitesClientSettings.EnvironmentName"/></li>
    ///     <li><c>--event</c> via <see cref="StaticSitesClientSettings.EventJsonPath"/></li>
    ///     <li><c>--headBranch</c> via <see cref="StaticSitesClientSettings.HeadBranch"/></li>
    ///     <li><c>--oryxEnabled</c> via <see cref="StaticSitesClientSettings.OryxEnabled"/></li>
    ///     <li><c>--outputLocation</c> via <see cref="StaticSitesClientSettings.OutputLocation"/></li>
    ///     <li><c>--pullRequestTitle</c> via <see cref="StaticSitesClientSettings.PullRequestTitle"/></li>
    ///     <li><c>--repositoryUrl</c> via <see cref="StaticSitesClientSettings.RepositoryUrl"/></li>
    ///     <li><c>--repoToken</c> via <see cref="StaticSitesClientSettings.RepoToken"/></li>
    ///     <li><c>--routesLocation</c> via <see cref="StaticSitesClientSettings.RoutesLocation"/></li>
    ///     <li><c>--skipAppBuild</c> via <see cref="StaticSitesClientSettings.SkipAppBuild"/></li>
    ///     <li><c>--verbose</c> via <see cref="StaticSitesClientSettings.Verbose"/></li>
    ///   </ul>
    /// </remarks>
    public static IEnumerable<(StaticSitesClientSettings Settings, IReadOnlyCollection<Output> Output)> StaticSitesClient(CombinatorialConfigure<StaticSitesClientSettings> configurator, int degreeOfParallelism = 1, bool completeOnFailure = false)
    {
        return configurator.Invoke(StaticSitesClient, StaticSitesClientLogger, degreeOfParallelism, completeOnFailure);
    }
}
#region StaticSitesClientSettings
/// <summary>
///   Used within <see cref="StaticSitesClientTasks"/>.
/// </summary>
[PublicAPI]
[ExcludeFromCodeCoverage]
[Serializable]
public partial class StaticSitesClientSettings : ToolSettings
{
    /// <summary>
    ///   Path to the StaticSitesClient executable.
    /// </summary>
    public override string ProcessToolPath => base.ProcessToolPath ?? StaticSitesClientTasks.StaticSitesClientPath;
    public override Action<OutputType, string> ProcessLogger => base.ProcessLogger ?? StaticSitesClientTasks.StaticSitesClientLogger;
    public override Action<ToolSettings, IProcess> ProcessExitHandler => base.ProcessExitHandler ?? StaticSitesClientTasks.StaticSitesClientExitHandler;
    /// <summary>
    ///   Enables verbose logging.
    /// </summary>
    public virtual bool? Verbose { get; internal set; }
    /// <summary>
    ///   Time limit of oryx build in minutes.
    /// </summary>
    public virtual int? BuildTimeoutInMinutes { get; internal set; }
    /// <summary>
    ///   Directory of app source code.
    /// </summary>
    public virtual string AppLocation { get; internal set; }
    /// <summary>
    ///   Directory of api source code.
    /// </summary>
    public virtual string ApiLocation { get; internal set; }
    /// <summary>
    ///   Path to the routes file.
    /// </summary>
    public virtual string RoutesLocation { get; internal set; }
    /// <summary>
    ///   Path to the staticwebapp.config.json file.
    /// </summary>
    public virtual string ConfigFileLocation { get; internal set; }
    /// <summary>
    ///   Directory of built application artifacts.
    /// </summary>
    public virtual string OutputLocation { get; internal set; }
    /// <summary>
    ///   Directory of built application artifacts.
    /// </summary>
    public virtual string AppArtifactLocation { get; internal set; }
    /// <summary>
    ///   Filepath of the event json.
    /// </summary>
    public virtual string EventJsonPath { get; internal set; }
    /// <summary>
    ///   ApiToken.
    /// </summary>
    public virtual string ApiToken { get; internal set; }
    /// <summary>
    ///   RepoToken.
    /// </summary>
    public virtual string RepoToken { get; internal set; }
    /// <summary>
    ///   Deprecated.
    /// </summary>
    public virtual bool? OryxEnabled { get; internal set; }
    /// <summary>
    ///   App Build Command.
    /// </summary>
    public virtual string AppBuildCommand { get; internal set; }
    /// <summary>
    ///   Api Build Command.
    /// </summary>
    public virtual string ApiBuildCommand { get; internal set; }
    /// <summary>
    ///   Deployment provider.
    /// </summary>
    public virtual StaticSitesClientDeploymentProvider DeploymentProvider { get; internal set; }
    /// <summary>
    ///   Skips Oryx build for app folder.
    /// </summary>
    public virtual bool? SkipAppBuild { get; internal set; }
    /// <summary>
    ///   Specifies the action to run.
    /// </summary>
    public virtual StaticSitesClientDeploymentAction DeploymentAction { get; internal set; }
    /// <summary>
    ///   Repository Url.
    /// </summary>
    public virtual string RepositoryUrl { get; internal set; }
    /// <summary>
    ///   Environment Name.
    /// </summary>
    public virtual string EnvironmentName { get; internal set; }
    /// <summary>
    ///   Branch.
    /// </summary>
    public virtual string Branch { get; internal set; }
    /// <summary>
    ///   Pull request title for staging sites.
    /// </summary>
    public virtual string PullRequestTitle { get; internal set; }
    /// <summary>
    ///   Head branch name for staging sites.
    /// </summary>
    public virtual string HeadBranch { get; internal set; }
    protected override Arguments ConfigureProcessArguments(Arguments arguments)
    {
        arguments
          .Add("run")
          .Add("--verbose", Verbose)
          .Add("--buildTimeoutInMinutes {value}", BuildTimeoutInMinutes)
          .Add("--app {value}", AppLocation)
          .Add("--api {value}", ApiLocation)
          .Add("--routesLocation {value}", RoutesLocation)
          .Add("--configFileLocation {value}", ConfigFileLocation)
          .Add("--outputLocation {value}", OutputLocation)
          .Add("--appArtifactLocation {value}", AppArtifactLocation)
          .Add("--event {value}", EventJsonPath)
          .Add("--apiToken {value}", ApiToken, secret: true)
          .Add("--repoToken {value}", RepoToken, secret: true)
          .Add("--oryxEnabled", OryxEnabled)
          .Add("--appBuildCommand {value}", AppBuildCommand)
          .Add("--apiBuildCommand {value}", ApiBuildCommand)
          .Add("--deploymentProvider {value}", DeploymentProvider)
          .Add("--skipAppBuild", SkipAppBuild)
          .Add("--deploymentaction {value}", DeploymentAction)
          .Add("--repositoryUrl {value}", RepositoryUrl)
          .Add("--environmentName {value}", EnvironmentName)
          .Add("--branch {value}", Branch)
          .Add("--pullRequestTitle {value}", PullRequestTitle)
          .Add("--headBranch {value}", HeadBranch);
        return base.ConfigureProcessArguments(arguments);
    }
}
#endregion
#region StaticSitesClientSettingsExtensions
/// <summary>
///   Used within <see cref="StaticSitesClientTasks"/>.
/// </summary>
[PublicAPI]
[ExcludeFromCodeCoverage]
public static partial class StaticSitesClientSettingsExtensions
{
    #region Verbose
    /// <summary>
    ///   <p><em>Sets <see cref="StaticSitesClientSettings.Verbose"/></em></p>
    ///   <p>Enables verbose logging.</p>
    /// </summary>
    [Pure]
    public static T SetVerbose<T>(this T toolSettings, bool? verbose) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.Verbose = verbose;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="StaticSitesClientSettings.Verbose"/></em></p>
    ///   <p>Enables verbose logging.</p>
    /// </summary>
    [Pure]
    public static T ResetVerbose<T>(this T toolSettings) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.Verbose = null;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Enables <see cref="StaticSitesClientSettings.Verbose"/></em></p>
    ///   <p>Enables verbose logging.</p>
    /// </summary>
    [Pure]
    public static T EnableVerbose<T>(this T toolSettings) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.Verbose = true;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Disables <see cref="StaticSitesClientSettings.Verbose"/></em></p>
    ///   <p>Enables verbose logging.</p>
    /// </summary>
    [Pure]
    public static T DisableVerbose<T>(this T toolSettings) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.Verbose = false;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Toggles <see cref="StaticSitesClientSettings.Verbose"/></em></p>
    ///   <p>Enables verbose logging.</p>
    /// </summary>
    [Pure]
    public static T ToggleVerbose<T>(this T toolSettings) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.Verbose = !toolSettings.Verbose;
        return toolSettings;
    }
    #endregion
    #region BuildTimeoutInMinutes
    /// <summary>
    ///   <p><em>Sets <see cref="StaticSitesClientSettings.BuildTimeoutInMinutes"/></em></p>
    ///   <p>Time limit of oryx build in minutes.</p>
    /// </summary>
    [Pure]
    public static T SetBuildTimeoutInMinutes<T>(this T toolSettings, int? buildTimeoutInMinutes) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.BuildTimeoutInMinutes = buildTimeoutInMinutes;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="StaticSitesClientSettings.BuildTimeoutInMinutes"/></em></p>
    ///   <p>Time limit of oryx build in minutes.</p>
    /// </summary>
    [Pure]
    public static T ResetBuildTimeoutInMinutes<T>(this T toolSettings) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.BuildTimeoutInMinutes = null;
        return toolSettings;
    }
    #endregion
    #region AppLocation
    /// <summary>
    ///   <p><em>Sets <see cref="StaticSitesClientSettings.AppLocation"/></em></p>
    ///   <p>Directory of app source code.</p>
    /// </summary>
    [Pure]
    public static T SetAppLocation<T>(this T toolSettings, string appLocation) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.AppLocation = appLocation;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="StaticSitesClientSettings.AppLocation"/></em></p>
    ///   <p>Directory of app source code.</p>
    /// </summary>
    [Pure]
    public static T ResetAppLocation<T>(this T toolSettings) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.AppLocation = null;
        return toolSettings;
    }
    #endregion
    #region ApiLocation
    /// <summary>
    ///   <p><em>Sets <see cref="StaticSitesClientSettings.ApiLocation"/></em></p>
    ///   <p>Directory of api source code.</p>
    /// </summary>
    [Pure]
    public static T SetApiLocation<T>(this T toolSettings, string apiLocation) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.ApiLocation = apiLocation;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="StaticSitesClientSettings.ApiLocation"/></em></p>
    ///   <p>Directory of api source code.</p>
    /// </summary>
    [Pure]
    public static T ResetApiLocation<T>(this T toolSettings) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.ApiLocation = null;
        return toolSettings;
    }
    #endregion
    #region RoutesLocation
    /// <summary>
    ///   <p><em>Sets <see cref="StaticSitesClientSettings.RoutesLocation"/></em></p>
    ///   <p>Path to the routes file.</p>
    /// </summary>
    [Pure]
    public static T SetRoutesLocation<T>(this T toolSettings, string routesLocation) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.RoutesLocation = routesLocation;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="StaticSitesClientSettings.RoutesLocation"/></em></p>
    ///   <p>Path to the routes file.</p>
    /// </summary>
    [Pure]
    public static T ResetRoutesLocation<T>(this T toolSettings) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.RoutesLocation = null;
        return toolSettings;
    }
    #endregion
    #region ConfigFileLocation
    /// <summary>
    ///   <p><em>Sets <see cref="StaticSitesClientSettings.ConfigFileLocation"/></em></p>
    ///   <p>Path to the staticwebapp.config.json file.</p>
    /// </summary>
    [Pure]
    public static T SetConfigFileLocation<T>(this T toolSettings, string configFileLocation) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.ConfigFileLocation = configFileLocation;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="StaticSitesClientSettings.ConfigFileLocation"/></em></p>
    ///   <p>Path to the staticwebapp.config.json file.</p>
    /// </summary>
    [Pure]
    public static T ResetConfigFileLocation<T>(this T toolSettings) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.ConfigFileLocation = null;
        return toolSettings;
    }
    #endregion
    #region OutputLocation
    /// <summary>
    ///   <p><em>Sets <see cref="StaticSitesClientSettings.OutputLocation"/></em></p>
    ///   <p>Directory of built application artifacts.</p>
    /// </summary>
    [Pure]
    public static T SetOutputLocation<T>(this T toolSettings, string outputLocation) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.OutputLocation = outputLocation;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="StaticSitesClientSettings.OutputLocation"/></em></p>
    ///   <p>Directory of built application artifacts.</p>
    /// </summary>
    [Pure]
    public static T ResetOutputLocation<T>(this T toolSettings) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.OutputLocation = null;
        return toolSettings;
    }
    #endregion
    #region AppArtifactLocation
    /// <summary>
    ///   <p><em>Sets <see cref="StaticSitesClientSettings.AppArtifactLocation"/></em></p>
    ///   <p>Directory of built application artifacts.</p>
    /// </summary>
    [Pure]
    public static T SetAppArtifactLocation<T>(this T toolSettings, string appArtifactLocation) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.AppArtifactLocation = appArtifactLocation;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="StaticSitesClientSettings.AppArtifactLocation"/></em></p>
    ///   <p>Directory of built application artifacts.</p>
    /// </summary>
    [Pure]
    public static T ResetAppArtifactLocation<T>(this T toolSettings) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.AppArtifactLocation = null;
        return toolSettings;
    }
    #endregion
    #region EventJsonPath
    /// <summary>
    ///   <p><em>Sets <see cref="StaticSitesClientSettings.EventJsonPath"/></em></p>
    ///   <p>Filepath of the event json.</p>
    /// </summary>
    [Pure]
    public static T SetEventJsonPath<T>(this T toolSettings, string eventJsonPath) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.EventJsonPath = eventJsonPath;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="StaticSitesClientSettings.EventJsonPath"/></em></p>
    ///   <p>Filepath of the event json.</p>
    /// </summary>
    [Pure]
    public static T ResetEventJsonPath<T>(this T toolSettings) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.EventJsonPath = null;
        return toolSettings;
    }
    #endregion
    #region ApiToken
    /// <summary>
    ///   <p><em>Sets <see cref="StaticSitesClientSettings.ApiToken"/></em></p>
    ///   <p>ApiToken.</p>
    /// </summary>
    [Pure]
    public static T SetApiToken<T>(this T toolSettings, [Secret] string apiToken) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.ApiToken = apiToken;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="StaticSitesClientSettings.ApiToken"/></em></p>
    ///   <p>ApiToken.</p>
    /// </summary>
    [Pure]
    public static T ResetApiToken<T>(this T toolSettings) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.ApiToken = null;
        return toolSettings;
    }
    #endregion
    #region RepoToken
    /// <summary>
    ///   <p><em>Sets <see cref="StaticSitesClientSettings.RepoToken"/></em></p>
    ///   <p>RepoToken.</p>
    /// </summary>
    [Pure]
    public static T SetRepoToken<T>(this T toolSettings, [Secret] string repoToken) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.RepoToken = repoToken;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="StaticSitesClientSettings.RepoToken"/></em></p>
    ///   <p>RepoToken.</p>
    /// </summary>
    [Pure]
    public static T ResetRepoToken<T>(this T toolSettings) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.RepoToken = null;
        return toolSettings;
    }
    #endregion
    #region OryxEnabled
    /// <summary>
    ///   <p><em>Sets <see cref="StaticSitesClientSettings.OryxEnabled"/></em></p>
    ///   <p>Deprecated.</p>
    /// </summary>
    [Pure]
    public static T SetOryxEnabled<T>(this T toolSettings, bool? oryxEnabled) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.OryxEnabled = oryxEnabled;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="StaticSitesClientSettings.OryxEnabled"/></em></p>
    ///   <p>Deprecated.</p>
    /// </summary>
    [Pure]
    public static T ResetOryxEnabled<T>(this T toolSettings) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.OryxEnabled = null;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Enables <see cref="StaticSitesClientSettings.OryxEnabled"/></em></p>
    ///   <p>Deprecated.</p>
    /// </summary>
    [Pure]
    public static T EnableOryxEnabled<T>(this T toolSettings) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.OryxEnabled = true;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Disables <see cref="StaticSitesClientSettings.OryxEnabled"/></em></p>
    ///   <p>Deprecated.</p>
    /// </summary>
    [Pure]
    public static T DisableOryxEnabled<T>(this T toolSettings) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.OryxEnabled = false;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Toggles <see cref="StaticSitesClientSettings.OryxEnabled"/></em></p>
    ///   <p>Deprecated.</p>
    /// </summary>
    [Pure]
    public static T ToggleOryxEnabled<T>(this T toolSettings) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.OryxEnabled = !toolSettings.OryxEnabled;
        return toolSettings;
    }
    #endregion
    #region AppBuildCommand
    /// <summary>
    ///   <p><em>Sets <see cref="StaticSitesClientSettings.AppBuildCommand"/></em></p>
    ///   <p>App Build Command.</p>
    /// </summary>
    [Pure]
    public static T SetAppBuildCommand<T>(this T toolSettings, string appBuildCommand) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.AppBuildCommand = appBuildCommand;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="StaticSitesClientSettings.AppBuildCommand"/></em></p>
    ///   <p>App Build Command.</p>
    /// </summary>
    [Pure]
    public static T ResetAppBuildCommand<T>(this T toolSettings) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.AppBuildCommand = null;
        return toolSettings;
    }
    #endregion
    #region ApiBuildCommand
    /// <summary>
    ///   <p><em>Sets <see cref="StaticSitesClientSettings.ApiBuildCommand"/></em></p>
    ///   <p>Api Build Command.</p>
    /// </summary>
    [Pure]
    public static T SetApiBuildCommand<T>(this T toolSettings, string apiBuildCommand) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.ApiBuildCommand = apiBuildCommand;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="StaticSitesClientSettings.ApiBuildCommand"/></em></p>
    ///   <p>Api Build Command.</p>
    /// </summary>
    [Pure]
    public static T ResetApiBuildCommand<T>(this T toolSettings) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.ApiBuildCommand = null;
        return toolSettings;
    }
    #endregion
    #region DeploymentProvider
    /// <summary>
    ///   <p><em>Sets <see cref="StaticSitesClientSettings.DeploymentProvider"/></em></p>
    ///   <p>Deployment provider.</p>
    /// </summary>
    [Pure]
    public static T SetDeploymentProvider<T>(this T toolSettings, StaticSitesClientDeploymentProvider deploymentProvider) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.DeploymentProvider = deploymentProvider;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="StaticSitesClientSettings.DeploymentProvider"/></em></p>
    ///   <p>Deployment provider.</p>
    /// </summary>
    [Pure]
    public static T ResetDeploymentProvider<T>(this T toolSettings) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.DeploymentProvider = null;
        return toolSettings;
    }
    #endregion
    #region SkipAppBuild
    /// <summary>
    ///   <p><em>Sets <see cref="StaticSitesClientSettings.SkipAppBuild"/></em></p>
    ///   <p>Skips Oryx build for app folder.</p>
    /// </summary>
    [Pure]
    public static T SetSkipAppBuild<T>(this T toolSettings, bool? skipAppBuild) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.SkipAppBuild = skipAppBuild;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="StaticSitesClientSettings.SkipAppBuild"/></em></p>
    ///   <p>Skips Oryx build for app folder.</p>
    /// </summary>
    [Pure]
    public static T ResetSkipAppBuild<T>(this T toolSettings) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.SkipAppBuild = null;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Enables <see cref="StaticSitesClientSettings.SkipAppBuild"/></em></p>
    ///   <p>Skips Oryx build for app folder.</p>
    /// </summary>
    [Pure]
    public static T EnableSkipAppBuild<T>(this T toolSettings) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.SkipAppBuild = true;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Disables <see cref="StaticSitesClientSettings.SkipAppBuild"/></em></p>
    ///   <p>Skips Oryx build for app folder.</p>
    /// </summary>
    [Pure]
    public static T DisableSkipAppBuild<T>(this T toolSettings) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.SkipAppBuild = false;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Toggles <see cref="StaticSitesClientSettings.SkipAppBuild"/></em></p>
    ///   <p>Skips Oryx build for app folder.</p>
    /// </summary>
    [Pure]
    public static T ToggleSkipAppBuild<T>(this T toolSettings) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.SkipAppBuild = !toolSettings.SkipAppBuild;
        return toolSettings;
    }
    #endregion
    #region DeploymentAction
    /// <summary>
    ///   <p><em>Sets <see cref="StaticSitesClientSettings.DeploymentAction"/></em></p>
    ///   <p>Specifies the action to run.</p>
    /// </summary>
    [Pure]
    public static T SetDeploymentAction<T>(this T toolSettings, StaticSitesClientDeploymentAction deploymentAction) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.DeploymentAction = deploymentAction;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="StaticSitesClientSettings.DeploymentAction"/></em></p>
    ///   <p>Specifies the action to run.</p>
    /// </summary>
    [Pure]
    public static T ResetDeploymentAction<T>(this T toolSettings) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.DeploymentAction = null;
        return toolSettings;
    }
    #endregion
    #region RepositoryUrl
    /// <summary>
    ///   <p><em>Sets <see cref="StaticSitesClientSettings.RepositoryUrl"/></em></p>
    ///   <p>Repository Url.</p>
    /// </summary>
    [Pure]
    public static T SetRepositoryUrl<T>(this T toolSettings, string repositoryUrl) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.RepositoryUrl = repositoryUrl;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="StaticSitesClientSettings.RepositoryUrl"/></em></p>
    ///   <p>Repository Url.</p>
    /// </summary>
    [Pure]
    public static T ResetRepositoryUrl<T>(this T toolSettings) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.RepositoryUrl = null;
        return toolSettings;
    }
    #endregion
    #region EnvironmentName
    /// <summary>
    ///   <p><em>Sets <see cref="StaticSitesClientSettings.EnvironmentName"/></em></p>
    ///   <p>Environment Name.</p>
    /// </summary>
    [Pure]
    public static T SetEnvironmentName<T>(this T toolSettings, string environmentName) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.EnvironmentName = environmentName;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="StaticSitesClientSettings.EnvironmentName"/></em></p>
    ///   <p>Environment Name.</p>
    /// </summary>
    [Pure]
    public static T ResetEnvironmentName<T>(this T toolSettings) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.EnvironmentName = null;
        return toolSettings;
    }
    #endregion
    #region Branch
    /// <summary>
    ///   <p><em>Sets <see cref="StaticSitesClientSettings.Branch"/></em></p>
    ///   <p>Branch.</p>
    /// </summary>
    [Pure]
    public static T SetBranch<T>(this T toolSettings, string branch) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.Branch = branch;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="StaticSitesClientSettings.Branch"/></em></p>
    ///   <p>Branch.</p>
    /// </summary>
    [Pure]
    public static T ResetBranch<T>(this T toolSettings) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.Branch = null;
        return toolSettings;
    }
    #endregion
    #region PullRequestTitle
    /// <summary>
    ///   <p><em>Sets <see cref="StaticSitesClientSettings.PullRequestTitle"/></em></p>
    ///   <p>Pull request title for staging sites.</p>
    /// </summary>
    [Pure]
    public static T SetPullRequestTitle<T>(this T toolSettings, string pullRequestTitle) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.PullRequestTitle = pullRequestTitle;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="StaticSitesClientSettings.PullRequestTitle"/></em></p>
    ///   <p>Pull request title for staging sites.</p>
    /// </summary>
    [Pure]
    public static T ResetPullRequestTitle<T>(this T toolSettings) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.PullRequestTitle = null;
        return toolSettings;
    }
    #endregion
    #region HeadBranch
    /// <summary>
    ///   <p><em>Sets <see cref="StaticSitesClientSettings.HeadBranch"/></em></p>
    ///   <p>Head branch name for staging sites.</p>
    /// </summary>
    [Pure]
    public static T SetHeadBranch<T>(this T toolSettings, string headBranch) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.HeadBranch = headBranch;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="StaticSitesClientSettings.HeadBranch"/></em></p>
    ///   <p>Head branch name for staging sites.</p>
    /// </summary>
    [Pure]
    public static T ResetHeadBranch<T>(this T toolSettings) where T : StaticSitesClientSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.HeadBranch = null;
        return toolSettings;
    }
    #endregion
}
#endregion
#region StaticSitesClientDeploymentProvider
/// <summary>
///   Used within <see cref="StaticSitesClientTasks"/>.
/// </summary>
[PublicAPI]
[Serializable]
[ExcludeFromCodeCoverage]
[TypeConverter(typeof(TypeConverter<StaticSitesClientDeploymentProvider>))]
public partial class StaticSitesClientDeploymentProvider : Enumeration
{
    public static StaticSitesClientDeploymentProvider GitHub = (StaticSitesClientDeploymentProvider) "GitHub";
    public static StaticSitesClientDeploymentProvider DevOps = (StaticSitesClientDeploymentProvider) "DevOps";
    public static StaticSitesClientDeploymentProvider GitLab = (StaticSitesClientDeploymentProvider) "GitLab";
    public static StaticSitesClientDeploymentProvider Custom = (StaticSitesClientDeploymentProvider) "Custom";
    public static implicit operator StaticSitesClientDeploymentProvider(string value)
    {
        return new StaticSitesClientDeploymentProvider { Value = value };
    }
}
#endregion
#region StaticSitesClientDeploymentAction
/// <summary>
///   Used within <see cref="StaticSitesClientTasks"/>.
/// </summary>
[PublicAPI]
[Serializable]
[ExcludeFromCodeCoverage]
[TypeConverter(typeof(TypeConverter<StaticSitesClientDeploymentAction>))]
public partial class StaticSitesClientDeploymentAction : Enumeration
{
    public static StaticSitesClientDeploymentAction upload = (StaticSitesClientDeploymentAction) "upload";
    public static StaticSitesClientDeploymentAction run = (StaticSitesClientDeploymentAction) "run";
    public static implicit operator StaticSitesClientDeploymentAction(string value)
    {
        return new StaticSitesClientDeploymentAction { Value = value };
    }
}
#endregion
