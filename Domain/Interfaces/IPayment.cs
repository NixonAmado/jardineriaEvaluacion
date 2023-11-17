
using Domain.Entities;
namespace Domain.Interfaces;
public interface IPayment : IGenericRepository<Payment>
{
    Task<IEnumerable<Payment>> GetByPaymentMethodYear(string paymentMethod,int year);
}
