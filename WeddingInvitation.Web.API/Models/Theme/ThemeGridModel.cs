namespace WeddingInvitation.Web.API.Models
{
    public class ThemeGridModel
    {
        public long ThemeId { get; set; }
        public string? ThemeName { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
