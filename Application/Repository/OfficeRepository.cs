using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;


namespace Application.Repository;

    public class OfficeRepository : GenericRepository<Office>, IOffice
    {
        private readonly DbAppContext _context;

        public OfficeRepository(DbAppContext context): base(context)
        {
            _context = context;
        }
        // 7. Devuelve las oficinas donde no trabajan ninguno de los empleados que hayan sido los representantes de ventas de algún cliente que haya realizado la compra de algún producto de la gama Frutales.
        public async Task<IEnumerable<Office>> GetByEmployeeWithProductGama(string gama)
        {
            return await _context.Offices
                            .Include(o => o.Address)
                            .ThenInclude(a => a.City)
                            .Where(o => o.Employees
                            .Any(e => e.JobTitle.ToUpper() == "REPRESENTANTE VENTAS") &&
                            !o.Employees.Any(e => e.Orders.Any(o => o.OrderDetails
                            .Any(od => od.Product.GamaNavigation.Gama.Equals(gama))))).ToListAsync();
        }
        public override async Task<(int totalRegistros, IEnumerable<Office> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
            {
                var query = _context.Offices as IQueryable<Office>;
    
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
