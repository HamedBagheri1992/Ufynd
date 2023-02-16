using System.Collections.Generic;

namespace Ufynd.Task1.WebApi.Models
{
    public class DataExtractorDto
    {
        public string HotelName { get; set; }
        public string Address { get; set; }
        public int Classification { get; set; }
        public float ReviewPoints { get; set; }
        public int NumberOfReviews { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> RoomCategories { get; set; }
        public IEnumerable<string> AlternativeHotels { get; set; }
    }
}
