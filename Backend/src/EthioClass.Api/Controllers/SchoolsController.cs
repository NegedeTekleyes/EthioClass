using EthioClass.Application.Schools.Commands;
using EthioClass.Application.Schools.Queries;
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
            return CreatedAtAction(nameof(GetSchoolById), new { id = result.Id }, result);
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

    [HttpGet("{id:int}", Name = nameof(GetSchoolById))]
    public async Task<IActionResult> GetSchoolById(int id, CancellationToken ct)
    {
        var school = await mediator.Send(new GetSchoolByIdQuery(id), ct);
        return school is not null ? Ok(school) : NotFound();
    }
}