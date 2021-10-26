using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.Docker;
using Nuke.Common.Utilities;

partial class Build
{
    public Build()
    {
        DockerTasks.DockerLogger = (s, e) => Logger.Normal(e);
    }

    public void SaveParameter<T>(Expression<Func<T>> expression, T value = default)
    {
        var parametersFile = NukeBuild.RootDirectory / ".nuke" / "parameters.json";
        var json = File.ReadAllText(parametersFile);
        var jobject = JsonConvert.DeserializeObject<JObject>(json);
        var member = expression.GetMemberInfo();
        var target = expression.GetTarget();
        jobject[member.Name] = (value ?? member.GetValue<T>(target)).ToString();
        File.WriteAllText(parametersFile, JsonConvert.SerializeObject(jobject, Formatting.Indented));
    }
}

public static class Extensions
{
    public static object GetTarget(this LambdaExpression expression)
    {
        var memberExpression = expression.Body as MemberExpression;
        var constantExpression = memberExpression?.Expression as ConstantExpression;
        return constantExpression?.Value;
    }

    public static string[] ParseCommandLineArguments(this string commandLine)
    {
        var inSingleQuotes = false;
        var inDoubleQuotes = false;
        var escaped = false;
        return commandLine.Split((c, _) =>
                {
                    if (c == '\"' && !inSingleQuotes && !escaped)
                        inDoubleQuotes = !inDoubleQuotes;

                    if (c == '\'' && !inDoubleQuotes && !escaped)
                        inSingleQuotes = !inSingleQuotes;

                    escaped = c == '\\' && !escaped;

                    return c == ' ' && !(inDoubleQuotes || inSingleQuotes);
                },
                includeSplitCharacter: true)
            .Select(x =>
                StringExtensions.TrimMatchingDoubleQuotes(x.Trim()).TrimMatchingQuotes().Replace("\\\"", "\"").Replace("\\\'", "'"))
            .Where(x => !string.IsNullOrEmpty(x))
            .ToArray();
    }

    [Pure]
    public static T AddArgs<T, TSettings>(this T toolSettings, Configure<TSettings> configurator)
        where T : DockerRunSettings
        where TSettings : ToolSettings, new()
    {
        toolSettings = toolSettings.NewInstance();
        var settings = configurator.Invoke(new TSettings());
        var arguments = settings.GetProcessArguments().RenderForExecution().ParseCommandLineArguments();
        return toolSettings
            .AddArgs(settings.ProcessToolPath)
            .AddArgs(arguments);
    }

    public static JObject StdOutputToJson(this IEnumerable<Output> output, bool ensureOnlyStd = false)
    {
        return SerializationTasks.JsonDeserialize<JObject>(
            output.Where(x => x.Type == OutputType.Std)
                .Select(x => x.Text)
                .JoinNewLine());
    }
}