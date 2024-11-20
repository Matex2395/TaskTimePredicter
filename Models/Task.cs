using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaskTimePredicter.Models
{
    public class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskId { get; set; }
        [Required]
        public required string TaskName { get; set; }
        [Required]
        public double EstimatedTime { get; set; }
        public double? ActualTime { get; set; }
        [Required]
        public required string TaskState { get; set; }
        public DateOnly CreationDate { get; set; }
        
        public int? UserId { get; set; }
        public User? User { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
