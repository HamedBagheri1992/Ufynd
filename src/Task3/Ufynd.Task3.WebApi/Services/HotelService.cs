using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ufynd.Task3.WebApi.Models;
using Ufynd.Task3.WebApi.Services.Contracts;

namespace Ufynd.Task3.WebApi.Services
{
    public class HotelService : IHotelService
    {
        public async Task<List<HotelJsonModel>> FilterAsync(IFormFile formFile, long hotelID, DateTimeOffset arrivalDate)
        {
            using var streamReader = new StreamReader(formFile.OpenReadStream());
            var jsonContent = await streamReader.ReadToEndAsync();

            var hotelJsonModel = JsonConvert.DeserializeObject<List<HotelJsonModel>>(jsonContent);

            var filtered = hotelJsonModel.Where(h => h.hotel.hotelID == hotelID)
                .Select(h => new HotelJsonModel
                {
                    hotel = h.hotel,
                    hotelRates = h.hotelRates.Where(x => x.targetDay == arrivalDate).ToList()
                }).ToList();

            return filtered;
        }
    }
}
