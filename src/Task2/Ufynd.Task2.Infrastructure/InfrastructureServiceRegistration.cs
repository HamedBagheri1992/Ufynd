using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ufynd.Task2.Application.Common.Settings;
using Ufynd.Task2.Application.Contracts.Infrastructure;
using Ufynd.Task2.Application.Contracts.Persistence;
using Ufynd.Task2.Infrastructure.HostedServices;
using Ufynd.Task2.Infrastructure.Persistence;
using Ufynd.Task2.Infrastructure.Repositories;
using Ufynd.Task2.Infrastructure.Services;

namespace Ufynd.Task2.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ReportSettingsConfigurationModel>(configuration.GetSection(ReportSettingsConfigurationModel.NAME));

            services.AddDbContext<Task2DbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                builder => builder.MigrationsAssembly(typeof(Task2DbContext).Assembly.FullName))
                , ServiceLifetime.Scoped);

            services.AddScoped<Task2DbContextInitializer>();
            services.AddScoped<IAutoProcessingRepository, AutoProcessingRepository>();

            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddTransient<IEmailService, EmailService>();

            services.AddHostedService<ReportHostedService>();

            return services;

        }
    }
}
