
using Domain.Entities;
namespace Domain.Interfaces;
public interface IOffice : IGenericRepository<Office>
{
     Task<IEnumerable<Office>> GetByEmployeeWithProductGama(string gama);
}
