using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AdessoCase.Core.DTOs
{
    public class TravelDto
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int DepartureCityId { get; set; }
        public int ArrivalCityId { get; set; }
        public DateTime TravelDate { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
        public int SeatCount { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
    }
}
