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

    public virtual DbSet<AggregatedCounter> AggregatedCounters { get; set; }

    public virtual DbSet<Counter> Counters { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Diamond> Diamonds { get; set; }

    public virtual DbSet<Diamondcertificate> Diamondcertificates { get; set; }

    public virtual DbSet<Earring> Earrings { get; set; }

    public virtual DbSet<Earringpairing> Earringpairings { get; set; }

    public virtual DbSet<Frametype> Frametypes { get; set; }

    public virtual DbSet<Hash> Hashes { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<JobParameter> JobParameters { get; set; }

    public virtual DbSet<JobQueue> JobQueues { get; set; }

    public virtual DbSet<List> Lists { get; set; }

    public virtual DbSet<Metaltype> Metaltypes { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Orderitem> Orderitems { get; set; }

    public virtual DbSet<Pendant> Pendants { get; set; }

    public virtual DbSet<Pendantpairing> Pendantpairings { get; set; }

    public virtual DbSet<Ring> Rings { get; set; }

    public virtual DbSet<Ringpairing> Ringpairings { get; set; }

    public virtual DbSet<Ringshapedetail> Ringshapedetails { get; set; }

    public virtual DbSet<Ringsubtype> Ringsubtypes { get; set; }

    public virtual DbSet<Ringtype> Ringtypes { get; set; }

    public virtual DbSet<Schema> Schemas { get; set; }

    public virtual DbSet<Server> Servers { get; set; }

    public virtual DbSet<Set> Sets { get; set; }

    public virtual DbSet<Shape> Shapes { get; set; }

    public virtual DbSet<Specialfeature> Specialfeatures { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<Stonecut> Stonecuts { get; set; }

    public virtual DbSet<VnpaymentRequest> VnpaymentRequests { get; set; }

    public virtual DbSet<VnpaymentResponse> VnpaymentResponses { get; set; }

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
        modelBuilder.Entity<AggregatedCounter>(entity =>
        {
            entity.HasKey(e => e.Key).HasName("PK_HangFire_CounterAggregated");

            entity.ToTable("AggregatedCounter", "HangFire");

            entity.HasIndex(e => e.ExpireAt, "IX_HangFire_AggregatedCounter_ExpireAt").HasFilter("([ExpireAt] IS NOT NULL)");

            entity.Property(e => e.Key).HasMaxLength(100);
            entity.Property(e => e.ExpireAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Counter>(entity =>
        {
            entity.HasKey(e => new { e.Key, e.Id }).HasName("PK_HangFire_Counter");

            entity.ToTable("Counter", "HangFire");

            entity.Property(e => e.Key).HasMaxLength(100);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ExpireAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__CUSTOMER__A4AE64B85811DFD5");

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
            entity.HasKey(e => e.DProductId).HasName("PK__DIAMOND__C72A4046927FD7F1");

            entity.ToTable("DIAMOND");

            entity.Property(e => e.DProductId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("D_ProductID");
            entity.Property(e => e.Available).HasDefaultValue(true);
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ImageURL");
            entity.Property(e => e.ShapeId).HasColumnName("ShapeID");

            entity.HasOne(d => d.Shape).WithMany(p => p.Diamonds)
                .HasForeignKey(d => d.ShapeId)
                .HasConstraintName("FK__DIAMOND__ShapeID__34956655");
        });

        modelBuilder.Entity<Diamondcertificate>(entity =>
        {
            entity.HasKey(e => e.CertificateId).HasName("PK__DIAMONDC__BBF8A7E119AD0C81");

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
            entity.HasKey(e => e.EarringId).HasName("PK__EARRING__F4379708E14F21E9");

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
            entity.HasKey(e => e.EProductId).HasName("PK__EARRINGP__41B11BB462278CE3");

            entity.ToTable("EARRINGPAIRING");

            entity.Property(e => e.EProductId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("E_ProductID");
            entity.Property(e => e.DiamondId).HasColumnName("DiamondID");
            entity.Property(e => e.EarringId).HasColumnName("EarringID");

            entity.HasOne(d => d.Diamond).WithMany(p => p.Earringpairings)
                .HasForeignKey(d => d.DiamondId)
                .HasConstraintName("FK__EARRINGPA__Diamo__6350553E");

            entity.HasOne(d => d.Earring).WithMany(p => p.Earringpairings)
                .HasForeignKey(d => d.EarringId)
                .HasConstraintName("FK__EARRINGPA__Earri__625C3105");
        });

        modelBuilder.Entity<Frametype>(entity =>
        {
            entity.HasKey(e => e.FrameTypeId).HasName("PK__FRAMETYP__7B3206B54FB453D3");

            entity.ToTable("FRAMETYPE");

            entity.Property(e => e.FrameTypeId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("FrameTypeID");
            entity.Property(e => e.FrameTypeName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Hash>(entity =>
        {
            entity.HasKey(e => new { e.Key, e.Field }).HasName("PK_HangFire_Hash");

            entity.ToTable("Hash", "HangFire");

            entity.HasIndex(e => e.ExpireAt, "IX_HangFire_Hash_ExpireAt").HasFilter("([ExpireAt] IS NOT NULL)");

            entity.Property(e => e.Key).HasMaxLength(100);
            entity.Property(e => e.Field).HasMaxLength(100);
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_HangFire_Job");

            entity.ToTable("Job", "HangFire");

            entity.HasIndex(e => e.ExpireAt, "IX_HangFire_Job_ExpireAt").HasFilter("([ExpireAt] IS NOT NULL)");

            entity.HasIndex(e => e.StateName, "IX_HangFire_Job_StateName").HasFilter("([StateName] IS NOT NULL)");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            entity.Property(e => e.StateName).HasMaxLength(20);
        });

        modelBuilder.Entity<JobParameter>(entity =>
        {
            entity.HasKey(e => new { e.JobId, e.Name }).HasName("PK_HangFire_JobParameter");

            entity.ToTable("JobParameter", "HangFire");

            entity.Property(e => e.Name).HasMaxLength(40);

            entity.HasOne(d => d.Job).WithMany(p => p.JobParameters)
                .HasForeignKey(d => d.JobId)
                .HasConstraintName("FK_HangFire_JobParameter_Job");
        });

        modelBuilder.Entity<JobQueue>(entity =>
        {
            entity.HasKey(e => new { e.Queue, e.Id }).HasName("PK_HangFire_JobQueue");

            entity.ToTable("JobQueue", "HangFire");

            entity.Property(e => e.Queue).HasMaxLength(50);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.FetchedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<List>(entity =>
        {
            entity.HasKey(e => new { e.Key, e.Id }).HasName("PK_HangFire_List");

            entity.ToTable("List", "HangFire");

            entity.HasIndex(e => e.ExpireAt, "IX_HangFire_List_ExpireAt").HasFilter("([ExpireAt] IS NOT NULL)");

            entity.Property(e => e.Key).HasMaxLength(100);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ExpireAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Metaltype>(entity =>
        {
            entity.HasKey(e => e.MetalTypeId).HasName("PK__METALTYP__3D2E4D7EA5B0676A");

            entity.ToTable("METALTYPE");

            entity.Property(e => e.MetalTypeId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("MetalTypeID");
            entity.Property(e => e.MetalTypeName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__ORDERS__C3905BAF6B307F8D");

            entity.ToTable("ORDERS");

            entity.Property(e => e.OrderId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("OrderID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.OrderEmail)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.OrderFirstName)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.OrderLastName)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.OrderPhone)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasDefaultValue("Payment Pending");
            entity.Property(e => e.TotalAmount).HasColumnType("money");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__ORDERS__Customer__255322C5");
        });

        modelBuilder.Entity<Orderitem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("PK__ORDERITE__57ED06A18C20C0F7");

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
                .HasConstraintName("FK__ORDERITEM__Order__6BE59B3F");

            entity.HasOne(d => d.PendantPairing).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.PendantPairingId)
                .HasConstraintName("FK_Orderitem_Pendantpairing");

            entity.HasOne(d => d.RingPairing).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.RingPairingId)
                .HasConstraintName("FK_Orderitem_Ringpairing");
        });

        modelBuilder.Entity<Pendant>(entity =>
        {
            entity.HasKey(e => e.PendantId).HasName("PK__PENDANT__3B25748DDB84EFB5");

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
            entity.HasKey(e => e.PProductId).HasName("PK__PENDANTP__8078635679D05CBC");

            entity.ToTable("PENDANTPAIRING");

            entity.Property(e => e.PProductId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("P_ProductID");
            entity.Property(e => e.DiamondId).HasColumnName("DiamondID");
            entity.Property(e => e.PendantId).HasColumnName("PendantID");

            entity.HasOne(d => d.Diamond).WithMany(p => p.Pendantpairings)
                .HasForeignKey(d => d.DiamondId)
                .HasConstraintName("FK__PENDANTPA__Diamo__68150A5B");

            entity.HasOne(d => d.Pendant).WithMany(p => p.Pendantpairings)
                .HasForeignKey(d => d.PendantId)
                .HasConstraintName("FK__PENDANTPA__Penda__6720E622");
        });

        modelBuilder.Entity<Ring>(entity =>
        {
            entity.HasKey(e => e.RingId).HasName("PK__RING__F1D5904A5D38207F");

            entity.ToTable("RING");

            entity.Property(e => e.RingId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("RingID");
            entity.Property(e => e.FrameTypeId).HasColumnName("FrameTypeID");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ImageURL");
            entity.Property(e => e.MetalTypeId).HasColumnName("MetalTypeID");
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.RingName)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.RingSize)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.RingSubtypeId).HasColumnName("RingSubtypeID");
            entity.Property(e => e.RingTypeId).HasColumnName("RingTypeID");
            entity.Property(e => e.SpecialFeatureId).HasColumnName("SpecialFeatureID");
            entity.Property(e => e.StoneCutId).HasColumnName("StoneCutID");

            entity.HasOne(d => d.FrameType).WithMany(p => p.Rings)
                .HasForeignKey(d => d.FrameTypeId)
                .HasConstraintName("FK__RING__FrameTypeI__4C6CEFE6");

            entity.HasOne(d => d.MetalType).WithMany(p => p.Rings)
                .HasForeignKey(d => d.MetalTypeId)
                .HasConstraintName("FK__RING__MetalTypeI__4D61141F");

            entity.HasOne(d => d.RingSubtype).WithMany(p => p.Rings)
                .HasForeignKey(d => d.RingSubtypeId)
                .HasConstraintName("FK__RING__RingSubtyp__4B78CBAD");

            entity.HasOne(d => d.RingType).WithMany(p => p.Rings)
                .HasForeignKey(d => d.RingTypeId)
                .HasConstraintName("FK__RING__RingTypeID__4A84A774");

            entity.HasOne(d => d.SpecialFeature).WithMany(p => p.Rings)
                .HasForeignKey(d => d.SpecialFeatureId)
                .HasConstraintName("FK__RING__SpecialFea__4F495C91");

            entity.HasOne(d => d.StoneCut).WithMany(p => p.Rings)
                .HasForeignKey(d => d.StoneCutId)
                .HasConstraintName("FK__RING__StoneCutID__4E553858");
        });

        modelBuilder.Entity<Ringpairing>(entity =>
        {
            entity.HasKey(e => e.RProductId).HasName("PK__RINGPAIR__7BEB278C3BB68DDD");

            entity.ToTable("RINGPAIRING");

            entity.Property(e => e.RProductId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("R_ProductID");
            entity.Property(e => e.DiamondId).HasColumnName("DiamondID");
            entity.Property(e => e.RingId).HasColumnName("RingID");

            entity.HasOne(d => d.Diamond).WithMany(p => p.Ringpairings)
                .HasForeignKey(d => d.DiamondId)
                .HasConstraintName("FK__RINGPAIRI__Diamo__5E8BA021");

            entity.HasOne(d => d.Ring).WithMany(p => p.Ringpairings)
                .HasForeignKey(d => d.RingId)
                .HasConstraintName("FK__RINGPAIRI__RingI__5D977BE8");
        });

        modelBuilder.Entity<Ringshapedetail>(entity =>
        {
            entity.HasKey(e => e.RingShapeDetailId).HasName("PK__RINGSHAP__C587A0CFB84E6EE3");

            entity.ToTable("RINGSHAPEDETAIL");

            entity.Property(e => e.RingShapeDetailId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("RingShapeDetailID");
            entity.Property(e => e.FrameDescription)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ImageURL");
            entity.Property(e => e.RingId).HasColumnName("RingID");
            entity.Property(e => e.ShapeId).HasColumnName("ShapeID");

            entity.HasOne(d => d.Ring).WithMany(p => p.Ringshapedetails)
                .HasForeignKey(d => d.RingId)
                .HasConstraintName("FK__RINGSHAPE__RingI__5319ED75");

            entity.HasOne(d => d.Shape).WithMany(p => p.Ringshapedetails)
                .HasForeignKey(d => d.ShapeId)
                .HasConstraintName("FK__RINGSHAPE__Shape__540E11AE");
        });

        modelBuilder.Entity<Ringsubtype>(entity =>
        {
            entity.HasKey(e => e.RingSubtypeId).HasName("PK__RINGSUBT__14E527DE19E3E913");

            entity.ToTable("RINGSUBTYPE");

            entity.Property(e => e.RingSubtypeId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("RingSubtypeID");
            entity.Property(e => e.RingTypeId).HasColumnName("RingTypeID");
            entity.Property(e => e.SubtypeName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.RingType).WithMany(p => p.Ringsubtypes)
                .HasForeignKey(d => d.RingTypeId)
                .HasConstraintName("FK__RINGSUBTY__RingT__3B4263E4");
        });

        modelBuilder.Entity<Ringtype>(entity =>
        {
            entity.HasKey(e => e.RingTypeId).HasName("PK__RINGTYPE__CE375EDE40688FA5");

            entity.ToTable("RINGTYPE");

            entity.Property(e => e.RingTypeId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("RingTypeID");
            entity.Property(e => e.TypeName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Schema>(entity =>
        {
            entity.HasKey(e => e.Version).HasName("PK_HangFire_Schema");

            entity.ToTable("Schema", "HangFire");

            entity.Property(e => e.Version).ValueGeneratedNever();
        });

        modelBuilder.Entity<Server>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_HangFire_Server");

            entity.ToTable("Server", "HangFire");

            entity.HasIndex(e => e.LastHeartbeat, "IX_HangFire_Server_LastHeartbeat");

            entity.Property(e => e.Id).HasMaxLength(200);
            entity.Property(e => e.LastHeartbeat).HasColumnType("datetime");
        });

        modelBuilder.Entity<Set>(entity =>
        {
            entity.HasKey(e => new { e.Key, e.Value }).HasName("PK_HangFire_Set");

            entity.ToTable("Set", "HangFire");

            entity.HasIndex(e => e.ExpireAt, "IX_HangFire_Set_ExpireAt").HasFilter("([ExpireAt] IS NOT NULL)");

            entity.HasIndex(e => new { e.Key, e.Score }, "IX_HangFire_Set_Score");

            entity.Property(e => e.Key).HasMaxLength(100);
            entity.Property(e => e.Value).HasMaxLength(256);
            entity.Property(e => e.ExpireAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Shape>(entity =>
        {
            entity.HasKey(e => e.ShapeId).HasName("PK__SHAPE__70FC83A16F5440FD");

            entity.ToTable("SHAPE");

            entity.Property(e => e.ShapeId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ShapeID");
            entity.Property(e => e.ShapeName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Specialfeature>(entity =>
        {
            entity.HasKey(e => e.SpecialFeatureId).HasName("PK__SPECIALF__C84AA8FC1FAA5819");

            entity.ToTable("SPECIALFEATURE");

            entity.Property(e => e.SpecialFeatureId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("SpecialFeatureID");
            entity.Property(e => e.FeatureDescription)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => new { e.JobId, e.Id }).HasName("PK_HangFire_State");

            entity.ToTable("State", "HangFire");

            entity.HasIndex(e => e.CreatedAt, "IX_HangFire_State_CreatedAt");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.Reason).HasMaxLength(100);

            entity.HasOne(d => d.Job).WithMany(p => p.States)
                .HasForeignKey(d => d.JobId)
                .HasConstraintName("FK_HangFire_State_Job");
        });

        modelBuilder.Entity<Stonecut>(entity =>
        {
            entity.HasKey(e => e.StoneCutId).HasName("PK__STONECUT__D8082D0F8DB87821");

            entity.ToTable("STONECUT");

            entity.Property(e => e.StoneCutId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("StoneCutID");
            entity.Property(e => e.StoneCutName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VnpaymentRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__VNPaymen__33A8517A7BBFF0A9");

            entity.ToTable("VNPaymentRequest");

            entity.Property(e => e.RequestId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Amount).HasColumnType("money");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");

            entity.HasOne(d => d.Order).WithMany(p => p.VnpaymentRequests)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VNPayment__Order__2923B3A9");
        });

        modelBuilder.Entity<VnpaymentResponse>(entity =>
        {
            entity.HasKey(e => e.ResponseId).HasName("PK__VNPaymen__1AAA646C1C1432F1");

            entity.ToTable("VNPaymentResponse");

            entity.Property(e => e.ResponseId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Amount).HasColumnType("money");
            entity.Property(e => e.BankCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BankTransactionNumber)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CardType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.OrderInfo)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");

            entity.HasOne(d => d.Order).WithMany(p => p.VnpaymentResponses)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VNPayment__Order__2CF4448D");
        });

        modelBuilder.Entity<Warranty>(entity =>
        {
            entity.HasKey(e => e.WarrantyId).HasName("PK__WARRANTY__2ED318F358032381");

            entity.ToTable("WARRANTY");

            entity.HasIndex(e => e.OrderItemId, "UQ__WARRANTY__57ED06A0323654E3").IsUnique();

            entity.Property(e => e.WarrantyId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("WarrantyID");
            entity.Property(e => e.OrderItemId).HasColumnName("OrderItemID");
            entity.Property(e => e.WarrantyEndDate).HasColumnType("datetime");
            entity.Property(e => e.WarrantyStartDate).HasColumnType("datetime");

            entity.HasOne(d => d.OrderItem).WithOne(p => p.Warranty)
                .HasForeignKey<Warranty>(d => d.OrderItemId)
                .HasConstraintName("FK__WARRANTY__OrderI__784B7224");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
