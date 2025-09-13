using GameShop.Domain.Entities;
using GameShop.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Infrastructure.Persistence
{
    public static class DataSeeder
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.Parse("C0A8E3B0-4C5A-4B1E-8B3B-1B3B3B3B3B3B"),
                    Username = "admin",
                    HashPassword = "$2a$11$UGrgL.f6fM9XhIuCoJ9j4.d5Vw/xJg3jKl.S.Q/m.E/p8c.w0bI.W",
                    Email = "admin@gameshop.com",
                    UserRole = UserRole.Admin,
                    FirstName = "Admin",
                    LastName = "User",
                    Age = 30,
                    Address = "123 Admin Street",
                    Phone = "09120000001"
                },
                new User
                {
                    Id = Guid.Parse("D1B9F4E0-5D6B-4C2D-9C6B-2C4C4C4C4C4C"),
                    Username = "customer",
                    HashPassword = "$2a$11$wK1x.Z.f8gL9i.o.J.lK3u.d.VwXyZg.hIjKlMnOpQr.sTu.vW",
                    Email = "customer@email.com",
                    UserRole = UserRole.Customer,
                    FirstName = "Test",
                    LastName = "Customer",
                    Age = 25,
                    Address = "456 Customer Avenue",
                    Phone = "09120000002"
                }
            );

            modelBuilder.Entity<Game>().HasData(
                new { Id = Guid.Parse("E2C0A5F0-6E7C-4B3E-9D7C-3D5D5D5D5D5D"), Name = "The Last of Us Part II", Description = "Action-adventure game", Price = 59.99m, Stock = 100, Platform = Platform.Playstation4, IsDeleted = false },
                new { Id = Guid.Parse("F3D1B6A0-7F8D-4C4F-AF8D-4E6E6E6E6E6E"), Name = "Cyberpunk 2077", Description = "Action role-playing video game", Price = 49.99m, Stock = 150, Platform = Platform.PC, IsDeleted = false },
                new { Id = Guid.Parse("A4E2C7B0-8A9E-4D5A-BA9E-5F7F7F7F7F7F"), Name = "God of War Ragnarök", Description = "Action-adventure game by Santa Monica Studio", Price = 69.99m, Stock = 80, Platform = Platform.Playstation5, IsDeleted = false },
                new { Id = Guid.Parse("B5F3D8C0-9BAF-4E6B-CB0F-6A8A8A8A8A8A"), Name = "Halo Infinite", Description = "First-person shooter game", Price = 55.50m, Stock = 120, Platform = Platform.XboxSeries, IsDeleted = false },
                new { Id = Guid.Parse("C6A4E9D0-AB0A-4F7C-DC1A-7B9B9B9B9B9B"), Name = "The Legend of Zelda: Tears of the Kingdom", Description = "Action-adventure game", Price = 65.00m, Stock = 200, Platform = Platform.Nintendo, IsDeleted = false }
            );
        }
    }
}