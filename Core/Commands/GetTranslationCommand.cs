using Core.Dtos;
using MediatR;

namespace Core.Commands;

public class GetTranslationCommand : IRequest<TranslationResult>
{
    public required string Text { get; init; }
    
    public required IEnumerable<string> Languages { get; init; }
}
