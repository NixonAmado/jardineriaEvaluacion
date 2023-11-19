
using Domain.Entities;
namespace Domain.Interfaces;
public interface IEmployee : IGenericRepository<Employee>
{
    Task<IEnumerable<object>> GetNameAndBossChief();
    Task<IEnumerable<Employee>> GetNotAssociatedEmployeeOffice(); 
    Task<IEnumerable<Employee>> GetNotAssociatedEmployeeAndOffice();
    Task<IEnumerable<Employee>> GetNotAssociatedcustomerBossName();
    Task<object> GetEmployeesQuantity ();
    Task<IEnumerable<object>> GetEmployeesCustomerQuantity();
    Task<IEnumerable<object>> GetEmployeesWithoutOrder();
    Task<IEnumerable<object>> GetDataByJobTitle();
}   
