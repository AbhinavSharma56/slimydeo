using System.ComponentModel.DataAnnotations;

namespace MentalHealthServiceAPI.Models
{
    public class MentalHealthLog
    {
        [Key]
        public int LogId { get; set; }
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public int MoodId { get; set; }
        [Required]
        public int Intensity{ get; set; }
        public string Notes { get; set; } = string.Empty;
        [Required]
        public DateTime LogDate { get; set; }
    }
}
