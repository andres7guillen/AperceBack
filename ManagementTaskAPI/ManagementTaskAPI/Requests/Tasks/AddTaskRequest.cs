namespace ManagementTaskAPI.Requests.Tasks;

public sealed record AddTaskRequest(
    Guid UserId,
    string Title
);
