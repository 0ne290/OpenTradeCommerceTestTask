using Newtonsoft.Json;

namespace ConsoleClient.Models;

public class Information
{
    [JsonProperty(Required = Required.Always, PropertyName = "cacheProvider")]
    public required string CacheProvider { get; init; }

    [JsonProperty(Required = Required.Always, PropertyName = "server")]
    public required string Server { get; init; }
}
