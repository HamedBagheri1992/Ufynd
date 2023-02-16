using MediatR;
using Newtonsoft.Json;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Ufynd.Task2.Application.Common;
using Ufynd.Task2.Application.Common.Models;

namespace Ufynd.Task2.Application.Features.ConvertorFeature.Commands.ConvertJsonToExcel
{
    public class ConvertJsonToExcelCommandHandler : IRequestHandler<ConvertJsonToExcelCommand, ConvertJsonToExcelDto>
    {
        public async Task<ConvertJsonToExcelDto> Handle(ConvertJsonToExcelCommand request, CancellationToken cancellationToken)
        {
            using var streamReader = new StreamReader(request.FormFile.OpenReadStream());
            var jsonContent = await streamReader.ReadToEndAsync();

            var hotelJsonModel = JsonConvert.DeserializeObject<HotelJsonModel>(jsonContent);
            var workbook = hotelJsonModel.ToWorkBook();

            var memoryStream = workbook.ToMemoryStream();
            var dto = new ConvertJsonToExcelDto
            {
                Memory = memoryStream,
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                FileName = string.IsNullOrEmpty(request.FileName) ? "converted.xlsx" : $"{request.FileName}.xlsx"
            };

            return dto;
        }
    }
}
