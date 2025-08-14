using InsuranceSuite.Application.Features.PreRegistrations.Commands.CreatePreRegistration;
using InsuranceSuite.Application.Features.PreRegistrations.Commands.UpdatePreRegistration;
using InsuranceSuite.Application.Features.PreRegistrations.Commands.DeletePreRegistration;
using InsuranceSuite.Application.Features.PreRegistrations.Queries.GetPreRegistrationById;
using InsuranceSuite.Application.Features.PreRegistrations.Queries.GetPreRegistrations;
using InsuranceSuite.Application.Features.PreRegistrations.Dto;
using System.Collections.Generic;
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

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<PreRegistrationDto>>> GetAll()
    {
        var list = await _mediator.Send(new GetPreRegistrationsQuery());
        return Ok(list);
    }

    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<ActionResult<PreRegistrationDto>> Get(Guid id)
    {
        var pre = await _mediator.Send(new GetPreRegistrationByIdQuery(id));
        return Ok(pre);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Guid>> Create([FromBody] CreatePreRegistrationCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpPut("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePreRegistrationCommand command)
    {
        if (id != command.Id)
            return BadRequest();

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeletePreRegistrationCommand(id));
        return NoContent();
    }
}
