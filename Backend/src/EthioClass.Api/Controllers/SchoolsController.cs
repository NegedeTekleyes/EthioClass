
using EthioClass.Application.Schools.Commands;
using EthioClass.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Microsoft.AspNetCore.Components.Route("api/schools")]
public class SchoolsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateSchool(CreateSchoolCommand command, CancellationToken ct)
    {
        try
        {
            var result = await mediator.Send(command, ct);
            return CreatedAtAction(nameof(CreateSchool), new { id = result.Id }, result);
        }
        catch (InvalidOperationException ex) when(ex.Message.Contains("already exist"))

        {
            return Conflict(new ProblemDetails
            {
                Title = "School code already exists",
                Detail = ex.Message,
                Status = StatusCodes.Status409Conflict
            });

        }
    }
}