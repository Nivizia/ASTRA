﻿using System;
using System.Collections.Generic;
using DiamondAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DiamondAPI.Data;

public partial class DiamondprojectContext : DbContext
{
    public DiamondprojectContext()
    {
    }

    public DiamondprojectContext(DbContextOptions<DiamondprojectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Diamond> Diamonds { get; set; }

    public virtual DbSet<Diamondcertificate> Diamondcertificates { get; set; }

    public virtual DbSet<Earring> Earrings { get; set; }

    public virtual DbSet<Earringpairing> Earringpairings { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Orderitem> Orderitems { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Pendant> Pendants { get; set; }

    public virtual DbSet<Pendantpairing> Pendantpairings { get; set; }

    public virtual DbSet<Ring> Rings { get; set; }

    public virtual DbSet<Ringpairing> Ringpairings { get; set; }

    public virtual DbSet<Shippingaddress> Shippingaddresses { get; set; }

    public virtual DbSet<Warranty> Warranties { get; set; }

    private string GetConnectionString()
    {
        IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", true, true).Build();
        return configuration["ConnectionStrings:DBDefault"];
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(GetConnectionString());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__CUSTOMER__A4AE64B8923B5DF1");

            entity.ToTable("CUSTOMER");

            entity.Property(e => e.CustomerId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("CustomerID");
            entity.Property(e => e.Email)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationDate).HasColumnType("datetime");
            entity.Property(e => e.Username)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Diamond>(entity =>
        {
            entity.HasKey(e => e.DProductId).HasName("PK__DIAMOND__C72A4046A740FDAC");

            entity.ToTable("DIAMOND");

            entity.Property(e => e.DProductId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("D_ProductID");
            entity.Property(e => e.Available).HasDefaultValue(true);
            entity.Property(e => e.DType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("D_Type");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ImageURL");
            entity.Property(e => e.Price).HasColumnType("money");
        });

        modelBuilder.Entity<Diamondcertificate>(entity =>
        {
            entity.HasKey(e => e.CertificateId).HasName("PK__DIAMONDC__BBF8A7E1293C79A6");

            entity.ToTable("DIAMONDCERTIFICATE");

            entity.Property(e => e.CertificateId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("CertificateID");
            entity.Property(e => e.CertificateNumber)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.IssueDate).HasColumnType("datetime");
            entity.Property(e => e.IssuedBy)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Product).WithMany(p => p.Diamondcertificates)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_DiamondCertificate_Diamond");
        });

        modelBuilder.Entity<Earring>(entity =>
        {
            entity.HasKey(e => e.EarringId).HasName("PK__EARRING__F43797088E44A0EB");

            entity.ToTable("EARRING");

            entity.Property(e => e.EarringId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("EarringID");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ImageURL");
            entity.Property(e => e.MetalType)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("money");
        });

        modelBuilder.Entity<Earringpairing>(entity =>
        {
            entity.HasKey(e => e.EProductId).HasName("PK__EARRINGP__41B11BB4AB056458");

            entity.ToTable("EARRINGPAIRING");

            entity.Property(e => e.EProductId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("E_ProductID");
            entity.Property(e => e.DiamondId).HasColumnName("DiamondID");
            entity.Property(e => e.EarringId).HasColumnName("EarringID");

            entity.HasOne(d => d.Diamond).WithMany(p => p.Earringpairings)
                .HasForeignKey(d => d.DiamondId)
                .HasConstraintName("FK__EARRINGPA__Diamo__5FF32EF8");

            entity.HasOne(d => d.Earring).WithMany(p => p.Earringpairings)
                .HasForeignKey(d => d.EarringId)
                .HasConstraintName("FK__EARRINGPA__Earri__5EFF0ABF");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__ORDERS__C3905BAF9E16ABA9");

            entity.ToTable("ORDERS");

            entity.Property(e => e.OrderId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("OrderID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.TotalAmount).HasColumnType("money");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__ORDERS__Customer__38D961D7");
        });

        modelBuilder.Entity<Orderitem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("PK__ORDERITE__57ED06A1D240267D");

            entity.ToTable("ORDERITEM");

            entity.Property(e => e.OrderItemId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("OrderItemID");
            entity.Property(e => e.DiamondId).HasColumnName("DiamondID");
            entity.Property(e => e.EarringPairingId).HasColumnName("EarringPairingID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.PendantPairingId).HasColumnName("PendantPairingID");
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.ProductType)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.RingPairingId).HasColumnName("RingPairingID");

            entity.HasOne(d => d.Diamond).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.DiamondId)
                .HasConstraintName("FK_Orderitem_Diamond");

            entity.HasOne(d => d.EarringPairing).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.EarringPairingId)
                .HasConstraintName("FK_Orderitem_Earringpairing");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__ORDERITEM__Order__3CA9F2BB");

            entity.HasOne(d => d.PendantPairing).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.PendantPairingId)
                .HasConstraintName("FK_Orderitem_Pendantpairing");

            entity.HasOne(d => d.RingPairing).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.RingPairingId)
                .HasConstraintName("FK_Orderitem_Ringpairing");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__PAYMENT__9B556A580D1B0F0E");

            entity.ToTable("PAYMENT");

            entity.HasIndex(e => e.OrderId, "UQ__PAYMENT__C3905BAE9C7C9251").IsUnique();

            entity.Property(e => e.PaymentId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("PaymentID");
            entity.Property(e => e.Amount).HasColumnType("money");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentMessage)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.Order).WithOne(p => p.Payment)
                .HasForeignKey<Payment>(d => d.OrderId)
                .HasConstraintName("FK__PAYMENT__OrderID__453F38BC");
        });

        modelBuilder.Entity<Pendant>(entity =>
        {
            entity.HasKey(e => e.PendantId).HasName("PK__PENDANT__3B25748DF8845219");

            entity.ToTable("PENDANT");

            entity.Property(e => e.PendantId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("PendantID");
            entity.Property(e => e.ChainLength)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ChainType)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ClaspType)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ImageURL");
            entity.Property(e => e.Name)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("money");
        });

