using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Ufynd.Task1.WebApi.Models;
using Ufynd.Task1.WebApi.Services.Contracts;

namespace Ufynd.Task1.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataExtractorController : ControllerBase
    {
        private readonly IDataExtractorService _dataExtractorService;

        public DataExtractorController(IDataExtractorService dataExtractorService)
        {
            _dataExtractorService = dataExtractorService;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<DataExtractorDto>> GetByUri([FromQuery] string requestUri)
        {
            var result = await _dataExtractorService.GetByUriAsync(requestUri);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<DataExtractorDto>> GetFromFile()
        {
            var result = await _dataExtractorService.GetFromFileAsync();
            return Ok(result);
        }
    }
}
