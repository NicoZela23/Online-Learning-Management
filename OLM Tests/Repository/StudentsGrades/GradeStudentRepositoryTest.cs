using Microsoft.EntityFrameworkCore;
using Online_Learning_Management.Domain.Entities.GradeStudents;
using Online_Learning_Management.Infrastructure.Data;
using Online_Learning_Management.Infrastructure.Repositories.GradeStudent;

namespace OLM_Tests.Repository.StudentsGrades
{
    public class GradeStudentRepositoryTest
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        private GradeStudents CreateTestGrade(Guid studentId, Guid courseId)
        {
            return new GradeStudents
            {
                Id = Guid.NewGuid(),
                StudentId = studentId,
                CourseId = courseId,
                Score = 90,
                Description = "Amazing!"
            };
        }

        [Fact]
        public async Task AddGradeAsync_ShouldAddGrade()
        {
            var context = GetDbContext();
            var repository = new GradeStudentRepository(context);
            var grade = CreateTestGrade(Guid.NewGuid(), Guid.NewGuid());

            await repository.AddGradeAsync(grade);

            var result = await context.GradeStudents.FindAsync(grade.Id);
            Assert.NotNull(result);
            Assert.Equal(grade.Score, result.Score);
        }

        [Fact]
        public async Task GetGradeStudentById_ShouldReturnGrade()
        {
            var context = GetDbContext();
            var grade = CreateTestGrade(Guid.NewGuid(), Guid.NewGuid());
            await context.GradeStudents.AddAsync(grade);
            await context.SaveChangesAsync();

            var repository = new GradeStudentRepository(context);
            var result = await repository.GetGradeStudentById(grade.Id);

            Assert.NotNull(result);
            Assert.Equal(grade.Score, result.Score);
        }

        [Fact]
        public async Task GetGradeStudentByStudentId_ShouldReturnGradesForStudent()
        {
            var context = GetDbContext();
            var studentId = Guid.NewGuid();
            var courseId1 = Guid.NewGuid();
            var courseId2 = Guid.NewGuid();
            var grade1 = CreateTestGrade(studentId, courseId1);
            var grade2 = CreateTestGrade(studentId, courseId2);
            await context.GradeStudents.AddRangeAsync(grade1, grade2);
            await context.SaveChangesAsync();

            var repository = new GradeStudentRepository(context);
            var result = await repository.GetGradeStudentByStudentId(studentId);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetGradeStudentByCourseId_ShouldReturnGradesForCourse()
        {
            var context = GetDbContext();
            var courseId = Guid.NewGuid();
            var studentId1 = Guid.NewGuid();
            var studentId2 = Guid.NewGuid();
            var grade1 = CreateTestGrade(studentId1, courseId);
            var grade2 = CreateTestGrade(studentId2, courseId);
            await context.GradeStudents.AddRangeAsync(grade1, grade2);
            await context.SaveChangesAsync();

            var repository = new GradeStudentRepository(context);
            var result = await repository.GetGradeStudentByCourseId(courseId);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task UpdateGradeAsync_ShouldUpdateGrade()
        {
            var context = GetDbContext();
            var grade = CreateTestGrade(Guid.NewGuid(), Guid.NewGuid());
            await context.GradeStudents.AddAsync(grade);
            await context.SaveChangesAsync();

            var repository = new GradeStudentRepository(context);
            grade.Score = 95;
            await repository.UpdateGradeAsync(grade);

            var updatedGrade = await context.GradeStudents.FindAsync(grade.Id);
            Assert.NotNull(updatedGrade);
            Assert.Equal(95, updatedGrade.Score);
        }

        [Fact]
        public async Task DeleteGradeAsync_ShouldRemoveGrade()
        {
            var context = GetDbContext();
            var grade = CreateTestGrade(Guid.NewGuid(), Guid.NewGuid());
            await context.GradeStudents.AddAsync(grade);
            await context.SaveChangesAsync();

            var repository = new GradeStudentRepository(context);
            await repository.DeleteGradeAsync(grade.Id);

            var result = await context.GradeStudents.FindAsync(grade.Id);
            Assert.Null(result);
        }
    }
}
