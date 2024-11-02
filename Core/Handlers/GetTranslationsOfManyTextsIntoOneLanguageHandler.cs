using Core.Commands;
using Core.Dtos;
using Core.Interfaces;
using MediatR;

namespace Core.Handlers;

public class GetTranslationsOfManyTextsIntoOneLanguage(ITranslator translator, ICache cache) : IRequestHandler<GetTranslationsOfManyTextsIntoOneLanguageCommand, TranslatingManyTextsIntoOneLanguageResult>
{
    public async Task<TranslatingManyTextsIntoOneLanguageResult> Handle(GetTranslationsOfManyTextsIntoOneLanguageCommand request, CancellationToken cancellationToken)
    {
        var sourceTexts = request.Texts.ToHashSet();
        var uncachedSourceTexts = new List<string>(sourceTexts.Count);
        var translationsBySourceTexts = new Dictionary<string, string>(sourceTexts.Count);

        foreach (var sourceText in sourceTexts)
        {
            if (_cache.TryGetTranslation(sourceText, request.Language, out var translation))
                translationsBySourceTexts.Add(sourceText, translation!);
            else
                uncachedSourceTexts.Add(sourceText);
        }

        if (uncachedSourceTexts.Count > 0)
        {
            var result = await _translator.Translate(uncachedSourceTexts, request.Language);

            IList<string> translations;
            if (result.IsFailed)
            {
                translations = new string[uncachedSourceTexts.Count];
                Array.Fill((string[])translations, string.Join(" ", result.Errors.Select(e => e.Message)));
            }
            else
                translations = result.Value.ToList();

            for (var i = 0; i < uncachedSourceTexts.Count; i++)
            {
                _cache.SetTranslation(uncachedSourceTexts[i], request.Language, translations[i]);
                translationsBySourceTexts.Add(uncachedSourceTexts[i], translations[i]);
            }
        }

        return new TranslatingManyTextsIntoOneLanguageResult { Language = request.Language, TranslationsBySourceTexts = translationsBySourceTexts };
    }

    private readonly ITranslator _translator = translator;

    private readonly ICache _cache = cache;
}