namespace Core.Commands;

public class GetTranslationCommand
{
    public required string Text { get; init; }
    
    public required ICollection<string> Languages { get; init; }
}