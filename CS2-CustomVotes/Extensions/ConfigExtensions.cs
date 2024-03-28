using System.Reflection;
using System.Text.Json;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;

namespace CS2_CustomVotes.Extensions;

public static class ConfigExtensions
{
    private static readonly string AssemblyName = Assembly.GetExecutingAssembly().GetName().Name ?? "";
    public static readonly string CfgPath = $"{Server.GameDirectory}/csgo/addons/counterstrikesharp/configs/plugins/{AssemblyName}/{AssemblyName}.json";
    
    public static void Update<T>(this T config) where T : BasePluginConfig, new()
    {
        // get current config version
        var newCfgVersion = new T().Version;
        
        // loaded config is up to date
        if (config.Version == newCfgVersion)
            return;
        
        // update the version
        config.Version = newCfgVersion;
        
        // serialize the updated config back to json
        var updatedJsonContent = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(CfgPath, updatedJsonContent);
    }
}