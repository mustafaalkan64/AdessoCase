using AdessoCase.Core.Enums;
using System.Text.Json.Serialization;

namespace AdessoCase.Core.DTOs
{
    public class ChangeTravelStatusDto
    {
        public int TravelId { get; set; }
        public TravelStatus TravelStatus { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
    }
}
