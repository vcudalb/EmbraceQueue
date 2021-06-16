using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace EmbraceQueue.Infrastructure.Entities
{
    public partial class EmbraceQueueDbContext : IdentityDbContext<User, Role, string>
    {
        public EmbraceQueueDbContext(DbContextOptions<EmbraceQueueDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<DigitalTicket> DigitalTickets { get; set; }
        public virtual DbSet<WorkingDay> WorkingDays { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<ServiceLine> ServiceLines { get; set; }
        public virtual DbSet<ServicesServiceLine> ServicesServiceLines { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            modelBuilder.Entity<Role>().ToTable("Roles").HasData
            (
                new Role { Id = Guid.NewGuid().ToString(), Name = "enduser", NormalizedName = "ENDUSER" },
                new Role { Id = Guid.NewGuid().ToString(), Name = "helpdeskemployee", NormalizedName = "HELPDESKEMPLOYEE" },
                new Role { Id = Guid.NewGuid().ToString(), Name = "branchmanager", NormalizedName = "BRANCHMANAGER" },
                new Role { Id = Guid.NewGuid().ToString(), Name = "superadmin", NormalizedName = "SUPERADMIN" }
            );

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.PhoneNumberSubmissionDateTime).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.ServiceFinishDateTime).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.HasOne(d => d.DigitalTicket)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.DigitalTicketId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<DigitalTicket>(entity =>
            {
                entity.HasOne(d => d.Company)
                    .WithMany(p => p.DigitalTickets)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ServiceLine>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.LastIncrementedDateTime).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.ServiceLines)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ServicesServiceLine>(entity =>
            {
                entity.HasKey(e => new { e.ServiceId, e.ServiceLineId });

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ServicesServiceLines)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
