using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ufynd.Task2.Application.Features.ConvertorFeature.Commands.ConvertJsonToExcel;

namespace Ufynd.Task2.Application.UnitTests.Features.ConvertorFeature.Commands
{
    [TestFixture]
    public class ConvertJsonToExcelTests
    {
        private ConvertJsonToExcelCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _handler = new ConvertJsonToExcelCommandHandler();
        }

        [Test]
        public async Task Handle_WithValidRequest_ReturnsDtoWithCorrectProperties()
        {
            // Arrange
            var formFileMock = new Mock<IFormFile>();
            var jsonContent = GetSampleJson();
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonContent));
            formFileMock.Setup(x => x.OpenReadStream()).Returns(stream);
            var request = new ConvertJsonToExcelCommand(formFileMock.Object, "Test");

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Memory.Should().NotBeNull();
            result.ContentType.Should().Be("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            result.FileName.Should().Be("Test.xlsx");
        }


        private string GetSampleJson()
        {
            return "{\r\n  \"hotel\": {\r\n  \t\"hotelID\": 7294,\r\n    \"classification\": 5, \r\n    \"name\": \"Kempinski Bristol Berlin\", \r\n    \"reviewscore\": 8.3\r\n  }, \r\n  \"hotelRates\": [\r\n    {\r\n      \"adults\": 2, \r\n      \"los\": 1, \r\n      \"price\": {\r\n        \"currency\": \"EUR\", \r\n        \"numericFloat\": 116.1, \r\n        \"numericInteger\": 11610\r\n      }, \r\n      \"rateDescription\": \"Unsere Classic Zimmer bieten Ihnen allen Komfort, den Sie von einem 5-Sterne-Hotel erwarten. Helle und freundliche Farben sorgen f\\u00fcr ein angenehmes Ambiente, damit Sie Ihren Aufenthalt im Herzen Berlins voll und ganz genie\\u00dfen k\\u00f6nnen. 20m\\u00b2. Doppelbett oder zwei separate Betten. Max. Kapazit\\u00e4t: 2 Erwachsene oder 1 Erwachsener und 1 Kind.      \", \r\n      \"rateID\": \"-739857498\", \r\n      \"rateName\": \"Classic Zimmer - Fr\\u00fchbucher Rate\", \r\n      \"rateTags\": [\r\n        {\r\n          \"name\": \"breakfast\", \r\n          \"shape\": false\r\n        }\r\n      ], \r\n      \"targetDay\": \"2016-03-15T00:00:00.000+01:00\"\r\n    }\r\n  ]\r\n}\r\n";
        }
    }
}
