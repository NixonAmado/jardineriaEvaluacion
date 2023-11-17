using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;


namespace Application.Repository;

    public class PaymentMethodRepository : GenericRepository<PaymentMethod>, IPaymentMethod
    {
        private readonly DbAppContext _context;

        public PaymentMethodRepository(DbAppContext context): base(context)
        {
            _context = context;
        }
        //14. Devuelve un listado con todas las formas de pago que aparecen en la tabla pago. Tenga en cuenta que no deben aparecer formas de pago repetidas.
         //Por mi parte normalice los metodos de pago, así que solamente es necesario utilizar el metodo generico GetAllAsync.
        public override async Task<(int totalRegistros, IEnumerable<PaymentMethod> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
            {
                var query = _context.PaymentMethods as IQueryable<PaymentMethod>;
    
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
