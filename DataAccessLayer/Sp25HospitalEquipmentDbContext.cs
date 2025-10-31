using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace DataAccessLayer;

public partial class Sp25HospitalEquipmentDbContext : DbContext
{
    public Sp25HospitalEquipmentDbContext()
    {
    }

    public Sp25HospitalEquipmentDbContext(DbContextOptions<Sp25HospitalEquipmentDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EquipmentInformation> EquipmentInformations { get; set; }

    public virtual DbSet<StoreAccount> StoreAccounts { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();
        var strConn = config["ConnectionStrings:DefaultConnection"];

        return strConn;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var cs = GetConnectionString();
            if (!string.IsNullOrWhiteSpace(cs))
            {
                optionsBuilder.UseSqlServer(cs);
            }
        }
    }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Server=IPHONE\\MNAM;Uid=sa;Pwd=1234567890;Database=Sp25HospitalEquipmentDB;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EquipmentInformation>(entity =>
        {
            entity.HasKey(e => e.EquipmentId).HasName("PK__Equipmen__34474599C89FB147");

            entity.ToTable("EquipmentInformation");

            entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EquipmentName).HasMaxLength(100);
            entity.Property(e => e.LocationInHospital).HasMaxLength(200);
            entity.Property(e => e.MaintenanceInstructions).HasMaxLength(1000);
            entity.Property(e => e.ModelNumber).HasMaxLength(50);
            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.TechnicalSpecifications).HasMaxLength(500);

            entity.HasOne(d => d.Supplier).WithMany(p => p.EquipmentInformations)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Equipment__Suppl__2C3393D0");
        });

        modelBuilder.Entity<StoreAccount>(entity =>
        {
            entity.HasKey(e => e.StoreAccountId).HasName("PK__StoreAcc__42D52A6A5C5E012B");

            entity.ToTable("StoreAccount");

            entity.HasIndex(e => e.EmailAddress, "UQ__StoreAcc__49A14740FCD97CBD").IsUnique();

            entity.Property(e => e.StoreAccountId).HasColumnName("StoreAccountID");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Department).HasMaxLength(50);
            entity.Property(e => e.EmailAddress).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.StoreAccountPassword).HasMaxLength(100);
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK__Supplier__4BE66694526A1D39");

            entity.ToTable("Supplier");

            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.ContactEmail).HasMaxLength(100);
            entity.Property(e => e.ContactPerson).HasMaxLength(100);
            entity.Property(e => e.ContactPhone).HasMaxLength(20);
            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.IsPreferred).HasDefaultValue(false);
            entity.Property(e => e.SupplierName).HasMaxLength(100);
            entity.Property(e => e.WebsiteUrl)
                .HasMaxLength(200)
                .HasColumnName("WebsiteURL");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
