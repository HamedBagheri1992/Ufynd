using System.Threading.Tasks;
using Ufynd.Task1.WebApi.Models;

namespace Ufynd.Task1.WebApi.Services.Contracts
{
    public interface IDataExtractorService
    {
        Task<DataExtractorDto> GetByUriAsync(string requestUri);
        Task<DataExtractorDto> GetFromFileAsync();
    }
}
