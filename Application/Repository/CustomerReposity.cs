using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;


namespace Application.Repository;

    public class CustomerRepository : GenericRepository<Customer>, ICustomer
    {
        private readonly DbAppContext _context;

        public CustomerRepository(DbAppContext context): base(context)
        {
            _context = context;
        }
        //1. Devuelve un listado con el nombre de los todos los clientes españoles.
        public async Task<IEnumerable<Customer>> GetByCountry(string country)
        {
            return await _context.Customers
                .Where(c => c.Address.City.State.Country.Name == country)
                .ToListAsync();
        }
        //. Devuelve un listado con el código de cliente de aquellos clientes que
        // realizaron algún pago en X. Tenga en cuenta que deberá eliminar
        // aquellos códigos de cliente que aparezcan repetidos. Resuelva la consulta:
        // • Utilizando la función YEAR de MySQL.
        // • Utilizando la función DATE_FORMAT de MySQL.
        // • Sin utilizar ninguna de las funciones anteriores.

        public async Task<IEnumerable<Customer>> GetIdByPaymentDate(int year)
        {
            return await _context.Customers
                                .Where(c => c.Orders.Any(o => o.Payment.PaymentDate.Year == year))
                                .Distinct()
                                .ToListAsync();
        }


  public override async Task<(int totalRegistros, IEnumerable<Customer> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
            {
                var query = _context.Customers as IQueryable<Customer>;
    
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
