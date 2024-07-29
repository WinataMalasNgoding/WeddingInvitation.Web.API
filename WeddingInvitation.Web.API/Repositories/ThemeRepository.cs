using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using WeddingInvitation.Web.API.Contract;
using WeddingInvitation.Web.API.Dapper;
using WeddingInvitation.Web.API.Models;

namespace WeddingInvitation.Web.API.Repositories
{
    public class ThemeRepository : IThemeRepository
    {
        private DynamicParameters parameters = new DynamicParameters();
        private readonly IDapperContext _context;

        public ThemeRepository(IDapperContext context)
        {
            _context = context;
        }

        public async Task<ThemeGridModel?> GetThemeById(long themeId)
        {
            try
            {
                string queryGetTheme = @"SELECT THE.ThemeId [ThemeId]
                                                  ,THE.ThemeName [ThemeName] 
                                                  ,THE.CreatedBy [CreatedBy]
                                                  ,THE.CreatedDate [CreatedDate]
                                                  ,THE.LastUpdatedBy [LastUpdatedBy]
                                                  ,THE.LastUpdatedDate [LastUpdatedDate]
                                            FROM dbo.Theme THE
                                            WHERE THE.ThemeId = @ThemeId";

                parameters = new DynamicParameters(new
                {
                    ThemeId = themeId
                });

                return await _context.Get<ThemeGridModel>(queryGetTheme, parameters);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<ThemeGridModel>> GetThemeGridAll()
        {
            try
            {
                string queryGetThemeGridAll = @"SELECT THE.ThemeId [ThemeId]
                                                      ,THE.ThemeName [ThemeName] 
                                                      ,THE.CreatedBy [CreatedBy]
                                                      ,THE.CreatedDate [CreatedDate]
                                                      ,THE.LastUpdatedBy [LastUpdatedBy]
                                                      ,THE.LastUpdatedDate [LastUpdatedDate]
                                                FROM dbo.Theme THE
                                                WHERE THE.IsDeleted != 1
                                                ORDER BY THE.CreatedDate ASC";

                var resultThemeGridAll = await _context.GetAll<ThemeGridModel>(queryGetThemeGridAll);

                return resultThemeGridAll;
            }
            catch
            {
                throw;
            }
        }

        public async Task<ThemeModel?> InsertTheme(ThemeInsertModel themeInput)
        {
            try
            {
                string queryInsertTheme = @"DECLARE @DateNow DATETIME = GETDATE();
                                            INSERT INTO dbo.Theme
                                                       (ThemeName
                                                       ,CreatedBy
                                                       ,CreatedDate
                                                       ,LastUpdatedBy
                                                       ,LastUpdatedDate
                                                       ,IsDeleted)
                                            OUTPUT INSERTED.*
                                            VALUES
                                                (@ThemeName
                                                ,@CreatedBy
                                                ,@DateNow
                                                ,@LastUpdatedBy
                                                ,@DateNow
                                                ,0)";

                parameters = new DynamicParameters(new
                {
                    ThemeName = themeInput.ThemeName,
                    CreatedBy = themeInput.CreatedBy,
                    LastUpdatedBy = themeInput.LastUpdatedBy
                });

                return await _context.Insert<ThemeModel>(queryInsertTheme, parameters);
            }
            catch
            {
                throw;
            }
        }

        public async Task<ThemeModel?> UpdateTheme(long themeId, ThemeUpdateModel themeInput)
        {
            try
            {
                string queryUpdateTheme = @"UPDATE dbo.Theme
                                            SET ThemeName = @ThemeName
                                                ,LastUpdatedBy = @LastUpdatedBy
                                                ,LastUpdatedDate = GETDATE()
                                            OUTPUT INSERTED.*
                                            WHERE ThemeId = @ThemeId";

                parameters = new DynamicParameters(new
                {
                    ThemeId = themeId,
                    ThemeName = themeInput.ThemeName,
                    LastUpdatedBy = themeInput.LastUpdatedBy
                });

                return await _context.Update<ThemeModel>(queryUpdateTheme, parameters);
            }
            catch
            {
                throw;
            }
        }

        public async Task<string?> DeleteTheme(long themeId)
        {
            try
            {
                string queryDeleteTheme = @"UPDATE dbo.Theme
                                            SET IsDeleted = 1
                                            WHERE ThemeId = @ThemeId

                                            IF @@ROWCOUNT > 0
                                            BEGIN
                                                SELECT 'Theme have been deleted !';
                                            END
                                            ELSE
                                            BEGIN
                                                SELECT 'Theme to delete user !';
                                            END";

                parameters = new DynamicParameters(new
                {
                    ThemeId = themeId
                });

                return await _context.Delete<string>(queryDeleteTheme, parameters);
            }
            catch
            {
                throw;
            }
        }
    }
}
