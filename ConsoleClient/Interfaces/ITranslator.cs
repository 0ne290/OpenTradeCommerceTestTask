using ConsoleClient.Models;

namespace ConsoleClient.Interfaces;

public interface ITranslator
{
    Task<TranslatingOneTextIntoManyLanguagesResult> GetTranslationsOfOneTextIntoManyLanguages(
        GetTranslationsOfOneTextIntoManyLanguagesCommand request);

    Task<TranslatingManyTextsIntoOneLanguageResult> GetTranslationsOfManyTextsIntoOneLanguage(
        GetTranslationsOfManyTextsIntoOneLanguageCommand request);

    Task<Information> GetInformation();
}