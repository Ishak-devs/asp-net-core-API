using Microsoft.EntityFrameworkCore;


namespace projet_aspnet_api
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<Produits> Produits { get; set; }
        public DbSet<User> Users { get; set; }
    }

}
