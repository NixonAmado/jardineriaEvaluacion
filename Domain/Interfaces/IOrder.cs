
using Domain.Entities;
namespace Domain.Interfaces;
public interface IOrder : IGenericRepository<Order>
{
    Task<IEnumerable<Object>> GetAllStatus();
    Task<IEnumerable<Order>> GetAllNotDeliveredOnTime();
    Task<IEnumerable<Order>> GetAllDeliveredEarlier();
}
