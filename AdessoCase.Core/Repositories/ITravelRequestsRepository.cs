namespace AdessoCase.Core.Repositories
{
    public interface ITravelRequestsRepository : IGenericRepository<TravelRequests>
    {
        Task<Travel?> GetTravelById(int travelId);
    }
}
