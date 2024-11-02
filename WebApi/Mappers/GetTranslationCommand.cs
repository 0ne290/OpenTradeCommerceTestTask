using Riok.Mapperly.Abstractions;

namespace WebApi.Mappers;

[Mapper]
public static partial class GetTranslationCommand
{
    public static partial Core.Commands.GetTranslationsOfOneTextIntoManyLanguagesCommand FromProtobufMessageToCoreCommand(gRpc.GetTranslationCommand message);
}