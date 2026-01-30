using ManagementTaskApplication.Commands.Tasks.AddTask;
using ManagementTaskApplication.Commands.Tasks.CompleteTask;
using ManagementTaskApplication.Commands.Tasks.StartTask;
using ManagementTaskApplication.Commands.Users.CreateUser;
using ManagementTaskApplication.Commands.Users.DeleteUser;
using ManagementTaskApplication.Queries.Tasks.GetTasks;
using ManagementTaskApplication.Queries.Tasks.GetTasksByUser;
using ManagementTaskApplication.Queries.Users.GetUsers;
using ManagementTaskDomain.Repositories;
using ManagementTaskInfrastructure.Repositoriees;
using System.Reflection;

namespace ManagementTaskAPI.Utilities;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterBusinessServices(this IServiceCollection services)
    {
        //Repositories
        services.AddScoped<ITaskItemRepository, TaskItemRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        //MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
            Assembly.GetExecutingAssembly(),
            typeof(AddTaskToUserCommand).Assembly,
            typeof(CompleteTaskCommand).Assembly,
            typeof(StartTaskCommand).Assembly,
            typeof(CreateUserCommand).Assembly,
            typeof(DeleteUserCommand).Assembly,
            typeof(GetTasksQuery).Assembly,
            typeof(GetTasksByUserQuery).Assembly,
            typeof(GetUsersQuery).Assembly
            ));
        return services;
    }    
}
