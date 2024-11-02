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
        GetTranslationsOfOneTextIntoManyLanguagesCommand? request1 = null;
        GetTranslationsOfManyTextsIntoOneLanguageCommand? request2 = null;
        try
        {
            Console.Write("Запрос на перевод одного текста на множество языков: ");
            request1 = JsonConvert.DeserializeObject<GetTranslationsOfOneTextIntoManyLanguagesCommand>(Console.ReadLine()!);
            Console.Write("Запрос на перевод множества текстов на один язык: ");
            request2 = JsonConvert.DeserializeObject<GetTranslationsOfManyTextsIntoOneLanguageCommand>(Console.ReadLine()!);
        }
        catch (Exception)
        {
            // ignored
        }

        var services = new ServiceCollection();
        services.AddGrpcClient<gRpc.Translator.TranslatorClient>(o =>
        {
            o.Address = new Uri("https://localhost:443");
        });
        services.AddScoped<ITranslator, GRpcTranslator>();
        
        var serviceProvider = services.BuildServiceProvider();

        await using var scope = serviceProvider.CreateAsyncScope();
        var translator = scope.ServiceProvider.GetRequiredService<ITranslator>();

        var info = await translator.GetInformation();
        Console.WriteLine(JsonConvert.SerializeObject(info));
        
        if (request1 != null)
            Console.WriteLine(JsonConvert.SerializeObject(await translator.GetTranslationsOfOneTextIntoManyLanguages(request1)));
        if (request2 != null)
            Console.WriteLine(JsonConvert.SerializeObject(await translator.GetTranslationsOfManyTextsIntoOneLanguage(request2)));
        
        Console.Write(Environment.NewLine + "Для завершения программы нажмите любую клавишу...");
        Console.ReadKey();
    }
}