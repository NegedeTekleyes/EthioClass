using EthioClass.Application.Schools.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EthioClass.Api.Controllers;

[ApiController]
[Route("api/schools")]
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
        catch (FluentValidation.ValidationException ex)
        {
            var errors = ex.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
            return ValidationProblem(new ValidationProblemDetails(errors));
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