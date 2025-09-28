using MeatManager.Data.Interfaces;
using MeatManager.Model.Entities;

namespace MeatManager.Model.Interfaces
{
    public interface IBuyerRepository : IRepository<Buyer, Guid>
    {
        Task<bool> HasOrdersAsync(Guid id);
    }
}
