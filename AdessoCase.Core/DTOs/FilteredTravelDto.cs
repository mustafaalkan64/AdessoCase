using AdessoCase.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace AdessoCase.Core.DTOs
{
    public class TravelListDto
    {
        public int Id { get; set; }
        public DateTime TravelDate { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public int SeatCount { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }

        public TravelListDto(Travel travel)
        {
            Arrival = travel.Arrival?.Name ?? string.Empty;
            Departure = travel.Departure?.Name ?? string.Empty;
            Id = travel.Id;
            Description = travel.Description;
            SeatCount = travel.SeatCount;
            Status = ((TravelStatus)travel.Status).ToString();
            TravelDate = travel.TravelDate;
        }
    }
}
