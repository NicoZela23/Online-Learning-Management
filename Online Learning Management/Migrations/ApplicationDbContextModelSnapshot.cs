﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Online_Learning_Management.Infrastructure.Data;

#nullable disable

namespace Online_Learning_Management.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Online_Learning_Management.Domain.Entities.CourseStudent.CourseStudent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CourseID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EnrollmentDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Progress")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("StudentID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("CourseStudents");
                });

            modelBuilder.Entity("Online_Learning_Management.Domain.Entities.Courses.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdInstructor")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Online_Learning_Management.Domain.Entities.Forums.Forum", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CourseID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Forums");
                });

            modelBuilder.Entity("Online_Learning_Management.Domain.Entities.GradeStudents.GradeStudents", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Score")
                        .IsRequired()
                        .HasPrecision(5, 2)
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Grades", null, t =>
                        {
                            t.HasCheckConstraint("CK_Grade_Score", "[Score] >= 0 AND [Score] <= 100");
                        });
                });

            modelBuilder.Entity("Online_Learning_Management.Domain.Entities.ModuleTasks.ModuleTask", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly?>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime?>("Deadline")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("ModuleID")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("ModuleID");

                    b.ToTable("ModuleTasks");
                });

            modelBuilder.Entity("Online_Learning_Management.Domain.Entities.Modules.Module", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AssessmentMethods")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("CourseID")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(3000)");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date");

                    b.Property<string>("LearningOutcomes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Prerequisites")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Resources")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("CourseID");

                    b.ToTable("Modules");
                });

            modelBuilder.Entity("Online_Learning_Management.Domain.Entities.ModuleTasks.ModuleTask", b =>
                {
                    b.HasOne("Online_Learning_Management.Domain.Entities.Modules.Module", null)
                        .WithMany()
                        .HasForeignKey("ModuleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Online_Learning_Management.Domain.Entities.Modules.Module", b =>
                {
                    b.HasOne("Online_Learning_Management.Domain.Entities.Courses.Course", null)
                        .WithMany()
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
