
using Domain.Entities;
namespace Domain.Interfaces;
public interface IProductGama : IGenericRepository<ProductGama>
{
    Task<IEnumerable<object>> GetByProductGama();
}
