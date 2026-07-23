using EthioClass.Application.Users.Commands;
using EthioClass.Application.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EthioClass.Api.Controllers
{
   [ApiController]
   [Route("api/users")]
   [Authorize]
   public class UsersController(IMediator mediator) : ControllerBase
   {
      [HttpPost("register")]
      [AllowAnonymous]
      public async Task<IActionResult> Register(RegisterUserCommand command, CancellationToken ct)
      {
         try
         {
            var result = await mediator.Send(command, ct);
            return CreatedAtAction(nameof(Register), new { id = result.Id }, result);
         }
         catch (FluentValidation.ValidationException ex)
         {
            var errors = ex.Errors
               .GroupBy(e => e.PropertyName)
               .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
            return ValidationProblem(new ValidationProblemDetails(errors));
         }
         catch (InvalidOperationException ex) when (ex.Message.Contains("already registered"))
         {
            return Conflict(new ProblemDetails
            {
               Title = "Email already registerd",
               Detail = ex.Message,
               Status = StatusCodes.Status409Conflict
            });
         }
      }


      [HttpPost("login")]
      [AllowAnonymous]
      public async Task<IActionResult> Login(LoginQuery query, CancellationToken ct)
      {
         try
         {
            var result = await mediator.Send(query, ct);
            return Ok(result);
         }
         catch (InvalidOperationException ex)
         {
            return Unauthorized(new ProblemDetails
            {
               Title = "Login failed",
               Detail = ex.Message,
               Status = StatusCodes.Status401Unauthorized
            });
         }
      }

   }
}