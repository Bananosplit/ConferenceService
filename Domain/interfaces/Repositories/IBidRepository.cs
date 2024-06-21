using Domain.Entity;

namespace Domain.interfaces.Repositories
{
    public interface IBidRepository
    {
        Task<Bid?> Get(Guid id);
        Task<Bid?> Get(int id);
        Task<List<Bid>?> GetBidsByDate(DateTime date, bool isSumbited = false);
        Task<bool> Create(Bid bid);
        Task<bool> Update(Bid bid);
        Task<bool> Delete(Bid bid);
    }
}