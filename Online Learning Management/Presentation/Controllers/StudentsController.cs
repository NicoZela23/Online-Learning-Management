using Microsoft.AspNetCore.Mvc;
using Online_Learning_Management.Domain.Entities;
using System.Linq;

namespace Online_Learning_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
     {
       private readonly AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        
        [HttpGet("{id}")]
        public ActionResult<User> GetStudent(int id)
        {
           var student = _context.Users.Find(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public ActionResult<User> DeleteStudent(int id)
        {
            var student = _context.Users.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Users.Remove(student);
            _context.SaveChanges();

            return student;
        }
    }

   
}