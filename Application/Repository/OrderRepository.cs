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
                .Where(o => (o.DeliveryDate.HasValue ? o.DeliveryDate.Value.Day + 2  : DateTime.MinValue.Day) <= o.ExpectedDate.Day)
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
