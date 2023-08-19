namespace AdessoCase.Core.Repositories
{
    public interface ITravelRepository : IGenericRepository<Travel>
    {
        Task<List<Travel>> GetTravelsByDepartureAndArrivalAsync(string departure, string arrival);
    }
}
