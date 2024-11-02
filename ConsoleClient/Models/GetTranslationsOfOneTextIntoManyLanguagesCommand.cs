namespace ConsoleClient.Models;

public class GetTranslationsOfOneTextIntoManyLanguagesCommand
{
    public required string Text { get; init; }
    
    public required IEnumerable<string> Languages { get; init; }
}
