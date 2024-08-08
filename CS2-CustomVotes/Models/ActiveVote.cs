using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Modules.Menu;
using CS2_CustomVotes.Extensions;
using Timer = CounterStrikeSharp.API.Modules.Timers.Timer;

namespace CS2_CustomVotes.Models;

public class ActiveVote
{
    private readonly CustomVotes _plugin;
    public ActiveVote(CustomVotes plugin, CustomVote vote)
    {
        _plugin = plugin;
        Vote = vote;
        OptionVotes = vote.Options.ToDictionary(x => x.Key, _ => new List<uint>());
    }
    
    public CustomVote Vote { get; set; }
    public Dictionary<string, List<uint>> OptionVotes { get; set; }

    public Timer? VoteTimeout { get; set; }
    public BaseMenu? VoteMenu { get; set; }

    public void OpenMenuForAll()
    {
        var players = Utilities.GetPlayers().Where(p => p.IsPlayer()).ToList();
        foreach (var player in players)
        {
            // open vote menu for player
            if (VoteMenu is CenterHtmlMenu)
                MenuManager.OpenCenterHtmlMenu(_plugin, player, (VoteMenu! as CenterHtmlMenu)!);
            else
                MenuManager.OpenChatMenu(player, (VoteMenu! as ChatMenu)!);
        }
    }
    public void CloseMenuForAll()
    {
        if (VoteMenu is null)
            return;
        
        var players = Utilities.GetPlayers().Where(p => p.IsPlayer()).ToList();
        foreach (var player in players)
            MenuManager.CloseActiveMenu(player);
    }

    public KeyValuePair<string, List<uint>> GetWinningOption()
    {
        if (OptionVotes.All(o => o.Value.Count == 0))
            return new KeyValuePair<string, List<uint>>(Vote.DefaultOption, []);
        
        var winningOption = OptionVotes.MaxBy(x => x.Value.Count);
        var totalVotes = OptionVotes.Sum(x => x.Value.Count);

        if (Vote.MinVotePercentage < 0 || winningOption.Value.Count >= totalVotes * Vote.MinVotePercentage / 100) 
            return winningOption;
        
        return new KeyValuePair<string, List<uint>>(Vote.DefaultOption, []);
    }
}