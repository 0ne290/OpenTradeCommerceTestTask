namespace ConsoleClient.Models;

public class GetTranslationsOfManyTextsIntoOneLanguageCommand
{
    public required IEnumerable<string> Texts { get; init; }
    
    public required string Language { get; init; }
}
