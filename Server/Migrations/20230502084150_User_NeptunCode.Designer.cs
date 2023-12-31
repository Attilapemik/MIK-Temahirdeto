﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PannonBlazor.Server.Data;

#nullable disable

namespace PannonBlazor.Server.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230502084150_User_NeptunCode")]
    partial class User_NeptunCode
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PannonBlazor.Shared.Models.Entity.Approval", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Approvals");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Jóváhagyásra vár"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Jóváhagyva"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Elutasítva"
                        });
                });

            modelBuilder.Entity("PannonBlazor.Shared.Models.Entity.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("PannonBlazor.Shared.Models.Entity.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("PannonBlazor.Shared.Models.Entity.Programme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Programmes");
                });

            modelBuilder.Entity("PannonBlazor.Shared.Models.Entity.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DisplayName = "Oktató",
                            Name = "Instructor"
                        },
                        new
                        {
                            Id = 2,
                            DisplayName = "Szakfelelős",
                            Name = "Programme Leader"
                        },
                        new
                        {
                            Id = 3,
                            DisplayName = "Tanszéki admin",
                            Name = "Head of department"
                        },
                        new
                        {
                            Id = 4,
                            DisplayName = "Admin",
                            Name = "Admin"
                        });
                });

            modelBuilder.Entity("PannonBlazor.Shared.Models.Entity.Semester", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVisibleInstructor")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVisibleStudent")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Semesters");
                });

            modelBuilder.Entity("PannonBlazor.Shared.Models.Entity.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Statuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Aktív"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Inaktív"
                        });
                });

            modelBuilder.Entity("PannonBlazor.Shared.Models.Entity.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("NeptunCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ProgrammeId")
                        .HasColumnType("int");

                    b.Property<int>("SemesterId")
                        .HasColumnType("int");

                    b.Property<string>("StudentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ThemeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProgrammeId");

                    b.HasIndex("ThemeId");

                    b.HasIndex("SemesterId", "ProgrammeId", "NeptunCode")
                        .IsUnique();

                    b.ToTable("Students");
                });

            modelBuilder.Entity("PannonBlazor.Shared.Models.Entity.Theme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ExternalCompanyId")
                        .HasColumnType("int");

                    b.Property<string>("ExternalInstructorCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExternalInstructorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InstructorId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDual")
                        .HasColumnType("bit");

                    b.Property<bool>("IsExternal")
                        .HasColumnType("bit");

                    b.Property<int?>("SourceThemeId")
                        .HasColumnType("int");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<string>("Student")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("StudentProgrammeId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExternalCompanyId");

                    b.HasIndex("InstructorId");

                    b.HasIndex("SourceThemeId");

                    b.HasIndex("StatusId");

                    b.HasIndex("StudentProgrammeId");

                    b.ToTable("Themes");
                });

            modelBuilder.Entity("PannonBlazor.Shared.Models.Entity.ThemeProgramme", b =>
                {
                    b.Property<int>("ThemeId")
                        .HasColumnType("int");

                    b.Property<int>("ProgrammeId")
                        .HasColumnType("int");

                    b.Property<int>("SemesterId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ApprovalDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ApprovalId")
                        .HasColumnType("int");

                    b.Property<string>("DenyReason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ThemeId", "ProgrammeId", "SemesterId");

                    b.HasIndex("ApprovalId");

                    b.HasIndex("ProgrammeId");

                    b.HasIndex("SemesterId");

                    b.ToTable("ThemeProgrammes");
                });

            modelBuilder.Entity("PannonBlazor.Shared.Models.Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LdapUid")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NeptunCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<int?>("ProgrammeId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("ProgrammeId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.Property<int>("RolesId")
                        .HasColumnType("int");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("RolesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("RoleUser");
                });

            modelBuilder.Entity("PannonBlazor.Shared.Models.Entity.Student", b =>
                {
                    b.HasOne("PannonBlazor.Shared.Models.Entity.Programme", "Programme")
                        .WithMany()
                        .HasForeignKey("ProgrammeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PannonBlazor.Shared.Models.Entity.Semester", "Semester")
                        .WithMany()
                        .HasForeignKey("SemesterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PannonBlazor.Shared.Models.Entity.Theme", "Theme")
                        .WithMany("Students")
                        .HasForeignKey("ThemeId");

                    b.Navigation("Programme");

                    b.Navigation("Semester");

                    b.Navigation("Theme");
                });

            modelBuilder.Entity("PannonBlazor.Shared.Models.Entity.Theme", b =>
                {
                    b.HasOne("PannonBlazor.Shared.Models.Entity.Company", "ExternalCompany")
                        .WithMany()
                        .HasForeignKey("ExternalCompanyId");

                    b.HasOne("PannonBlazor.Shared.Models.Entity.User", "Instructor")
                        .WithMany("Themes")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PannonBlazor.Shared.Models.Entity.Theme", "SourceTheme")
                        .WithMany()
                        .HasForeignKey("SourceThemeId");

                    b.HasOne("PannonBlazor.Shared.Models.Entity.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PannonBlazor.Shared.Models.Entity.Programme", "StudentProgramme")
                        .WithMany()
                        .HasForeignKey("StudentProgrammeId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("ExternalCompany");

                    b.Navigation("Instructor");

                    b.Navigation("SourceTheme");

                    b.Navigation("Status");

                    b.Navigation("StudentProgramme");
                });

            modelBuilder.Entity("PannonBlazor.Shared.Models.Entity.ThemeProgramme", b =>
                {
                    b.HasOne("PannonBlazor.Shared.Models.Entity.Approval", "Approval")
                        .WithMany()
                        .HasForeignKey("ApprovalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PannonBlazor.Shared.Models.Entity.Programme", "Programme")
                        .WithMany("ThemeProgrammes")
                        .HasForeignKey("ProgrammeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PannonBlazor.Shared.Models.Entity.Semester", "Semester")
                        .WithMany("ThemeProgrammes")
                        .HasForeignKey("SemesterId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PannonBlazor.Shared.Models.Entity.Theme", "Theme")
                        .WithMany("ThemeProgrammes")
                        .HasForeignKey("ThemeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Approval");

                    b.Navigation("Programme");

                    b.Navigation("Semester");

                    b.Navigation("Theme");
                });

            modelBuilder.Entity("PannonBlazor.Shared.Models.Entity.User", b =>
                {
                    b.HasOne("PannonBlazor.Shared.Models.Entity.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId");

                    b.HasOne("PannonBlazor.Shared.Models.Entity.Programme", "Programme")
                        .WithMany("Users")
                        .HasForeignKey("ProgrammeId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Department");

                    b.Navigation("Programme");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.HasOne("PannonBlazor.Shared.Models.Entity.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PannonBlazor.Shared.Models.Entity.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PannonBlazor.Shared.Models.Entity.Programme", b =>
                {
                    b.Navigation("ThemeProgrammes");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("PannonBlazor.Shared.Models.Entity.Semester", b =>
                {
                    b.Navigation("ThemeProgrammes");
                });

            modelBuilder.Entity("PannonBlazor.Shared.Models.Entity.Theme", b =>
                {
                    b.Navigation("Students");

                    b.Navigation("ThemeProgrammes");
                });

            modelBuilder.Entity("PannonBlazor.Shared.Models.Entity.User", b =>
                {
                    b.Navigation("Themes");
                });
#pragma warning restore 612, 618
        }
    }
}
