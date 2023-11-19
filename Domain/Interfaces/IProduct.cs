
using Domain.Entities;
namespace Domain.Interfaces;
public interface IProduct : IGenericRepository<Product>
{
    Task<IEnumerable<Product>> GetByGamaStock(string gama, int stock);
    Task<IEnumerable<Product>> GetNeverInOrder();
     Task<IEnumerable<object>> GetNeverInOrderspecified();
}
