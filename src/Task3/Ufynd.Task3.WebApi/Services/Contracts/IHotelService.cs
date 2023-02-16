using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ufynd.Task3.WebApi.Models;

namespace Ufynd.Task3.WebApi.Services.Contracts
{
    public interface IHotelService
    {
        Task<List<HotelJsonModel>> FilterAsync(IFormFile formFile, long hotelID, DateTimeOffset arrivalDate);
    }
}