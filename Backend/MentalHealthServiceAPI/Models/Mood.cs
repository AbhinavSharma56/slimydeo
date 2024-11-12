using System.ComponentModel.DataAnnotations;

namespace MentalHealthServiceAPI.Models
{
    public class Mood
    {
        [Key]
        public int MoodId { get; set; }
        [Required]
        public string MoodName { get; set; }
       
        public string Description { get; set; }
      
        public int CreatedBy {  get; set; }
        
        public DateTime CreatedAt { get; set; }
    }
}
