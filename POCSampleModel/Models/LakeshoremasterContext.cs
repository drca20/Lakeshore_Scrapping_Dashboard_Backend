using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace POCSampleModel.Models;

public partial class LakeshoremasterContext : DbContext
{
    public LakeshoremasterContext(DbContextOptions<LakeshoremasterContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Applicationobject> Applicationobjects { get; set; }

    public virtual DbSet<Applicationobjecttype> Applicationobjecttypes { get; set; }

    public virtual DbSet<Compititor> Compititors { get; set; }

    public virtual DbSet<Compititorproduct> Compititorproducts { get; set; }

    public virtual DbSet<Compititorproductmap> Compititorproductmaps { get; set; }

    public virtual DbSet<Lakeshoreproduct> Lakeshoreproducts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Productmap> Productmaps { get; set; }

    public virtual DbSet<Productpricehistory> Productpricehistories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Applicationobject>(entity =>
        {
            entity.HasKey(e => e.ApplicationObjectId).HasName("PRIMARY");
        });

        modelBuilder.Entity<Applicationobjecttype>(entity =>
        {
            entity.HasKey(e => e.ApplicationObjectTypeId).HasName("PRIMARY");
        });

        modelBuilder.Entity<Compititor>(entity =>
        {
            entity.HasKey(e => e.CompititorsId).HasName("PRIMARY");
        });

        modelBuilder.Entity<Compititorproduct>(entity =>
        {
            entity.HasKey(e => e.CompititorProductId).HasName("PRIMARY");
        });

        modelBuilder.Entity<Compititorproductmap>(entity =>
        {
            entity.HasKey(e => e.CompititorproductmapId).HasName("PRIMARY");
        });

        modelBuilder.Entity<Lakeshoreproduct>(entity =>
        {
            entity.HasKey(e => e.Lakeshoreproductid).HasName("PRIMARY");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PRIMARY");

            entity.HasOne(d => d.Compititor).WithMany(p => p.Products).HasConstraintName("fk_products_compititor");
        });

        modelBuilder.Entity<Productmap>(entity =>
        {
            entity.HasKey(e => e.ProductMapId).HasName("PRIMARY");
        });

        modelBuilder.Entity<Productpricehistory>(entity =>
        {
            entity.HasKey(e => e.ProductPriceHistoryId).HasName("PRIMARY");

            entity.HasOne(d => d.Product).WithMany(p => p.Productpricehistories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_producthistory_product");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
