using HtmlAgilityPack;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ufynd.Task1.WebApi.Services.Contracts
{
    public interface IHtmlAgilityService
    {
        HtmlDocument GetHtmlDocument(string html);
        Task<string> GetHotlNameAsync(HtmlDocument htmlDocument);
        Task<string> GetAddressAsync(HtmlDocument htmlDocument);
        Task<string> GetDescriptionAsync(HtmlDocument htmlDocument);
        Task<int> GetClassificationAsync(HtmlDocument htmlDocument);
        Task<float> GetreviewPointsAsync(HtmlDocument htmlDocument);
        Task<int> GetNumberOfReviewsAsync(HtmlDocument htmlDocument);
        Task<IEnumerable<string>> GetRoomCategoriesAsync(HtmlDocument htmlDocument);
        Task<IEnumerable<string>> GetAlternativeHotelsAsync(HtmlDocument htmlDocument);
    }
}
