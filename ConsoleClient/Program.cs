using ConsoleClient.Implementations;
using ConsoleClient.Interfaces;
using ConsoleClient.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace ConsoleClient;

internal static class Program
{
    private static async Task Main()
    {
        var services = new ServiceCollection();

        Console.Write("Введите способ доступа к сервису перевода (g - gRPC, r - REST): ");
        switch (Console.ReadLine()!)
        {
            case "g":
                services.AddGrpcClient<gRpc.Translator.TranslatorClient>(o =>
                {
                    o.Address = new Uri("https://localhost:443");
                });
                services.AddScoped<ITranslator, GRpcTranslator>();
                break;
            case "r":
                services.AddHttpClient("TranslatorApi", httpClient =>
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.BaseAddress = new Uri("https://localhost");
                });
                services.AddScoped<ITranslator, RestTranslator>(serviceProvider => new RestTranslator(serviceProvider.GetRequiredService<IHttpClientFactory>().CreateClient("TranslatorApi"), "translator/translate-one-text-into-many-languages", "translator/translate-many-texts-into-one-language", "translator/information"));
                break;
            default:
                Console.Write(Environment.NewLine + "Некорректный ввод. Для завершения программы нажмите любую клавишу...");
                Console.ReadKey();
                break;
        }
        
        GetTranslationsOfOneTextIntoManyLanguagesCommand? request1 = null;
        GetTranslationsOfManyTextsIntoOneLanguageCommand? request2 = null;
        try
        {
            Console.Write("Введите запрос на перевод одного текста на множество языков: ");
            request1 = JsonConvert.DeserializeObject<GetTranslationsOfOneTextIntoManyLanguagesCommand>(Console.ReadLine()!);
            Console.Write("Введите запрос на перевод множества текстов на один язык: ");
            request2 = JsonConvert.DeserializeObject<GetTranslationsOfManyTextsIntoOneLanguageCommand>(Console.ReadLine()!);
        }
        catch (Exception)
        {
            // ignored
        }
        
        var serviceProvider = services.BuildServiceProvider();

        await using var scope = serviceProvider.CreateAsyncScope();
        var translator = scope.ServiceProvider.GetRequiredService<ITranslator>();

        var info = await translator.GetInformation();
        Console.WriteLine($"{Environment.NewLine}Информация о сервисе: {JsonConvert.SerializeObject(info)}.");
        
        if (request1 != null)
            Console.WriteLine($"Результат выполнения запроса на перевод одного текста на множество языков: {JsonConvert.SerializeObject(await translator.GetTranslationsOfOneTextIntoManyLanguages(request1))}.");
        if (request2 != null)
            Console.WriteLine($"Результат выполнения запроса на перевод множества текстов на один язык: {JsonConvert.SerializeObject(await translator.GetTranslationsOfManyTextsIntoOneLanguage(request2))}.");
        
        Console.Write(Environment.NewLine + "Для завершения программы нажмите любую клавишу...");
        Console.ReadKey();
    }
}
