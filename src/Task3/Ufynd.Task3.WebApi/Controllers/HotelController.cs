using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ufynd.Task3.WebApi.Models;
using Ufynd.Task3.WebApi.Services.Contracts;

namespace Ufynd.Task3.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpPost("[action]/{HotelID}/{ArrivalDate}")]
        public async Task<ActionResult<List<HotelJsonModel>>> FilterOnFile(IFormFile formFile, [FromRoute] long HotelID, [FromRoute] DateTimeOffset ArrivalDate)
        {
            if (formFile == null || formFile.Length == 0)
                return BadRequest();

            var filteredList = await _hotelService.FilterAsync(formFile, HotelID, ArrivalDate);
            return Ok(filteredList);
        }
    }
}
