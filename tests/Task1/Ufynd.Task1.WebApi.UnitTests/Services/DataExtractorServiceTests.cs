using FluentAssertions;
using HtmlAgilityPack;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using Ufynd.Task1.WebApi.Exceptions;
using Ufynd.Task1.WebApi.Services;
using Ufynd.Task1.WebApi.Services.Contracts;

namespace Ufynd.Task1.WebApi.UnitTests.Services
{
    [TestFixture]
    public class DataExtractorServiceTests
    {
        private Mock<IOnlineHtmlAgilityService> _onlineHtmlAgilityServiceMock;
        private Mock<IOfflineHtmlAgilityService> _offlineHtmlAgilityServiceMock;
        private DataExtractorService _dataExtractorService;


        [SetUp]
        public void Setup()
        {
            _onlineHtmlAgilityServiceMock = new Mock<IOnlineHtmlAgilityService>();
            _offlineHtmlAgilityServiceMock = new Mock<IOfflineHtmlAgilityService>();
            _dataExtractorService = new DataExtractorService(_onlineHtmlAgilityServiceMock.Object, _offlineHtmlAgilityServiceMock.Object);
        }

        [Test]
        public async Task GetByUriAsync_ShouldReturnDataExtractorDto_WhenHtmlAndHtmlDocumentAreValid()
        {
            // Arrange
            string requestUri = "https://example.com";
            string html = GetHtml();
            var htmlDocument = GetHtmlDocument(html);

            _onlineHtmlAgilityServiceMock.Setup(x => x.ReadHtmlPageAsync(requestUri)).ReturnsAsync(html);
            _onlineHtmlAgilityServiceMock.Setup(x => x.GetHtmlDocument(html)).Returns(htmlDocument);
            _onlineHtmlAgilityServiceMock.Setup(x => x.GetHotlNameAsync(htmlDocument)).ReturnsAsync(() => "Hotel Name");
            _onlineHtmlAgilityServiceMock.Setup(x => x.GetAddressAsync(htmlDocument)).ReturnsAsync(() => "Address");
            _onlineHtmlAgilityServiceMock.Setup(x => x.GetDescriptionAsync(htmlDocument)).ReturnsAsync(() => "Description");
            _onlineHtmlAgilityServiceMock.Setup(x => x.GetClassificationAsync(htmlDocument)).ReturnsAsync(() => 3);
            _onlineHtmlAgilityServiceMock.Setup(x => x.GetreviewPointsAsync(htmlDocument)).ReturnsAsync(() => 4.5f);
            _onlineHtmlAgilityServiceMock.Setup(x => x.GetNumberOfReviewsAsync(htmlDocument)).ReturnsAsync(() => 1234);
            _onlineHtmlAgilityServiceMock.Setup(x => x.GetRoomCategoriesAsync(htmlDocument)).ReturnsAsync(() => new[] { "RoomType 1", "RoomType 2" });
            _onlineHtmlAgilityServiceMock.Setup(x => x.GetAlternativeHotelsAsync(htmlDocument)).ReturnsAsync(() => new[] { "Hotel 1", "Hotel 2" });

            // Act
            var result = await _dataExtractorService.GetByUriAsync(requestUri);

            // Assert
            result.Should().NotBeNull();
            result.HotelName.Should().Be("Hotel Name");
            result.Address.Should().Be("Address");
            result.Description.Should().Be("Description");
            result.Classification.Should().Be(3);
            result.ReviewPoints.Should().Be(4.5f);
            result.NumberOfReviews.Should().Be(1234);
            result.RoomCategories.Should().BeEquivalentTo(new[] { "RoomType 1", "RoomType 2" });
            result.AlternativeHotels.Should().BeEquivalentTo(new[] { "Hotel 1", "Hotel 2" });
        }

        [Test]
        public void GetByUriAsync_ShouldThrowBadRequestException_WhenHtmlIsNull()
        {
            // Arrange
            string requestUri = "https://example.com";
            _onlineHtmlAgilityServiceMock.Setup(x => x.ReadHtmlPageAsync(requestUri)).ReturnsAsync(() => null);

            // Act and Assert
            Assert.ThrowsAsync<BadRequestException>(() => _dataExtractorService.GetByUriAsync(requestUri));
        }

        #region Privates

        private string GetHtml()
        {
            return "<html><body><div id=\"hotelName\">Hotel Name</div><div id=\"address\">Address</div><div id=\"description\">Description</div><div id=\"classification\"><span></span><span></span><span></span></div><div id=\"reviewPoints\">4.5</div><div id=\"numberOfReviews\">1234 reviews</div><div id=\"roomCategories\"><ul><li>Category 1</li><li>Category 2</li></ul></div><div id=\"alternativeHotels\"><ul><li>Hotel 1</li><li>Hotel 2</li></ul></div></body></html>";
        }

        private HtmlDocument GetHtmlDocument(string html)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            return htmlDocument;
        }

        #endregion
    }
}
