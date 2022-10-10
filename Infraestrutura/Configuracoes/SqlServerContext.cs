using Entidades.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Configuracoes
{
    public class SqlServerContext : DbContext
    {
        public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Usuario>().HasKey(item => item.Id);

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ObterConnectionString());
                base.OnConfiguring(optionsBuilder);
            }
        }

        public string ObterConnectionString()
        {
            return "Server=localhost;Database=Portal_Usuario;Trusted_Connection=True;";
        }
    }
}
