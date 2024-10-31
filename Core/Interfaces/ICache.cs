namespace Core.Interfaces;

public interface ICache
{
    bool TryGetTranslation(string text, string language, out string? translation);
}
