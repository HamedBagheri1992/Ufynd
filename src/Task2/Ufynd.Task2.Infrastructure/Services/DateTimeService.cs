using System;
using Ufynd.Task2.Application.Contracts.Infrastructure;

namespace Ufynd.Task2.Infrastructure.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.Now;
    }
}
