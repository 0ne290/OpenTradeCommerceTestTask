using Riok.Mapperly.Abstractions;

namespace ConsoleClient.Mappers;

[Mapper]
public static partial class TranslatingManyTextsIntoOneLanguageResult
{
    public static partial Models.TranslatingManyTextsIntoOneLanguageResult FromProtobufMessageToModel(gRpc.TranslatingManyTextsIntoOneLanguageResult message);
}