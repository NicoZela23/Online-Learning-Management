using AutoMapper;
using Online_Learning_Management.Application.GradeStudent.Services;
using Online_Learning_Management.Domain.Entities.GradeStudents;
using Online_Learning_Management.Domain.Interfaces.GradeStudent;
using Online_Learning_Management.Infrastructure.DTOs.GradeStudent;
using System.ComponentModel.DataAnnotations;

namespace Online_Learning_Management.Infrastructure.DTOs.GradeStudent
{
    public class CreateGradeStudentDTO
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid StudentId { get; set; }
        [Required]
        public Guid CourseId { get; set; }
        [Required]
        public decimal? Score { get; set; }
        [Required]
        public string? Description { get; set; }
    }
}


