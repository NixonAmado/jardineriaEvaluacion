
using Domain.Entities;
namespace Domain.Interfaces;
public interface ICountry : IGenericRepository<Country>
{
    Task<IEnumerable<object>> GetCustomersQuantityByCountry();
}
