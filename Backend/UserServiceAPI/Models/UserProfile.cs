using System.ComponentModel.DataAnnotations;

namespace UserServiceAPI.Models
{
    public class UserProfile
    {
        [Key]
        public int UserProfileId { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public DateOnly DOB { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile number must be 10 digits")]
        public long MobileNumber { get; set; }

        [Required]
        public string Gender { get; set; } = "Prefer not to say";

        public string Address { get; set; } = string.Empty;

        public string ProfilePicture { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public float Weight { get; set; }

        public float Height { get; set; }
    }
}