using Microsoft.EntityFrameworkCore;
using Online_Learning_Management.Domain.Entities.CourseStudent;
using Online_Learning_Management.Domain.Entities.Modules;
using Online_Learning_Management.Infrastructure.Repositories.Modules;
using Online_Learning_Management.Domain.Entities.ModuleTasks;
using Online_Learning_Management.Infrastructure.Repositories.ModuleTasks;
using Online_Learning_Management.Infrastructure.Repositories.Courses;
using Online_Learning_Management.Domain.Entities.Forums;
using Online_Learning_Management.Infrastructure.Repositories.Forum;
using Online_Learning_Management.Domain.Entities.GradeStudents;
using Online_Learning_Management.Infrastructure.Repositories.GradeStudentss;
using Online_Learning_Management.Domain.Entities.Courses;
using Online_Learning_Management.Domain.Entities.Students;
using Online_Learning_Management.Infrastructure.Repositories.Students;
using Online_Learning_Management.Domain.Entities.Files;
using Online_Learning_Management.Infrastructure.Repositories.Files;
using Online_Learning_Management.Infrastructure.Repositories;
using Online_Learning_Management.Domain.Entities.Auth;
using Online_Learning_Management.Infrastructure.Repositories.Auth;
using Online_Learning_Management.Domain.Entities.ReportCourses;
using Online_Learning_Management.Infrastructure.Repositories.ReportCourses;
using Online_Learning_Management.Domain.Entities.TaskStudents;
using Online_Learning_Management.Infrastructure.Repositories.TaskStudents;

namespace Online_Learning_Management.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Module> Modules{ get; set; }
        public DbSet<ModuleTask> ModuleTasks { get; set; } 
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseStudent> CourseStudents { get; set; }
        public DbSet<Forum> Forums { get; set; }
        public DbSet<GradeStudents> GradeStudents { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<FileMetadata> UploadedFiles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ReportCourse> ReportCourses { get; set; }
        public DbSet<TaskStudent> TaskStudents { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfiguration(new ModuleConfiguration());
            modelBuilder.ApplyConfiguration(new ModuleTaskConfiguration());
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new CourseStudentConfiguration());
            modelBuilder.ApplyConfiguration(new ForumConfiguration());
            modelBuilder.ApplyConfiguration(new GradeStudentConfiguration());
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new FileConfiguration());
            modelBuilder.ApplyConfiguration(new UserAuthConfiguration());
            modelBuilder.ApplyConfiguration(new ReportCourseConfiguration());
            modelBuilder.ApplyConfiguration(new TaskStudentConfiguration());
        }
    }
}
