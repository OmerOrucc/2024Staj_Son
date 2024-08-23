using Microsoft.EntityFrameworkCore;

namespace UserAuthService.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("kullanici");

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.id).HasColumnName("Id").ValueGeneratedOnAdd();
                entity.Property(e => e.kullaniciMail).HasColumnName("kullaniciMail").IsRequired();
                entity.Property(e => e.kullaniciAdi).HasColumnName("kullaniciAdi").IsRequired();
                entity.Property(e => e.kullaniciSoyadi).HasColumnName("kullaniciSoyadi").IsRequired();
                entity.Property(e => e.kullaniciAdres).HasColumnName("kullaniciAdres");
                entity.Property(e => e.kullaniciSifre).HasColumnName("kullaniciSifre").IsRequired();
            });

        }
    }
}
