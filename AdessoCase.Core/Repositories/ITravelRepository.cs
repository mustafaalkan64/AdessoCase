namespace AdessoCase.Core.Repositories
{
    public interface ITravelRepository : IGenericRepository<Travel>
    {
        Task<List<Travel>> GetTravelsByDepartureAndArrivalAsync(string departure, string arrival, CancellationToken cancellationToken = default);
        Task<List<Travel>> GetAllWithLocaltions(CancellationToken cancellationToken = default);
        Task<Travel?> GetByIdWithLocaltions(int travelId, CancellationToken cancellationToken = default);
    }
}
