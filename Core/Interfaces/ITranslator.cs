using FluentResults;

namespace Core.Interfaces;

public interface ITranslator
{
    Task<Result<string>> Translate(string text, string language);
}