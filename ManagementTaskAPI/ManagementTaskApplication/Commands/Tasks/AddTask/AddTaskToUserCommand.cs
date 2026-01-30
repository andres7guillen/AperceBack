using CSharpFunctionalExtensions;
using ManagementTaskDomain.Repositories;
using MediatR;

namespace ManagementTaskApplication.Commands.Tasks.AddTask;

public sealed record AddTaskToUserCommand(Guid UserId,string Title) : IRequest<Result>
{
    public sealed class Handler : IRequestHandler<AddTaskToUserCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITaskItemRepository _taskRepository;

        public Handler(IUserRepository userRepository, ITaskItemRepository taskRepository)
        {
            _userRepository = userRepository;
            _taskRepository = taskRepository;
        }

        public async Task<Result> Handle(AddTaskToUserCommand request, CancellationToken cancellationToken)
        {
            var maybeUser = await _userRepository.GetByIdAsync(request.UserId);
            if (maybeUser.HasNoValue)
                return Result.Failure("User not found.");

            var user = maybeUser.Value;
            var taskId = Guid.NewGuid();

            var taskResult = user.AddTask(taskId, request.Title);
            if (taskResult.IsFailure)
                return taskResult;

            await _taskRepository.AddAsync(taskResult.Value);
            await _userRepository.SaveChangesAsync();

            return Result.Success();
        }
    }
}
