using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;


namespace Application.Repository;

    public class ProductRepository : GenericRepository<Product>, IProduct
    {
        private readonly DbAppContext _context;

        public ProductRepository(DbAppContext context): base(context)
        {
            _context = context;
        }
        
        //15. Devuelve un listado con todos los productos que pertenecen a la gama Ornamentales y que tienen más de 100 unidades en stock. El listado deberá estar ordenado por su precio de venta, mostrando en primer lugar los de mayor precio.

        public async Task<IEnumerable<Product>> GetByGamaStock(string gama, int stock)
        {
            return await _context.Products
                                .Where(p => p.GamaNavigation.Gama == gama && p.StockQuantity > stock )
                                .OrderByDescending(p => p.SalePrice)
                                .ToListAsync();
        }

        //5. Devuelve un listado de los productos que nunca han aparecido en un pedido.
        public async Task<IEnumerable<Product>> GetNeverInOrder()
        {
            return await _context.Products
                                .Where(p => !p.OrderDetails.Any())
                                .ToListAsync();
        }
        //6. Devuelve un listado de los productos que nunca han aparecido en un pedido. El resultado debe mostrar el nombre, la descripción y la imagen del producto.
        public async Task<IEnumerable<object>> GetNeverInOrderspecified()
        {
            return await _context.Products
                                .Where(p => !p.OrderDetails.Any())
                                .Select(p => new{
                                    p.Name,
                                    p.Description,
                                    p.GamaNavigation.Image
                                }).ToListAsync();
        }

        public override async Task<(int totalRegistros, IEnumerable<Product> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
            {
                var query = _context.Products as IQueryable<Product>;
    
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
