using System.Text;
using Core.Interfaces;
using Newtonsoft.Json;

namespace Infrastructure.Translator;

public class YandexTranslate(HttpClient httpClient, string folderId) : ITranslator
{
    public async Task<string> Translate(string text, string language)
    {
        var response = await _httpClient.PostAsync((string?)null,
            new StringContent(
                $"{{ \"folderId\": \"{_folderId}\", \"texts\": [\"{text}\"], \"targetLanguageCode\": \"{language}\" }}",
                Encoding.UTF8, "application/json"));

        if (!response.IsSuccessStatusCode)
            throw new InvalidOperationException("Failed to translate text. Please check if the folder ID and API key are correct.");
        
        var responseBody = await response.Content.ReadAsStringAsync();
        var translation = (string)JsonConvert.DeserializeObject<dynamic>(responseBody)!.translations[0].text;

        return translation;
    }
    
    private readonly HttpClient _httpClient = httpClient;

    private readonly string _folderId = folderId;
}