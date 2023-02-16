using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;
using Ufynd.Task2.Application.Common.Settings;
using Ufynd.Task2.Application.Contracts.Infrastructure;
using Ufynd.Task2.Application.Contracts.Persistence;
using Ufynd.Task2.Domain.Entities;

namespace Ufynd.Task2.Infrastructure.HostedServices
{
    public class ReportHostedService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly IServiceProvider _services;
        private readonly IOptionsMonitor<ReportSettingsConfigurationModel> _reportSettings;
        private readonly ILogger<ReportHostedService> _logger;

        public ReportHostedService(IServiceProvider services, IOptionsMonitor<ReportSettingsConfigurationModel> reportSettings, ILogger<ReportHostedService> logger)
        {
            _services = services;
            _reportSettings = reportSettings;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("ReportHostedService has started.");

            int inteval = _reportSettings.CurrentValue.Interval;
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(inteval));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _logger.LogInformation("Started to Checking Db.");
            using var scope = _services.CreateScope();
            var _autoProcessingRepository = scope.ServiceProvider.GetRequiredService<IAutoProcessingRepository>();
            var _emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

            var emailsToSend = _autoProcessingRepository.GetReadyToSend();

            foreach (AutoProcessing item in emailsToSend)
            {
                var result = _emailService.SendEmailWithAttachment(item.Email, item.FileAddress);
                item.IsSend = result;
            }
            if (emailsToSend.Count > 0)
                _autoProcessingRepository.Update(emailsToSend);

            _logger.LogInformation("Checked the data for Sending.");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("ReportHostedService has stopped.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
