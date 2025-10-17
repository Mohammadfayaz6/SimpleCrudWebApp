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
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Try to read the connection string from environment variable first (used in deployment)
            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

            // If not found, fallback to local SQL Express for development
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = "Server=localhost\\SQLEXPRESS;Database=Organization;Trusted_Connection=True;TrustServerCertificate=True;";
            }

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
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
