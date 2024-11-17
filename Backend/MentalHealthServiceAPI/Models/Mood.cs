using System.ComponentModel.DataAnnotations;

namespace MentalHealthServiceAPI.Models
{
    public class Mood
    {
        [Key]
        public int MoodId { get; set; }
        [Required]
        public string MoodName { get; set; } = string.Empty;
       
        public string Description { get; set; } = string.Empty;

        public string CreatedBy { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; }
    }
}
