using CSharpFunctionalExtensions;
using ManagementTaskDomain.Entities;

namespace ManagementTaskDomain.Repositories;

public interface IUserRepository
{
    Task<Maybe<User>> GetByIdAsync(Guid id);
    Task<Result<IReadOnlyList<User>>> GetAllAsync();

    Task<Result<User>> AddAsync(User user);
    Task<Result<bool>> Remove(User user);
    Task<Result<bool>> Update(User model);
    Task SaveChangesAsync();
}
