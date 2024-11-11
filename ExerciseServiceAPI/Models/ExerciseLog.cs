using System.ComponentModel.DataAnnotations;

namespace ExerciseServiceAPI.Models
{
    public class ExerciseLog
    {
        [Key]
        public int LogId { get; set; }
        public int UserId { get; set; }
        public int ExerciseTypeId { get; set; }
        public TimeSpan Duration { get; set; }
        public int CaloriesBurned { get; set; }
        public DateTime ExerciseDate { get; set; }
    }
}
