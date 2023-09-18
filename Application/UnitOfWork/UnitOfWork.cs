using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Repository;
using Domain.Interfaces;
using Persistencia.Data;

namespace Application.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        private readonly PracticasTokenContext _context;
        private RolRepository _rols;
        private UserRepository _users;

        public UnitOfWork(PracticasTokenContext context)
        {
            _context = context;
        }

        public IRol Rols
        {
            get
            {
                if(_rols == null)
                {
                    _rols = new RolRepository(_context);
                }
                return (IRol)_rols;
            }
        }
        public IUser Users{
            get{
                if(_users == null)
                {
                    _users = new UserRepository(_context); 
                }
                return _users;
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