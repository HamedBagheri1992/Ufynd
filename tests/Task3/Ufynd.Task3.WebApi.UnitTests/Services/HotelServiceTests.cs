using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Ufynd.Task3.WebApi.Models;
using Ufynd.Task3.WebApi.Services;

namespace Ufynd.Task3.WebApi.UnitTests.Services
{
    [TestFixture]
    public class HotelServiceTests
    {
        private Mock<IFormFile> _formFileMock;
        private HotelService _hotelService;


        [SetUp]
        public void SetUp()
        {
            _formFileMock = new Mock<IFormFile>();
            _hotelService = new HotelService();
        }

        [Test]
        public async Task FilterAsync_WithValidInput_ReturnsFilteredHotels()
        {
            // Arrange
            var hotelId = 1;
            var arrivalDate = DateTimeOffset.Now;
            var hotels = GetHotels(hotelId, arrivalDate);

            var jsonContent = JsonConvert.SerializeObject(hotels);
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonContent));
            _formFileMock.Setup(f => f.OpenReadStream()).Returns(stream);

            // Act
            var result = await _hotelService.FilterAsync(_formFileMock.Object, hotelId, arrivalDate);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
            result[0].hotel.hotelID.Should().Be(hotelId);
            result[0].hotelRates.Should().NotBeNull();
            result[0].hotelRates.Should().HaveCount(1);
            result[0].hotelRates[0].targetDay.Should().Be(arrivalDate);
        }


        private List<HotelJsonModel> GetHotels(int hotelId, DateTimeOffset arrivalDate)
        {
            var hotels = new List<HotelJsonModel>();
            hotels.Add(new HotelJsonModel
            {
                hotel = new Hotel
                {
                    hotelID = hotelId
                },
                hotelRates = new List<HotelRate>
                {
                    new HotelRate
                    {
                        targetDay = arrivalDate
                    }
                }
            });
            hotels.Add(new HotelJsonModel
            {
                hotel = new Hotel
                {
                    hotelID = 2
                },
                hotelRates = new List<HotelRate>
                {
                    new HotelRate
                    {
                        targetDay = DateTimeOffset.Now.AddDays(1)
                    }
                }
            });
            return hotels;
        }
    }
}
