using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ufynd.Task2.Domain.Entities;

namespace Ufynd.Task2.Application.Contracts.Persistence
{
    public interface IAutoProcessingRepository
    {
        Task<AutoProcessing> CreateAsync(AutoProcessing autoProcessing, CancellationToken cancellationToken);
        List<AutoProcessing> GetReadyToSend();
        void Update(List<AutoProcessing> emailsToSend);
    }
}
