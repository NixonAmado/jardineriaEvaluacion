
using Domain.Entities;

namespace Domain.Interfaces;

    public interface IUser : IGenericRepository<User>
    {
        Task<User> GetByNameAsync(string name);
        Task<User> GetByRefreshTokenAsync(string refreshToken);
    }
