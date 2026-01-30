using CSharpFunctionalExtensions;
namespace ManagementTaskDomain.Entities;

public class TaskItem
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public Enums.TaskStatus Status { get; private set; }

    public Guid UserId { get; private set; }
    public User User { get; private set; }

    private TaskItem() { }

    private TaskItem(Guid id, string title, Guid userId)
    {
        Id = id;
        Title = title;
        Status = Enums.TaskStatus.Pending;
        UserId = userId;
        User = null;
    }

    public static Result<TaskItem> Create(Guid id, string title, Guid userId)
    {
        if (string.IsNullOrWhiteSpace(title))
            return Result.Failure<TaskItem>("TaskItem title is required.");

        if (userId == Guid.Empty)
            return Result.Failure<TaskItem>("TaskItem must have an assigned user.");

        return Result.Success(new TaskItem(id, title, userId));
    }

    public Result Complete()
    {
        if (Status != Enums.TaskStatus.InProgress)
            return Result.Failure("Only InProgress tasks can be completed.");

        Status = Enums.TaskStatus.Done;
        return Result.Success();
    }

    public Result Start()
    {
        if (Status != Enums.TaskStatus.Pending)
            return Result.Failure("Only pending tasks can be started.");

        Status = Enums.TaskStatus.InProgress;
        return Result.Success();
    }

    internal void SetUser(User user)
    {
        User = user;
        UserId = user.Id;
    }


}

