using Riok.Mapperly.Abstractions;

namespace WebApi.Mappers;

[Mapper]
public static partial class TranslatingOneTextIntoManyLanguagesResult
{
    public static partial gRpc.TranslatingOneTextIntoManyLanguagesResult FromCoreDtoToProtobufMessage(Core.Dtos.TranslatingOneTextIntoManyLanguagesResult dto);
}