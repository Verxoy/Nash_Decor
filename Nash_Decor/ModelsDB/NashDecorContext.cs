using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Nash_Decor.ModelsDB;

public partial class NashDecorContext : DbContext
{
    public NashDecorContext()
    {
    }

    public NashDecorContext(DbContextOptions<NashDecorContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EdIzmerenie> EdIzmerenies { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<MaterialType> MaterialTypes { get; set; }

    public virtual DbSet<NameOfMaterial> NameOfMaterials { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductMaterial> ProductMaterials { get; set; }

    public virtual DbSet<ProductType> ProductTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=verxoy;Database=Nash_decor;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EdIzmerenie>(entity =>
        {
            entity.HasKey(e => e.UnitId).HasName("PK__ed_izmer__D3AF5BD75BC657F7");

            entity.ToTable("ed_izmerenie");

            entity.Property(e => e.UnitId)
                .ValueGeneratedNever()
                .HasColumnName("unit_id");
            entity.Property(e => e.UnitName)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("unit_name");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.MaterialId).HasName("PK__material__6BFE1D28DB4F8C31");

            entity.ToTable("materials");

            entity.Property(e => e.MaterialId)
                .ValueGeneratedNever()
                .HasColumnName("material_id");
            entity.Property(e => e.MaterialName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("material_name");
            entity.Property(e => e.MaterialTypeId).HasColumnName("material_type_id");
            entity.Property(e => e.MinQuantity).HasColumnName("min_quantity");
            entity.Property(e => e.QuantityPerPackage).HasColumnName("quantity_per_package");
            entity.Property(e => e.StockQuantity).HasColumnName("stock_quantity");
            entity.Property(e => e.UnitId).HasColumnName("unit_id");
            entity.Property(e => e.UnitPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("unit_price");

            entity.HasOne(d => d.MaterialType).WithMany(p => p.Materials)
                .HasForeignKey(d => d.MaterialTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__materials__mater__3F466844");

            entity.HasOne(d => d.Unit).WithMany(p => p.Materials)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__materials__unit___403A8C7D");
        });

        modelBuilder.Entity<MaterialType>(entity =>
        {
            entity.HasKey(e => e.MaterialTypeId).HasName("PK__material__42F2A3493416F8F3");

            entity.ToTable("material_type");

            entity.Property(e => e.MaterialTypeId)
                .ValueGeneratedNever()
                .HasColumnName("material_type_id");
            entity.Property(e => e.MaterialDefectPercentage)
                .HasColumnType("decimal(5, 4)")
                .HasColumnName("material_defect_percentage");
            entity.Property(e => e.MaterialTypeName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("material_type_name");
        });

        modelBuilder.Entity<NameOfMaterial>(entity =>
        {
            entity.HasKey(e => e.MaterialNameId).HasName("PK__name_of___511D7247BDC283E2");

            entity.ToTable("name_of_material");

            entity.Property(e => e.MaterialNameId)
                .ValueGeneratedNever()
                .HasColumnName("material_name_id");
            entity.Property(e => e.MaterialName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("material_name");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__products__47027DF5FE377B0F");

            entity.ToTable("products");

            entity.Property(e => e.ProductId)
                .ValueGeneratedNever()
                .HasColumnName("product_id");
            entity.Property(e => e.Article)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("article");
            entity.Property(e => e.MinPartnerPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("min_partner_price");
            entity.Property(e => e.ProductName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("product_name");
            entity.Property(e => e.ProductTypeId).HasColumnName("product_type_id");
            entity.Property(e => e.RollWidth)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("roll_width");

            entity.HasOne(d => d.ProductType).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__products__produc__4316F928");
        });

        modelBuilder.Entity<ProductMaterial>(entity =>
        {
            entity.HasKey(e => e.ProductMaterialId).HasName("PK__product___A0E9C950A2299235");

            entity.ToTable("product_materials");

            entity.Property(e => e.ProductMaterialId)
                .ValueGeneratedNever()
                .HasColumnName("product_material_id");
            entity.Property(e => e.MaterialId).HasColumnName("material_id");
            entity.Property(e => e.ProductName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("product_name");
            entity.Property(e => e.RequiredQuantity)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("required_quantity");

            entity.HasOne(d => d.Material).WithMany(p => p.ProductMaterials)
                .HasForeignKey(d => d.MaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__product_m__mater__45F365D3");
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.HasKey(e => e.ProductTypeId).HasName("PK__product___6EED3ED605F39C5F");

            entity.ToTable("product_type");

            entity.Property(e => e.ProductTypeId)
                .ValueGeneratedNever()
                .HasColumnName("product_type_id");
            entity.Property(e => e.ProductTypeCoefficient)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("product_type_coefficient");
            entity.Property(e => e.ProductTypeName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("product_type_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
