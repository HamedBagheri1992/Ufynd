using System;

namespace Ufynd.Task2.Application.Common.Models
{
    public class HotelJsonModel
    {
        public Hotel hotel { get; set; }
        public Hotelrate[] hotelRates { get; set; }
    }

    public class Hotel
    {
        public int hotelID { get; set; }
        public int classification { get; set; }
        public string name { get; set; }
        public float reviewscore { get; set; }
    }

    public class Hotelrate
    {
        public int adults { get; set; }
        public int los { get; set; }
        public Price price { get; set; }
        public string rateDescription { get; set; }
        public string rateID { get; set; }
        public string rateName { get; set; }
        public Ratetag[] rateTags { get; set; }
        public DateTime targetDay { get; set; }
    }

    public class Price
    {
        public string currency { get; set; }
        public double numericFloat { get; set; }
        public int numericInteger { get; set; }
    }

    public class Ratetag
    {
        public string name { get; set; }
        public bool shape { get; set; }
    }
}
