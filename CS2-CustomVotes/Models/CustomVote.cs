using System.Text.Json.Serialization;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Admin;
using CS2_CustomVotes.Shared.Models;

namespace CS2_CustomVotes.Models;

public class Permission
{
    [JsonPropertyName("RequiresAll")]
    public bool RequiresAll { get; init; }

    [JsonPropertyName("Permissions")]
    public List<string> Permissions { get; init; } = [];
}

public class CustomVote
{
    [JsonPropertyName("Command")]
    public string Command { get; init; } = null!;
    [JsonPropertyName("CommandAliases")]
    public List<string> CommandAliases { get; init; } = new();
    
    [JsonPropertyName("Description")]
    public string Description { get; init; } = null!;
    
    [JsonPropertyName("TimeToVote")]
    public float TimeToVote { get; init; }
    
    [JsonPropertyName("Options")]
    public Dictionary<string, VoteOption> Options { get; init; } = new();
    
    [JsonPropertyName("DefaultOption")] 
    public string DefaultOption { get; init; } = null!;
    
    [JsonPropertyName("Style")] 
    public string Style { get; init; } = null!;

    [JsonPropertyName("MinVotePercentage")]
    public int MinVotePercentage { get; init; } = -1;

    [JsonPropertyName("Permission")]
    public Permission Permission { get; init; } = new();

    [JsonIgnore] 
    public float LastVoteTime { get; set; } = 0;
    
    public void ExecuteCommand(string? optionName = null)
    {
        optionName ??= DefaultOption;
        
        var optionCommands = Options.TryGetValue(optionName, out var option ) ? option.Commands : new List<string>();
        
        foreach (var command in optionCommands)
            Server.ExecuteCommand(command);
    }
    
    public bool CheckPermissions(CCSPlayerController? player)
    {
        if (Permission.Permissions.Count == 0)
            return true;
        
        return Permission.RequiresAll
            ? AdminManager.PlayerHasPermissions(player, Permission.Permissions.ToArray())
            : Permission.Permissions.Any(permission => AdminManager.PlayerHasPermissions(player, permission));
    }
}