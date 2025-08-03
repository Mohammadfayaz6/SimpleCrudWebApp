using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SimpleCrudWebApp.Read;

namespace SimpleCrudWebApp.Models;

public partial class OrgDbContext : DbContext
{
    public OrgDbContext()
    {
    }

    public OrgDbContext(DbContextOptions<OrgDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<SalaryDetail> SalaryDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Organization;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SalaryDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SalaryDe__3214EC0721A80355");

            entity.HasOne(d => d.Employee).WithMany(p => p.SalaryDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SalaryDetails_Employees");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
