using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using Ufynd.Task1.WebApi.Controllers;
using Ufynd.Task1.WebApi.Models;
using Ufynd.Task1.WebApi.Services.Contracts;

namespace Ufynd.Task1.WebApi.UnitTests.Controllers
{
    [TestFixture]
    public class DataExtractorControllerTests
    {
        private Mock<IDataExtractorService> _dataExtractorServiceMock;
        private DataExtractorController _controller;

        [SetUp]
        public void Setup()
        {
            _dataExtractorServiceMock = new Mock<IDataExtractorService>();
            _controller = new DataExtractorController(_dataExtractorServiceMock.Object);
        }

        [Test]
        public async Task GetByUri_WhenCalled_ReturnsDataExtractorDto()
        {
            // Arrange
            var requestUri = "https://www.example.com";
            var expectedDto = GetDataExtractorDto();
            _dataExtractorServiceMock.Setup(svc => svc.GetByUriAsync(requestUri)).ReturnsAsync(expectedDto);

            // Act
            var result = await _controller.GetByUri(requestUri);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(expectedDto);
        }


        #region Privates

        private DataExtractorDto GetDataExtractorDto()
        {
            return new DataExtractorDto
            {
                HotelName = "Hotel Example",
                Classification = 5,
                ReviewPoints = 8.5f,
                Description = "This is a Description",
                Address = "Tehran, Iran",
                NumberOfReviews = 100,
                RoomCategories = new[] { "RoomType_1", "RoomType_1" },
                AlternativeHotels = new[] { "Hotel Good", "Hotel Best" }
            };
        }

        #endregion
    }
}
