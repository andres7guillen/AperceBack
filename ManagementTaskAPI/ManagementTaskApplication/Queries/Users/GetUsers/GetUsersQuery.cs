using ManagementTaskApplication.DTOs;
using ManagementTaskDomain.Repositories;
using MediatR;

namespace ManagementTaskApplication.Queries.Users.GetUsers;

public sealed record GetUsersQuery : IRequest<IReadOnlyList<UserDto>>
{
    public sealed class Handler : IRequestHandler<GetUsersQuery, IReadOnlyList<UserDto>>
    {
        private readonly IUserRepository _userRepository;

        public Handler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IReadOnlyList<UserDto>> Handle(
            GetUsersQuery request,
            CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();

            return users
                .Value
                .Select(u => new UserDto(
                    u.Id,
                    u.Name,
                    u.Email
                ))
                .ToList();
        }
    }
}
