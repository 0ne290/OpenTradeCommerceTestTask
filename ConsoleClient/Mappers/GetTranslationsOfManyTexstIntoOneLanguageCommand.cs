using Riok.Mapperly.Abstractions;

namespace ConsoleClient.Mappers;

[Mapper]
public static partial class GetTranslationsOfManyTextsIntoOneLanguageCommand
{
    public static partial gRpc.GetTranslationsOfManyTextsIntoOneLanguageCommand FromModelToProtobufMessage(Models.GetTranslationsOfManyTextsIntoOneLanguageCommand model);
}