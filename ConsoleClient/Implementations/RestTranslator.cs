using ConsoleClient.Interfaces;
using ConsoleClient.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace ConsoleClient.Implementations;

public class RestTranslator(HttpClient httpClient, string getTranslationsOfOneTextIntoManyLanguagesUrl, string getTranslationsOfManyTextsIntoOneLanguageUrl, string getInformationUrl) : ITranslator
{
    public async Task<TranslatingOneTextIntoManyLanguagesResult> GetTranslationsOfOneTextIntoManyLanguages(GetTranslationsOfOneTextIntoManyLanguagesCommand request)
    {
        var response = await _httpClient.PostAsync(_getTranslationsOfOneTextIntoManyLanguagesUrl,
            new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

        return JsonConvert.DeserializeObject<TranslatingOneTextIntoManyLanguagesResult>(await response.Content.ReadAsStringAsync());
    }

    public async Task<TranslatingManyTextsIntoOneLanguageResult> GetTranslationsOfManyTextsIntoOneLanguage(GetTranslationsOfManyTextsIntoOneLanguageCommand request)
    {
        var response = await _httpClient.PostAsync(_getTranslationsOfManyTextsIntoOneLanguageUrl,
            new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

        return JsonConvert.DeserializeObject<TranslatingManyTextsIntoOneLanguageResult>(await response.Content.ReadAsStringAsync());
    }

    public async Task<Information> GetInformation()
    {
        var response = await _httpClient.GetAsync(_getInformationUrl);

        return JsonConvert.DeserializeObject<Information>(await response.Content.ReadAsStringAsync());
    }

    private readonly HttpClient _httpClient = httpClient;

    private readonly string _getTranslationsOfOneTextIntoManyLanguagesUrl = getTranslationsOfOneTextIntoManyLanguagesUrl;

    private readonly string _getTranslationsOfManyTextsIntoOneLanguageUrl = getTranslationsOfManyTextsIntoOneLanguageUrl;

    private readonly string _getInformationUrl = getInformationUrl;
}
