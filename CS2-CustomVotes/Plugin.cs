using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using CounterStrikeSharp.API.Core.Capabilities;
using CS2_CustomVotes.Factories;
using CS2_CustomVotes.Helpers;
using CS2_CustomVotes.Models;
using CS2_CustomVotes.Services;
using CS2_CustomVotes.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CS2_CustomVotes;

[MinimumApiVersion(191)]
public class CustomVotes : BasePlugin, IPluginConfig<CustomVotesConfig>
{
    public override string ModuleName => "Custom Votes";
    public override string ModuleDescription => "Allows you to create custom votes for your server.";
    public override string ModuleVersion => "1.0.0";
    public override string ModuleAuthor => "imi-tat0r";
    
    public CustomVotesConfig Config { get; set; } = null!;
    
    private readonly ILogger<CustomVotes> _logger;
    private readonly IServiceProvider _serviceProvider;

    public static PluginCapability<ICustomVoteApi> CustomVoteCapability { get; } = new("custom_votes:api");

    public CustomVotes(ILogger<CustomVotes> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public override void Load(bool hotReload)
    {
        base.Load(hotReload);
        
        _logger.LogInformation("[CustomVotes] Registering custom vote API");
        var customVoteApi = _serviceProvider.GetRequiredService<ICustomVoteApi>();
        Capabilities.RegisterPluginCapability(CustomVoteCapability, () => customVoteApi);
        
        _logger.LogInformation("[CustomVotes] Registering event handlers");
        var voteManager = _serviceProvider.GetRequiredService<IVoteManager>();
        RegisterEventHandler<EventPlayerConnectFull>(voteManager.OnPlayerConnectFull);
        RegisterEventHandler<EventPlayerDisconnect>(voteManager.OnPlayerDisconnect);
    }

    public void OnConfigParsed(CustomVotesConfig config)
    {
        Config = config;
        
        var voteManager = _serviceProvider.GetRequiredService<IVoteManager>();

        foreach (var customVote in Config.CustomVotes)
            voteManager.AddVote(customVote);
        
        config.Update();
    }
    
    public override void Unload(bool hotReload)
    {
        base.Unload(hotReload);
    }
}

public class CustomVotesServiceCollection : IPluginServiceCollection<CustomVotes>
{
    public void ConfigureServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton(this);
        serviceCollection.AddSingleton<IVoteManager, VoteManager>();
        serviceCollection.AddSingleton<ICustomVoteApi, CustomVoteApi>();
        serviceCollection.AddScoped<IActiveVoteFactory, ActiveVoteFactory>();
        
        serviceCollection.AddLogging(options =>
        {
            options.AddConsole();
        });
    }
}