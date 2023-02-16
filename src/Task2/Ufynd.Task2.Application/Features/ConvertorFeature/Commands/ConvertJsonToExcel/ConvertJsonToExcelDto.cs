using System.IO;

namespace Ufynd.Task2.Application.Features.ConvertorFeature.Commands.ConvertJsonToExcel
{
    public class ConvertJsonToExcelDto
    {
        public string ContentType { get; set; }
        public string FileName { get; set; }
        public MemoryStream Memory { get; set; }
    }
}
