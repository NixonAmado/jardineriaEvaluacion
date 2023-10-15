using Application.Repository;
using Domain.Interfaces;
using Persistencia.Data;

namespace Application.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        private readonly DbAppContext _context;
        private RoleRepository _roles;
        private UserRepository _users;

        public UnitOfWork(DbAppContext context)
        {
            _context = context;
        }

        public IRole Roles
        {
            get
            {
                if(_roles == null)
                {
                    _roles = new RoleRepository(_context);
                }
                return _roles;
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