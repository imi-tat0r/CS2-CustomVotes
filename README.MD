![Copyright ev0lve Digital](https://img.shields.io/badge/Copyright-ev0lve%20Digital-blue) ![GitHub License](https://img.shields.io/github/license/imi-tat0r/CS2-CustomVotes) ![Issues](https://img.shields.io/github/issues/imi-tat0r/CS2-CustomVotes) ![Downloads](https://img.shields.io/github/downloads/imi-tat0r/CS2-CustomVotes/total) ![Stars](https://img.shields.io/github/stars/imi-tat0r/CS2-CustomVotes)

# CS2 Custom Votes (1.1.3)
![image](https://du.hurenso.hn/r/X4jQaq.png) ![image](https://du.hurenso.hn/r/81Imq8.png)

# About
CS2-CustomVotes is a plugin for Counter-Strike 2 that allows you to easily create preference votes for your server. Create votes with any number of options and let your players decide on the settings of your server.  
Every vote options can trigger multiple commands at once allowing for a wide range of possibilities.

# Features
- Custom votes with any number of options
- Execute multiple commands per vote option
- Customizable duration for each vote
- Cooldown between votes
- Localized chat messages
- **CS2-CustomVotes.Shared API** for other plugins to register votes

# Dependencies
[Metamod:Source (2.x)](https://www.sourcemm.net/downloads.php/?branch=master)  
[CounterStrikeSharp(v191)](https://github.com/roflmuffin/CounterStrikeSharp/releases)

# Installation
1. Install Metamod:Source and CounterStrikeSharp
2. Place the `addons` folder in your servers `game/csgo/` directory  
   ![extract](https://du.hurenso.hn/r/0NyFPY.png)
3. Add your custom votes to the config file
   3.1. Located at `addons/counterstrikesharp/configs/plugins/CS2-CustomVotes/CS2-CustomVotes.json`
4. Restart your server

# Config
```json
{
   "CustomVotesEnabled": true, // global enable/disable for custom votes
   "VoteCooldown": 60, // cooldown between votes in seconds
   "ChatPrefix": "[{DarkBlue}CustomVotes{Default}]", // chat prefix for plugin messages, supports all ChatColors
   "ForceStyle": "none", // "none", "center" or "chat" - none will use the style from the vote settings
   "CustomVotes": [ // list of custom votes
      {
         "Command": "cheats", // command to trigger the vote
         "CommandAliases": [ // aliases for the command
            "sv_cheats"
         ],
         "Description": "Vote to enable sv_cheats", // description of the vote, will be displayed in the vote menu
         "TimeToVote": 30, // time in seconds players have to vote
         "Options": { // vote options
            "Enable": { // name of the option
               "Text": "{Green}Enable", // text to display in the vote menu (supports ChatColors when using the "chat" style)
               "Commands": [ // commands to execute when the option is selected
                  "sv_cheats 1"
               ]
            },
            "Disable": {
               "Text": "{Red}Disable",
               "Commands": [
                  "sv_cheats 0"
               ]
            }
         },
         "DefaultOption": "Disable", // default option (must be a key from the "Options" object)
         "Style": "chat", // Menu style - "center" or "chat" (will be overridden by the global ForceStyle if not "none")
         "MinVotePercentage": -1, // minimum percentage of votes required to pass the vote (-1 behaves like 50%)
         "Permission": {
            "RequiresAll": false, // if true, all permissions must be present to vote
            "Permissions": [] // list of permissions required to start this vote (empty list allows everyone to start the vote)
         }
      }
   ],
   "ConfigVersion": 2
}
```

# API
CS2-CustomVotes provides a shared API for other plugins to register custom votes.

Add a reference to `CS2-CustomVotes.Shared` to your project in one of the following ways: 
1. Download the source code and build it yourself
2. Download the latest release from the [releases page](https://github.com/imi-tat0r/CS2-CustomVotes/releases).
3. Install the package using the .NET CLI, run: `dotnet add package CS2-CustomVotes.Shared`

After adding the reference to your project, you can create custom votes like this:
```csharp
public interface ICustomVoteApi
{
    public void AddCustomVote(string name, string description, string defaultOption, float timeToVote, Dictionary<string, VoteOption> options, string style);
    public void AddCustomVote(string name, List<string> aliases, string description, string defaultOption, float timeToVote, Dictionary<string, VoteOption> options, string style);
    public void AddCustomVote(string name, string description, string defaultOption, float timeToVote, Dictionary<string, VoteOption> options, string style, int minVotePercentage);
    public void AddCustomVote(string name, List<string> aliases, string description, string defaultOption, float timeToVote, Dictionary<string, VoteOption> options, string style, int minVotePercentage);
    public void StartCustomVote(CCSPlayerController? player, string name);
    public void EndCustomVote(string name);
    public void RemoveCustomVote(string name);
}

customVotesApi.AddCustomVote(
            "cheats", // command to trigger the vote
            new List<string>(), // aliases for the command (optional)
            "Vote to enable sv_cheats", // description of the vote, will be displayed in the vote menu
            "Disable", // default option (must be a key from the "Options" object)
            30, // time in seconds players have to vote
            new Dictionary<string, VoteOption> // vote options
            {
                { "Enable", new VoteOption("{Green}Enable", new List<string> { "sv_cheats 1" })},
                { "Disable", new VoteOption("{Red}Disable", new List<string> { "sv_cheats 0" })},
            },
            "chat", // Menu style - "center" or "chat"
            -1); // minimum percentage of votes required to pass the vote (-1 behaves like 50%)

customVotesApi.RemoveCustomVote("cheats");
```

# Credits
- [Metamod:Source](https://www.sourcemm.net/)
- [CounterStrikeSharp](https://github.com/roflmuffin/CounterStrikeSharp)
