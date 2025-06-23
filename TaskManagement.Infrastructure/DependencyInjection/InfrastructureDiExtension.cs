using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Core.Abstractions.Repositories;
using TaskManagement.Core.Interfaces;
using TaskManagement.Infrastructure.Dao.Sql;
using TaskManagement.Infrastructure.Services;

namespace TaskManagement.Infrastructure.DependencyInjection
{
    public static class InfrastructureDiExtension
    {
        public static void AddInfrastructureDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDbTransactionService, DbTransactionService>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITaskService, TaskService>();
        }
    }
}
