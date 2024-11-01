using Core.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Cache;

public class MemoryCache(IMemoryCache cache) : ICache
{
    public bool TryGetTranslation(string text, string language, out string? translation) =>
        _cache.TryGetValue($"Text : {text}; Language : {language}.", out translation);

    public void SetTranslation(string text, string language, string translation) =>
        _cache.Set($"Text : {text}; Language : {language}.", translation);

    private readonly IMemoryCache _cache = cache;
}