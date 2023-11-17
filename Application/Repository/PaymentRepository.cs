using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;


namespace Application.Repository;

    public class PaymentRepository : GenericRepository<Payment>, IPayment
    {
        private readonly DbAppContext _context;

        public PaymentRepository(DbAppContext context): base(context)
        {
            _context = context;
        }
        //13. Devuelve un listado con todos los pagos que se realizaron en el año X mediante X. Ordene el resultado de mayor a menor.
        public async Task<IEnumerable<Payment>> GetByPaymentMethodYear(string paymentMethod,int year)
        // lo ordené de mayor a menor por el precio, ya que, no se especifica como debo ordenarlo.
        {
            return await _context.Payments
                                .Include(p => p.PaymentMethod)
                                .Where(p => p.PaymentMethod.Description.ToUpper() == paymentMethod.ToUpper() && p.PaymentDate.Year == year)
                                .OrderByDescending(p => p.Total) 
                                .ToListAsync(); 
        }


          public override async Task<(int totalRegistros, IEnumerable<Payment> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
            {
                var query = _context.Payments as IQueryable<Payment>;
    
                if(!string.IsNullOrEmpty(search))
                {
                    query = query.Where(p => p.Id.ToString() == search);
                }
    
                query = query.OrderBy(p => p.Id);
                var totalRegistros = await query.CountAsync();
                var registros = await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
    
                return (totalRegistros, registros);
            }
    }
