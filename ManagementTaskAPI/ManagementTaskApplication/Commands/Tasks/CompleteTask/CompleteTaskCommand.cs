using CSharpFunctionalExtensions;
using ManagementTaskDomain.Repositories;
using MediatR;

namespace ManagementTaskApplication.Commands.Tasks.CompleteTask;

public sealed record CompleteTaskCommand(Guid UserId,Guid TaskId) : IRequest<Result>
{
    public sealed class Handler : IRequestHandler<CompleteTaskCommand, Result>
    {
        private readonly IUserRepository _userRepository;

        public Handler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(
            CompleteTaskCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user.HasNoValue)
                return Result.Failure("User not found.");

            var result = user.Value.CompleteTask(request.TaskId);
            if (result.IsFailure)
                return result;

            await _userRepository.SaveChangesAsync();
            return Result.Success();
        }
    }
}
