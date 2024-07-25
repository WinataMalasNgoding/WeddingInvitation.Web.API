using System.ComponentModel.DataAnnotations;

namespace WeddingInvitation.Web.API.Models
{
    public class UserUpdateModel
    {
        [Required]
        public string? Password { get; set; }

        [Required]
        public bool IsAdmin { get; set; }

        [Required]
        public string? FullName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        [Required]
        public bool? IsActive { get; set; }

        [Required]
        public string? LastUpdatedBy { get; set; }
    }
}
