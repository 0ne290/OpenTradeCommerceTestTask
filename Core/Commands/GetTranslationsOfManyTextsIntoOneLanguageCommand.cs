using Core.Dtos;
using MediatR;
using Newtonsoft.Json;

namespace Core.Commands;

public class GetTranslationsOfManyTextsIntoOneLanguageCommand : IRequest<TranslatingManyTextsIntoOneLanguageResult>
{
    [JsonProperty(Required = Required.Always, PropertyName = "texts")]
    public required IEnumerable<string> Texts { get; init; }
    
    [JsonProperty(Required = Required.Always, PropertyName = "language")]
    public required string Language { get; init; }
}
