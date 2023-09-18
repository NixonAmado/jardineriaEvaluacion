using System.Reflection;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistencia.Data;

public class PracticasTokenContext: DbContext
{
    public PracticasTokenContext(DbContextOptions options) : base(options)
    {

    }
    public DbSet<Rol> Rols{get;set;}
    public DbSet<User> Users {get;set;}
    public DbSet<UserRol> UsersRols {get;set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}