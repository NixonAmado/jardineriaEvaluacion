using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;


namespace Application.Repository;

    public class CustomerRepository : GenericRepository<Customer>, ICustomer
    {
        private readonly DbAppContext _context;

        public CustomerRepository(DbAppContext context): base(context)
        {
            _context = context;
        }
        //1. Devuelve un listado con el nombre de los todos los clientes españoles.
        public async Task<IEnumerable<Customer>> GetByCountry(string country)
        {
            return await _context.Customers
                .Where(c => c.Address.City.State.Country.Name == country)
                .ToListAsync();
        }
        //. Devuelve un listado con el código de cliente de aquellos clientes que
        // realizaron algún pago en X. Tenga en cuenta que deberá eliminar
        // aquellos códigos de cliente que aparezcan repetidos. Resuelva la consulta:
        // • Utilizando la función YEAR de MySQL.
        // • Utilizando la función DATE_FORMAT de MySQL.
        // • Sin utilizar ninguna de las funciones anteriores.

        public async Task<IEnumerable<Customer>> GetIdByPaymentDate(int year)
        {
            return await _context.Customers
                                .Where(c => c.Orders.Any(o => o.Payment.PaymentDate.Year == year))
                                .Distinct()
                                .ToListAsync();
        }
        // 16. Devuelve un listado con todos los clientes que sean de la ciudad de Madrid y cuyo representante de ventas tenga el código de empleado 11 o 30. 1.4.5 Consultas multitabla (Composición interna) Resuelva todas las consultas utilizando la sintaxis de SQL1 y SQL2. Las consultas con sintaxis de SQL2 se deben resolver con INNER JOIN y NATURAL JOIN.

        public async Task<IEnumerable<Customer>> GetByCityEmployee(string city, int employeeId1, int employeeId2)
        {
            return  await _context.Customers
                                .Where(c => c.Address.City.Name.ToUpper() == city.ToUpper() && c.Orders.All(o => o.EmployeeId == employeeId1 || o.EmployeeId == employeeId2))
                                .ToListAsync();
        }
        //1. Obtén un listado con el nombre de cada cliente y el nombre y apellido de su representante de ventas.
        public async Task<IEnumerable<object>> GetNameAndEmployee()
        {
            //En la normalización cambie la relación de empleado con cliente, para que tanto el cliente como el empleado como el pago apuntaran a la orden, así que, en este caso un 
            //cliente no tiene un solo representante de ventas, si no que, puede estar ligado a muchos representantes, ya que puede hacer muchas ordenes.
            return await _context.Customers
                                .Include(c => c.Orders)
                                .ThenInclude(o => o.Employee)
                                .Select(c => new 
                                {
                                    c.Name,
                                    associatedEmployees = 
                                    c.Orders.Select(o => new
                                    {
                                        o.Employee.Name,
                                        LastName = o.Employee.LastName1 , o.Employee.LastName2
                                    }).Distinct()
                                })
                                .ToListAsync();
        }
        //2. Muestra el nombre de los clientes que hayan realizado pagos junto con el nombre de sus representantes de ventas.
        public async Task<IEnumerable<object>> GetByOrderEmployee()
        {
            //En la normalización cambie la relación de empleado con cliente, para que tanto el cliente como el empleado como el pago apuntaran a la orden, así que, en este caso un 
            //cliente no tiene un solo representante de ventas, si no que, puede estar ligado a muchos representantes, ya que puede hacer muchas ordenes.
           return await _context.Customers
                                .Include(c => c.Orders)
                                .ThenInclude(o => o.Employee)
                                .Where(c => c.Orders.Any())
                                .Select(c => new 
                                {
                                    c.Name,
                                    associatedEmployees = 
                                    c.Orders.Select(o => new
                                    {
                                        o.Employee.Name,
                                    }).Distinct()
                                })
                                .ToListAsync();
        }

        //3. Muestra el nombre de los clientes que no hayan realizado pagos junto con el nombre de sus representantes de ventas.
        public async Task<IEnumerable<object>> GetByOrderNotPaymentEmployee()
        {
            //En la normalización cambie la conexión de empleado con cliente, para que tanto el cliente como el empleado como el pago apuntaran a la orden, así que, en este caso un 
            //cliente no tiene un solo representante de ventas, si no que, puede estar ligado a muchos representantes, ya que puede hacer muchas ordenes.
           return await _context.Customers
                                .Include(c => c.Orders)
                                .ThenInclude(o => o.Employee)
                                .Where(c => c.Orders.Any(o => o.PaymentId == null))
                                .Select(c => new 
                                {
                                    c.Name,
                                    associatedEmployees = 
                                    c.Orders.Select(o => new
                                    {
                                        o.Employee.Name,
                                    }).Distinct()
                                })
                                .ToListAsync();
        }
        // 4. Devuelve el nombre de los clientes que han hecho pagos y el nombre de sus representantes junto con la ciudad de la oficina a la que pertenece el representante.
        public async Task<IEnumerable<object>> GetByOrderPaymentEmployee()
        {
            //En la normalización cambie la conexión de empleado con cliente, para que tanto el cliente como el empleado como el pago apuntaran a la orden, así que, en este caso un 
            //cliente no tiene un solo representante de ventas, si no que, puede estar ligado a muchos representantes, ya que puede hacer muchas ordenes.
           return await _context.Customers
                                .Include(c => c.Orders)
                                .ThenInclude(o => o.Employee)
                                .Where(c => c.Orders.Any(o => o.PaymentId != null))
                                .Select(c => new 
                                {
                                    c.Name,
                                    associatedEmployees = 
                                    c.Orders.Select(o => new
                                    {
                                        o.Employee.Name,
                                        City = o.Employee.Office.Address.City.Name
                                    }).Distinct()
                                })
                                .ToListAsync();
        }
        //5. Devuelve el nombre de los clientes que no hayan hecho pagos y el nombre de sus representantes junto con la ciudad de la oficina a la que pertenece el representante.
        public async Task<IEnumerable<object>> GetByOrderNotPaymentEmployeeCity()
        {
            
           return await _context.Customers
                                .Include(c => c.Orders)
                                .ThenInclude(o => o.Employee)
                                .Where(c => c.Orders.Any(o => o.PaymentId == null))
                                .Select(c => new 
                                {
                                    c.Name,
                                    associatedEmployees = 
                                    c.Orders.Select(o => new
                                    {
                                        o.Employee.Name,
                                        City = o.Employee.Office.Address.City.Name
                                    }).Distinct()
                                })
                                .ToListAsync();
        }
        //7. Devuelve el nombre de los clientes a los que no se les ha entregado a tiempo un pedido.
        public async Task<IEnumerable<Customer>> GetNameNoDeliveryOnTime()
        {
           return await _context.Customers
                                .Where(c => c.Orders.Any(o => o.DeliveryDate > o.ExpectedDate))                                
                                .ToListAsync();
        }
        //1. Devuelve un listado que muestre solamente los clientes que no han realizado ningún pago.

        //8. Devuelve un listado con los clientes que han realizado algún pedido pero no han realizado ningún pago.
        
        public async Task<IEnumerable<Customer>> GetByOrderNotPaid()
        {
           return await _context.Customers
                                .Where(c => c.Orders.Any(o => o.PaymentId == null))                                
                                .ToListAsync();
        }  
        //2. Devuelve un listado que muestre los clientes que no han realizado ningún pago y los que no han realizado ningún pedido.
        public async Task<IEnumerable<Customer>> GetByNotPaidAndNotOrder()
        {
            //En este caso, con la normalización se redirecciono pedido para que este vinculado con la orden directamente, entoces, si un cliente no tiene asociada una order, tampoco un pago.
           return await _context.Customers
                                .Where(c => !c.Orders.Any())
                                .ToListAsync();
         
        }

        //5. ¿Cuántos clientes existen con domicilio en la ciudad de Madrid?
        public async Task<object> GetByCustomerQuantityInCity(string city)
        {
            return  new {customerQuantity = await _context.Customers.Where(c => c.Address.City.Name.ToUpper() == city.ToUpper()).CountAsync()};
        }
        //6. ¿Calcula cuántos clientes tiene cada una de las ciudades que empiezan por M?
        public async Task<object> GetByCustomerQuantityInLetterCity(string letter)
        {
            return  new {customerQuantity = await _context.Customers
            .Where(c => c.Address.City.Name.ToUpper().StartsWith(letter.ToUpper())).CountAsync()};
        }
        //8. Calcula el número de clientes que no tiene asignado representante de ventas.
        public async Task<object> GetByNotAssignedEmployee()
        {
            //tabla normalizada
            //asumiendo que un cliente no puede hacer un pedido sin un empleado asosiado
            //si el cliente no tiene pedido, no tiene un empleado
        return new { CustomerQuantity = await _context.Customers.Where(c => !c.Orders.Any()).CountAsync()};
        }
        //9. Calcula la fecha del primer y último pago realizado por cada uno de los clientes. El listado deberá mostrar el nombre y los apellidos de cada cliente.
        public async Task<IEnumerable<object>> GetFirstLastPaymentByCustomer()
        {
            return await _context.Customers
                                .Where(c => c.Orders.Any(o => o.PaymentId != null))
                                .Select( c => new{
                                    firstPayment = c.Orders.Min(o => o.Payment.PaymentDate),
                                    LastPayment = c.Orders.Max(o => o.Payment.PaymentDate),
                                    c.Name,
                                    lastName = c.ContactName + " " + c.ContactLastName
                                }).ToListAsync();
        }

        //1. Devuelve el nombre del cliente con mayor límite de crédito
        //8. Devuelve el nombre del cliente con mayor límite de crédito. En linq tanto Any como All esperaran una condicion para hacer un filtro, no vi manera de solucionarlo con alguno de estos metodos.
        
        public async Task<Customer> GetByGreatestCreditLimit()
        {
            return await _context.Customers 
                                .OrderByDescending(c => c.CreditLimit)
                                .FirstOrDefaultAsync();
        }
        //4. Los clientes cuyo límite de crédito sea mayor que los pagos que haya realizado. (Sin utilizar INNER JOIN).
        public async Task<IEnumerable<Customer>> GetByHigherCreditLimitThanPayment()
        {
            return await _context.Customers
                                .Where(c => c.Orders.Any(o => o.PaymentId != null ))
                                .Where(c => c.CreditLimit > c.Orders.Sum(o => o.Payment.Total))
                                .ToListAsync();
        }

        //11. Devuelve un listado que muestre solamente los clientes que no han realizado ningún pago.
        public async Task<IEnumerable<Customer>> GetByNotOrder()
        
        {
            //mi forma de simular un in
            return await _context.Customers 
                                .Where(c => c.Orders.Any(o => o.Payment == null))
                                .ToListAsync();
        }
        //12. Devuelve un listado que muestre solamente los clientes que sí han realizado
        public async Task<IEnumerable<Customer>> GetByOrderPaid()
        {
            //mi forma de simular un in
            return await _context.Customers 
                                .Where(c => c.Orders.Any(o => o.Payment != null))
                                .ToListAsync();
        }
        //1. Devuelve el listado de clientes indicando el nombre del cliente y cuántos pedidos ha realizado. Tenga en cuenta que pueden existir clientes que no han realizado ningún pedido.

        public async Task<IEnumerable<object>> GetNameAndOrdersQuantity()
        {
            return await _context.Customers
                                .Select(c => new
                                {
                                    c.Name,
                                    c.Orders.Count
                                }).ToListAsync();
        }
        //2. Devuelve el nombre de los clientes que hayan hecho pedidos en 2008 ordenados alfabéticamente de menor a mayor.
        public async Task<IEnumerable<Customer>> GetByOrderInYear(int year)
        {
            return await _context.Customers
                                .Where(c => c.Orders.All(o => o.OrderDate.Year > year))
                                .OrderBy(c => c.Name)
                                .ToListAsync();
        }
        //3. Devuelve el nombre del cliente, el nombre y primer apellido de su representante de ventas y el número de teléfono de la oficina del representante de ventas, de aquellos clientes que no hayan realizado ningún pago.
        public async Task<IEnumerable<object>> GetDataAndEmployee()
        {
            return await _context.Customers
                                .Where(c => c.Orders.Any(o => o.PaymentId == null))
                                .Select(c => new
                                {
                                    c.Name,
                                    AssociatedEmployees = c.Orders.Select(o => new
                                    {
                                        o.Employee.Name,
                                        o.Employee.LastName1,
                                        OfficeNumber = o.Employee.Office.Phone
                                    })
                                }).ToListAsync();
        }
        //4. Devuelve el listado de clientes donde aparezca el nombre del cliente, el nombre y primer apellido de su representante de ventas y la ciudad donde está su oficina.
        //En la normalización, un cliente puede estar asociado a más de un empleado(1*)
        public async Task<IEnumerable<object>> GetDataAndEmployeeCity()
        {
            return await _context.Customers
                                .Select(c => new
                                {
                                    c.Name,
                                    AssociatedEmployees = c.Orders.Select(o => new
                                    {
                                        o.Employee.Name,
                                        o.Employee.LastName1,
                                        OfficeCity = o.Employee.Office.Address.City.Name
                                    })
                                }).ToListAsync();
        }
        
        public override async Task<(int totalRegistros, IEnumerable<Customer> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
            {
                var query = _context.Customers as IQueryable<Customer>;
    
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
