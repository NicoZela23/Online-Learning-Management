using System.ComponentModel.DataAnnotations;

namespace Online_Learning_Management.Infrastructure.DTOs.Forum
{
    public class CreateForumDTO
    {
       
        public Guid Id { get; set; }
    
        public Guid CourseID { get; set; }
     
        public string? Title { get; set; }
       
        public string? Description { get; set; }
    }
}
