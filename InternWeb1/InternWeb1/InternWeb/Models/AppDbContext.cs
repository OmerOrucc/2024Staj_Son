using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
namespace InternWeb.Models
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration)
               : base(options)
        {
            _configuration = configuration;
        }

        // DbSet'ler
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Urunler { get; set; }
        public DbSet<kategori> Kategori { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User Entity Konfigürasyonu
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("kullanici");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.KullaniciMail).HasColumnName("kullaniciMail").IsRequired();
                entity.Property(e => e.KullaniciAdi).HasColumnName("kullaniciAdi").IsRequired();
                entity.Property(e => e.KullaniciSoyadi).HasColumnName("kullaniciSoyadi").IsRequired();
                entity.Property(e => e.KullaniciAdres).HasColumnName("kullaniciAdres");
                entity.Property(e => e.KullaniciSifre).HasColumnName("kullaniciSifre").IsRequired();
            });

            // Product Entity Konfigürasyonu
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("urunler");
                entity.HasKey(e => e.id);
                entity.Property(e => e.id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.kategoriId).HasColumnName("kategoriId").IsRequired();
                entity.Property(e => e.urunAdi).HasColumnName("urunAdi").IsRequired();
                entity.Property(e => e.urunAciklama).HasColumnName("urunAciklama");
                entity.Property(e => e.urunFiyat).HasColumnName("urunFiyat").HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(e => e.urunStok).HasColumnName("urunStok").IsRequired();
                entity.Property(e => e.urunGorsel).HasColumnName("urunGorsel");
                entity.Property(e => e.urunDurum).HasColumnName("urunDurum").IsRequired();

                entity.HasOne(e => e.Kategori)
                      .WithMany(c => c.urunler)
                      .HasForeignKey(e => e.kategoriId);
            });

            // Kategori Entity Konfigürasyonu
            modelBuilder.Entity<kategori>(entity =>
            {
                entity.ToTable("kategori");
                entity.HasKey(e => e.id);
                entity.Property(e => e.id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.kategoriAdi).HasColumnName("kategoriAdi").IsRequired();
            });
        }
    }
}
