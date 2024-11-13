using System.ComponentModel.DataAnnotations;

namespace ExerciseServiceAPI.Models
{
    public class ExerciseType
    {
        [Key]
        public int ExerciseTypeId { get; set; }
        [Required]
        public string ExerciseName{ get; set; } = string.Empty;
        public string? Description { get; set; }
        public string CreatedBy { get; set;} = string.Empty;
        public DateTime CreatedAt { get; set;}
    }
}
