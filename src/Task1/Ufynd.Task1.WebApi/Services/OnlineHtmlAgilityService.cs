﻿using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Ufynd.Task1.WebApi.Common;
using Ufynd.Task1.WebApi.Common.Settings;
using Ufynd.Task1.WebApi.Services.Contracts;

namespace Ufynd.Task1.WebApi.Services
{
    public class OnlineHtmlAgilityService : IOnlineHtmlAgilityService
    {
        private IOptionsMonitor<OnlineXPathStringsConfigurationModel> _onlineXPathStringsOptionMonitor;

        public OnlineHtmlAgilityService(IOptionsMonitor<OnlineXPathStringsConfigurationModel> xPathStringsOptionMonitor)
        {
            _onlineXPathStringsOptionMonitor = xPathStringsOptionMonitor;
        }

        public async Task<string> ReadHtmlPageAsync(string requestUri)
        {
            if (string.IsNullOrEmpty(requestUri))
                return string.Empty;

            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(requestUri);

            return html;
        }

        public HtmlDocument GetHtmlDocument(string html)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            return htmlDoc;
        }

        public async Task<string> GetHotlNameAsync(HtmlDocument htmlDocument)
        {
            return await GetNodeInnerTextByXPath(htmlDocument, _onlineXPathStringsOptionMonitor.CurrentValue.HotelName);
        }

        public async Task<string> GetAddressAsync(HtmlDocument htmlDocument)
        {
            return await GetNodeInnerTextByXPath(htmlDocument, _onlineXPathStringsOptionMonitor.CurrentValue.Address);
        }

        public async Task<string> GetDescriptionAsync(HtmlDocument htmlDocument)
        {
            return await GetNodeInnerTextByXPath(htmlDocument, _onlineXPathStringsOptionMonitor.CurrentValue.Description);
        }

        public async Task<int> GetClassificationAsync(HtmlDocument htmlDocument)
        {
            var node = await Task.FromResult(htmlDocument.DocumentNode.SelectSingleNode(_onlineXPathStringsOptionMonitor.CurrentValue.Classification));
            if (node == null)
                return 0;

            return node.ChildNodes.Count;
        }

        public async Task<float> GetreviewPointsAsync(HtmlDocument htmlDocument)
        {
            var reviewPoints = await GetNodeInnerTextByXPath(htmlDocument, _onlineXPathStringsOptionMonitor.CurrentValue.ReviewPoints);
            if (float.TryParse(reviewPoints, out float rp))
                return rp;

            return 0;
        }

        public async Task<int> GetNumberOfReviewsAsync(HtmlDocument htmlDocument)
        {
            var numberOfReviews = await GetNodeInnerTextByXPath(htmlDocument, _onlineXPathStringsOptionMonitor.CurrentValue.NumberOfReviews);
            if (int.TryParse(numberOfReviews.ToJustNumbers(), out int nr))
                return nr;

            return 0;
        }

        public async Task<IEnumerable<string>> GetRoomCategoriesAsync(HtmlDocument htmlDocument)
        {
            var nodes = await Task.FromResult(htmlDocument.DocumentNode.SelectNodes(_onlineXPathStringsOptionMonitor.CurrentValue.RoomCategories));
            if (nodes == null)
                return Enumerable.Empty<string>();

            return nodes.Select(n => n.InnerText.Trim()).Where(n => string.IsNullOrEmpty(n) == false).ToList();
        }

        public async Task<IEnumerable<string>> GetAlternativeHotelsAsync(HtmlDocument htmlDocument)
        {
            var nodes = await Task.FromResult(htmlDocument.DocumentNode.SelectNodes(_onlineXPathStringsOptionMonitor.CurrentValue.AlternativeHotels));
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
