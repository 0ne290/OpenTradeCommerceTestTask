using Core.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebApi.Controllers;

[Route("translator")]
public class TranslatorController(IMediator mediator) : Controller
{
    [Route("translate-one-text-into-many-languages")]
    [HttpPost]
    public async Task<IActionResult> GetTranslation([FromBody] GetTranslationsOfOneTextIntoManyLanguagesCommand request) =>
        Ok(JsonConvert.SerializeObject(await _mediator.Send(request)));
    
    [Route("translate-many-texts-into-one-language")]
    [HttpPost]
    public async Task<IActionResult> GetTranslation([FromBody] GetTranslationsOfManyTextsIntoOneLanguageCommand request) =>
        Ok(JsonConvert.SerializeObject(await _mediator.Send(request)));
    
    [Route("information")]
    [HttpGet]
    public IActionResult GetInformation() =>
        Ok("{ \"cacheProvider\": \"MemoryCache\", \"server\": \"REST\" }");

    private readonly IMediator _mediator = mediator;
}