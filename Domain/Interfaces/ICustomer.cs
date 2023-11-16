
using Domain.Entities;
namespace Domain.Interfaces;
public interface ICustomer : IGenericRepository<Customer>
{
    Task<IEnumerable<Customer>> GetByCountry(string country);
}
