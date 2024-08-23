using DevineWebRazor_Temp.Models;
using Microsoft.EntityFrameworkCore;

namespace DevineWebRazor_Temp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Metal", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Metal(Rare)", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Chemical Element", DisplayOrder = 3 }
                );
        }
    }
}
