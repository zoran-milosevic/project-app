using Microsoft.EntityFrameworkCore;
using ProjectApp.Data.Configurations;
using ProjectApp.Domain.Entities;
using ProjectApp.Model.Entities.User;

namespace ProjectApp.Data.Context
{
    public class AppDbContext : DbContext
    {
        // private readonly string _connectionstring;

        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //var sqlServerOptionsExtension = options.FindExtension<SqlServerOptionsExtension>();

            //if (sqlServerOptionsExtension != null)
            //{
            //    _connectionstring = sqlServerOptionsExtension.ConnectionString;
            //}
        }

        public DbSet<InternalUser> InternalUser { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Text> Text { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // In case when no database provider has been configured for this DbContext at database migrations. EF Core Tools commands require a default AppDbContext()
                // contructor instance to be created at design time in order to gather details about the application's entity types and how they map to a database schema.
                optionsBuilder.UseSqlServer(@"Data Source=localhost;Initial catalog=Database;User ID=db;Password=db;Persist Security Info=False;Packet Size=4096");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // MODEL CONFIGURATION
            modelBuilder.ApplyConfiguration(new UserProfileConfiguration());
            modelBuilder.ApplyConfiguration(new TextConfiguration());
        }
    }
}
