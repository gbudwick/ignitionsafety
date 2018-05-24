using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using IS.Data.DbContexts;

namespace IS.Data.Migrations
{
    [DbContext(typeof(IsDbContext))]
    partial class IsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IS.Data.Model.Account", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AccountLocked");

                    b.Property<string>("AccountOwner");

                    b.Property<string>("Address1");

                    b.Property<string>("Address2");

                    b.Property<string>("City");

                    b.Property<string>("CompanyName");

                    b.Property<DateTime?>("DateCanceled");

                    b.Property<DateTime?>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<string>("PrimaryContactUserId");

                    b.Property<bool>("ReadOnly");

                    b.Property<string>("SecondaryContactUserId");

                    b.Property<string>("State");

                    b.Property<DateTime>("TrialPeriodExpiration");

                    b.Property<DateTime?>("TrialStartDate");

                    b.Property<string>("UserCreated");

                    b.Property<string>("UserModified");

                    b.Property<string>("ZipCode");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("IS.Data.Model.Department", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountId");

                    b.Property<DateTime?>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<string>("SafetyZoneId");

                    b.Property<string>("UserCreated");

                    b.Property<string>("UserModified");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("SafetyZoneId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("IS.Data.Model.EventLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedTime");

                    b.Property<int>("EventId");

                    b.Property<string>("LogLevel")
                        .HasMaxLength(50);

                    b.Property<string>("Message")
                        .HasMaxLength(4000);

                    b.HasKey("Id");

                    b.ToTable("EventLogs");
                });

            modelBuilder.Entity("IS.Data.Model.IgnitionUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("AccountId");

                    b.Property<bool?>("AccountIsLocked");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DisabledOn");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<bool?>("IsPropertyOwner");

                    b.Property<bool?>("IsRenter");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<DateTime?>("UpdatedOn");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("IS.Data.Model.MembersStatus", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<bool>("Locked");

                    b.Property<string>("MemberId");

                    b.Property<int>("Status");

                    b.Property<string>("UserCreated");

                    b.Property<string>("UserModified");

                    b.HasKey("id");

                    b.ToTable("MembersStatuses");
                });

            modelBuilder.Entity("IS.Data.Model.RosterMember", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountId");

                    b.Property<string>("ContactPhone");

                    b.Property<DateTime?>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<string>("DepartmentId");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("UserCreated");

                    b.Property<string>("UserModified");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("RosterMembers");
                });

            modelBuilder.Entity("IS.Data.Model.SafetyZone", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountId");

                    b.Property<DateTime?>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<string>("Name");

                    b.Property<string>("UserCreated");

                    b.Property<string>("UserModified");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("SafetyZones");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("IS.Data.Model.Department", b =>
                {
                    b.HasOne("IS.Data.Model.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");

                    b.HasOne("IS.Data.Model.SafetyZone", "SafetyZone")
                        .WithMany()
                        .HasForeignKey("SafetyZoneId");
                });

            modelBuilder.Entity("IS.Data.Model.RosterMember", b =>
                {
                    b.HasOne("IS.Data.Model.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");

                    b.HasOne("IS.Data.Model.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId");
                });

            modelBuilder.Entity("IS.Data.Model.SafetyZone", b =>
                {
                    b.HasOne("IS.Data.Model.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("IS.Data.Model.IgnitionUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("IS.Data.Model.IgnitionUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("IS.Data.Model.IgnitionUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
