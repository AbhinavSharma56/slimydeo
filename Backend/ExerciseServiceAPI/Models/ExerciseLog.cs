using System.ComponentModel.DataAnnotations;

namespace ExerciseServiceAPI.Models
{
    public class ExerciseLog
    {
        [Key]
        public int LogId { get; set; }
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public int ExerciseTypeId { get; set; }
        [Required]
        public TimeSpan Duration { get; set; }
        public int CaloriesBurned { get; set; }
        [Required]
        public DateTime ExerciseDate { get; set; }
    }
}
