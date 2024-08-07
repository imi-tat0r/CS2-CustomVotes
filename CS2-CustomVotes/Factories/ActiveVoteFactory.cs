using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Menu;
using CS2_CustomVotes.Helpers;
using CS2_CustomVotes.Models;
using Microsoft.Extensions.Localization;

namespace CS2_CustomVotes.Factories;

public interface IActiveVoteFactory
{
    public ActiveVote Create(CustomVote vote, Action<string> onEndVote, Action<CCSPlayerController?, string> onPlayerVoted);
}

public class ActiveVoteFactory : IActiveVoteFactory
{
    private readonly CustomVotes _plugin;
    private readonly IStringLocalizer _localizer;

    public ActiveVoteFactory(CustomVotes plugin, IStringLocalizer localizer)
    {
        _plugin = plugin;
        _localizer = localizer;
    }

    public ActiveVote Create(CustomVote vote, Action<string> onEndVote, Action<CCSPlayerController?, string> onPlayerVoted)
    {
        var activeVote = new ActiveVote(_plugin, vote);
        
        // start vote timeout and save handle
        activeVote.VoteTimeout = _plugin.AddTimer(activeVote.Vote.TimeToVote, () => onEndVote(vote.Command));
        
        // set vote style (global override always takes priority)
        var style = _plugin.Config.ForceStyle == "none" ? vote.Style : _plugin.Config.ForceStyle;
        
        if (style == "center")
            activeVote.VoteMenu = new CenterHtmlMenu(activeVote.Vote.Description, _plugin);
        else
            activeVote.VoteMenu = new ChatMenu(activeVote.Vote.Description);

        foreach (var voteOption in activeVote.Vote.Options)
            activeVote.VoteMenu.AddMenuOption(style == "center" ? _localizer[voteOption.Key] : Chat.FormatMessage(_localizer[voteOption.Value.Text]),
                (caller, option) => onPlayerVoted(caller, option.Text));
        
        return activeVote;
    }
}