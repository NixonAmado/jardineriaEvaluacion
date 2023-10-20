using System.Reflection;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistencia.Data;

public class DbAppContext: DbContext
{
    public DbAppContext(DbContextOptions options) : base(options)
    {

    }
    public DbSet<Role> Roles{get;set;}
    public DbSet<User> Users {get;set;}
    public DbSet<UserRole> UsersRoles {get;set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        var roles = new[]
        {
            new Role { Id = 1, Description = "Administrador" },
            new Role { Id = 2, Description = "Empleado" },
            // Agrega otros roles según tus necesidades
        };

        modelBuilder.Entity<Role>().HasData(roles);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}