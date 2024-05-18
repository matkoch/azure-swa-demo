using System;
using Nuke.Common.Tooling;
using Nuke.Common.Utilities;

partial class AzTasks
{
    static string GetResult(IProcess process, AzStaticWebAppUsersInviteSettings toolSettings)
    {
        var result = process.Output.StdOutputToJson();
        return result.GetPropertyStringValue("invitationUrl");
    }

    static void PreProcess(ref AzStaticWebAppUsersInviteSettings toolSettings)
    {
    }

    static void PreProcess(ref AzDeploymentGroupCreateSettings toolSettings)
    {
    }

    static void PreProcess(ref AzStaticWebAppHostnameShowSettings toolSettings)
    {
    }

    static void PreProcess(ref AzStaticWebAppHostnameSetSettings toolSettings)
    {
    }
}
