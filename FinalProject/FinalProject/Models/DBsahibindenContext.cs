using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Models;

public partial class DBsahibindenContext : DbContext
{
    public DBsahibindenContext()
    {
    }

    public DBsahibindenContext(DbContextOptions<DBsahibindenContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Kategori> Kategoris { get; set; }

    public virtual DbSet<Kullanici> Kullanicis { get; set; }

    public virtual DbSet<Urunler> Urunlers { get; set; }
    public DbSet<SupportMessage> SupportMessages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=dBSahibinden;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true");

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

            entity.HasOne(d => d.Kategori).WithMany(p => p.Urunlers)
                .HasForeignKey(d => d.KategoriId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__urunler__kategor__4316F928");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
