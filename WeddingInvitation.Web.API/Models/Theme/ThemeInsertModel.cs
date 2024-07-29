using System.ComponentModel.DataAnnotations;

namespace WeddingInvitation.Web.API.Models
{
    public class ThemeInsertModel
    {
        [Required]
        public string? ThemeName { get; set; }

        [Required]
        public string? CreatedBy { get; set; }

        [Required]
        public string? LastUpdatedBy { get; set; }
    }
}
