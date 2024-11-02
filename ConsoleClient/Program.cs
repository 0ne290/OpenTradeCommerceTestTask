using ConsoleClient.gRpc;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace ConsoleClient;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddGrpcClient<Translator.TranslatorClient>(o =>
        {
            o.Address = new Uri("https://localhost:443");
        });
        var serviceProvider = services.BuildServiceProvider();
        var client = serviceProvider.GetRequiredService<Translator.TranslatorClient>();

        var info = await client.GetInformationAsync(new Empty());
        var translations = await client.GetTranslationAsync(new GetTranslationCommand
            { Text = "Текст для тестирования перевода с доступом через gRPC.", Languages = { "en", "de", "fshv" } });
        
        //Console.WriteLine($"Info: {info}. Translation result:");
        //Console.WriteLine($"\tSourceText: {translations.SourceText}.");
        //foreach (var translationByLanguage in translations.TranslationsByLanguage)
        //{
        //    Console.WriteLine($"\tLanguage: {translationByLanguage.Key}.");
        //    Console.WriteLine($"\tTranslation: {translationByLanguage.Value}.");
        //}
        
        Console.WriteLine(JsonConvert.SerializeObject(info));
        Console.WriteLine(JsonConvert.SerializeObject(translations));
    }
}