using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Persistencia.Data;

namespace Application.Repository
{
    public class RolRepository
    {
        private readonly PracticasTokenContext _context;

      public RolRepository(PracticasTokenContext context)
        {
            _context = context;
        }

    }
}