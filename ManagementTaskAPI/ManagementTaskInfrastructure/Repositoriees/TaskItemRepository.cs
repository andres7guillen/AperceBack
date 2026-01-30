using CSharpFunctionalExtensions;
using ManagementTaskData.Context;
using ManagementTaskDomain.Entities;
using ManagementTaskDomain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ManagementTaskInfrastructure.Repositoriees;

public sealed class TaskItemRepository : ITaskItemRepository
{
    private readonly ApplicationDbContext _context;

    public TaskItemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<TaskItem>> AddAsync(TaskItem task)
    {
        await _context.Tasks.AddAsync(task);
        return await _context.SaveChangesAsync() > 0
            ? Result.Success(task)
            : Result.Failure<TaskItem>("Failed to add task");
    }

    public async Task<Result<IReadOnlyList<TaskItem>>> GetAllAsync()
    {
        var tasks = await _context.Tasks
            .Include(c => c.User)
            .AsNoTracking()
            .ToListAsync();
        return tasks.Any()
            ? Result.Success((IReadOnlyList<TaskItem>)tasks) : Result.Failure<IReadOnlyList<TaskItem>>("No tasks found");
    }

    public async Task<Result<IReadOnlyList<TaskItem>>> GetByUserIdAsync(Guid userId)
    {
        var tasks = await _context.Tasks
            .AsNoTracking()
            .Where(t => t.UserId == userId)
            .ToListAsync();
        return tasks.Any()
            ? Result.Success((IReadOnlyList<TaskItem>)tasks) : Result.Failure<IReadOnlyList<TaskItem>>("No tasks found for the specified user");
    }


}
