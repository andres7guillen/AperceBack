using CSharpFunctionalExtensions;
using ManagementTaskDomain.Entities;
using ManagementTaskDomain.Repositories;
using MediatR;

namespace ManagementTaskApplication.Commands.Users.CreateUser;

public record CreateUserCommand(string Name,string Email) : IRequest<Result>
{
    public sealed class Handler : IRequestHandler<CreateUserCommand, Result>
    {
        private readonly IUserRepository _userRepository;

        public Handler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(
            CreateUserCommand request,
            CancellationToken cancellationToken)
        {
            var userId = Guid.NewGuid();

            var userResult = User.Create(userId, request.Name, request.Email);
            if (userResult.IsFailure)
                return userResult;

            var userCreated = await _userRepository.AddAsync(userResult.Value);
            return Result.Success(userCreated);
        }
    }
}
