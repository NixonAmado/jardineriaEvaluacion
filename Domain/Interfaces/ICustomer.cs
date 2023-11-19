
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
    Task<IEnumerable<Customer>> GetNameNoDeliveryOnTime();
    Task<IEnumerable<Customer>> GetByOrderNotPaid();
    Task<IEnumerable<Customer>> GetByNotPaidAndNotOrder();
    Task<object> GetByCustomerQuantityInCity(string city);
    Task<object> GetByCustomerQuantityInLetterCity(string letter);
    Task<object> GetByNotAssignedEmployee();
    Task<Customer> GetByGreatestCreditLimit();
    Task<IEnumerable<object>> GetFirstLastPaymentByCustomer();
    Task<IEnumerable<Customer>> GetByHigherCreditLimitThanPayment();
    Task<IEnumerable<Customer>> GetByNotOrder();
    Task<IEnumerable<Customer>> GetByOrderPaid();
    Task<IEnumerable<object>> GetNameAndOrdersQuantity();
    Task<IEnumerable<Customer>> GetByOrderInYear(int year);
    Task<IEnumerable<object>> GetDataAndEmployee();
    Task<IEnumerable<object>> GetDataAndEmployeeCity();
}
