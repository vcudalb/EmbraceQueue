﻿// <auto-generated />
using System;
using EmbraceQueue.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EmbraceQueue.Infrastructure.Migrations
{
    [DbContext(typeof(EmbraceQueueDbContext))]
    [Migration("20210424205145_FixedBranchManagerRoleName")]
    partial class FixedBranchManagerRoleName
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EmbraceQueue.Infrastructure.Entities.Branch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("WaitingTimeInSeconds")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("WorkDayEndTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("WorkDayStartTime")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "CompanyId" }, "IX_Branches_CompanyId");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("EmbraceQueue.Infrastructure.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("EmbraceQueue.Infrastructure.Entities.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "CategoryId" }, "IX_Companies_CategoryId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("EmbraceQueue.Infrastructure.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CustomerPhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DigitalTicketId")
                        .HasColumnType("int");

                    b.Property<bool>("HasReceivedSmsreminder")
                        .HasColumnType("bit")
                        .HasColumnName("HasReceivedSMSReminder");

                    b.Property<bool>("HasReceivedSmsticket")
                        .HasColumnType("bit")
                        .HasColumnName("HasReceivedSMSTicket");

                    b.Property<bool>("HasShownUpAndGotServed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("PhoneNumberSubmissionDateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                    b.Property<string>("SequentialNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ServiceFinishDateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                    b.Property<int>("ServiceLineId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "DigitalTicketId" }, "IX_Customers_DigitalTicketId");

                    b.HasIndex(new[] { "ServiceLineId" }, "IX_Customers_ServiceLineId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("EmbraceQueue.Infrastructure.Entities.DigitalTicket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("MessageTemplateOne")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MessageTemplateTwo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NotificationsNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "CompanyId" }, "IX_DigitalTickets_CompanyId");

                    b.ToTable("DigitalTickets");
                });

            modelBuilder.Entity("EmbraceQueue.Infrastructure.Entities.Holiday", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.Property<string>("Day")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "BranchId" }, "IX_Holidays_BranchId");

                    b.ToTable("Holidays");
                });

            modelBuilder.Entity("EmbraceQueue.Infrastructure.Entities.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Area")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.Property<int?>("Building")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mall")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NearbyLandmark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "BranchId" }, "IX_Locations_BranchId")
                        .IsUnique();

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("EmbraceQueue.Infrastructure.Entities.Role", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = "494ba0e8-d1bc-4fd9-a0a3-441ea76d7626",
                            ConcurrencyStamp = "2d14e171-f06a-4fb1-93a9-78beeb5604dc",
                            Name = "enduser",
                            NormalizedName = "ENDUSER"
                        },
                        new
                        {
                            Id = "fbeb44bb-43b5-498e-9741-ed30036355d8",
                            ConcurrencyStamp = "14cafbba-28b1-4820-88fb-c3d2994f4dac",
                            Name = "helpdeskemployee",
                            NormalizedName = "HELPDESKEMPLOYEE"
                        },
                        new
                        {
                            Id = "6ef1f52f-febd-45b8-8cc5-71b54884d32c",
                            ConcurrencyStamp = "c0b60502-5026-45ce-b934-78eee60564ba",
                            Name = "branchmanager",
                            NormalizedName = "BRANCHMANAGER"
                        },
                        new
                        {
                            Id = "818a1cd7-0337-4b3c-9bcb-0ed2840d133d",
                            ConcurrencyStamp = "d314885e-c5db-4fac-a622-b1496c84bcf9",
                            Name = "superadmin",
                            NormalizedName = "SUPERADMIN"
                        });
                });

            modelBuilder.Entity("EmbraceQueue.Infrastructure.Entities.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<int>("RecentIncrementedLineId")
                        .HasColumnType("int");

                    b.Property<string>("ServiceType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "CompanyId" }, "IX_Services_CompanyId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("EmbraceQueue.Infrastructure.Entities.ServiceLine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.Property<string>("CounterNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                    b.Property<int>("CurrentQueueStatus")
                        .HasColumnType("int");

                    b.Property<int>("CurrentSequentialNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastIncrementedDateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                    b.Property<int>("PeopleGotInLineCounter")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "BranchId" }, "IX_ServiceLines_BranchId");

                    b.ToTable("ServiceLines");
                });

            modelBuilder.Entity("EmbraceQueue.Infrastructure.Entities.ServicesServiceLine", b =>
                {
                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.Property<int>("ServiceLineId")
                        .HasColumnType("int");

                    b.HasKey("ServiceId", "ServiceLineId");

                    b.HasIndex(new[] { "ServiceLineId" }, "IX_ServicesServiceLines_ServiceLineId");

                    b.ToTable("ServicesServiceLines");
                });

            modelBuilder.Entity("EmbraceQueue.Infrastructure.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsSubscribed")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("EmbraceQueue.Infrastructure.Entities.Branch", b =>
                {
                    b.HasOne("EmbraceQueue.Infrastructure.Entities.Company", "Company")
                        .WithMany("Branches")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("EmbraceQueue.Infrastructure.Entities.Company", b =>
                {
                    b.HasOne("EmbraceQueue.Infrastructure.Entities.Category", "Category")
                        .WithMany("Companies")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("EmbraceQueue.Infrastructure.Entities.Customer", b =>
                {
                    b.HasOne("EmbraceQueue.Infrastructure.Entities.DigitalTicket", "DigitalTicket")
                        .WithMany("Customers")
                        .HasForeignKey("DigitalTicketId")
                        .IsRequired();

                    b.HasOne("EmbraceQueue.Infrastructure.Entities.ServiceLine", "ServiceLine")
                        .WithMany("Customers")
                        .HasForeignKey("ServiceLineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DigitalTicket");

                    b.Navigation("ServiceLine");
                });

            modelBuilder.Entity("EmbraceQueue.Infrastructure.Entities.DigitalTicket", b =>
                {
                    b.HasOne("EmbraceQueue.Infrastructure.Entities.Company", "Company")
                        .WithMany("DigitalTickets")
                        .HasForeignKey("CompanyId")
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("EmbraceQueue.Infrastructure.Entities.Holiday", b =>
                {
                    b.HasOne("EmbraceQueue.Infrastructure.Entities.Branch", "Branch")
                        .WithMany("Holidays")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");
                });

            modelBuilder.Entity("EmbraceQueue.Infrastructure.Entities.Location", b =>
                {
                    b.HasOne("EmbraceQueue.Infrastructure.Entities.Branch", "Branch")
                        .WithOne("Location")
                        .HasForeignKey("EmbraceQueue.Infrastructure.Entities.Location", "BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");
                });

            modelBuilder.Entity("EmbraceQueue.Infrastructure.Entities.Service", b =>
                {
                    b.HasOne("EmbraceQueue.Infrastructure.Entities.Company", "Company")
                        .WithMany("Services")
                        .HasForeignKey("CompanyId")
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("EmbraceQueue.Infrastructure.Entities.ServiceLine", b =>
                {
                    b.HasOne("EmbraceQueue.Infrastructure.Entities.Branch", "Branch")
                        .WithMany("ServiceLines")
                        .HasForeignKey("BranchId")
                        .IsRequired();

                    b.Navigation("Branch");
                });

            modelBuilder.Entity("EmbraceQueue.Infrastructure.Entities.ServicesServiceLine", b =>
                {
                    b.HasOne("EmbraceQueue.Infrastructure.Entities.Service", "Service")
                        .WithMany("ServicesServiceLines")
                        .HasForeignKey("ServiceId")
                        .IsRequired();

                    b.HasOne("EmbraceQueue.Infrastructure.Entities.ServiceLine", "ServiceLine")
                        .WithMany("ServicesServiceLines")
                        .HasForeignKey("ServiceLineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Service");

                    b.Navigation("ServiceLine");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("EmbraceQueue.Infrastructure.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("EmbraceQueue.Infrastructure.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("EmbraceQueue.Infrastructure.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("EmbraceQueue.Infrastructure.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmbraceQueue.Infrastructure.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("EmbraceQueue.Infrastructure.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EmbraceQueue.Infrastructure.Entities.Branch", b =>
                {
                    b.Navigation("Holidays");

                    b.Navigation("Location");

                    b.Navigation("ServiceLines");
                });

            modelBuilder.Entity("EmbraceQueue.Infrastructure.Entities.Category", b =>
                {
                    b.Navigation("Companies");
                });

            modelBuilder.Entity("EmbraceQueue.Infrastructure.Entities.Company", b =>
                {
                    b.Navigation("Branches");

                    b.Navigation("DigitalTickets");

                    b.Navigation("Services");
                });

            modelBuilder.Entity("EmbraceQueue.Infrastructure.Entities.DigitalTicket", b =>
                {
                    b.Navigation("Customers");
                });

            modelBuilder.Entity("EmbraceQueue.Infrastructure.Entities.Service", b =>
                {
                    b.Navigation("ServicesServiceLines");
                });

            modelBuilder.Entity("EmbraceQueue.Infrastructure.Entities.ServiceLine", b =>
                {
                    b.Navigation("Customers");

                    b.Navigation("ServicesServiceLines");
                });
#pragma warning restore 612, 618
        }
    }
}
