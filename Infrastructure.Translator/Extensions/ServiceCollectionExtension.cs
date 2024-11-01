using System.Net.Http.Headers;
using Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Translator.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services, string yandexTranslateApiUrl, string apiKey, string folderId)
    {
        services.AddHttpClient("YandexTranslateApi", httpClient =>
        {
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.BaseAddress = new Uri(yandexTranslateApiUrl);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Api-Key", apiKey);
        });

        services.AddScoped<ITranslator, YandexTranslate>(serviceProvider =>
            new YandexTranslate(
                serviceProvider.GetRequiredService<IHttpClientFactory>().CreateClient("YandexTranslateApi"), folderId));

        return services;
    }
}