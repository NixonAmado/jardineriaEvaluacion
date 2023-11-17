
using Domain.Entities;
namespace Domain.Interfaces;
public interface IOrder : IGenericRepository<Order>
{
    Task<IEnumerable<Object>> GetAllStatus();
    Task<IEnumerable<Order>> GetAllNotDeliveredOnTime();
    Task<IEnumerable<Order>> GetAllDeliveredEarlier();
    Task<IEnumerable<Order>> GetOrderByStatusYear(string status, int year);
    Task<IEnumerable<Order>> GetAllByMonth(string status, string Month);
}