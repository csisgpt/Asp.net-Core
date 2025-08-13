using InsuranceSuite.Application.Features.PreRegistrations.Commands.CreatePreRegistration;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceSuite.Api.Controllers;

[ApiController]
[Route("api/preregistrations")]
public class PreRegistrationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PreRegistrationsController(IMediator mediator)
        => _mediator = mediator;

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Guid>> Create([FromBody] CreatePreRegistrationCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }
}
