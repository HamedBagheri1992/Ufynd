using System.Threading.Tasks;

namespace Ufynd.Task1.WebApi.Services.Contracts
{
    public interface IOnlineHtmlAgilityService : IHtmlAgilityService
    {
        Task<string> ReadHtmlPageAsync(string requestUri);
    }
}
