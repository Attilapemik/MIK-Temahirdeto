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
    [Migration("20220413012938_ThemeStatusRelation")]
    partial class ThemeStatusRelation
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

            modelBuilder.Entity("PannonBlazor.Shared.Semester", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

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

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InstructorId")
                        .HasColumnType("int");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<string>("Student")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("InstructorId");

                    b.HasIndex("StatusId");

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
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProgrammeId")
                        .HasColumnType("int");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("ProgrammeId");

                    b.ToTable("Users");
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

                    b.Navigation("Instructor");

                    b.Navigation("Status");
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
                        .WithMany()
                        .HasForeignKey("SemesterId")
                        .OnDelete(DeleteBehavior.Cascade)
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
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Programme");
                });

            modelBuilder.Entity("PannonBlazor.Shared.Programme", b =>
                {
                    b.Navigation("ThemeProgrammes");

                    b.Navigation("Users");
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
