using CSharpFunctionalExtensions;
using ManagementTaskDomain.Repositories;
using MediatR;

namespace ManagementTaskApplication.Commands.Users.DeleteUser;

public sealed record DeleteUserCommand(Guid UserId) : IRequest<Result<bool>>
{
    public sealed class Handler : IRequestHandler<DeleteUserCommand, Result<bool>>
    {
        private readonly IUserRepository _userRepository;

        public Handler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<bool>> Handle(DeleteUserCommand request,CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user.HasNoValue)
                return Result.Failure<bool>("User not found.");

            return await _userRepository.Remove(user.Value);
        }
    }
}
