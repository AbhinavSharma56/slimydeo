namespace ExerciseServiceAPI.Models
{
    public class ExerciseType
    {
        public int ExerciseTypeId { get; set; }
        public string ExerciseName{ get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set;}
        public DateTime CreatedAt { get; set;}
    }
}
