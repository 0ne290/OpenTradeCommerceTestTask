using Riok.Mapperly.Abstractions;

namespace ConsoleClient.Mappers;

[Mapper]
public static partial class GetTranslationsOfOneTextIntoManyLanguagesCommand
{
    public static partial gRpc.GetTranslationsOfOneTextIntoManyLanguagesCommand FromModelToProtobufMessage(Models.GetTranslationsOfOneTextIntoManyLanguagesCommand model);
}