public class TranslationResult
{
  public required string SourceText { get; init; }
  
  public IDictionary<string, string> TranslationsByLanguage { get; init; }
}
