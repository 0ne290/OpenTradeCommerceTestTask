namespace Core.Interfaces;

public interface ITranslator
{
    Task<string> Translate(string text, string language);
}