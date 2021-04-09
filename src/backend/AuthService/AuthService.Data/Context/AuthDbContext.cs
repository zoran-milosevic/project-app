using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AuthService.Model.Entities;

namespace AuthService.Data.Context
{
    public class AuthDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public AuthDbContext()
        {
        }

        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<ApplicationRole> ApplicationRole { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.EnableSensitiveDataLogging();

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=localhost;Initial catalog=Auth;User ID=db;Password=db;Persist Security Info=False;Packet Size=4096");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // TABLE NAME MAPPINGS
            modelBuilder.Entity<ApplicationUser>(entity => entity.ToTable(name: "User", schema: "Security"));
            modelBuilder.Entity<ApplicationRole>(entity => entity.ToTable(name: "Role", schema: "Security"));
            modelBuilder.Entity<IdentityUserRole<int>>(entity => entity.ToTable(name: "UserRole", schema: "Security"));
            modelBuilder.Entity<IdentityUserLogin<int>>(entity => entity.ToTable(name: "UserLogin", schema: "Security"));
            modelBuilder.Entity<IdentityUserToken<int>>(entity => entity.ToTable(name: "UserToken", schema: "Security"));
            modelBuilder.Entity<IdentityUserClaim<int>>(entity => entity.ToTable(name: "UserClaim", schema: "Security"));
            modelBuilder.Entity<IdentityRoleClaim<int>>(entity => entity.ToTable(name: "RoleClaim", schema: "Security"));

            // MODEL CONFIGURATION
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("UserId");
                entity.Property(e => e.FirstName).HasMaxLength(50);
                entity.Property(e => e.LastName).HasMaxLength(50);
                entity.Property(e => e.PhoneNumber).HasMaxLength(50);
                entity.Property(e => e.PasswordHash).HasMaxLength(250);
                entity.Property(e => e.SecurityStamp).HasMaxLength(50);
                entity.Property(e => e.ConcurrencyStamp).HasMaxLength(50);
            });

            modelBuilder.Entity<ApplicationRole>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("RoleId");
                entity.Property(e => e.Name).HasMaxLength(50);
                entity.Property(e => e.NormalizedName).HasMaxLength(50);
                entity.Property(e => e.ConcurrencyStamp).HasMaxLength(50);
            });

            modelBuilder.Entity<IdentityUserRole<int>>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserId");
                entity.Property(e => e.RoleId).HasColumnName("RoleId");
            });

            modelBuilder.Entity<IdentityUserLogin<int>>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserId");
                entity.Property(e => e.ProviderDisplayName).HasMaxLength(50);
            });

            modelBuilder.Entity<IdentityUserToken<int>>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserId");
                entity.Property(e => e.Value).HasMaxLength(250);
            });

            modelBuilder.Entity<IdentityUserClaim<int>>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserId");
                entity.Property(e => e.Id).HasColumnName("UserClaimId");
                entity.Property(e => e.ClaimType).HasMaxLength(50);
                entity.Property(e => e.ClaimValue).HasMaxLength(50);
            });

            modelBuilder.Entity<IdentityRoleClaim<int>>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("RoleClaimId");
                entity.Property(e => e.RoleId).HasColumnName("RoleId");
                entity.Property(e => e.ClaimType).HasMaxLength(50);
                entity.Property(e => e.ClaimValue).HasMaxLength(50);
            });

            // SEED DATA
            var hasher = new PasswordHasher<ApplicationUser>();

            modelBuilder.Entity<ApplicationRole>()
                .HasData(
                    new ApplicationRole
                    {
                        Id = 1,
                        Name = "SuperAdmin",
                        NormalizedName = "SUPERADMIN"
                    },
                    new ApplicationRole
                    {
                        Id = 2,
                        Name = "Admin",
                        NormalizedName = "ADMIN"
                    },
                    new ApplicationRole
                    {
                        Id = 3,
                        Name = "User",
                        NormalizedName = "USER"
                    }
                );
            
            modelBuilder.Entity<ApplicationUser>()
                .HasData(
                    new ApplicationUser
                    {
                        Id = 1,
                        FirstName = "Zoran",
                        LastName = "Milosevic",
                        UserName = "zoran.milosevic@test.test",
                        NormalizedUserName = "zoran.milosevic@test.test".ToUpper(),
                        Email = "zoran.milosevic@test.test",
                        NormalizedEmail = "zoran.milosevic@test.test",
                        EmailConfirmed = true,
                        PasswordHash = hasher.HashPassword(null, "P@ssw0rd"),
                        SecurityStamp = System.Guid.NewGuid().ToString(),
                        Created = System.DateTime.Now
                    }
                );
            
            modelBuilder.Entity<IdentityUserRole<int>>()
                .HasData(
                    new IdentityUserRole<int>
                    {
                        UserId = 1,
                        RoleId = 2
                    },
                    new IdentityUserRole<int>
                    {
                        UserId = 1,
                        RoleId = 3
                    }
                );
        }
    }
}
