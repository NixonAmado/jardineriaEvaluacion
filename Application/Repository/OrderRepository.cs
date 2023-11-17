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
