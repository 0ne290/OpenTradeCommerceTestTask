using Core.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebApi.Controllers;

[Route("translator")]
public class TranslatorController(IMediator mediator) : Controller
{
    [Route("translate")]
    [HttpPost]
    public async Task<IActionResult> GetTranslation([FromBody] GetTranslationCommand request) =>
        Ok(JsonConvert.SerializeObject(await _mediator.Send(request)));
    
    [Route("information")]
    [HttpGet]
    public IActionResult GetInformation() =>
        Ok("{ \"cacheProvider\": \"MemoryCache\", \"server\": \"REST\" }");

    private readonly IMediator _mediator = mediator;
}