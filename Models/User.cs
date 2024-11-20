﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTimePredicter.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Required]
        public required string UserName { get; set; }
        [Required]
        public required string UserEmail { get; set; }
        [Required]
        public required string UserPassword { get; set; }
        [Required]
        public required string UserRole { get; set; }
        [Required]
        public DateOnly CreatedAt { get; set; }
        public virtual ICollection<Quest> Quests { get; set; } = new List<Quest>();
    }
}