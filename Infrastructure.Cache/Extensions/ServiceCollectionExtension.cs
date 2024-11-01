using Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Cache.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services, string yandexTranslateApiUrl, string apiKey, string folderId)
    {
        services.AddMemoryCache(o => o.SizeLimit = 65536);

        services.AddSingleton<ICache, MemoryCache>();

        return services;
    }
}