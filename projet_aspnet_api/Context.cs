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
        public DbSet<Users> Users { get; set; }
    }

    public class Produits
    {
        public int Id { get; set; }
        public string Nom { get; set; }
    }

    public class Users
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Password { get; set; }
        public string role { get; set; }
    }
}
