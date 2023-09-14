namespace Persistencia.Data;

public class PracticasTokenContext : DbContext
{
    PracticasTokenContext(DbContextOptions options): base(options)
    {
    }

        public DbSet<RefreshToken> RefreshTokens {get;set;}
        public DbSet<Rol> Rols {get;set;}
        public DbSet<User> Users {get;set;}
        public DbSet<UserRol> UsersRols {get;set;}

    public override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}