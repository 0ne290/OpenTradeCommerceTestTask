using Riok.Mapperly.Abstractions;

namespace WebApi.Mappers;

[Mapper]
public static partial class TranslationResult
{
    public static partial gRpc.TranslationResult FromCoreDtoToProtobufMessage(Core.Dtos.TranslatingOneTextIntoManyLanguagesResult dto);
}