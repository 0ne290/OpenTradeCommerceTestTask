using FluentResults;

namespace Core.Interfaces;

public interface ITranslator
{
    Task<Result<string>> Translate(string text, string language);
    
    Task<Result<IEnumerable<string>>> Translate(IEnumerable<string> texts, string language);
}