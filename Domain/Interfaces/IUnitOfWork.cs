

namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        public IRole Roles {get;}
        public IUser Users {get;}
        public IAdress Addresses {get;}
        public ICity Cities {get;}
        public ICountry Countries {get;}
        public ICustomer Customers {get;}
        public IEmployee Employees {get;}
        public IOffice Offices {get;}
        public IOrder Orders {get;}
        public IProduct Products {get;}
        public IProductGama ProductGamas {get;}
        public IPayment Payments {get;}
        public IPaymentMethod PaymentMethods {get;}
        public IState States {get;}

        Task<int> SaveAsync();
    }
}