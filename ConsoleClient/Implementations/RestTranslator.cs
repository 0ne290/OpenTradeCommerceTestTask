using ConsoleClient.Interfaces;
using ConsoleClient.Models;

namespace ConsoleClient.Implementations;

public class RestTranslator : ITranslator
{
    public Task<TranslatingOneTextIntoManyLanguagesResult> GetTranslationsOfOneTextIntoManyLanguages(GetTranslationsOfOneTextIntoManyLanguagesCommand request)
    {
        throw new NotImplementedException();
    }

    public Task<TranslatingManyTextsIntoOneLanguageResult> GetTranslationsOfManyTextsIntoOneLanguage(GetTranslationsOfManyTextsIntoOneLanguageCommand request)
    {
        throw new NotImplementedException();
    }

    public Task<Information> GetInformation()
    {
        throw new NotImplementedException();
    }
}