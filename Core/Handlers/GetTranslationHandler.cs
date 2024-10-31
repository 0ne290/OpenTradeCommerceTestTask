using Core.Commands;

namespace Core.Handlers;

public class GetTranslationHandler(ITranslator translator, ICache cache)
{
    public async Task<string> Handle(GetTranslationCommand request)
    {
        var translationsByLanguage = new Dictionary<string, string>(request.Languages.Count);
        
        foreach (var language in request.Languages)
        {
            if (_cache.TryGetTranslation(request.Text, language, out var translation))
                translationsByLanguage.Add(language, translation);
            else
            {
                var translation = await _translator.Translate(text, language);
                _cache.SetTranslation(request.Text, language, translation);
                translationsByLanguage.Add(language, translation);
            }
        }

        return new TranslationResult { SourceText = request.Text, TranslationsByLanguage = translationsByLanguage };
    }

    private ITranslator _translator = translator;

    private ICache _cache = cache;
}
