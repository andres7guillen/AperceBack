using CSharpFunctionalExtensions;

namespace ManagementTaskDomain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }

    public List<TaskItem> Tasks { get; set; } = new();


    private User() { } 

    private User(Guid id,string name, string email)
    {
        Name = name;
        Email = email;
        Id = id;
    }

    public static Result<User> Create(Guid id,string name, string email)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<User>("Name is required.");

        if (string.IsNullOrWhiteSpace(email))
            return Result.Failure<User>("Email is required.");

        if (!email.Contains("@"))
            return Result.Failure<User>("Invalid email format.");

        return Result.Success(new User(id,name, email));
    }

    public Result<TaskItem> AddTask(Guid id, string title)
    {
        var taskResult = TaskItem.Create(id, title, this.Id);
        if (taskResult.IsFailure)
            return Result.Failure<TaskItem>(taskResult.Error);

        var task = taskResult.Value;
        task.SetUser(this); // Necesitamos un método interno en TaskItem
        Tasks.Add(task);
        return Result.Success(taskResult.Value);
    }

    public Result StartTask(Guid taskId)
    {
        var task = Tasks.FirstOrDefault(t => t.Id == taskId);
        if (task is null)
            return Result.Failure("Task not found.");

        return task.Start();
    }

    public Result CompleteTask(Guid taskId)
    {
        var task = Tasks.FirstOrDefault(t => t.Id == taskId);
        if (task is null)
            return Result.Failure("Task not found.");

        return task.Complete();
    }

}

