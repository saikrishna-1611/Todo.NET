using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ToDoApi.Models
{
    public class User
    {
        [Key] // Primary Key
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Name { get; set; } // ✅ Required Modifier

        [Required]
        [MaxLength(100)]
        public required string Email { get; set; } // ✅ Required Modifier

        [Required]
        public required string PasswordHash { get; set; } // ✅ Required Modifier

        // Navigation Property: A user can have multiple tasks
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>(); // ✅ Default Value
    }
}