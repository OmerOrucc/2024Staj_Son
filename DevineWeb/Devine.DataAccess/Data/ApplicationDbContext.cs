using Devine.Models.Models;
using Devine.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DevineWeb.Data
{
    public class ApplicationDbContext:IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
              
        }
        public DbSet<Category>Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Metal", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Metal(Rare)", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Chemical Element", DisplayOrder = 3 }
                );
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "Gold(Ons)",
                    Seller="Oruc Mining Group Company",
                    ListPrice = 3000,
                    CategoryId = 1,
                    ImageUrl=""
                },
                new Product
                {
                    Id=2,
                    Title = "Diamond(1 carat)",
                    Seller = "Oruc Mining Group Company",
                    ListPrice = 16000,
                    CategoryId = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 3,
                    Title = "Silver(Ton)",
                    Seller = "Oruc Mining Group Company",
                    ListPrice = 45000,
                    CategoryId = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 4,
                    Title = "Platinum(Ons)",
                    Seller = "Oruc Mining Group Company",
                    ListPrice = 1200,
                    CategoryId = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 5,
                    Title = "Palladium(Ons)",
                    Seller = "Oruc Mining Group Company",
                    ListPrice = 1160,
                    CategoryId = 2,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 6,
                    Title = "Lithium(Kg)",
                    Seller = "Oruc Mining Group Company",
                    ListPrice = 15168,
                    CategoryId = 2,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 7,
                    Title = "Uranium(Ton)",
                    Seller = "Oruc Mining Group Company",
                    ListPrice = 3264000,
                    CategoryId = 3,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 8,
                    Title = "Cobalt(Ton)",
                    Seller = "Oruc Mining Group Company",
                    ListPrice = 37463,
                    CategoryId = 2,
                    ImageUrl = ""
                }

            );
        }
    }
}
