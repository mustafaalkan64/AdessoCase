using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AdessoCase.Core.DTOs
{
    public class FilteredTravelDto
    {
        public int Id { get; set; }
        public DateTime TravelDate { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
        public int SeatCount { get; set; }
        public CityDto Departure { get; set; }
        public CityDto Arrival { get; set; }
        public int DepartureCityId { get; set; }
        public int ArrivalCityId { get; set; }
    }
}
