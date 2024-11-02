using Newtonsoft.Json;

namespace ConsoleClient.Models;

public class TranslatingOneTextIntoManyLanguagesResult
{
    [JsonProperty(Required = Required.Always, PropertyName = "sourceText")]
    public required string SourceText { get; init; }

    [JsonProperty(Required = Required.Always, PropertyName = "translationsByLanguage")]
    public required IDictionary<string, string> TranslationsByLanguage { get; init; }
}
