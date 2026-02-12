using Api.Features.Catalog.Models;
using Api.Features.Purchase.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // --- DbSets (Tablas) ---
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductVariant> ProductVariants => Set<ProductVariant>();
    public DbSet<ProductImageGroup> ProductImageGroups => Set<ProductImageGroup>();
    public DbSet<ProductImage> ProductImages => Set<ProductImage>();
    public DbSet<ProductMeasurement> ProductMeasurements => Set<ProductMeasurement>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // =========================================================
        // 1. Configuración de PRODUCT
        // =========================================================
        modelBuilder.Entity<Product>(builder =>
        {
            builder.HasKey(p => p.Id);

            // SKU Único e indexado para búsquedas rápidas
            builder.HasIndex(p => p.Sku).IsUnique();
            builder.Property(p => p.Sku).HasMaxLength(50).IsRequired();

            // Precio con precisión monetaria (18 dígitos total, 2 decimales)
            builder.Property(p => p.Price).HasPrecision(18, 2);

            // Guardar el Tipo de producto como texto (Ej: "TShirt" en vez de 0)
            builder.Property(p => p.Type).HasConversion<string>();

            // Relaciones: Si borras el Producto, se borra todo su contenido
            builder.HasMany(p => p.Variants)
                   .WithOne(v => v.Product)
                   .HasForeignKey(v => v.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.ImageGroups)
                   .WithOne()
                   .HasForeignKey(g => g.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.Type).HasConversion<string>();
        });

        // =========================================================
        // 2. Configuración de PRODUCT VARIANT
        // =========================================================
        modelBuilder.Entity<ProductVariant>(builder =>
        {
            builder.HasKey(v => v.Id);

            // Conversión de Enums a String para legibilidad en BD
            builder.Property(v => v.Size).HasConversion<string>();
            builder.Property(v => v.Color).HasConversion<string>();
            builder.Property(v => v.Fabric).HasConversion<string>();
            builder.Property(v => v.NeckType).HasConversion<string>();
            builder.Property(v => v.Fit).HasConversion<string>();

            builder.HasMany(v => v.Measurements)
                .WithOne()
                .HasForeignKey(m => m.ProductVariantId) 
                .OnDelete(DeleteBehavior.Cascade);
        });

        // =========================================================
        // 3. Configuración de PRODUCT IMAGE GROUP (El modelo)
        // =========================================================
        modelBuilder.Entity<ProductImageGroup>(builder =>
        {
            builder.HasKey(g => g.Id);

            // Talle que usa el modelo (Enum -> String)
            builder.Property(g => g.ModelWearingSize).HasConversion<string>();

            // Relación con sus imágenes internas
            builder.HasMany(g => g.Images)
                   .WithOne()
                   .HasForeignKey(i => i.ProductImageGroupId)
                   .OnDelete(DeleteBehavior.Cascade);
        });

        // =========================================================
        // 4. Configuración de PRODUCT IMAGE (La foto real)
        // =========================================================
        modelBuilder.Entity<ProductImage>(builder =>
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.ImageUrl).IsRequired();
        });

        // =========================================================
        // 5. Configuración de PRODUCT MEASUREMENT (Tabla de talles)
        // =========================================================
        modelBuilder.Entity<ProductMeasurement>(builder =>
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.MeasurementName)
                   .HasMaxLength(100)
                   .IsRequired();

            // Valor numérico (Ej: 52.50)
            builder.Property(m => m.Value).HasPrecision(10, 2);

            // Talle asociado (Enum -> String)
            builder.Property(m => m.Size).HasConversion<string>();
        });
    }
}