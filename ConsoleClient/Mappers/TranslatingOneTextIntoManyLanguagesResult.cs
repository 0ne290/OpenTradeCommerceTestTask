using Riok.Mapperly.Abstractions;

namespace ConsoleClient.Mappers;

[Mapper]
public static partial class TranslatingOneTextIntoManyLanguagesResult
{
    public static partial Models.TranslatingOneTextIntoManyLanguagesResult FromProtobufMessageToModel(gRpc.TranslatingOneTextIntoManyLanguagesResult message);
}