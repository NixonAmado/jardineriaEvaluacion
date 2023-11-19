using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;


namespace Application.Repository;

    public class ProductGamaRepository : GenericRepository<ProductGama>, IProductGama
    {
        private readonly DbAppContext _context;

        public ProductGamaRepository(DbAppContext context): base(context)
        {
            _context = context;
        }
        //8. Devuelve un listado de las diferentes gamas de producto que ha comprado cada cliente. 1.4.6 Consultas multitabla (Composición externa) Resuelva todas las consultas utilizando las cláusulas LEFT JOIN, RIGHT JOIN, NATURAL LEFT JOIN y NATURAL RIGHT JOIN.
        public async Task<IEnumerable<object>> GetByProductGama()
        {
            return await _context.Products
                                .Include(p => p.GamaNavigation)
                                .Select(p => new{ProductGama = p.GamaNavigation.Gama})
                                .Distinct()
                                .ToListAsync();
        }
        public override async Task<(int totalRegistros, IEnumerable<ProductGama> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
            {
                var query = _context.ProductGamas as IQueryable<ProductGama>;
    
                if(!string.IsNullOrEmpty(search))
                {
                    query = query.Where(p => p.Gama.ToLower() == search.ToLower());
                }
    
                query = query.OrderBy(p => p.Gama);
                var totalRegistros = await query.CountAsync();
                var registros = await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
    
                return (totalRegistros, registros);
            }
    }
