using System.ComponentModel.DataAnnotations.Schema;

namespace AdessoCase.Core
{
    public class Travel : BaseEntity
    {
        [ForeignKey("Departure")]
        public int DepartureCityId { get; set; }
        [ForeignKey("Arrival")]
        public int ArrivalCityId { get; set; }
        public DateTime TravelDate { get; set; }
        public int Status { get; set; }
        public int SeatCount { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public City Departure { get; set; }
        public City Arrival { get; set; }
        public ICollection<TravelRequests> TravelRequests { get; set; }
    }
}
