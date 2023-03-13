using System;
using System.Collections.Generic;
using Context_aware_System.ContextDbModels;
using Microsoft.EntityFrameworkCore;

namespace Context_aware_System.Data;

public partial class ContextAwareDataBaseContext : DbContext
{
    public ContextAwareDataBaseContext()
    {
    }

    public ContextAwareDataBaseContext(DbContextOptions<ContextAwareDataBaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<NewProducedProductsInfo> NewProducedProductsInfos { get; set; }

    public virtual DbSet<StopsVerification> StopsVerifications { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-19JNKGR\\SQLEXPRESS;Database=ContextAwareDataBase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NewProducedProductsInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NewProdu__3214EC079F5C3797");

            entity.ToTable("NewProducedProductsInfo");

            entity.Property(e => e.VerificationDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<StopsVerification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StopsVer__3214EC071BDF28FE");

            entity.ToTable("StopsVerification");

            entity.Property(e => e.VerificationDate).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
