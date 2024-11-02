using Newtonsoft.Json;

namespace Core.Dtos;

public class TranslatingOneTextIntoManyLanguagesResult
{
    [JsonProperty(Required = Required.Always, PropertyName = "sourceText")]
    public required string SourceText { get; init; }

    [JsonProperty(Required = Required.Always, PropertyName = "translationsByLanguage")]
    public required IDictionary<string, string> TranslationsByLanguage { get; init; }
}
