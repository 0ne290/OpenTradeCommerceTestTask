using Riok.Mapperly.Abstractions;

namespace WebApi.Mappers;

[Mapper]
public static partial class GetTranslationCommand
{
    public static partial Core.Commands.GetTranslationCommand FromProtobufMessageToCoreCommand(gRpc.GetTranslationCommand message);
}