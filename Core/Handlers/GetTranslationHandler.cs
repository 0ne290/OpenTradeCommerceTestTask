using Core.Commands;
using Core.Dtos;
using Core.Interfaces;
using MediatR;

namespace Core.Handlers;

public class GetTranslationHandler(ITranslator translator, ICache cache) : IRequestHandler<GetTranslationCommand, TranslationResult>
{
    public async Task<TranslationResult> Handle(GetTranslationCommand request, CancellationToken cancellationToken)
    {
        var languages = request.Languages.ToHashSet();
        var translationsByLanguage = new Dictionary<string, string>(languages.Count);

        foreach (var language in languages)
        {
            if (_cache.TryGetTranslation(request.Text, language, out var translation))
                translationsByLanguage.Add(language, translation!);
            else
            {
                var result = await _translator.Translate(request.Text, language);

                translation = result.IsFailed ? string.Join(" ", result.Errors.Select(e => e.Message)) : result.Value;

                _cache.SetTranslation(request.Text, language, translation);
                translationsByLanguage.Add(language, translation);
            }
        }

        return new TranslationResult { SourceText = request.Text, TranslationsByLanguage = translationsByLanguage };
    }

    private readonly ITranslator _translator = translator;

    private readonly ICache _cache = cache;
}