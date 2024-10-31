namespace Core.Interfaces;

public interface ICache
{
    string TryGetTranslation(string text, string language);
}