using System.Threading.Tasks;

namespace Ufynd.Task1.WebApi.Services.Contracts
{
    public interface IOfflineHtmlAgilityService : IHtmlAgilityService
    {
        Task<string> ReadHtmlPageFromFileAsync();
    }
}
