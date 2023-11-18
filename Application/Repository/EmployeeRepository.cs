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

        //6. Devuelve un listado que muestre el nombre de cada empleado, el nombre de su jefe y el nombre del jefe de su jefe.
        public async Task<IEnumerable<object>> GetNameAndBossChief()
        {
           return await _context.Employees
                                .Select(c => new 
                                {
                                    c.Name,
                                    Boss = c.Boss.Name,
                                    Chief = c.Boss.Boss.Name    

                                })
                                .ToListAsync();
        }
                  
        //3. Devuelve un listado que muestre solamente los empleados que no tienen un cliente asociado junto con los datos de la oficina donde trabajan

        public async Task<IEnumerable<Employee>> GetNotAssociatedEmployeeOffice()
        {
        // Como el empleado se vincula al cliente por medio de la orden, si el empleado no tiene una orden, significa que no ha tenido un empleado asociado
           return await _context.Employees
                                .Include(e => e.Office)
                                .ThenInclude(o => o.Address)
                                .Where(e => !e.Orders.Any()).ToListAsync();
        }

        //4. Devuelve un listado que muestre los empleados que no tienen una oficina asociada y los que no tienen un cliente asociado.
        public async Task<IEnumerable<Employee>> GetNotAssociatedEmployeeAndOffice()
        {
            return await _context.Employees
                                .Include(e => e.Office)
                                .ThenInclude(o => o.Address)
                                .Where(e => e.OfficeId == null || !e.Orders.Any()).ToListAsync();
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
