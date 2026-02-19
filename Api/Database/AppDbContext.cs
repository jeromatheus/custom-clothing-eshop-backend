using Api.Model;
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
    public DbSet<FixedAttribute> FixedAttributes { get; set; }
    public DbSet<VariableAttribute> VariableAttributes { get; set; }
    public DbSet<StockItem> StockItems { get; set; }
    public DbSet<ImageGroup> ImageGroups { get; set; }
    public DbSet<Image> Images { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // =========================================================
        // 1. FIXED ATTRIBUTE (PRODUCTO BASE)
        // =========================================================
        modelBuilder.Entity<FixedAttribute>(entity =>
        {
            entity.HasKey(f => f.Id);

            entity.Property(f => f.Garment).HasConversion<string>().HasMaxLength(50);
            entity.Property(f => f.Material).HasConversion<string>().HasMaxLength(50);
            entity.Property(f => f.Neck).HasConversion<string>().HasMaxLength(50);
            entity.Property(f => f.Fit).HasConversion<string>().HasMaxLength(50);
            entity.Property(f => f.WarmthLevel).HasConversion<string>().HasMaxLength(50);
            entity.Property(f => f.WarmthLevel);

            entity.HasMany(f => f.VariableAttributes)
                  .WithOne(v => v.FixedAttribute)
                  .HasForeignKey(v => v.FixedAttributeId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(f => new { f.Garment, f.Material, f.Neck, f.Fit })
                  .IsUnique();

            entity.Property(f => f.Price)
                  .HasColumnType("decimal(18,2)") 
                  .HasDefaultValue(0)
                  .IsRequired();
        });

        // =========================================================
        // 2. VARIABLE ATTRIBUTE (COLOR)
        // =========================================================
        modelBuilder.Entity<VariableAttribute>(entity =>
        {
            entity.HasKey(v => v.Id);

            entity.Property(v => v.Color)
                  .HasConversion<string>()
                  .HasMaxLength(50);

            entity.HasIndex(v => new { v.FixedAttributeId, v.Color })
                  .IsUnique();

            entity.HasMany(v => v.StockItems)
                  .WithOne(s => s.VariableAttribute)
                  .HasForeignKey(s => s.VariableAttributeId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(v => v.ImageGroups)
                  .WithOne(g => g.VariableAttribute)
                  .HasForeignKey(g => g.VariableAttributeId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // =========================================================
        // 3. STOCK ITEM (SIZE + CANTIDAD)
        // =========================================================
        modelBuilder.Entity<StockItem>(entity =>
        {
            entity.HasKey(s => s.Id);

            entity.Property(s => s.Size)
                  .HasConversion<string>()
                  .HasMaxLength(20);

            entity.HasIndex(s => new { s.VariableAttributeId, s.Size })
                  .IsUnique();
        });

        // =========================================================
        // 4. IMAGE GROUP
        // =========================================================
        modelBuilder.Entity<ImageGroup>(entity =>
        {
            entity.HasKey(g => g.Id);

            entity.Property(g => g.ModelWearingSize)
                  .HasConversion<string>()
                  .HasMaxLength(20);

            entity.HasMany(g => g.Images)
                  .WithOne(i => i.ImageGroup)
                  .HasForeignKey(i => i.ImageGroupId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // =========================================================
        // 5. IMAGE
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
