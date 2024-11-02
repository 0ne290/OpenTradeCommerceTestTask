namespace ConsoleClient.Models;

public class TranslatingManyTextsIntoOneLanguageResult
{
    [JsonProperty(Required = Required.Always, PropertyName = "language")]
    public required string Language { get; init; }

    [JsonProperty(Required = Required.Always, PropertyName = "translationsBySourceTexts")]
    public required IDictionary<string, string> TranslationsBySourceTexts { get; init; }
}
