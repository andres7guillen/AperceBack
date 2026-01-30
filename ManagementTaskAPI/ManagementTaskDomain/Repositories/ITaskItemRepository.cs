using CSharpFunctionalExtensions;
using ManagementTaskDomain.Entities;

namespace ManagementTaskDomain.Repositories;

public interface ITaskItemRepository
{
    Task<Result<IReadOnlyList<TaskItem>>> GetAllAsync();
    Task<Result<IReadOnlyList<TaskItem>>> GetByUserIdAsync(Guid userId);
    Task<Result<TaskItem>> AddAsync(TaskItem task);
}
