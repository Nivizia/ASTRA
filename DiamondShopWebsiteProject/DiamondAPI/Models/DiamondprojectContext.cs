using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DiamondAPI.Models;

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

    public virtual DbSet<Refundproduct> Refundproducts { get; set; }

    public virtual DbSet<Ring> Rings { get; set; }

    public virtual DbSet<Ringpairing> Ringpairings { get; set; }

    public virtual DbSet<Shippingaddress> Shippingaddresses { get; set; }

    public virtual DbSet<Warranty> Warranties { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=(local); database=DIAMONDPROJECT;uid=sa;pwd=123456;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__CUSTOMER__A4AE64B8C0209EBB");

            entity.ToTable("CUSTOMER");

            entity.Property(e => e.CustomerId)
                .ValueGeneratedNever()
                .HasColumnName("CustomerID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Diamond>(entity =>
        {
            entity.HasKey(e => e.DProductId).HasName("PK__DIAMOND__C72A404623631777");

            entity.ToTable("DIAMOND");

            entity.Property(e => e.DProductId)
                .ValueGeneratedNever()
                .HasColumnName("D_ProductID");
            entity.Property(e => e.CaratWeight)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Clarity)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Color)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Cut)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.DType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("D_Type");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ImageURL");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("money");
        });

        modelBuilder.Entity<Diamondcertificate>(entity =>
        {
            entity.HasKey(e => e.CertificateId).HasName("PK__DIAMONDC__BBF8A7E1E2514849");

            entity.ToTable("DIAMONDCERTIFICATE");

            entity.Property(e => e.CertificateId)
                .ValueGeneratedNever()
                .HasColumnName("CertificateID");
            entity.Property(e => e.CertificateNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IssueDate).HasColumnType("datetime");
            entity.Property(e => e.IssuedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Product).WithMany(p => p.Diamondcertificates)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_DiamondCertificate_Diamond");

            entity.HasOne(d => d.ProductNavigation).WithMany(p => p.Diamondcertificates)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_DiamondCertificate_Refundproduct");
        });

        modelBuilder.Entity<Earring>(entity =>
        {
            entity.HasKey(e => e.EarringId).HasName("PK__EARRING__F4379708E6E65AB5");

            entity.ToTable("EARRING");

            entity.Property(e => e.EarringId)
                .ValueGeneratedNever()
                .HasColumnName("EarringID");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ImageURL");
            entity.Property(e => e.MetalType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("money");
        });

        modelBuilder.Entity<Earringpairing>(entity =>
        {
            entity.HasKey(e => e.EProductId).HasName("PK__EARRINGP__41B11BB46D1C6B7A");

            entity.ToTable("EARRINGPAIRING");

            entity.Property(e => e.EProductId)
                .ValueGeneratedNever()
                .HasColumnName("E_ProductID");
            entity.Property(e => e.DiamondId).HasColumnName("DiamondID");
            entity.Property(e => e.EarringId).HasColumnName("EarringID");

            entity.HasOne(d => d.Diamond).WithMany(p => p.Earringpairings)
                .HasForeignKey(d => d.DiamondId)
                .HasConstraintName("FK__EARRINGPA__Diamo__72C60C4A");

            entity.HasOne(d => d.Earring).WithMany(p => p.Earringpairings)
                .HasForeignKey(d => d.EarringId)
                .HasConstraintName("FK__EARRINGPA__Earri__71D1E811");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__ORDERS__C3905BAF1DE1DAB2");

            entity.ToTable("ORDERS");

            entity.Property(e => e.OrderId)
                .ValueGeneratedNever()
                .HasColumnName("OrderID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TotalAmount).HasColumnType("money");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__ORDERS__Customer__5629CD9C");
        });

        modelBuilder.Entity<Orderitem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("PK__ORDERITE__57ED06A1066C7D11");

            entity.ToTable("ORDERITEM");

            entity.Property(e => e.OrderItemId)
                .ValueGeneratedNever()
                .HasColumnName("OrderItemID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ProductType)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Order).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__ORDERITEM__Order__59063A47");

            entity.HasOne(d => d.Product).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Orderitem_Diamond");

            entity.HasOne(d => d.ProductNavigation).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Orderitem_Earringpairing");

            entity.HasOne(d => d.Product1).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Orderitem_Pendantpairing");

            entity.HasOne(d => d.Product2).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Orderitem_Ringpairing");

            entity.HasOne(d => d.Product3).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Orderitem_Refundproduct");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__PAYMENT__9B556A580EA76A2D");

            entity.ToTable("PAYMENT");

            entity.HasIndex(e => e.OrderId, "UQ__PAYMENT__C3905BAE822DB03C").IsUnique();

            entity.Property(e => e.PaymentId)
                .ValueGeneratedNever()
                .HasColumnName("PaymentID");
            entity.Property(e => e.Amount).HasColumnType("money");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentMessage)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Order).WithOne(p => p.Payment)
                .HasForeignKey<Payment>(d => d.OrderId)
                .HasConstraintName("FK__PAYMENT__OrderID__5FB337D6");
        });

        modelBuilder.Entity<Pendant>(entity =>
        {
            entity.HasKey(e => e.PendantId).HasName("PK__PENDANT__3B25748DC1807224");

            entity.ToTable("PENDANT");

            entity.Property(e => e.PendantId)
                .ValueGeneratedNever()
                .HasColumnName("PendantID");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ImageURL");
            entity.Property(e => e.MetalType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("money");
        });

        modelBuilder.Entity<Pendantpairing>(entity =>
        {
            entity.HasKey(e => e.PProductId).HasName("PK__PENDANTP__80786356DC4AB3EA");

            entity.ToTable("PENDANTPAIRING");

            entity.Property(e => e.PProductId)
                .ValueGeneratedNever()
                .HasColumnName("P_ProductID");
            entity.Property(e => e.DiamondId).HasColumnName("DiamondID");
            entity.Property(e => e.PendantId).HasColumnName("PendantID");

            entity.HasOne(d => d.Diamond).WithMany(p => p.Pendantpairings)
                .HasForeignKey(d => d.DiamondId)
                .HasConstraintName("FK__PENDANTPA__Diamo__76969D2E");

            entity.HasOne(d => d.Pendant).WithMany(p => p.Pendantpairings)
                .HasForeignKey(d => d.PendantId)
                .HasConstraintName("FK__PENDANTPA__Penda__75A278F5");
        });

        modelBuilder.Entity<Refundproduct>(entity =>
        {
            entity.HasKey(e => e.RfProductId).HasName("PK__REFUNDPR__6C9C8D1E83EB1E63");

            entity.ToTable("REFUNDPRODUCT");

            entity.Property(e => e.RfProductId)
                .ValueGeneratedNever()
                .HasColumnName("RF_ProductID");
            entity.Property(e => e.CaratWeight)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Clarity)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Color)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Cut)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ImageURL");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.Specifications).HasColumnType("text");
        });

        modelBuilder.Entity<Ring>(entity =>
        {
            entity.HasKey(e => e.RingId).HasName("PK__RING__F1D5904AE9CCDFFB");

            entity.ToTable("RING");

            entity.Property(e => e.RingId)
                .ValueGeneratedNever()
                .HasColumnName("RingID");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ImageURL");
            entity.Property(e => e.MetalType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.RingSize)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Ringpairing>(entity =>
        {
            entity.HasKey(e => e.RProductId).HasName("PK__RINGPAIR__7BEB278CE7B50DCA");

            entity.ToTable("RINGPAIRING");

            entity.Property(e => e.RProductId)
                .ValueGeneratedNever()
                .HasColumnName("R_ProductID");
            entity.Property(e => e.DiamondId).HasColumnName("DiamondID");
            entity.Property(e => e.RingId).HasColumnName("RingID");

            entity.HasOne(d => d.Diamond).WithMany(p => p.Ringpairings)
                .HasForeignKey(d => d.DiamondId)
                .HasConstraintName("FK__RINGPAIRI__Diamo__6EF57B66");

            entity.HasOne(d => d.Ring).WithMany(p => p.Ringpairings)
                .HasForeignKey(d => d.RingId)
                .HasConstraintName("FK__RINGPAIRI__RingI__6E01572D");
        });

        modelBuilder.Entity<Shippingaddress>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__SHIPPING__091C2A1B0FED06FE");

            entity.ToTable("SHIPPINGADDRESS");

            entity.Property(e => e.AddressId)
                .ValueGeneratedNever()
                .HasColumnName("AddressID");
            entity.Property(e => e.AddressLine1)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.AddressLine2)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.District)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Customer).WithMany(p => p.Shippingaddresses)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__SHIPPINGA__Custo__5BE2A6F2");
        });

        modelBuilder.Entity<Warranty>(entity =>
        {
            entity.HasKey(e => e.WarrantyId).HasName("PK__WARRANTY__2ED318F3FD0DD6CF");

            entity.ToTable("WARRANTY");

            entity.HasIndex(e => e.OrderItemId, "UQ__WARRANTY__57ED06A09AEF1E2C").IsUnique();

            entity.Property(e => e.WarrantyId)
                .ValueGeneratedNever()
                .HasColumnName("WarrantyID");
            entity.Property(e => e.OrderItemId).HasColumnName("OrderItemID");
            entity.Property(e => e.WarrantyEndDate).HasColumnType("datetime");
            entity.Property(e => e.WarrantyStartDate).HasColumnType("datetime");

            entity.HasOne(d => d.OrderItem).WithOne(p => p.Warranty)
                .HasForeignKey<Warranty>(d => d.OrderItemId)
                .HasConstraintName("FK__WARRANTY__OrderI__6383C8BA");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
