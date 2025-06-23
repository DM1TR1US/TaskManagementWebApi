
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Core.Abstractions;
using TaskManagement.Core.UseCases;

namespace TaskManagement.Core.Common.DependencyInjection;

public static class CoreDependencyInjectionExtension
{
    public static void AddCoreDependencyInjection(this IServiceCollection services)
    {
        services.AddTransient<ITaskCreationUc, TaskCreationUc>();
        services.AddTransient<IUserCreationUc, UserCreationUc>();
        services.AddTransient<IReassignmentUc, ReassignmentUc>();
    }
}
