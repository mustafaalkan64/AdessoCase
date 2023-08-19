namespace AdessoCase.Core
{
    public class TravelRequests: BaseEntity
    {
        public int TravelId { get; set; }
        public int UserId { get; set; }
        public bool Approved { get; set; }

        public Travel Travel { get; set; }
    }
}
