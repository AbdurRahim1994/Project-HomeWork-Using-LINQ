using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PeopleDeskHomeWorkUsingSQL.Models.Data.Entity;

namespace PeopleDeskHomeWorkUsingSQL.Models.Data
{
    public partial class HomeWorkDbContext : DbContext
    {
        public HomeWorkDbContext()
        {
        }

        public HomeWorkDbContext(DbContextOptions<HomeWorkDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblItem> TblItems { get; set; } = null!;
        public virtual DbSet<TblPartner> TblPartners { get; set; } = null!;
        public virtual DbSet<TblPartnerType> TblPartnerTypes { get; set; } = null!;
        public virtual DbSet<TblPurchase> TblPurchases { get; set; } = null!;
        public virtual DbSet<TblPurchaseDetail> TblPurchaseDetails { get; set; } = null!;
        public virtual DbSet<TblSale> TblSales { get; set; } = null!;
        public virtual DbSet<TblSalesDetail> TblSalesDetails { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source= DESKTOP-6QNOIP0;Initial Catalog=PeopleDeskHomeWork; Trusted_Connection =True; Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblItem>(entity =>
            {
                entity.HasKey(e => e.IntItemId);

                entity.ToTable("tblItem");

                entity.Property(e => e.IntItemId).HasColumnName("intItemId");

                entity.Property(e => e.DteCreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("dteCreatedAt")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DteUpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("dteUpdatedAt");

                entity.Property(e => e.IntCreatedBy).HasColumnName("intCreatedBy");

                entity.Property(e => e.IntUpdatedBy).HasColumnName("intUpdatedBy");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.NumStockPrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("numStockPrice");

                entity.Property(e => e.NumStockQuantity)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("numStockQuantity");

                entity.Property(e => e.NumTotalPrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("numTotalPrice");

                entity.Property(e => e.StrItemName)
                    .HasMaxLength(250)
                    .HasColumnName("strItemName");
            });

            modelBuilder.Entity<TblPartner>(entity =>
            {
                entity.HasKey(e => e.IntPartnerId);

                entity.ToTable("tblPartner");

                entity.Property(e => e.IntPartnerId).HasColumnName("intPartnerId");

                entity.Property(e => e.IntPartnerTypeId).HasColumnName("intPartnerTypeId");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.StrPartnerName)
                    .HasMaxLength(250)
                    .HasColumnName("strPartnerName");

                entity.Property(e => e.StrPartnerTypeName)
                    .HasMaxLength(250)
                    .HasColumnName("strPartnerTypeName");
            });

            modelBuilder.Entity<TblPartnerType>(entity =>
            {
                entity.HasKey(e => e.IntPartnerTypeId);

                entity.ToTable("tblPartnerType");

                entity.Property(e => e.IntPartnerTypeId).HasColumnName("intPartnerTypeId");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.StrPartnerTypeName)
                    .HasMaxLength(50)
                    .HasColumnName("strPartnerTypeName");
            });

            modelBuilder.Entity<TblPurchase>(entity =>
            {
                entity.HasKey(e => e.IntPurchaseId);

                entity.ToTable("tblPurchase");

                entity.Property(e => e.IntPurchaseId).HasColumnName("intPurchaseId");

                entity.Property(e => e.DtePurchaseDate)
                    .HasColumnType("datetime")
                    .HasColumnName("dtePurchaseDate");

                entity.Property(e => e.IntSupplierId).HasColumnName("intSupplierId");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.StrSupplierName)
                    .HasMaxLength(250)
                    .HasColumnName("strSupplierName");
            });

            modelBuilder.Entity<TblPurchaseDetail>(entity =>
            {
                entity.HasKey(e => e.IntPurchaseDetailsId);

                entity.ToTable("tblPurchaseDetails");

                entity.Property(e => e.IntPurchaseDetailsId).HasColumnName("intPurchaseDetailsId");

                entity.Property(e => e.DteCreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("dteCreatedAt")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DteUpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("dteUpdatedAt");

                entity.Property(e => e.IntCreatedBy).HasColumnName("intCreatedBy");

                entity.Property(e => e.IntItemId).HasColumnName("intItemId");

                entity.Property(e => e.IntPurchaseId).HasColumnName("intPurchaseId");

                entity.Property(e => e.IntUpdatedBy).HasColumnName("intUpdatedBy");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.NumItemQuantity)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("numItemQuantity");

                entity.Property(e => e.NumTotalPrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("numTotalPrice");

                entity.Property(e => e.NumUnitPrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("numUnitPrice");

                entity.Property(e => e.StrItemName)
                    .HasMaxLength(250)
                    .HasColumnName("strItemName");
            });

            modelBuilder.Entity<TblSale>(entity =>
            {
                entity.HasKey(e => e.IntSalesId);

                entity.ToTable("tblSales");

                entity.Property(e => e.IntSalesId).HasColumnName("intSalesId");

                entity.Property(e => e.DteSalesDate)
                    .HasColumnType("datetime")
                    .HasColumnName("dteSalesDate");

                entity.Property(e => e.IntCustomerId).HasColumnName("intCustomerId");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.StrCustomerName)
                    .HasMaxLength(250)
                    .HasColumnName("strCustomerName");
            });

            modelBuilder.Entity<TblSalesDetail>(entity =>
            {
                entity.HasKey(e => e.IntSalesDetailsId);

                entity.ToTable("tblSalesDetails");

                entity.Property(e => e.IntSalesDetailsId).HasColumnName("intSalesDetailsId");

                entity.Property(e => e.DteCreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("dteCreatedAt")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DteUpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("dteUpdatedAt");

                entity.Property(e => e.IntCreatedBy).HasColumnName("intCreatedBy");

                entity.Property(e => e.IntItemId).HasColumnName("intItemId");

                entity.Property(e => e.IntSalesId).HasColumnName("intSalesId");

                entity.Property(e => e.IntUpdatedBy).HasColumnName("intUpdatedBy");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.NumTotalPrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("numTotalPrice");

                entity.Property(e => e.NumUnitPrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("numUnitPrice");

                entity.Property(e => e.NumtemQuantity)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("numtemQuantity");

                entity.Property(e => e.StrItemName)
                    .HasMaxLength(250)
                    .HasColumnName("strItemName");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
