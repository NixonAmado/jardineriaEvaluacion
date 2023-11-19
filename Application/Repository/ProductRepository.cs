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
        //2. Devuelve el nombre del producto que tenga el precio de venta más caro.
        public async Task<Product> GetByHigherSalesPrice()
        {
            return await _context.Products
                                .OrderByDescending(p => p.SalePrice)
                                .FirstOrDefaultAsync();
        }
        //3. Devuelve el nombre del producto del que se han vendido más unidades.
        //(Tenga en cuenta que tendrá que calcular cuál es el número total de unidades que se han vendido de cada producto a partir de los datos de la tabla detalle_pedido)

        public async Task<object> GetByHigherUnitsPrice()
        {
            return await _context.OrderDetails
                                .GroupBy( od => od.ProductId)
                                .Select(g => new
                                {
                                    g.FirstOrDefault().Product.Name,
                                    TotalUnits = g.Sum(od => Convert.ToInt32(od.Cantidad))
                                })
                                .OrderByDescending(p => p.TotalUnits)
                                .FirstOrDefaultAsync();
        }
        //13. Devuelve un listado de los productos que nunca han aparecido en un pedido.
        public async Task<IEnumerable<Product>> GetByNotInOrder()
        {
            return await _context.Products
                                .Where(p => !p.OrderDetails.Any())
                                .ToListAsync();
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
