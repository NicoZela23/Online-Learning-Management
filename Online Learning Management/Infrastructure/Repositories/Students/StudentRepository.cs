using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Online_Learning_Management.Domain.Entities;
using Online_Learning_Management.Domain.Interfaces;
using Online_Learning_Management.Infrastructure.Data;
using System.Collections.Generic;

public class StudentRepository : IStudentsRepository
{
    private readonly ApplicationDbContext _context;

    public StudentRepository(ApplicationDbContext context)
    {
        _context = context;
    }
     
    public async Task<Students> GetStudentByIdAsync(int id)
    {
        return await _context.Students.FindAsync(id);
    }
    
    

    public async Task DeleteStudentByIdAsync(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student != null)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }
    }
}