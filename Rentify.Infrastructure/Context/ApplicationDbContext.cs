using GenericRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rentify.Domain.Categories;
using Rentify.Domain.Items;
using Rentify.Domain.Reservations;
using Rentify.Domain.Users;

namespace Rentify.Infrastructure.Context;
internal sealed class ApplicationDbContext
    : IdentityDbContext<User, Role, Guid>, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // İstenmeyen entity'leri yok sayma
        modelBuilder.Ignore<IdentityUserClaim<Guid>>();
        modelBuilder.Ignore<IdentityRoleClaim<Guid>>();
        modelBuilder.Ignore<IdentityUserToken<Guid>>();
        modelBuilder.Ignore<IdentityUserLogin<Guid>>();

        // UserRole enum'ını integer olarak saklama
        modelBuilder.Entity<Role>()
            .Property(r => r.UserRole)
            .HasConversion<int>(); // Enum'ı integer olarak saklamak için

        // Rol Id'leri
        var adminRoleId = new Guid("cda48d5e-987c-496f-9731-7934f63d598a");
        var sellerRoleId = new Guid("b7f857bb-de17-4022-8ccd-8dd334dcd915");
        var normalUserRoleId = new Guid("be967297-21d2-4767-ae1b-c79b93f36950");

        // IdentityUserRole için primary key tanımlaması
        modelBuilder.Entity<IdentityUserRole<Guid>>()
            .HasKey(i => new { i.UserId, i.RoleId });

        // Role'ler için başlangıç verisi
        modelBuilder.Entity<Role>().HasData(
        new Role { Id = adminRoleId, Name = "Admin", NormalizedName = "ADMIN", UserRole = UserRole.Admin },
        new Role { Id = sellerRoleId, Name = "Seller", NormalizedName = "SELLER", UserRole = UserRole.Seller },
        new Role { Id = normalUserRoleId, Name = "NormalUser", NormalizedName = "NORMALUSER", UserRole = UserRole.NormalUser }
        );

    }

    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Item> Items { get; set; }
}

