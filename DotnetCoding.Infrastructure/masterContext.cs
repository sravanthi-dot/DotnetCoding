using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DotnetCoding.Infrastructure
{
    public partial class masterContext : DbContext
    {
        public masterContext()
        {
        }

        public masterContext(DbContextOptions<masterContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MasProduct> MasProducts { get; set; } = null!;
        public virtual DbSet<QueueApproval> QueueApprovals { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=10.230.0.160;user=Sravanthi.P;password=Sravanthi@2022;database=master");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<MasProduct>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.ToTable("mas_product");

                entity.Property(e => e.ProductId).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ProductDescription).HasMaxLength(500);

                entity.Property(e => e.ProductName).HasMaxLength(200);

                entity.Property(e => e.ProductPrice).HasColumnType("numeric(18, 3)");
            });

            modelBuilder.Entity<QueueApproval>(entity =>
            {
                entity.HasKey(e => e.QueueId);

                entity.ToTable("queue_approval");

                entity.Property(e => e.QueueId).ValueGeneratedNever();

                entity.Property(e => e.ApprovedDate).HasColumnType("datetime");

                entity.Property(e => e.RequestedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.RequestedReason).HasMaxLength(100);

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
