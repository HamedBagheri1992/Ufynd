using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ufynd.Task3.WebApi.Controllers;
using Ufynd.Task3.WebApi.Models;
using Ufynd.Task3.WebApi.Services.Contracts;

namespace Ufynd.Task3.WebApi.UnitTests.Controllers
{
    public class HotelControllerTests
    {
        private Mock<IHotelService> _mockHotelService;
        private HotelController _hotelController;

        [SetUp]
        public void SetUp()
        {
            _mockHotelService = new Mock<IHotelService>();
            _hotelController = new HotelController(_mockHotelService.Object);
        }


        [Test]
        public async Task FilterOnFile_Returns_BadRequest_When_FormFile_Is_Null()
        {
            // Arrange
            IFormFile formFile = null;
            long hotelId = 1;
            var arrivalDate = new DateTimeOffset();

            // Act
            var result = await _hotelController.FilterOnFile(formFile, hotelId, arrivalDate);

            // Assert
            result.Result.Should().BeOfType<BadRequestResult>();
        }

        [Test]
        public async Task FilterOnFile_Returns_BadRequest_When_FormFile_Length_Is_Zero()
        {
            // Arrange
            var formFile = new Mock<IFormFile>();
            formFile.Setup(f => f.Length).Returns(0);
            long hotelId = 1;
            DateTimeOffset arrivalDate = new DateTimeOffset();

            // Act
            var result = await _hotelController.FilterOnFile(formFile.Object, hotelId, arrivalDate);

            // Assert
            result.Result.Should().BeOfType<BadRequestResult>();
        }

        [Test]
        public async Task FilterOnFile_Returns_Ok_With_FilteredList()
        {
            // Arrange
            var formFileMock = new Mock<IFormFile>();
            formFileMock.Setup(f => f.Length).Returns(1);
            IFormFile formFile = formFileMock.Object;
            long hotelId = 1;
            DateTimeOffset arrivalDate = new DateTimeOffset();
            var filteredList = new List<HotelJsonModel>() { new HotelJsonModel() };
            _mockHotelService.Setup(s => s.FilterAsync(formFile, hotelId, arrivalDate)).ReturnsAsync(filteredList);

            // Act
            var result = await _hotelController.FilterOnFile(formFile, hotelId, arrivalDate);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = (OkObjectResult)result.Result;
            okResult.Value.Should().BeEquivalentTo(filteredList);
        }
    }
}
