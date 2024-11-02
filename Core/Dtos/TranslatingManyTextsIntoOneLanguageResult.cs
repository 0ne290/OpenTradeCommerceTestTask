namespace Core.Dtos;

public class TranslatingManyTextsIntoOneLanguageResult
{
    public required string Language { get; init; }

    public required IDictionary<string, string> TranslationsBySourceTexts { get; init; }
}