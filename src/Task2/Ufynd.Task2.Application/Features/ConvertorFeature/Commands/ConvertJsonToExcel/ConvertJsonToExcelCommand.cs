using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ufynd.Task2.Application.Features.ConvertorFeature.Commands.ConvertJsonToExcel
{
    public class ConvertJsonToExcelCommand : IRequest<ConvertJsonToExcelDto>
    {
        public IFormFile FormFile { get; set; }
        public string FileName { get; set; }

        public ConvertJsonToExcelCommand(IFormFile formFile, string fileName)
        {
            FormFile = formFile;
            FileName = fileName;
        }
    }
}
