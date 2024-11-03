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

        Console.WriteLine("Введите способ доступа к сервису перевода (g - gRPC, r - REST): ");
        var selectOfServer = Console.ReadLine()!;
        if (selectOfServer == "g")
        {
            services.AddGrpcClient<gRpc.Translator.TranslatorClient>(o =>
            {
                o.Address = new Uri("https://localhost:443");
            });
            services.AddScoped<ITranslator, GRpcTranslator>();
        }
        else if (selectOfServer == "r")
        {
            services.AddHttpClient("TranslatorApi", httpClient =>
            {
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.BaseAddress = new Uri("https://localhost:443/translator");
            });
            services.AddScoped<ITranslator, RestTranslator>(serviceProvider => new RestTranslator(serviceProvider.GetRequiredService<IHttpClientFactory>().CreateClient("TranslatorApi"), "translate-one-text-into-many-languages", "translate-many-texts-into-one-language", "information"));
        }
        else
        {
            Console.Write(Environment.NewLine + "Некорректный ввод. Для завершения программы нажмите любую клавишу...");
            Console.ReadKey();
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
        Console.WriteLine($"Информация о сервисе: {JsonConvert.SerializeObject(info)}.");
        
        if (request1 != null)
            Console.WriteLine($"Результат выполнения запроса на перевод одного текста на множество языков: {JsonConvert.SerializeObject(await translator.GetTranslationsOfOneTextIntoManyLanguages(request1))}.");
        if (request2 != null)
            Console.WriteLine($"Результат выполнения запроса на перевод множества текстов на один язык: {JsonConvert.SerializeObject(await translator.GetTranslationsOfManyTextsIntoOneLanguage(request2))}.");
        
        Console.Write(Environment.NewLine + "Для завершения программы нажмите любую клавишу...");
        Console.ReadKey();
    }
}
