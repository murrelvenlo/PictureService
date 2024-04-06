using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PictureService.Domain.Models;

public partial class AMDSContext : DbContext
{
    public AMDSContext()
    {
    }

    public AMDSContext(DbContextOptions<AMDSContext> options)
        : base(options)
    {
    }

    public virtual DbSet<GenericMappingTable> GenericMappingTables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=192.168.11.176;Initial Catalog=AMDS;UID=sa;PWD=Q123456s;MultipleActiveResultSets=true;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GenericMappingTable>(entity =>
        {
            entity.HasKey(e => e.MappingId).HasName("PK__GenericM__8B57819DB4291453");

            entity.ToTable("GenericMappingTable");

            entity.Property(e => e.MappingId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAtUtc).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedAtUtc).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.TableName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
