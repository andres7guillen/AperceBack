using ManagementTaskAPI.Requests.Tasks;
using ManagementTaskApplication.Commands.Tasks.AddTask;
using ManagementTaskApplication.Commands.Tasks.CompleteTask;
using ManagementTaskApplication.Commands.Tasks.StartTask;
using ManagementTaskApplication.Queries.Tasks.GetTasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManagementTaskAPI.Controllers;

[Route("api/tasks")]
[ApiController]
public class TasksController : ControllerBase
{
    private readonly IMediator _mediator;

    public TasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] AddTaskRequest request)
    {
        var command = new AddTaskToUserCommand(
            request.UserId,
            request.Title
        );

        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok();
    }

    [HttpPost("{taskId}/start")]
    public async Task<IActionResult> Start(
        Guid taskId,
        [FromBody] ChangeTaskStatusRequest request)
    {
        var command = new StartTaskCommand(
            request.UserId,
            taskId
        );

        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok();
    }

    [HttpPost("{taskId}/complete")]
    public async Task<IActionResult> Complete(
        Guid taskId,
        [FromBody] ChangeTaskStatusRequest request)
    {
        var command = new CompleteTaskCommand(
            request.UserId,
            taskId
        );

        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok();
    }


    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
        var tasks = await _mediator.Send(new GetTasksQuery());
        return Ok(tasks); 
    }


}
