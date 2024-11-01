using Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Cache.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services, long sizeLimit)
    {
        services.AddMemoryCache(o => o.SizeLimit = sizeLimit);

        services.AddSingleton<ICache, MemoryCache>();

        return services;
    }
}