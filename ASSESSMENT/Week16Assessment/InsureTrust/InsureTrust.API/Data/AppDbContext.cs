using Microsoft.EntityFrameworkCore;
using InsureTrust.API.Models;

namespace InsureTrust.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<PolicyType> PolicyTypes { get; set; }
    public DbSet<PolicyTerm> PolicyTerms { get; set; }
    public DbSet<PolicyRequiredField> PolicyRequiredFields { get; set; }
    public DbSet<UserPolicy> UserPolicies { get; set; }
    public DbSet<Claim> Claims { get; set; }
    public DbSet<SupportQuery> SupportQueries { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Payment> Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ──────────────────────────────────────────────────────────────
        // User – Fluent API configuration
        // ──────────────────────────────────────────────────────────────
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(u => u.Id);
            entity.Property(u => u.UserNumber).IsRequired().HasMaxLength(20);
            entity.Property(u => u.Name).IsRequired().HasMaxLength(100);
            entity.Property(u => u.Email).IsRequired().HasMaxLength(150);
            entity.Property(u => u.PasswordHash).IsRequired();
            entity.Property(u => u.PhoneNo).HasMaxLength(15);
            entity.Property(u => u.PanCard).HasMaxLength(10);
            entity.Property(u => u.KycDocumentPath).HasMaxLength(500);
            entity.Property(u => u.KycStatus).HasMaxLength(20).HasDefaultValue("Pending");
            entity.Property(u => u.Role).HasMaxLength(20).HasDefaultValue("Customer");
            entity.Property(u => u.Balance).HasPrecision(18, 2).HasDefaultValue(0m);
            entity.Property(u => u.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

            entity.HasIndex(u => u.Email).IsUnique();
            entity.HasIndex(u => u.UserNumber).IsUnique();

            // Relationships
            entity.HasMany(u => u.UserPolicies).WithOne(up => up.User)
                  .HasForeignKey(up => up.UserId).OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(u => u.SupportQueries).WithOne(sq => sq.User)
                  .HasForeignKey(sq => sq.UserId).OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(u => u.Notifications).WithOne(n => n.User)
                  .HasForeignKey(n => n.UserId).OnDelete(DeleteBehavior.Cascade);
        });

        // ──────────────────────────────────────────────────────────────
        // PolicyType – Fluent API configuration
        // ──────────────────────────────────────────────────────────────
        modelBuilder.Entity<PolicyType>(entity =>
        {
            entity.ToTable("PolicyTypes");
            entity.HasKey(pt => pt.Id);
            entity.Property(pt => pt.Name).IsRequired().HasMaxLength(100);
            entity.Property(pt => pt.Category).IsRequired().HasMaxLength(50);
            entity.Property(pt => pt.Description).HasMaxLength(500);
            entity.Property(pt => pt.Icon).HasMaxLength(50);
            entity.Property(pt => pt.CoverageDetails).HasMaxLength(1000);
            entity.Property(pt => pt.IsActive).HasDefaultValue(true);
            entity.Property(pt => pt.BaseMonthlyPremium).HasPrecision(18, 2).HasDefaultValue(5000m);
            entity.Property(pt => pt.MinTenureMonths).HasDefaultValue(12);
            entity.Property(pt => pt.MaxTenureMonths).HasDefaultValue(120);
            entity.Property(pt => pt.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

            entity.HasMany(pt => pt.PolicyTerms).WithOne(t => t.PolicyType)
                  .HasForeignKey(t => t.PolicyTypeId).OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(pt => pt.RequiredFields).WithOne(f => f.PolicyType)
                  .HasForeignKey(f => f.PolicyTypeId).OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(pt => pt.UserPolicies).WithOne(up => up.PolicyType)
                  .HasForeignKey(up => up.PolicyTypeId).OnDelete(DeleteBehavior.Restrict);
        });

        // ──────────────────────────────────────────────────────────────
        // PolicyTerm – Fluent API configuration
        // ──────────────────────────────────────────────────────────────
        modelBuilder.Entity<PolicyTerm>(entity =>
        {
            entity.ToTable("PolicyTerms");
            entity.HasKey(t => t.Id);
            entity.Property(t => t.TermText).IsRequired().HasMaxLength(1000);
        });

        // ──────────────────────────────────────────────────────────────
        // PolicyRequiredField – Fluent API configuration
        // ──────────────────────────────────────────────────────────────
        modelBuilder.Entity<PolicyRequiredField>(entity =>
        {
            entity.ToTable("PolicyRequiredFields");
            entity.HasKey(f => f.Id);
            entity.Property(f => f.FieldName).IsRequired().HasMaxLength(100);
            entity.Property(f => f.FieldType).HasMaxLength(20).HasDefaultValue("text");
            entity.Property(f => f.IsMandatory).HasDefaultValue(true);
        });

        // ──────────────────────────────────────────────────────────────
        // UserPolicy – Fluent API configuration
        // ──────────────────────────────────────────────────────────────
        modelBuilder.Entity<UserPolicy>(entity =>
        {
            entity.ToTable("UserPolicies");
            entity.HasKey(up => up.Id);
            entity.Property(up => up.PolicyNumber).IsRequired().HasMaxLength(20);
            entity.Property(up => up.Status).HasMaxLength(20).HasDefaultValue("Pending");
            entity.Property(up => up.PackageAmount).HasPrecision(18, 2);
            entity.Property(up => up.AdminRemarks).HasMaxLength(500);
            entity.Property(up => up.DynamicFieldsJson).HasColumnType("nvarchar(max)").HasDefaultValue("{}");

            entity.HasIndex(up => up.PolicyNumber).IsUnique();

            entity.HasMany(up => up.Claims).WithOne(c => c.UserPolicy)
                  .HasForeignKey(c => c.UserPolicyId).OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(up => up.Payments).WithOne(p => p.UserPolicy)
                  .HasForeignKey(p => p.UserPolicyId).OnDelete(DeleteBehavior.Cascade);
        });

        // ──────────────────────────────────────────────────────────────
        // Claim – Fluent API configuration
        // ──────────────────────────────────────────────────────────────
        modelBuilder.Entity<Claim>(entity =>
        {
            entity.ToTable("Claims");
            entity.HasKey(c => c.Id);
            entity.Property(c => c.ClaimNumber).IsRequired().HasMaxLength(20);
            entity.Property(c => c.ClaimStatus).HasMaxLength(20).HasDefaultValue("Pending");
            entity.Property(c => c.MaturityAmount).HasPrecision(18, 2).HasDefaultValue(0m);
            entity.Property(c => c.AdminRemarks).HasMaxLength(500);
            entity.Property(c => c.DocumentsSubmitted).HasColumnType("nvarchar(max)").HasDefaultValue("[]");

            entity.HasIndex(c => c.ClaimNumber).IsUnique();
        });

        // ──────────────────────────────────────────────────────────────
        // SupportQuery – Fluent API configuration
        // ──────────────────────────────────────────────────────────────
        modelBuilder.Entity<SupportQuery>(entity =>
        {
            entity.ToTable("SupportQueries");
            entity.HasKey(s => s.Id);
            entity.Property(s => s.TicketNumber).IsRequired().HasMaxLength(20);
            entity.Property(s => s.Subject).IsRequired().HasMaxLength(200);
            entity.Property(s => s.Description).HasMaxLength(2000);
            entity.Property(s => s.AttachmentPath).HasMaxLength(500);
            entity.Property(s => s.Status).HasMaxLength(20).HasDefaultValue("Pending");
            entity.Property(s => s.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            entity.Property(s => s.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");

            entity.HasIndex(s => s.TicketNumber).IsUnique();
        });

        // ──────────────────────────────────────────────────────────────
        // Notification – Fluent API configuration
        // ──────────────────────────────────────────────────────────────
        modelBuilder.Entity<Notification>(entity =>
        {
            entity.ToTable("Notifications");
            entity.HasKey(n => n.Id);
            entity.Property(n => n.Title).IsRequired().HasMaxLength(150);
            entity.Property(n => n.Message).HasMaxLength(1000);
            entity.Property(n => n.ColorCode).HasMaxLength(20).HasDefaultValue("Blue");
            entity.Property(n => n.RelatedFeature).HasMaxLength(50);
            entity.Property(n => n.IsRead).HasDefaultValue(false);
            entity.Property(n => n.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        });

        // ──────────────────────────────────────────────────────────────
        // Payment – Fluent API configuration
        // ──────────────────────────────────────────────────────────────
        modelBuilder.Entity<Payment>(entity =>
        {
            entity.ToTable("Payments");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.PaymentNumber).IsRequired().HasMaxLength(20);
            entity.Property(p => p.Amount).HasPrecision(18, 2);
            entity.Property(p => p.PaymentMethod).HasMaxLength(50);
            entity.Property(p => p.Status).HasMaxLength(20).HasDefaultValue("Success");

            entity.HasIndex(p => p.PaymentNumber).IsUnique();
        });

        // ──────────────────────────────────────────────────────────────
        //  Seed Data
        // ──────────────────────────────────────────────────────────────
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = 1,
            UserNumber = "ADMIN001",
            Name = "System Admin",
            Email = "admin@insuretrust.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
            Role = "Admin",
            PhoneNo = "9999999999",
            PanCard = "ADMIN1234A",
            KycStatus = "Verified",
            Balance = 0,
            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        });

        modelBuilder.Entity<PolicyType>().HasData(
            new PolicyType { Id = 1, Name = "Term Life", Category = "Personal", Icon = "shield", Description = "Comprehensive life coverage protecting your family's financial future.", CoverageDetails = "Death benefit, Terminal illness rider, Accidental death benefit", BaseMonthlyPremium = 5000, MinTenureMonths = 12, MaxTenureMonths = 360, IsActive = true, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new PolicyType { Id = 2, Name = "Health", Category = "Personal", Icon = "heart", Description = "Complete medical coverage for hospitalization, surgeries, and outpatient care.", CoverageDetails = "Hospitalization, Day care procedures, Pre & post hospitalization", BaseMonthlyPremium = 3000, MinTenureMonths = 12, MaxTenureMonths = 60, IsActive = true, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new PolicyType { Id = 3, Name = "Vehicle", Category = "Personal", Icon = "car", Description = "Full coverage for cars and bikes against accidents, theft, and damage.", CoverageDetails = "Own damage, Third party liability, Personal accident cover", BaseMonthlyPremium = 2000, MinTenureMonths = 12, MaxTenureMonths = 36, IsActive = true, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new PolicyType { Id = 4, Name = "Home", Category = "Personal", Icon = "home", Description = "Protect your home and belongings against fire, flood, and burglary.", CoverageDetails = "Structure damage, Contents, Liability protection", BaseMonthlyPremium = 1500, MinTenureMonths = 12, MaxTenureMonths = 120, IsActive = true, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new PolicyType { Id = 5, Name = "Property", Category = "Business", Icon = "building", Description = "Commercial property insurance for businesses and investment properties.", CoverageDetails = "Building damage, Business interruption, Liability", BaseMonthlyPremium = 8000, MinTenureMonths = 12, MaxTenureMonths = 120, IsActive = true, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new PolicyType { Id = 6, Name = "Employee Group Benefits", Category = "Business", Icon = "users", Description = "Group insurance plans covering all employees under a single policy.", CoverageDetails = "Group health, Group life, Disability coverage", BaseMonthlyPremium = 15000, MinTenureMonths = 12, MaxTenureMonths = 60, IsActive = true, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new PolicyType { Id = 7, Name = "Engineering", Category = "Business", Icon = "settings", Description = "Specialized coverage for construction projects, equipment, and machinery.", CoverageDetails = "Equipment breakdown, Contractors risk, Erection all risks", BaseMonthlyPremium = 12000, MinTenureMonths = 12, MaxTenureMonths = 60, IsActive = true, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
        );

        modelBuilder.Entity<PolicyTerm>().HasData(
            new PolicyTerm { Id = 1, PolicyTypeId = 1, TermText = "Policy is valid for the tenure period specified at purchase." },
            new PolicyTerm { Id = 2, PolicyTypeId = 1, TermText = "Premium must be paid monthly without grace period exceeding 30 days." },
            new PolicyTerm { Id = 3, PolicyTypeId = 1, TermText = "Death claims require submission within 90 days of the event." },
            new PolicyTerm { Id = 4, PolicyTypeId = 2, TermText = "Pre-existing diseases are covered after a 2-year waiting period." },
            new PolicyTerm { Id = 5, PolicyTypeId = 2, TermText = "Cashless treatment available at 5000+ network hospitals." },
            new PolicyTerm { Id = 6, PolicyTypeId = 3, TermText = "Vehicle must be registered in India and in roadworthy condition." },
            new PolicyTerm { Id = 7, PolicyTypeId = 3, TermText = "Claims must be filed within 7 days of the incident." }
        );

        modelBuilder.Entity<PolicyRequiredField>().HasData(
            new PolicyRequiredField { Id = 1, PolicyTypeId = 1, FieldName = "Nominee Name", FieldType = "text", IsMandatory = true },
            new PolicyRequiredField { Id = 2, PolicyTypeId = 1, FieldName = "Nominee Relation", FieldType = "text", IsMandatory = true },
            new PolicyRequiredField { Id = 3, PolicyTypeId = 1, FieldName = "Date of Birth", FieldType = "date", IsMandatory = true },
            new PolicyRequiredField { Id = 4, PolicyTypeId = 2, FieldName = "Date of Birth", FieldType = "date", IsMandatory = true },
            new PolicyRequiredField { Id = 5, PolicyTypeId = 2, FieldName = "Existing Medical Conditions", FieldType = "text", IsMandatory = false },
            new PolicyRequiredField { Id = 6, PolicyTypeId = 3, FieldName = "Vehicle Registration Number", FieldType = "text", IsMandatory = true },
            new PolicyRequiredField { Id = 7, PolicyTypeId = 3, FieldName = "Vehicle Make & Model", FieldType = "text", IsMandatory = true },
            new PolicyRequiredField { Id = 8, PolicyTypeId = 4, FieldName = "Property Address", FieldType = "text", IsMandatory = true },
            new PolicyRequiredField { Id = 9, PolicyTypeId = 4, FieldName = "Property Value (₹)", FieldType = "text", IsMandatory = true }
        );
    }
}
