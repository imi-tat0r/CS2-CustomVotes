using System.Text.Json.Serialization;

namespace CS2_CustomVotes.Shared.Models;

public class VoteOption
{
    public VoteOption(string text, List<string> commands)
    {
        Text = text;
        Commands = commands;
    }

    [JsonPropertyName("Text")] public string Text { get; set; } = null!;

    [JsonPropertyName("Commands")] public List<string> Commands { get; set; } = new();
}