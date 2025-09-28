using MeatManager.Data.Interfaces;
using MeatManager.Model.Entities;

namespace MeatManager.Model.Interfaces
{
    public interface IOrderRepository : IRepository<Order, Guid>
    {
    }
}
