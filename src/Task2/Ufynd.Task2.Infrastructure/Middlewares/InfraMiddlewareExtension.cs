using Microsoft.Extensions.DependencyInjection;
using System;
using Ufynd.Task2.Infrastructure.Persistence;

namespace Ufynd.Task2.Infrastructure.Middlewares
{
    public static class InfraMiddlewareExtension
    {
        public static void DbContextInitializer(this IServiceProvider service)
        {
            using (var scope = service.CreateScope())
            {
                var initialiser = scope.ServiceProvider.GetRequiredService<Task2DbContextInitializer>();
                initialiser.Initialize();
            }
        }
    }
}
