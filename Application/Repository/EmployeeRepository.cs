using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;


namespace Application.Repository;

    public class EmployeeRepository : GenericRepository<Employee>, IEmployee
    {
        private readonly DbAppContext _context;

        public EmployeeRepository(DbAppContext context): base(context)
        {
            _context = context;
        }
          public override async Task<(int totalRegistros, IEnumerable<Employee> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
            {
                var query = _context.Employees as IQueryable<Employee>;
    
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
