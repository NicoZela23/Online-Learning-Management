﻿using Microsoft.AspNetCore.Mvc;
using Online_Learning_Management.Domain.Entities.GradeStudents;
using Online_Learning_Management.Domain.Interfaces.GradeStudent;
using Online_Learning_Management.Infrastructure.DTOs.GradeStudent;

namespace Online_Learning_Management.Presentation.Controllers
{
    [Route("couse/grade")]
    [ApiController]
    public class GradeStudentController : ControllerBase
    {
        private readonly IGradeStudentService _gradeStudentService;
        public GradeStudentController(IGradeStudentService gradeStudentService)
        {
            _gradeStudentService = gradeStudentService;
        }

        [HttpGet("course/{courseId:guid}")]
        public async Task<IActionResult> GetCourse(Guid courseId)
        {
            try
            {
                var result = await _gradeStudentService.GetGradeStudentByCourseIdAsync(courseId);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]// 
        public async Task<ActionResult<GradeStudents>> GetGradeById(Guid id)
        {
            try
            {
                var grade = await _gradeStudentService.GetGradeStudentByIdAsync(id);

                if (grade == null)
                {
                    return NotFound();
                }

                return Ok(grade);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("student/{studentId:guid}/course/{courseId:guid}")]
        public async Task<IActionResult> GetStudentCourse(Guid studentId, Guid courseId)
        {
            try
            {
                var result = await _gradeStudentService.GetGradeStudentByStudentIdAndCourseIdAsync(studentId, courseId);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("student/{studentId:guid}")]
        public async Task<IActionResult> GetStudentbyStudent(Guid studentId)
        {
            try
            {
                var result = await _gradeStudentService.GetGradeStudentByStudentIdAsync(studentId);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        [HttpPost]
        public async Task<ActionResult> AddGrade(CreateGradeStudentDTO gradeDto)
        {
            try
            {
                await _gradeStudentService.AddGradeAsync(gradeDto);
                return StatusCode(201, "Grade successfully added.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGrade(Guid id, [FromBody] UpdateGradeStudentDTO gradeDto)
        {
            try
            {
                await _gradeStudentService.UpdateGradeAsync(id ,gradeDto);
                return Ok("Grade successfully updated.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGrade(Guid id)
        {
            try
            {
                await _gradeStudentService.DeleteGradeAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
