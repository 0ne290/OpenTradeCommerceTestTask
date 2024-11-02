using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using WebApi.gRpc;

namespace WebApi.gRpcServices;

public class TranslatorService(IMediator mediator) : Translator.TranslatorBase
{
    public override async Task<TranslationResult> GetTranslation(GetTranslationCommand request,
        ServerCallContext context)
    {
        var command = Mappers.GetTranslationCommand.FromProtobufMessageToCoreCommand(request);

        var translations = await _mediator.Send(command);

        return Mappers.TranslationResult.FromCoreDtoToProtobufMessage(translations);
    }

    public override async Task<Information> GetInformation(Empty _, ServerCallContext context) =>
        await Task.FromResult(new Information { CacheProvider = "MemoryCache", Server = "gRPC" });

    private readonly IMediator _mediator = mediator;
}