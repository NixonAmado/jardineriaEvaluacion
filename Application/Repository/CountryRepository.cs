using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;


namespace Application.Repository;

    public class CountryRepository : GenericRepository<Country>, ICountry
    {
        private readonly DbAppContext _context;

        public CountryRepository(DbAppContext context): base(context)
        {
            _context = context;
        }
                //2. ¿Cuántos clientes tiene cada país?
        public async Task<IEnumerable<object>> GetCustomersQuantityByCountry()
        {
            return await _context.Countries
                                .Select(c => new{
                                    c.Name,
                                    CustomersQuantity = 
                                    c.States.SelectMany(s => s.Cities
                                            .SelectMany(c => c.Addresses
                                            .Select(a => a.Customers.Count)
                                            )).Sum()
                                })
                                .ToListAsync();
         
        }
    public override async Task<(int totalRegistros, IEnumerable<Country> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
            {
                var query = _context.Countries as IQueryable<Country>;
    
                if(!string.IsNullOrEmpty(search))
                {
                    query = query.Where(p => p.Name.ToLower() == search.ToLower());
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
