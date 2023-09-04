namespace AdessoCase.Core.UnitOfWorks
{
    public interface IUnitOfWork
    {
        Task CommitAsync(CancellationToken token);
        void Commit();

    }
}
