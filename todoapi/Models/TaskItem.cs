using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApi.Models
{
    public class TaskItem
    {
        [Key] 
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public required string Title { get; set; }  

        public string? Description { get; set; }

        [Required]
        public string Status { get; set; } = "Pending"; 

        public DateTime DueDate { get; set; } = DateTime.UtcNow; 

        
        [ForeignKey("User")]
        public int UserId { get; set; }

        public  User? User { get; set; } 
    }
}