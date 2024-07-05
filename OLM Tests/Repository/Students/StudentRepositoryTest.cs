using Microsoft.EntityFrameworkCore;
using Online_Learning_Management.Domain.Entities.Students;
using Online_Learning_Management.Infrastructure.Data;
using Online_Learning_Management.Infrastructure.Students;


namespace OLM_Tests.Repository.Students
{
    public class StudentRepositoryTest
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }
        [Fact]
        public async Task AddStudentAsync_ShouldAddStudent()
        {
            var context = GetDbContext();
            var repository = new StudentRepository(context);
            var student = new Student
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Name = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                CreateAt = DateTime.UtcNow
            };

            var result = await repository.AddStudentAsync(student);

            var addedStudent = await context.Students.FindAsync(student.Id);
            Assert.NotNull(addedStudent);
            Assert.Equal(student.Name, addedStudent.Name);
            Assert.Equal(student.Email, addedStudent.Email);
        }

        [Fact]
        public async Task DeleteStudentAsync_ShouldDeleteStudent()
        {
            var context = GetDbContext();
            var repository = new StudentRepository(context);
            var studentId = Guid.NewGuid();
            var student = new Student
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Name = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                CreateAt = DateTime.UtcNow
            };
            await context.Students.AddAsync(student);
            await context.SaveChangesAsync();

            await repository.DeleteStudentAsync(studentId);

            var deletedStudent = await context.Students.FindAsync(studentId);
            Assert.Null(deletedStudent);
        }
        
        [Fact]
        public async Task GetAllStudentsAsync_ShouldReturnAllStudents()
        {
            // Arrange
            var context = GetDbContext();
            var repository = new StudentRepository(context);
            var students = new List<Student>
            {
                new Student {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Name = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                CreateAt = DateTime.UtcNow
            },
                new Student {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Name = "Elmer",
                LastName = "Quezada",
                Email = "Elmer@example.com",
                CreateAt = DateTime.UtcNow
            },
                new Student {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Name = "Camilo",
                LastName = "Gomez",
                Email = "CaMOl@example.com",
                CreateAt = DateTime.UtcNow
                }
            };
            await context.Students.AddRangeAsync(students);
            await context.SaveChangesAsync();

            var result = await repository.GetAllStudentsAsync();

            Assert.Equal(students.Count, result.Count());
            foreach (var student in students)
            {
                Assert.Contains(result, s => s.Name == student.Name);
            }
        }

        [Fact]
        public async Task GetStudentByIdAsync_ShouldReturnStudentById()
        {
            var context = GetDbContext();
            var repository = new StudentRepository(context);
            var studentId = Guid.NewGuid();
            var student = new Student
            {
                Id = studentId,
                UserId = Guid.NewGuid(),
                Name = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                CreateAt = DateTime.UtcNow
            };
            await context.Students.AddAsync(student);
            await context.SaveChangesAsync();

            var result = await repository.GetStudentByIdAsync(studentId);

            Assert.NotNull(result);
            Assert.Equal(studentId, result.Id);
            Assert.Equal(student.Name, result.Name);
        }

        [Fact]
        public async Task UpdateStudentAsync_ShouldUpdateStudent()
        {
            var context = GetDbContext();
            var repository = new StudentRepository(context);
            var studentId = Guid.NewGuid();
            var originalStudent = new Student
            {
                Id = studentId,
                UserId = Guid.NewGuid(),
                Name = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                CreateAt = DateTime.UtcNow
            };
            await context.Students.AddAsync(originalStudent);
            await context.SaveChangesAsync();

            context.Entry(originalStudent).State = EntityState.Detached;

            var updatedStudent = new Student { Id = studentId, Name = "Jane Smith" };

            await repository.UpdateStudentAsync(updatedStudent);

            var result = await context.Students.FindAsync(studentId);
            Assert.NotNull(result);
            Assert.Equal(updatedStudent.Name, result.Name);
        }
    }
}
