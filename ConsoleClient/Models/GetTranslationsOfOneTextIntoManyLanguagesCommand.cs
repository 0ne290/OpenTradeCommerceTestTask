namespace ConsoleClient.Models;

public class GetTranslationsOfOneTextIntoManyLanguagesCommand
{
    [JsonProperty(Required = Required.Always, PropertyName = "text")]
    public required string Text { get; init; }

    [JsonProperty(Required = Required.Always, PropertyName = "languages")]
    public required IEnumerable<string> Languages { get; init; }
}
