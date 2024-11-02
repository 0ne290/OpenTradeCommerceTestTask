using Riok.Mapperly.Abstractions;

namespace WebApi.Mappers;

[Mapper]
public static partial class GetTranslationsOfOneTextIntoManyLanguagesCommand
{
    public static partial Core.Commands.GetTranslationsOfOneTextIntoManyLanguagesCommand FromProtobufMessageToCoreCommand(gRpc.GetTranslationsOfOneTextIntoManyLanguagesCommand message);
}