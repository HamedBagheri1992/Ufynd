using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Ufynd.Task2.Application.Features.ConvertorFeature.Commands.ConvertJsonToExcel;
using Ufynd.Task2.Application.Features.ConvertorFeature.Commands.ConvertJsonToExcelAndEmail;

namespace Ufynd.Task2.WebApi.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ConvertorController : ApiControllerBase
    {
        [HttpPost("[action]")]
        public async Task<IActionResult> ConvertJsonToExcel(IFormFile formFile, [FromQuery] string fileName)
        {
            var command = new ConvertJsonToExcelCommand(formFile, fileName);
            var convertedDto = await Mediator.Send(command);

            return File(convertedDto.Memory, convertedDto.ContentType, convertedDto.FileName, true);
        }

        [HttpPost("[action]/{email}")]
        public async Task<IActionResult> ConvertJsonToExcelAndEmail(IFormFile formFile, [FromRoute] string email, [FromQuery] string fileName, [FromQuery] DateTime? sendTime)
        {
            var command = new ConvertJsonToExcelAndEmailCommand(formFile, fileName, email, sendTime);
            await Mediator.Send(command);
            return Ok();
        }
    }
}
