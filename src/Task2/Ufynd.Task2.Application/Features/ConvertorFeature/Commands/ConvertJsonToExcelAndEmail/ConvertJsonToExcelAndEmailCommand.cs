using MediatR;
using Microsoft.AspNetCore.Http;
using System;

namespace Ufynd.Task2.Application.Features.ConvertorFeature.Commands.ConvertJsonToExcelAndEmail
{
    public class ConvertJsonToExcelAndEmailCommand : IRequest
    {
        public IFormFile FormFile { get; set; }
        public string FileName { get; set; }
        public string Email { get; set; }
        public DateTime? SendTime { get; set; }

        public ConvertJsonToExcelAndEmailCommand(IFormFile formFile, string fileName, string email, DateTime? sendTime)
        {
            FormFile = formFile;
            FileName = fileName;
            Email = email;
            SendTime = sendTime;
        }
    }
}
