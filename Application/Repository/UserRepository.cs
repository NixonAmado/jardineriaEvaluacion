using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia.Data;

namespace Application.Repository
{
    public class UserRepository : GenericRepository<User>, IUser
    {
        private readonly DbAppContext _context1;

        public UserRepository(DbAppContext context) : base(context)
        {
            _context1 = context;
        }

        public async Task<User> GetByNameAsync(string name)
        {
            return await _context1.Users
                            .Include(p => p.Roles)
                            .FirstOrDefaultAsync(u => u.Name.ToLower() == name.ToLower());
        }
        public async Task<User> GetByRefreshTokenAsync(string refreshToken)
        {
        return await _context1.Users
            .Include(u => u.Roles)
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == refreshToken));
        }


    }
}