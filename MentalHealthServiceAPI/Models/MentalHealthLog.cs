using System.ComponentModel.DataAnnotations;

namespace MentalHealthServiceAPI.Models
{
    public class MentalHealthLog
    {
        [Key]
        public int LogId { get; set; }
        [Key]
        public int UserId { get; set; }
        [Key]
        public int MoodId { get; set; }
        [Required]
        public int Intensity{ get; set; }
        [Required]
        public string Notes { get; set; }
        [Required]
        public DateTime LogDate { get; set; }
    }
}
