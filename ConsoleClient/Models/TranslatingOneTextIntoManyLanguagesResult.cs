namespace ConsoleClient.Models;

public class TranslatingOneTextIntoManyLanguagesResult
{
    public required string SourceText { get; init; }

    public required IDictionary<string, string> TranslationsByLanguage { get; init; }
}