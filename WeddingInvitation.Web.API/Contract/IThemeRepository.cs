using WeddingInvitation.Web.API.Models;

namespace WeddingInvitation.Web.API.Contract
{
    public interface IThemeRepository
    {
        Task<ThemeGridModel?> GetThemeById(long themeId);
        Task<List<ThemeGridModel>> GetThemeGridAll();
        Task<ThemeModel?> InsertTheme(ThemeInsertModel themeInput);
        Task<ThemeModel?> UpdateTheme(long themeId, ThemeUpdateModel themeInput);
        Task<string?> DeleteTheme(long themeId);
    }
}
