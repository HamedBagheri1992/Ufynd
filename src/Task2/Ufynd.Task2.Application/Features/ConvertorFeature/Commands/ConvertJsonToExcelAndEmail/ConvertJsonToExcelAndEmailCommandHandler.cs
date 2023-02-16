using MediatR;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Ufynd.Task2.Application.Common;
using Ufynd.Task2.Application.Common.Models;
using Ufynd.Task2.Application.Contracts.Infrastructure;
using Ufynd.Task2.Application.Contracts.Persistence;
using Ufynd.Task2.Domain.Entities;

namespace Ufynd.Task2.Application.Features.ConvertorFeature.Commands.ConvertJsonToExcelAndEmail
{
    public class ConvertJsonToExcelAndEmailCommandHandler : IRequestHandler<ConvertJsonToExcelAndEmailCommand>
    {
        private readonly IAutoProcessingRepository _autoProcessingRepository;
        private readonly IDateTimeService _dateTimeService;

        public ConvertJsonToExcelAndEmailCommandHandler(IAutoProcessingRepository autoProcessingRepository, IDateTimeService dateTimeService)
        {
            _autoProcessingRepository = autoProcessingRepository;
            _dateTimeService = dateTimeService;
        }

        public async Task<Unit> Handle(ConvertJsonToExcelAndEmailCommand request, CancellationToken cancellationToken)
        {
            using var streamReader = new StreamReader(request.FormFile.OpenReadStream());
            var jsonContent = await streamReader.ReadToEndAsync();

            var hotelJsonModel = JsonConvert.DeserializeObject<HotelJsonModel>(jsonContent);
            var workbook = hotelJsonModel.ToWorkBook();

            var fileAddress = GetStoredPath(request.FileName);
            workbook.ToFile(fileAddress);

            await AddAutoProcessingAsync(request, fileAddress, cancellationToken);

            return Unit.Value;
        }

        private async Task AddAutoProcessingAsync(ConvertJsonToExcelAndEmailCommand request, string fileAddress, CancellationToken cancellationToken)
        {
            AutoProcessing autoProcessing = new AutoProcessing
            {
                Email = request.Email,
                FileAddress = fileAddress,
                IsSend = false,
                SendTime = request.SendTime.HasValue ? request.SendTime.Value : _dateTimeService.Now
            };

            await _autoProcessingRepository.CreateAsync(autoProcessing, cancellationToken);
        }

        private string GetStoredPath(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                fileName = $"Converted({DateTime.Now.ToString("yyyy-MM-dd_HH-mm")}).xlsx";
            else
                fileName = $"{fileName}.xlsx";

            var currentDirectory = Directory.GetCurrentDirectory();
            string storedPath = Path.Combine(currentDirectory, "ExcelFiles", Guid.NewGuid().ToString());

            if (Directory.Exists(storedPath) == false)
                Directory.CreateDirectory(storedPath);

            return Path.Combine(storedPath, fileName);
        }
    }
}
