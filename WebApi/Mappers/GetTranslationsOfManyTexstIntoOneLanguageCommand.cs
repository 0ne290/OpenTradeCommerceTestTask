using Riok.Mapperly.Abstractions;

namespace WebApi.Mappers;

[Mapper]
public static partial class GetTranslationsOfManyTextsIntoOneLanguageCommand
{
    public static partial Core.Commands.GetTranslationsOfManyTextsIntoOneLanguageCommand FromProtobufMessageToCoreCommand(gRpc.GetTranslationsOfManyTextsIntoOneLanguageCommand message);
}