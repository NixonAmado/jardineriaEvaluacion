
using Domain.Entities;
namespace Domain.Interfaces;
public interface IOrder : IGenericRepository<Order>
{
    Task<IEnumerable<Object>> GetAllStatus();
    Task<IEnumerable<Order>> GetAllNotDeliveredOnTime();
    Task<IEnumerable<Order>> GetAllDeliveredEarlier();
    Task<IEnumerable<Order>> GetOrderByStatusYear(string status, int year);
    Task<IEnumerable<Order>> GetAllByMonth(string status, string Month);
    Task<object> GetOrderPaymentAverangeInYear(int year);
    Task<object> GetOrdersQuantityByStatus();
    Task<object> GetByDifferentProdQuantity();
    Task<object> GetTotalSumProdInOrder();
    Task<IEnumerable<object>> GetMostSold();
    Task<IEnumerable<object>> GetMostSoldGroupedByCod();
     Task<IEnumerable<object>> GetMostSoldGroupedByCodFiltered(string letters);
     Task<IEnumerable<object>> GetTotalSaleByQuantityRange(int range);
}