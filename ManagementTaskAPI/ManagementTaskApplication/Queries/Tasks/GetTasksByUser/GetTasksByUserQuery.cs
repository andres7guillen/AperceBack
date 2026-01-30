using ManagementTaskApplication.DTOs;
using ManagementTaskDomain.Repositories;
using MediatR;

namespace ManagementTaskApplication.Queries.Tasks.GetTasksByUser;

public sealed record GetTasksByUserQuery(
    Guid UserId
) : IRequest<IReadOnlyList<TaskDto>>
{
    public sealed class Handler : IRequestHandler<GetTasksByUserQuery, IReadOnlyList<TaskDto>>
    {
        private readonly ITaskItemRepository _taskRepository;

        public Handler(ITaskItemRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IReadOnlyList<TaskDto>> Handle(
            GetTasksByUserQuery request,
            CancellationToken cancellationToken)
        {
            var tasks = await _taskRepository.GetByUserIdAsync(request.UserId);

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
