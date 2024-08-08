using System.Text.Json.Serialization;
using CounterStrikeSharp.API.Core;
using CS2_CustomVotes.Models;
using CS2_CustomVotes.Shared.Models;

namespace CS2_CustomVotes;

public class CustomVotesConfig : BasePluginConfig
{
    [JsonPropertyName("CustomVotesEnabled")]
    public bool CustomVotesEnabled { get; set; } = true;
    
    [JsonPropertyName("VoteCooldown")]
    public float VoteCooldown { get; set; } = 60;
    
    [JsonPropertyName("ChatPrefix")]
    public string ChatPrefix { get; set; } = "[{DarkBlue}CustomVotes{Default}]";
    
    [JsonPropertyName("ForceStyle")]
    public string ForceStyle { get; set; } = "none";
    
    [JsonPropertyName("CustomVotes")]
    public List<CustomVote> CustomVotes { get; set; } =
    [
        new CustomVote()
        {
            Command = "cheats",
            Description = "Vote to enable sv_cheats",
            TimeToVote = 30,
            Options = new Dictionary<string, VoteOption>()
            {
                { "Enable", new("{Green}Enable", ["sv_cheats 1"]) },
                { "Disable", new("{Red}Disable", ["sv_cheats 0"]) }
            },
            DefaultOption = "Disable",
            Style = "center",
            MinVotePercentage = 50,
            Permission = new Permission
            {
                RequiresAll = false,
                Permissions = []
            }
        }
    ];

    [JsonPropertyName("ConfigVersion")] 
    public override int Version { get; set; } = 2;
}