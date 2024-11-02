namespace ConsoleClient.Models;

public class TranslatingManyTextsIntoOneLanguageResult
{
    public required string Language { get; init; }

    public required IDictionary<string, string> TranslationsBySourceTexts { get; init; }
}