using ManagementTaskAPI.Requests.Users;
using ManagementTaskApplication.Commands.Users.CreateUser;
using ManagementTaskApplication.Queries.Users.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ManagementTaskAPI.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateUserRequest request)
    {
        var command = new CreateUserCommand(
            request.Name,
            request.Email
        );

        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return CreatedAtAction(nameof(GetAll), null);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _mediator.Send(new GetUsersQuery());
        return Ok(users);
    }
}