        modelBuilder.Entity<Pendantpairing>(entity =>
        {
            entity.HasKey(e => e.PProductId).HasName("PK__PENDANTP__80786356D9CAA41A");

            entity.ToTable("PENDANTPAIRING");

            entity.Property(e => e.PProductId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("P_ProductID");
            entity.Property(e => e.DiamondId).HasColumnName("DiamondID");
            entity.Property(e => e.PendantId).HasColumnName("PendantID");

            entity.HasOne(d => d.Diamond).WithMany(p => p.Pendantpairings)
                .HasForeignKey(d => d.DiamondId)
                .HasConstraintName("FK__PENDANTPA__Diamo__64B7E415");

            entity.HasOne(d => d.Pendant).WithMany(p => p.Pendantpairings)
                .HasForeignKey(d => d.PendantId)
                .HasConstraintName("FK__PENDANTPA__Penda__63C3BFDC");
        });

        modelBuilder.Entity<Ring>(entity =>
        {
            entity.HasKey(e => e.RingId).HasName("PK__RING__F1D5904AB4490D99");

            entity.ToTable("RING");

            entity.Property(e => e.RingId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("RingID");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ImageURL");
            entity.Property(e => e.MetalType)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.RingSize)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Ringpairing>(entity =>
        {
            entity.HasKey(e => e.RProductId).HasName("PK__RINGPAIR__7BEB278CECEB97DB");

            entity.ToTable("RINGPAIRING");

            entity.Property(e => e.RProductId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("R_ProductID");
            entity.Property(e => e.DiamondId).HasColumnName("DiamondID");
            entity.Property(e => e.RingId).HasColumnName("RingID");

            entity.HasOne(d => d.Diamond).WithMany(p => p.Ringpairings)
                .HasForeignKey(d => d.DiamondId)
                .HasConstraintName("FK__RINGPAIRI__Diamo__5B2E79DB");

            entity.HasOne(d => d.Ring).WithMany(p => p.Ringpairings)
                .HasForeignKey(d => d.RingId)
                .HasConstraintName("FK__RINGPAIRI__RingI__5A3A55A2");
        });

        modelBuilder.Entity<Shippingaddress>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__SHIPPING__091C2A1BF0ABD0F7");

            entity.ToTable("SHIPPINGADDRESS");

            entity.Property(e => e.AddressId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("AddressID");
            entity.Property(e => e.AddressLine1)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.AddressLine2)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.District)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.Customer).WithMany(p => p.Shippingaddresses)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__SHIPPINGA__Custo__407A839F");
        });

        modelBuilder.Entity<Warranty>(entity =>
        {
            entity.HasKey(e => e.WarrantyId).HasName("PK__WARRANTY__2ED318F35A5A1B89");

            entity.ToTable("WARRANTY");

            entity.HasIndex(e => e.OrderItemId, "UQ__WARRANTY__57ED06A0889987AC").IsUnique();

            entity.Property(e => e.WarrantyId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("WarrantyID");
            entity.Property(e => e.OrderItemId).HasColumnName("OrderItemID");
            entity.Property(e => e.WarrantyEndDate).HasColumnType("datetime");
            entity.Property(e => e.WarrantyStartDate).HasColumnType("datetime");

            entity.HasOne(d => d.OrderItem).WithOne(p => p.Warranty)
                .HasForeignKey<Warranty>(d => d.OrderItemId)
                .HasConstraintName("FK__WARRANTY__OrderI__4A03EDD9");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
