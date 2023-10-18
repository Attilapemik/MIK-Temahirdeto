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
    [Migration("20221019135920_Theme_IsDual")]
    partial class Theme_IsDual
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PannonBlazor.Shared.Approval", b =>
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

            modelBuilder.Entity("PannonBlazor.Shared.Department", b =>
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

            modelBuilder.Entity("PannonBlazor.Shared.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "This is the first product",
                            ImageUrl = "https://m.blog.hu/me/mepninja/image/product-development.png",
                            Price = 9.99m,
                            Title = "First Product"
                        },
                        new
                        {
                            Id = 2,
                            Description = "This is the second product",
                            ImageUrl = "https://i.ytimg.com/vi/7SSu0KtXI2c/maxresdefault.jpg",
                            Price = 7.99m,
                            Title = "Second Product"
                        },
                        new
                        {
                            Id = 3,
                            Description = "This is the third product",
                            ImageUrl = "https://marketingmix.co.uk/content/uploads/product.png",
                            Price = 6.99m,
                            Title = "Third Product"
                        });
                });

            modelBuilder.Entity("PannonBlazor.Shared.Programme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Programmes");
                });

            modelBuilder.Entity("PannonBlazor.Shared.Role", b =>
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

            modelBuilder.Entity("PannonBlazor.Shared.Semester", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVisibleInstructor")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVisibleStudent")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Semesters");
                });

            modelBuilder.Entity("PannonBlazor.Shared.Status", b =>
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

            modelBuilder.Entity("PannonBlazor.Shared.Theme", b =>
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

                    b.HasIndex("InstructorId");

                    b.HasIndex("StatusId");

                    b.HasIndex("StudentProgrammeId");

                    b.ToTable("Themes");
                });

            modelBuilder.Entity("PannonBlazor.Shared.ThemeProgramme", b =>
                {
                    b.Property<int>("ThemeId")
                        .HasColumnType("int");

                    b.Property<int>("ProgrammeId")
                        .HasColumnType("int");

                    b.Property<int>("ApprovalId")
                        .HasColumnType("int");

                    b.Property<int>("SemesterId")
                        .HasColumnType("int");

                    b.HasKey("ThemeId", "ProgrammeId");

                    b.HasIndex("ApprovalId");

                    b.HasIndex("ProgrammeId");

                    b.HasIndex("SemesterId");

                    b.ToTable("ThemeProgrammes");
                });

            modelBuilder.Entity("PannonBlazor.Shared.User", b =>
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

            modelBuilder.Entity("PannonBlazor.Shared.Theme", b =>
                {
                    b.HasOne("PannonBlazor.Shared.User", "Instructor")
                        .WithMany("Themes")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PannonBlazor.Shared.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PannonBlazor.Shared.Programme", "StudentProgramme")
                        .WithMany()
                        .HasForeignKey("StudentProgrammeId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Instructor");

                    b.Navigation("Status");

                    b.Navigation("StudentProgramme");
                });

            modelBuilder.Entity("PannonBlazor.Shared.ThemeProgramme", b =>
                {
                    b.HasOne("PannonBlazor.Shared.Approval", "Approval")
                        .WithMany()
                        .HasForeignKey("ApprovalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PannonBlazor.Shared.Programme", "Programme")
                        .WithMany("ThemeProgrammes")
                        .HasForeignKey("ProgrammeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PannonBlazor.Shared.Semester", "Semester")
                        .WithMany("ThemeProgrammes")
                        .HasForeignKey("SemesterId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PannonBlazor.Shared.Theme", "Theme")
                        .WithMany("ThemeProgrammes")
                        .HasForeignKey("ThemeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Approval");

                    b.Navigation("Programme");

                    b.Navigation("Semester");

                    b.Navigation("Theme");
                });

            modelBuilder.Entity("PannonBlazor.Shared.User", b =>
                {
                    b.HasOne("PannonBlazor.Shared.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId");

                    b.HasOne("PannonBlazor.Shared.Programme", "Programme")
                        .WithMany("Users")
                        .HasForeignKey("ProgrammeId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Department");

                    b.Navigation("Programme");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.HasOne("PannonBlazor.Shared.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PannonBlazor.Shared.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PannonBlazor.Shared.Programme", b =>
                {
                    b.Navigation("ThemeProgrammes");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("PannonBlazor.Shared.Semester", b =>
                {
                    b.Navigation("ThemeProgrammes");
                });

            modelBuilder.Entity("PannonBlazor.Shared.Theme", b =>
                {
                    b.Navigation("ThemeProgrammes");
                });

            modelBuilder.Entity("PannonBlazor.Shared.User", b =>
                {
                    b.Navigation("Themes");
                });
#pragma warning restore 612, 618
        }
    }
}
