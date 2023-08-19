using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AdessoCase.Core.DTOs
{
    public class FilteredTravelListDto
    {
        public int Id { get; set; }
        public DateTime TravelDate { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public int SeatCount { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }
    }
}
