using Core.Commands;

namespace Core.Handlers;

public class GetTranslationHandler(ITranslator translator, ICache cache)
{
    public async Task<string> Handle(GetTranslationCommand request)
    {
        var languages = request.Languages.ToHashSet();
        var translationsByLanguage = new Dictionary<string, string>(languages.Count);
        
        foreach (var language in languages)
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
