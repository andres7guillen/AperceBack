using ManagementTaskApplication.DTOs;
using ManagementTaskDomain.Repositories;
using MediatR;

namespace ManagementTaskApplication.Queries.Tasks.GetTasks;

public sealed record GetTasksQuery : IRequest<IReadOnlyList<TaskDto>>
{
    public sealed class Handler : IRequestHandler<GetTasksQuery, IReadOnlyList<TaskDto>>
    {
        private readonly ITaskItemRepository _taskRepository;

        public Handler(ITaskItemRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IReadOnlyList<TaskDto>> Handle(
            GetTasksQuery request,
            CancellationToken cancellationToken)
        {
            var tasks = await _taskRepository.GetAllAsync();
            if(tasks.IsFailure)
                return new List<TaskDto>();
            return tasks.Value
                .Select(t => new TaskDto(
                    t.Id,
                    t.Title,
                    t.Status.ToString(),
                    t.UserId,
                    t.User.Name
                ))
                .ToList();
        }
    }
}
