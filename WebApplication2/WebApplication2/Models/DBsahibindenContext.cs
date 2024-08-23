using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace WebApplication2.Models;

public partial class DBsahibindenContext : DbContext
{
    private readonly IConfiguration _configuration;
    public DBsahibindenContext()
    {
    }

    public DBsahibindenContext(DbContextOptions<DBsahibindenContext> options)
        : base(options)
    {
    }
    public DBsahibindenContext(DbContextOptions<DBsahibindenContext> options, IConfiguration configuration)
               : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Kategori> Kategori { get; set; }

    public virtual DbSet<Kullanici> Kullanici { get; set; }

    public virtual DbSet<Urunler> Urunler { get; set; }

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
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<Kategori>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__kategori__3213E83F84C171E7");

            entity.ToTable("kategori");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.KategoriAdi)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("kategoriAdi");
        });

        modelBuilder.Entity<Kullanici>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__kullanic__E011F77B073D8F2E");

            entity.ToTable("kullanici");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.KullaniciAdi)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.KullaniciAdres).HasColumnType("text");
            entity.Property(e => e.KullaniciMail)
                .HasMaxLength(30)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.KullaniciSifre).HasMaxLength(100);
            entity.Property(e => e.KullaniciSoyadi)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Urunler>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__urunler__3213E83F5DF9542D");

            entity.ToTable("urunler");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EklenmeTarihi)
                .HasColumnType("datetime")
                .HasColumnName("eklenmeTarihi");
            entity.Property(e => e.KategoriId).HasColumnName("kategoriId");
            entity.Property(e => e.UrunAciklama)
                .HasColumnType("text")
                .HasColumnName("urunAciklama");
            entity.Property(e => e.UrunAdi)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("urunAdi");
            entity.Property(e => e.UrunDurum)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("urunDurum");
            entity.Property(e => e.UrunFiyat)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("urunFiyat");
            entity.Property(e => e.UrunGorsel).HasColumnName("urunGorsel");
            entity.Property(e => e.UrunStok).HasColumnName("urunStok");

            entity.HasOne(d => d.Kategori).WithMany(p => p.Urunler)
                .HasForeignKey(d => d.KategoriId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__urunler__kategor__4316F928");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
