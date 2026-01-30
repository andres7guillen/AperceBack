using CSharpFunctionalExtensions;
using ManagementTaskData.Context;
using ManagementTaskDomain.Entities;
using ManagementTaskDomain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ManagementTaskInfrastructure.Repositoriees;

public sealed class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Maybe<User>> GetByIdAsync(Guid id)
    {
        var user = await _context.Users
            .Include(u => u.Tasks)
            .FirstOrDefaultAsync(u => u.Id == id);

        return user is null ? Maybe<User>.None : Maybe<User>.From(user);
    }

    public async Task<Result<IReadOnlyList<User>>> GetAllAsync()
    {
        return await _context.Users
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Result<User>> AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return Result.Success(user);
    }

    public async Task<Result<bool>> Remove(User user)
    {
        _context.Users.Remove(user);
        return await _context.SaveChangesAsync() > 0 
            ? Result.Success(true) : Result.Failure<bool>("Failed to remove user.");
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<Result<bool>> Update(User model)
    {
        _context.Users.Update(model);
        return await _context.SaveChangesAsync() > 0 
            ? Result.Success(true) : Result.Failure<bool>("Failed to update user.");
    }
}
