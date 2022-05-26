using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Orders500.Models
{
    public partial class OrdersDBContext : DbContext
    {
        public OrdersDBContext()
        {
        }

        public OrdersDBContext(DbContextOptions<OrdersDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CdTable> CdTables { get; set; } = null!;
        public virtual DbSet<OrdersTable> OrdersTables { get; set; } = null!;
        public virtual DbSet<SalesPersonTable> SalesPersonTables { get; set; } = null!;
        public virtual DbSet<StoreTable> StoreTables { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=MHOME\\DEVSQL;Database=OrdersDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CdTable>(entity =>
            {
                entity.HasKey(e => e.CdId);

                entity.ToTable("CdTable");

                entity.Property(e => e.CdId).HasColumnName("CdID");

                entity.Property(e => e.Artist).HasMaxLength(260);

                entity.Property(e => e.Cdname)
                    .HasMaxLength(260)
                    .HasColumnName("CDname");
            });

            modelBuilder.Entity<OrdersTable>(entity =>
            {
                entity.HasKey(e => e.OrdersId);

                entity.ToTable("OrdersTable");

                entity.Property(e => e.OrdersId).HasColumnName("OrdersID");

                entity.Property(e => e.CdId).HasColumnName("CdID");

                entity.Property(e => e.Date).HasMaxLength(50);

                entity.Property(e => e.SalesPersonId).HasColumnName("SalesPersonID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.HasOne(d => d.Cd)
                    .WithMany(p => p.OrdersTables)
                    .HasForeignKey(d => d.CdId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrdersTable_CdTable");

                entity.HasOne(d => d.SalesPerson)
                    .WithMany(p => p.OrdersTables)
                    .HasForeignKey(d => d.SalesPersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrdersTable_SalesPersonTable");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.OrdersTables)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrdersTable_StoreTable");
            });

            modelBuilder.Entity<SalesPersonTable>(entity =>
            {
                entity.HasKey(e => e.SalesPersonId);

                entity.ToTable("SalesPersonTable");

                entity.Property(e => e.SalesPersonId).HasColumnName("SalesPersonID");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.SalesPersonTables)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SalesPersonTable_StoreTable");
            });

            modelBuilder.Entity<StoreTable>(entity =>
            {
                entity.HasKey(e => e.StoreId);

                entity.ToTable("StoreTable");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.State).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
