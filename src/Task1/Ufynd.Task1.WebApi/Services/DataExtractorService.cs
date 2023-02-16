using HtmlAgilityPack;
using System.Threading.Tasks;
using Ufynd.Task1.WebApi.Exceptions;
using Ufynd.Task1.WebApi.Models;
using Ufynd.Task1.WebApi.Services.Contracts;

namespace Ufynd.Task1.WebApi.Services
{
    public class DataExtractorService : IDataExtractorService
    {
        private readonly IOnlineHtmlAgilityService _onlineHtmlAgilityService;
        private readonly IOfflineHtmlAgilityService _offlineHtmlAgilityService;

        public DataExtractorService(IOnlineHtmlAgilityService onlineHtmlAgilityService, IOfflineHtmlAgilityService offlineHtmlAgilityService)
        {
            _onlineHtmlAgilityService = onlineHtmlAgilityService;
            _offlineHtmlAgilityService = offlineHtmlAgilityService;
        }

        public async Task<DataExtractorDto> GetByUriAsync(string requestUri)
        {
            string html = await _onlineHtmlAgilityService.ReadHtmlPageAsync(requestUri);
            if (string.IsNullOrEmpty(html))
                throw new BadRequestException("The Html is Empty.");

            var htmlDocument = _onlineHtmlAgilityService.GetHtmlDocument(html);
            if (htmlDocument == null || htmlDocument.DocumentNode == null)
                throw new BadRequestException("htmlDocument is null.");

            var dataExtractorDto = await ExtractOnlie(htmlDocument);

            return dataExtractorDto;
        }

        public async Task<DataExtractorDto> GetFromFileAsync()
        {
            string html = await _offlineHtmlAgilityService.ReadHtmlPageFromFileAsync();
            if (string.IsNullOrEmpty(html))
                throw new BadRequestException("The Html is Empty.");

            var htmlDocument = _offlineHtmlAgilityService.GetHtmlDocument(html);
            if (htmlDocument == null || htmlDocument.DocumentNode == null)
                throw new BadRequestException("htmlDocument is null.");

            DataExtractorDto dataExtractorDto = await ExtractOffline(htmlDocument);

            return dataExtractorDto;
        }

        private async Task<DataExtractorDto> ExtractOnlie(HtmlDocument htmlDocument)
        {
            var dataExtractorDto = new DataExtractorDto();
            dataExtractorDto.HotelName = await _onlineHtmlAgilityService.GetHotlNameAsync(htmlDocument);
            dataExtractorDto.Address = await _onlineHtmlAgilityService.GetAddressAsync(htmlDocument);
            dataExtractorDto.Description = await _onlineHtmlAgilityService.GetDescriptionAsync(htmlDocument);
            dataExtractorDto.Classification = await _onlineHtmlAgilityService.GetClassificationAsync(htmlDocument);
            dataExtractorDto.ReviewPoints = await _onlineHtmlAgilityService.GetreviewPointsAsync(htmlDocument);
            dataExtractorDto.NumberOfReviews = await _onlineHtmlAgilityService.GetNumberOfReviewsAsync(htmlDocument);
            dataExtractorDto.RoomCategories = await _onlineHtmlAgilityService.GetRoomCategoriesAsync(htmlDocument);
            dataExtractorDto.AlternativeHotels = await _onlineHtmlAgilityService.GetAlternativeHotelsAsync(htmlDocument);
            return dataExtractorDto;
        }

        private async Task<DataExtractorDto> ExtractOffline(HtmlDocument htmlDocument)
        {
            var dataExtractorDto = new DataExtractorDto();
            dataExtractorDto.HotelName = await _offlineHtmlAgilityService.GetHotlNameAsync(htmlDocument);
            dataExtractorDto.Address = await _offlineHtmlAgilityService.GetAddressAsync(htmlDocument);
            dataExtractorDto.Description = await _offlineHtmlAgilityService.GetDescriptionAsync(htmlDocument);
            dataExtractorDto.Classification = await _offlineHtmlAgilityService.GetClassificationAsync(htmlDocument);
            dataExtractorDto.ReviewPoints = await _offlineHtmlAgilityService.GetreviewPointsAsync(htmlDocument);
            dataExtractorDto.NumberOfReviews = await _offlineHtmlAgilityService.GetNumberOfReviewsAsync(htmlDocument);
            dataExtractorDto.RoomCategories = await _offlineHtmlAgilityService.GetRoomCategoriesAsync(htmlDocument);
            dataExtractorDto.AlternativeHotels = await _offlineHtmlAgilityService.GetAlternativeHotelsAsync(htmlDocument);
            return dataExtractorDto;
        }
    }
}
