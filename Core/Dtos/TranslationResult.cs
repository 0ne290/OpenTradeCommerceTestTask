namespace Core.Dtos;

public class TranslationResult
{
    public required string SourceText { get; init; }

    public required IDictionary<string, string> TranslationsByLanguage { get; init; }
}