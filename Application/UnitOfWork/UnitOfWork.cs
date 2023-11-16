using Application.Repository;
using Domain.Interfaces;
using Persistence.Data;


namespace Application.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        private readonly DbAppContext _context;
        private RoleRepository _Roles;
        private UserRepository _Users;
        private AddressRepository _Addresses;   
        private CityRepository _Cities; 
        private CountryRepository _Countries; 
        private CustomerRepository _Customers; 
        private EmployeeRepository _Employees; 
        private OfficeRepository _Offices; 
        private OrderRepository _Orders; 
        private ProductRepository _Products; 
        private ProductGamaRepository _ProductGamas; 
        private PaymentRepository _Payments; 
        private PaymentMethodRepository _PaymentMethods; 
        private StateRepository _States;

        
        
        public UnitOfWork(DbAppContext context)
        {
            _context = context;
        }

        public IRole Roles
        {
            get
            {
                if(_Roles == null)
                {
                    _Roles = new RoleRepository(_context);
                }
                return _Roles;
            }
        }
        public IUser Users{
            get{
                if(_Users == null)
                {
                    _Users = new UserRepository(_context); 
                }
                return _Users;
            }
        }
        public IAdress Addresses {
            get{
                if(_Addresses == null)
                {
                    _Addresses = new AddressRepository(_context); 
                }
                return _Addresses;
            }
        }
        public ICity Cities {
              get{
                if(_Cities == null)
                {
                    _Cities = new CityRepository(_context); 
                }
                return _Cities;
            }
        }
        public ICountry Countries {
              get{
                if(_Countries == null)
                {
                    _Countries = new CountryRepository(_context); 
                }
                return _Countries;
            }
        }
        public ICustomer Customers {
              get{
                if(_Customers == null)
                {
                    _Customers = new CustomerRepository(_context); 
                }
                return _Customers;
            }
        }
        public IEmployee Employees {
              get{
                if(_Employees == null)
                {
                    _Employees = new EmployeeRepository(_context); 
                }
                return _Employees;
            }
        }
        public IOffice Offices {
              get{
                if(_Offices == null)
                {
                    _Offices = new OfficeRepository(_context); 
                }
                return _Offices;
            }
        }
        public IOrder Orders {
                get{
                if(_Orders == null)
                {
                    _Orders = new OrderRepository(_context); 
                }
                return _Orders;
            }
        }
        public IProduct Products {
                get{
                if(_Products == null)
                {
                    _Products = new ProductRepository(_context); 
                }
                return _Products;
            }
        }
        public IProductGama ProductGamas {
                get{
                if(_ProductGamas == null)
                {
                    _ProductGamas = new ProductGamaRepository(_context); 
                }
                return _ProductGamas;
            }
        }
        public IPayment Payments {
                get{
                if(_Payments == null)
                {
                    _Payments = new PaymentRepository(_context); 
                }
                return _Payments;
            }
        }
        public IPaymentMethod PaymentMethods {
                get{
                if(_PaymentMethods == null)
                {
                    _PaymentMethods = new PaymentMethodRepository(_context); 
                }
                return _PaymentMethods;
            }
        }
        public IState States {
                get{
                if(_States == null)
                {
                    _States = new StateRepository(_context); 
                }
                return _States;
            }
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        
    }
}