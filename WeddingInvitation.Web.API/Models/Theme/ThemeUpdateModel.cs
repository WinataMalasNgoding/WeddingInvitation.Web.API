using System.ComponentModel.DataAnnotations;

namespace WeddingInvitation.Web.API.Models
{
    public class ThemeUpdateModel
    {
        [Required]
        public string? ThemeName { get; set; }

        [Required]
        public string? LastUpdatedBy { get; set; }
    }
}
