using ConsoleClient.gRpc;
using ConsoleClient.Interfaces;
using Google.Protobuf.WellKnownTypes;

namespace ConsoleClient.Implementations;

public class GRpcTranslator(Translator.TranslatorClient translator) : ITranslator
{
    public async Task<Models.TranslatingOneTextIntoManyLanguagesResult> GetTranslationsOfOneTextIntoManyLanguages(
        Models.GetTranslationsOfOneTextIntoManyLanguagesCommand request)
    {
        var message = Mappers.GetTranslationsOfOneTextIntoManyLanguagesCommand.FromModelToProtobufMessage(request);

        var result = await _translator.GetTranslationsOfOneTextIntoManyLanguagesAsync(message);

        return Mappers.TranslatingOneTextIntoManyLanguagesResult.FromProtobufMessageToModel(result);
    }

    public async Task<Models.TranslatingManyTextsIntoOneLanguageResult> GetTranslationsOfManyTextsIntoOneLanguage(
        Models.GetTranslationsOfManyTextsIntoOneLanguageCommand request)
    {
        var message = Mappers.GetTranslationsOfManyTextsIntoOneLanguageCommand.FromModelToProtobufMessage(request);

        var result = await _translator.GetTranslationsOfManyTextsIntoOneLanguageAsync(message);

        return Mappers.TranslatingManyTextsIntoOneLanguageResult.FromProtobufMessageToModel(result);
    }

    public async Task<Models.Information> GetInformation()
    {
        var result = await _translator.GetInformationAsync(new Empty());

        return Mappers.Information.FromProtobufMessageToModel(result);
    }

    private readonly Translator.TranslatorClient _translator = translator;
}