using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using WebApi.gRpc;

namespace WebApi.gRpcServices;

public class TranslatorService(IMediator mediator) : Translator.TranslatorBase
{
    public override async Task<TranslatingOneTextIntoManyLanguagesResult> GetTranslationsOfOneTextIntoManyLanguages(GetTranslationsOfOneTextIntoManyLanguagesCommand request,
        ServerCallContext context)
    {
        var command = Mappers.GetTranslationsOfOneTextIntoManyLanguagesCommand.FromProtobufMessageToCoreCommand(request);

        var translations = await _mediator.Send(command);

        return Mappers.TranslatingOneTextIntoManyLanguagesResult.FromCoreDtoToProtobufMessage(translations);
    }
    
    public override async Task<TranslatingManyTextsIntoOneLanguageResult> GetTranslationsOfManyTextsIntoOneLanguage(GetTranslationsOfManyTextsIntoOneLanguageCommand request,
        ServerCallContext context)
    {
        var command = Mappers.GetTranslationsOfManyTextsIntoOneLanguageCommand.FromProtobufMessageToCoreCommand(request);

        var translations = await _mediator.Send(command);

        return Mappers.TranslatingManyTextsIntoOneLanguageResult.FromCoreDtoToProtobufMessage(translations);
    }

    public override async Task<Information> GetInformation(Empty _, ServerCallContext context) =>
        await Task.FromResult(new Information { CacheProvider = "MemoryCache", Server = "gRPC" });

    private readonly IMediator _mediator = mediator;
}