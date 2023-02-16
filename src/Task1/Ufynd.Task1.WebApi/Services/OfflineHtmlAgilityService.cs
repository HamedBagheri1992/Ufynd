using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ufynd.Task1.WebApi.Common;
using Ufynd.Task1.WebApi.Common.Settings;
using Ufynd.Task1.WebApi.Services.Contracts;

namespace Ufynd.Task1.WebApi.Services
{
    public class OfflineHtmlAgilityService : IOfflineHtmlAgilityService
    {
        private IOptionsMonitor<OfflineXPathStringsConfigurationModel> _offlineXPathStringsOptionMonitor;

        public OfflineHtmlAgilityService(IOptionsMonitor<OfflineXPathStringsConfigurationModel> offlineXPathStringsOptionMonitor)
        {
            _offlineXPathStringsOptionMonitor = offlineXPathStringsOptionMonitor;
        }

        public async Task<string> ReadHtmlPageFromFileAsync()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), _offlineXPathStringsOptionMonitor.CurrentValue.FileAddress);
            if (string.IsNullOrEmpty(path) && File.Exists(path) == false)
                return string.Empty;

            return await File.ReadAllTextAsync(path);
        }

        public HtmlDocument GetHtmlDocument(string html)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            return htmlDoc;
        }

        public async Task<string> GetHotlNameAsync(HtmlDocument htmlDocument)
        {
            return await GetNodeInnerTextByXPath(htmlDocument, _offlineXPathStringsOptionMonitor.CurrentValue.HotelName);
        }

        public async Task<string> GetAddressAsync(HtmlDocument htmlDocument)
        {
            return await GetNodeInnerTextByXPath(htmlDocument, _offlineXPathStringsOptionMonitor.CurrentValue.Address);
        }

        public async Task<string> GetDescriptionAsync(HtmlDocument htmlDocument)
        {
            var nodes = await Task.FromResult(htmlDocument.DocumentNode.SelectNodes(_offlineXPathStringsOptionMonitor.CurrentValue.Description));
            if (nodes == null)
                return string.Empty;

            return string.Join("", nodes.Select(n => n.InnerText.Trim()).Where(n => string.IsNullOrEmpty(n) == false).ToList());
        }

        public async Task<int> GetClassificationAsync(HtmlDocument htmlDocument)
        {
            var node = await Task.FromResult(htmlDocument.DocumentNode.SelectSingleNode(_offlineXPathStringsOptionMonitor.CurrentValue.Classification));
            if (node == null)
                return 0;

            string starClass = string.Join("", node.GetClasses()).ToJustNumbers();
            if (int.TryParse(starClass.ToJustNumbers(), out int star))
                return star;

            return 0;
        }

        public async Task<float> GetreviewPointsAsync(HtmlDocument htmlDocument)
        {
            var reviewPoints = await GetNodeInnerTextByXPath(htmlDocument, _offlineXPathStringsOptionMonitor.CurrentValue.ReviewPoints);
            if (float.TryParse(reviewPoints, out float rp))
                return rp;

            return 0;
        }

        public async Task<int> GetNumberOfReviewsAsync(HtmlDocument htmlDocument)
        {
            var numberOfReviews = await GetNodeInnerTextByXPath(htmlDocument, _offlineXPathStringsOptionMonitor.CurrentValue.NumberOfReviews);
            if (int.TryParse(numberOfReviews.ToJustNumbers(), out int nr))
                return nr;

            return 0;
        }

        public async Task<IEnumerable<string>> GetRoomCategoriesAsync(HtmlDocument htmlDocument)
        {
            var nodes = await Task.FromResult(htmlDocument.DocumentNode.SelectNodes(_offlineXPathStringsOptionMonitor.CurrentValue.RoomCategories));
            if (nodes == null)
                return Enumerable.Empty<string>();

            return nodes.Select(n => n.InnerText.Trim()).Where(n => string.IsNullOrEmpty(n) == false).ToList();
        }

        public async Task<IEnumerable<string>> GetAlternativeHotelsAsync(HtmlDocument htmlDocument)
        {
            var nodes = await Task.FromResult(htmlDocument.DocumentNode.SelectNodes(_offlineXPathStringsOptionMonitor.CurrentValue.AlternativeHotels));
            if (nodes == null)
                return Enumerable.Empty<string>();

            return nodes.Select(n => n.InnerText.Trim()).Where(n => string.IsNullOrEmpty(n) == false).ToList();
        }

        private static async Task<string> GetNodeInnerTextByXPath(HtmlDocument htmlDocument, string xPath)
        {
            var node = await Task.FromResult(htmlDocument.DocumentNode.SelectSingleNode(xPath));
            if (node == null)
                return string.Empty;

            return node.InnerText.Trim();
        }
    }
}
