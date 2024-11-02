using Core.Dtos;
using MediatR;
using Newtonsoft.Json;

namespace Core.Commands;

public class GetTranslationsOfOneTextIntoManyLanguagesCommand : IRequest<TranslatingOneTextIntoManyLanguagesResult>
{
    [JsonProperty(Required = Required.Always, PropertyName = "text")]
    public required string Text { get; init; }
    
    [JsonProperty(Required = Required.Always, PropertyName = "languages")]
    public required IEnumerable<string> Languages { get; init; }
}
