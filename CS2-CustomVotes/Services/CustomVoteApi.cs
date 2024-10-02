using CS2_CustomVotes.Shared;
﻿using CounterStrikeSharp.API;
﻿using CounterStrikeSharp.API.Core;
using CS2_CustomVotes.Shared.Models;

namespace CS2_CustomVotes.Services;

public class CustomVoteApi : ICustomVoteApi
{
    private readonly IVoteManager _voteManager;

    public CustomVoteApi(IVoteManager voteManager)
    {
        _voteManager = voteManager;
    }
    
    public void AddCustomVote(string name, string description, string defaultOption, float timeToVote, Dictionary<string, VoteOption> options, string style)
    {
        _voteManager.AddVote(name, [], description, defaultOption, timeToVote, options, style, -1);
    }

    public void AddCustomVote(string name, List<string> aliases, string description, string defaultOption, float timeToVote,
        Dictionary<string, VoteOption> options, string style)
    {
        _voteManager.AddVote(name, aliases, description, defaultOption, timeToVote, options, style, -1);
    }
    
    public void AddCustomVote(string name, string description, string defaultOption, float timeToVote, Dictionary<string, VoteOption> options, string style, int minVotePercentage)
    {
        _voteManager.AddVote(name, [], description, defaultOption, timeToVote, options, style, minVotePercentage);
    }

    public void AddCustomVote(string name, List<string> aliases, string description, string defaultOption, float timeToVote,
        Dictionary<string, VoteOption> options, string style, int minVotePercentage)
    {
        _voteManager.AddVote(name, aliases, description, defaultOption, timeToVote, options, style, minVotePercentage);
    }

    public void AddCustomVote(string name, List<string> aliases, string description, string defaultOption, float timeToVote,
        Dictionary<string, VoteOption> options, string style, int minVotePercentage)
    {
        _voteManager.AddVote(name, aliases, description, defaultOption, timeToVote, options, style, minVotePercentage);
    }

    public void StartCustomVote(CCSPlayerController? player, string name)
    {
        string baseName = "";
        string result = _voteManager.StartVote(player, name, out string baseName);
    }

    public void RemoveCustomVote(string name)
    {
        _voteManager.RemoveVote(name);
    }
}
