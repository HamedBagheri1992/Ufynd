using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ufynd.Task2.Application.Contracts.Persistence;
using Ufynd.Task2.Domain.Entities;
using Ufynd.Task2.Infrastructure.Persistence;

namespace Ufynd.Task2.Infrastructure.Repositories
{
    public class AutoProcessingRepository : IAutoProcessingRepository
    {
        private readonly Task2DbContext _context;

        public AutoProcessingRepository(Task2DbContext context)
        {
            _context = context;
        }

        public async Task<AutoProcessing> CreateAsync(AutoProcessing autoProcessing, CancellationToken cancellationToken)
        {
            await _context.AutoProcessings.AddAsync(autoProcessing, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return autoProcessing;
        }

        public List<AutoProcessing> GetReadyToSend()
        {
            return _context.AutoProcessings.Where(a => a.SendTime <= DateTime.Now && a.IsSend == false).ToList();
        }

        public void Update(List<AutoProcessing> emailsToSend)
        {
            _context.AutoProcessings.UpdateRange(emailsToSend);
            _context.SaveChanges();
        }
    }
}
