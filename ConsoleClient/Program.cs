using ConsoleClient.gRpc;
using ConsoleClient.Implementations;
using ConsoleClient.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using GetTranslationsOfManyTextsIntoOneLanguageCommand = ConsoleClient.Models.GetTranslationsOfManyTextsIntoOneLanguageCommand;
using GetTranslationsOfOneTextIntoManyLanguagesCommand = ConsoleClient.Models.GetTranslationsOfOneTextIntoManyLanguagesCommand;

namespace ConsoleClient;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        var request1 = JsonConvert.DeserializeObject<GetTranslationsOfOneTextIntoManyLanguagesCommand>(args[1]);
        var request2 = JsonConvert.DeserializeObject<GetTranslationsOfManyTextsIntoOneLanguageCommand>(args[2]);
        
        var services = new ServiceCollection();
        services.AddGrpcClient<Translator.TranslatorClient>(o =>
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