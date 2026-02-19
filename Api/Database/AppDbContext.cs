using Api.Models; // Asegúrate de que apunte a tu namespace correcto
using Microsoft.EntityFrameworkCore;

namespace Api.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // =========================================================
    // DBSETS (Tablas)
    // =========================================================
    public DbSet<Product> Products { get; set; }
    public DbSet<Variant> Variants { get; set; }
    public DbSet<SizeMeasurement> SizeMeasurements { get; set; }
    public DbSet<StockItem> StockItems { get; set; }
    public DbSet<ImageGroup> ImageGroups { get; set; }
    public DbSet<Image> Images { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // =========================================================
        // 1. PRODUCT (PRODUCTO BASE)
        // =========================================================
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);

            // Enums a String
            entity.Property(p => p.Garment).HasConversion<string>().HasMaxLength(50);
            entity.Property(p => p.Material).HasConversion<string>().HasMaxLength(50);
            entity.Property(p => p.Neck).HasConversion<string>().HasMaxLength(50);
            entity.Property(p => p.Fit).HasConversion<string>().HasMaxLength(50);
            entity.Property(p => p.Warmth).HasConversion<string>().HasMaxLength(50);    // TODO

            // Relación: Product 1 -> N Variants
            entity.HasMany(p => p.Variants)
                  .WithOne(v => v.Product)
                  .HasForeignKey(v => v.ProductId)
                  .OnDelete(DeleteBehavior.Cascade);

            // Relación: Product 1 -> N SizeMeasurements (Tabla de talles general)
            entity.HasMany(p => p.SizeMeasurements)
                  .WithOne(s => s.Product)
                  .HasForeignKey(s => s.ProductId)
                  .OnDelete(DeleteBehavior.Cascade);

            // Índice para no repetir exactamente el mismo producto base
            entity.HasIndex(p => new { p.Garment, p.Material, p.Neck, p.Fit })
                  .IsUnique();

            entity.Property(p => p.Price)
                  .HasColumnType("decimal(18,2)")
                  .HasDefaultValue(0)
                  .IsRequired();
        });

        // =========================================================
        // 2. VARIANT (COLOR / ARTÍCULO FÍSICO)
        // =========================================================
        modelBuilder.Entity<Variant>(entity =>
        {
            entity.HasKey(v => v.Id);

            entity.Property(v => v.Color)
                  .HasConversion<string>()
                  .HasMaxLength(50);

            // Índice: No puede haber dos variantes del mismo color para el mismo producto base
            entity.HasIndex(v => new { v.ProductId, v.Color })
                  .IsUnique();

            // Relación: Variant 1 -> N StockItems
            entity.HasMany(v => v.StockItems)
                  .WithOne(s => s.Variant)
                  .HasForeignKey(s => s.VariantId)
                  .OnDelete(DeleteBehavior.Cascade);

            // Relación: Variant 1 -> N ImageGroups
            entity.HasMany(v => v.ImageGroups)
                  .WithOne(g => g.Variant)
                  .HasForeignKey(g => g.VariantId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // =========================================================
        // 3. SIZE MEASUREMENT (MEDIDAS / GUÍA DE TALLES)
        // =========================================================
        modelBuilder.Entity<SizeMeasurement>(entity =>
        {
            entity.HasKey(s => s.Id);

            entity.Property(s => s.Size)
                  .HasConversion<string>()
                  .HasMaxLength(20);

            // Índice: No se pueden repetir medidas para el mismo talle en un mismo producto
            entity.HasIndex(s => new { s.ProductId, s.Size })
                  .IsUnique();
        });

        // =========================================================
        // 4. STOCK ITEM (INVENTARIO)
        // =========================================================
        modelBuilder.Entity<StockItem>(entity =>
        {
            entity.HasKey(s => s.Id);

            entity.Property(s => s.Size)
                  .HasConversion<string>()
                  .HasMaxLength(20);

            // Índice: Una variante solo puede tener un registro de stock por talle
            entity.HasIndex(s => new { s.VariantId, s.Size })
                  .IsUnique();
        });

        // =========================================================
        // 5. IMAGE GROUP (GRUPO DE IMÁGENES)
        // =========================================================
        modelBuilder.Entity<ImageGroup>(entity =>
        {
            entity.HasKey(g => g.Id);

            entity.Property(g => g.ModelWearingSize)
                  .HasConversion<string>()
                  .HasMaxLength(20);

            // Relación: ImageGroup 1 -> N Images
            entity.HasMany(g => g.Images)
                  .WithOne(i => i.ImageGroup)
                  .HasForeignKey(i => i.ImageGroupId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // =========================================================
        // 6. IMAGE (FOTO REAL)
        // =========================================================
        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(i => i.Id);

            entity.Property(i => i.ImageUrl)
                  .IsRequired()
                  .HasMaxLength(1000);

            entity.Property(i => i.IsMain)
                  .HasDefaultValue(false);
        });
    }
}