using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace api.Models
{
    public partial class dbTenantAdminContext : DbContext
    {
        private IConfiguration _config;

        public dbTenantAdminContext(IConfiguration configuration)
        {
            _config = configuration;
        }

        public dbTenantAdminContext(DbContextOptions<dbTenantAdminContext> options, IConfiguration configuration)
            : base(options)
        {
            _config = configuration;
        }

        public virtual DbSet<Tenant> Tenants { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_config["sqlConnectionString"]);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tenant>(entity =>
            {
                entity.Property(e => e.TenantId)
                    .HasColumnName("tenant_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.TenantAadtentantid)
                    .HasMaxLength(50)
                    .HasColumnName("tenant_aadtentantid");

                entity.Property(e => e.TenantDbname)
                    .HasMaxLength(50)
                    .HasColumnName("tenant_dbname");

                entity.Property(e => e.TenantDbserver)
                    .HasMaxLength(50)
                    .HasColumnName("tenant_dbserver");

                entity.Property(e => e.TenantName)
                    .HasMaxLength(50)
                    .HasColumnName("tenant_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
