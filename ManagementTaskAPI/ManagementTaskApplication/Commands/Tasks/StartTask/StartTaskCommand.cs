using CSharpFunctionalExtensions;
using ManagementTaskDomain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementTaskApplication.Commands.Tasks.StartTask;

public sealed record StartTaskCommand(Guid UserId,Guid TaskId) : IRequest<Result>
{
    public sealed class Handler : IRequestHandler<StartTaskCommand, Result>
    {
        private readonly IUserRepository _userRepository;

        public Handler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(StartTaskCommand request,CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user.HasNoValue)
                return Result.Failure("User not found.");

            var result = user.Value.StartTask(request.TaskId);
            if (result.IsFailure)
                return result;

            await _userRepository.SaveChangesAsync();
            return Result.Success();
        }
    }
}
