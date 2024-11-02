using Riok.Mapperly.Abstractions;

namespace ConsoleClient.Mappers;

[Mapper]
public static partial class Information
{
    public static partial Models.Information FromProtobufMessageToModel(gRpc.Information message);
}