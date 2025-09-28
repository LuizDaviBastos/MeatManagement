using MeatManager.Data.Interfaces;
using MeatManager.Model.Entities;

namespace MeatManager.Model.Interfaces
{
    public interface IMeatRepository : IRepository<Meat, Guid>
    {
        Task<bool> HasOrdersAsync(Guid id);
    }
}
