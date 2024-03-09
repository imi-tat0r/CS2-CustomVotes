using System.Reflection;
using CounterStrikeSharp.API.Modules.Utils;

namespace CS2_CustomVotes.Helpers;

public static class Chat
{
    private static readonly Dictionary<string, char> PredefinedColors = typeof(ChatColors)
        .GetFields(BindingFlags.Public | BindingFlags.Static)
        .ToDictionary(
            field => $"{{{field.Name}}}",
            field => (char)(field.GetValue(null) ?? '\x01')
        );

    public static string FormatMessage(string message)
    {
        foreach (var color in PredefinedColors)
            message = message.Replace(color.Key, $"{color.Value}");

        return message;
    }

    public static string CleanMessage(string message)
    {
        foreach (var color in PredefinedColors)
        {
            message = message.Replace(color.Key, "");
            message = message.Replace(color.Value.ToString(), "");
        }

        return message;
    }
}