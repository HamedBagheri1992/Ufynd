using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace Ufynd.Task2.Infrastructure.Persistence
{
    public class Task2DbContextInitializer
    {
        private readonly ILogger<Task2DbContextInitializer> _logger;
        private readonly Task2DbContext _context;

        public Task2DbContextInitializer(ILogger<Task2DbContextInitializer> logger, Task2DbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void Initialize()
        {
            try
            {
                _context.Database.Migrate();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }
    }
}
