using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        public IRol Rols {get;}
        public IUser Users {get;}
        Task<int> SaveAsync();
    }
}