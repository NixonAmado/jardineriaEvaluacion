using System.Globalization;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;


namespace Application.Repository;

public class OrderRepository : GenericRepository<Order>, IOrder
{
    private readonly DbAppContext _context;

    public OrderRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }

    //2. Devuelve un listado con los distintos estados por los que puede pasar un pedido.

    public async Task<IEnumerable<Object>> GetAllStatus()
    {
        var dato = await (
            from o in _context.Orders
            select new
            {
                Status = o.Status
            }
        ).Distinct()
        .ToListAsync();
        return dato;

    }
    //9. Devuelve un listado con el código de pedido, código de cliente, fecha esperada y fecha de entrega de los pedidos que no han sido entregados a tiempo.
    public async Task<IEnumerable<Order>> GetAllNotDeliveredOnTime()
    {
        return await _context.Orders
                .Where(o => o.ExpectedDate  > o.DeliveryDate)
                .ToListAsync();
    }
    //10. Devuelve un listado con el código de pedido, código de cliente, fecha esperada y fecha de entrega de los pedidos cuya fecha de entrega ha sido al menos dos días antes de la fecha esperada.
    public async Task<IEnumerable<Order>> GetAllDeliveredEarlier()
    {
        return await _context.Orders
                .Where(o => (o.DeliveryDate.HasValue ? o.DeliveryDate.Value.Day + 2  : DateTime.MinValue.Day) <= o.ExpectedDate.Day &&
                o.DeliveryDate.HasValue)
                .ToListAsync();
    }
    //11. Devuelve un listado de todos los pedidos que fueron X en X.
    public async Task<IEnumerable<Order>> GetOrderByStatusYear(string status, int year)
    {
        return await _context.Orders
                .Where(o => o.Status.ToUpper() == status.ToUpper() && o.OrderDate.Year == year)
                .ToListAsync();
    }
    //12. Devuelve un listado de todos los pedidos que han sido (status X) en el mes  X de cualquier año.
    public async Task<IEnumerable<Order>> GetAllByMonth(string status, string Month)
    {
        //quise hacerlo de esta manera para aprender a convertir un mes string es un su respectivo int.
        List<Order> OrdersByMonth = new();
        if(DateOnly.TryParseExact(Month,"MMMM", CultureInfo.CurrentCulture, DateTimeStyles.None, out  DateOnly targetDate))
        {
            OrdersByMonth = await _context.Orders
                .Where(o => o.Status.ToUpper() == status.ToUpper() && targetDate.Month == o.OrderDate.Month)
                .ToListAsync();
        }
        return OrdersByMonth;
    }
    

    //3. ¿Cuál fue el pago medio en 2009?
    public async Task<object> GetOrderPaymentAverangeInYear(int year)
    {
        return new {PaymentAverange =  await _context.Orders.Where(o => o.OrderDate.Year == year).AverageAsync( p => p.Payment.Total)};
    }
    //4. ¿Cuántos pedidos hay en cada estado? Ordena el resultado de forma descendente por el número de pedidos.
    public async Task<object> GetOrdersQuantityByStatus ()
    {
    var result = await _context.Orders
        .GroupBy(o => o.Status)
        .Select(g => new{ Status = g.Key, orderQuantity = g.Count()}).OrderByDescending(o => o.orderQuantity)
        .ToListAsync();

    return result;
    } 

    //10. Calcula el número de productos diferentes que hay en cada uno de los pedidos.
    
    //hay en total 276 productos -> select count(*) from jardineria.product
    public async Task<object> GetByDifferentProdQuantity()
    {
        return new{ DiffProductQuantity = await _context.Orders.Select(o => o.OrderDetails.Select(od => od.ProductId).Distinct()).CountAsync()};
    }

    //11. Calcula la suma de la cantidad total de todos los productos que aparecen en cada uno de los pedidos.
    //asimilo que el se refiere a cantidad total de productos no al precio total
    public async Task<object> GetTotalSumProdInOrder()
    {
        return await _context.OrderDetails
                            .GroupBy(od => od.OrderId)
                            
                            .Select(g => new
                            { 
                                OrderId =g.Key,
                                TotalProductSum = g.Sum(p => Convert.ToInt32(p.Cantidad))
                            }).ToListAsync();               
    }
    //12. Devuelve un listado de los 20 productos más vendidos y el número total de unidades que se han vendido de cada uno. El listado deberá estar ordenado por el número total de unidades vendidas.
        public async Task<IEnumerable<object>> GetMostSold()
        {
            return await _context.OrderDetails
                            .GroupBy(od => od.ProductId)
                            
                            .Select(g => new
                            { 
                                g.FirstOrDefault().Product.Name, 
                                g.FirstOrDefault().Product.GamaNavigation.Gama,
                                g.FirstOrDefault().Product.Supplier,
                                g.FirstOrDefault().Product.Description,
                                TotalProductSum = g.Sum(p => Convert.ToInt32(p.Cantidad))
                            })
                            .OrderByDescending(p => p.TotalProductSum)
                            .Take(20)
                            .ToListAsync();               

        }
        public async Task<IEnumerable<object>> GetMostSoldGroupedByCod()
        {
            return await _context.OrderDetails
                            .GroupBy(od => od.ProductId)
                            
                            .Select(g => new
                            { 
                                g.FirstOrDefault().Product.Id,
                                g.FirstOrDefault().Product.Name, 
                                g.FirstOrDefault().Product.GamaNavigation.Gama,
                                g.FirstOrDefault().Product.Supplier,
                                g.FirstOrDefault().Product.Description,
                                TotalProductSum = g.Sum(p => Convert.ToInt32(p.Cantidad))
                            })
                            .OrderByDescending(p => p.Id)
                            .Take(20)
                            .ToListAsync();               
        }
    //14. La misma información que en la pregunta anterior, pero agrupada por código de producto filtrada por los códigos que empiecen por OR. 
        public async Task<IEnumerable<object>> GetMostSoldGroupedByCodFiltered(string letters)
        {
            return await _context.OrderDetails
                            .Where(od => od.ProductId.ToUpper().StartsWith(letters.ToUpper()))
                            .GroupBy(od => od.ProductId)
                            
                            .Select(g => new
                            { 
                                g.FirstOrDefault().Product.Id,
                                g.FirstOrDefault().Product.Name, 
                                g.FirstOrDefault().Product.GamaNavigation.Gama,
                                g.FirstOrDefault().Product.Supplier,
                                g.FirstOrDefault().Product.Description,
                                TotalProductSum = g.Sum(p => Convert.ToInt32(p.Cantidad))
                            })
                            .OrderByDescending(p => p.Id)
                            .Take(20)
                            .ToListAsync();               
        }
        //15. Lista las ventas totales de los productos que hayan facturado más de X euros. Se mostrará el nombre, unidades vendidas, total facturado y total facturado con impuestos (21% IVA).
        public async Task<IEnumerable<object>> GetTotalSaleByQuantityRange(int range)
        {
            return await _context.OrderDetails
                            .GroupBy(od => od.ProductId)
                            .Select(g => new
                            { 
                                g.FirstOrDefault().Product.Name,
                                g.FirstOrDefault().UnitPrice,
                                Total = g.Sum(od => Math.Round(Convert.ToInt32(g.FirstOrDefault().Cantidad) * od.UnitPrice, 2)),
                                TotalWithTaxes = g.Sum(od => Math.Round(Convert.ToInt32(g.FirstOrDefault().Cantidad) * od.UnitPrice , 2)) * (decimal)1.21
                            })
                            .Where(g => g.Total > range)
                            .OrderByDescending(g => g.Total)
                            .ToListAsync();
        }



    public override async Task<(int totalRegistros, IEnumerable<Order> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Orders as IQueryable<Order>;

        if (!string.IsNullOrEmpty(search))
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
