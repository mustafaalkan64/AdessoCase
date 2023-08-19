using AdessoCase.Core.Enums;

namespace AdessoCase.Core.DTOs
{
    public class ChangeTravelStatusDto
    {
        public int TravelId { get; set; }
        public TravelStatus TravelStatus { get; set; }
    }
}
