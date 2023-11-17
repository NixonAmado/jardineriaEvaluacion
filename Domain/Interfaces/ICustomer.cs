
using Domain.Entities;
namespace Domain.Interfaces;
public interface ICustomer : IGenericRepository<Customer>
{
    Task<IEnumerable<Customer>> GetByCountry(string country);
    Task<IEnumerable<Customer>> GetIdByPaymentDate(int year);
    Task<IEnumerable<Customer>> GetByCityEmployee(string city, int employeeId1, int employeeId2);
    Task<IEnumerable<Object>> GetNameAndEmployee();
    Task<IEnumerable<object>> GetByOrderEmployee();
    Task<IEnumerable<object>> GetByOrderNotPaymentEmployee();
    Task<IEnumerable<object>> GetByOrderPaymentEmployee();
    Task<IEnumerable<object>> GetByOrderNotPaymentEmployeeCity();
}
