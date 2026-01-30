namespace ManagementTaskAPI.Requests.Tasks;

public sealed record CreateTaskRequest(Guid UserId,string Title);
