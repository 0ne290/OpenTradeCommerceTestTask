using System.Net;
using System.Text;
using Core.Interfaces;
using FluentResults;
using Newtonsoft.Json;

namespace Infrastructure.Translator;

public class YandexTranslate(HttpClient httpClient, string folderId) : ITranslator
{
    public async Task<Result<string>> Translate(string text, string language)
    {
        var response = await _httpClient.PostAsync((string?)null,
            new StringContent(
                $"{{ \"folderId\": \"{_folderId}\", \"texts\": [\"{text}\"], \"targetLanguageCode\": \"{language}\" }}",
                Encoding.UTF8, "application/json"));
        
        if (response.StatusCode == HttpStatusCode.BadRequest)
            return Result.Fail("Failed to translate text. Incorrect language code.");
        if (!response.IsSuccessStatusCode)
            throw new InvalidOperationException(
                "Failed to translate text. Incorrect folder ID, API key and/or security settings of folder.");

        var responseBody = await response.Content.ReadAsStringAsync();
        var translation = (string)JsonConvert.DeserializeObject<dynamic>(responseBody)!.translations[0].text;

        return translation;
    }

    private readonly HttpClient _httpClient = httpClient;

    private readonly string _folderId = folderId;
}
