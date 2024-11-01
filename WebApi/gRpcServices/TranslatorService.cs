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

    private readonly IMediator _mediator = mediator;
}