using Riok.Mapperly.Abstractions;

namespace WebApi.Mappers;

[Mapper]
public static partial class TranslatingManyTextsIntoOneLanguageResult
{
    public static partial gRpc.TranslatingManyTextsIntoOneLanguageResult FromCoreDtoToProtobufMessage(Core.Dtos.TranslatingManyTextsIntoOneLanguageResult dto);
}