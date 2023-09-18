using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia.Data;

namespace Application.Repository
{
    public class UserRepository : GenericRepository<User>, IUser
    {
        private readonly PracticasTokenContext _context;

        public UserRepository(PracticasTokenContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> getByNameAsync(string name)
        {
            return await _context.Users
                            .Include(p => p.Rols)
                            .FirstOrDefaultAsync(u => u.Name.ToLower() == name.ToLower());
        }

    }
}