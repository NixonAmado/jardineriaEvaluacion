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
