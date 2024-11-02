using Newtonsoft.Json;

namespace ConsoleClient.Models;

public class GetTranslationsOfManyTextsIntoOneLanguageCommand
{
    [JsonProperty(Required = Required.Always, PropertyName = "texts")]
    public required IEnumerable<string> Texts { get; init; }

    [JsonProperty(Required = Required.Always, PropertyName = "language")]
    public required string Language { get; init; }
}
