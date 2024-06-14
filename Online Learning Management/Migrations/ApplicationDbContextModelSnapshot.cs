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

                    b.Property<string>("CourseID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentID")
                        .HasColumnType("nvarchar(max)");


                    b.HasKey("Id");

                    

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");
                    
                    b.ToTable("Modules");
                    b.ToTable("CourseStudents");
                });
                
            modelBuilder.Entity("Online_Learning_Management.Domain.Entities.ModuleTasks.ModuleTask", b =>
              {
                  b.Property<Guid>("Id")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("uniqueidentifier");

                  b.Property<string>("Description")
                      .IsRequired()
                      .HasColumnType("text");

                  b.Property<string>("ModuleID")
                      .IsRequired()
                      .HasColumnType("nvarchar(max)");

                  b.Property<string>("Title")
                      .IsRequired()
                      .HasColumnType("nvarchar(100)");

                  b.HasKey("Id");

                  b.ToTable("ModuleTasks");
              });    
modelBuilder.Entity("Online_Learning_Management.Domain.Entities.Course", b =>
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

            modelBuilder.Entity("Online_Learning_Management.Domain.Entities.User", b =>

            modelBuilder.Entity("Online_Learning_Management.Domain.Entities.Modules.Module", b =>

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

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Modules");
                });
#pragma warning restore 612, 618
        }
    }
}
