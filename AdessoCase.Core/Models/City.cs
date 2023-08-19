namespace AdessoCase.Core
{
    public class City : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Travel> DepartureNavigation { get; set; }
        public ICollection<Travel> ArrivalNavigation { get; set; }
    }
}
