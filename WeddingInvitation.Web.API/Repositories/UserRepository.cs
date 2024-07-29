using Dapper;
using WeddingInvitation.Web.API.Contract;
using WeddingInvitation.Web.API.Dapper;
using WeddingInvitation.Web.API.Models;

namespace WeddingInvitation.Web.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DynamicParameters parameters = new DynamicParameters();
        private readonly IDapperContext _context;

        public UserRepository(IDapperContext context)
        {
            _context = context;
        }

        public async Task<UserGridModel?> GetUserById(long userId)
        {
            try
            {
                string queryGetUser = @"SELECT US.UserId [UserId]
		                                        ,US.IsAdmin [IsAdmin]
		                                        ,US.FullName [FullName]
		                                        ,US.PhoneNumber [PhoneNumber]
		                                        ,US.Email [Email]
		                                        ,US.IsActive [IsActive]
		                                        ,US.CreatedBy [CreatedBy]
		                                        ,US.CreatedDate [CreatedDate]
		                                        ,US.LastUpdatedBy [LastUpdatedBy]
		                                        ,US.LastUpdatedDate [LastUpdatedDate]
                                        FROM dbo.[User] US 
                                        WHERE US.UserId = @UserId";

                parameters = new DynamicParameters(new
                {
                    UserId = userId
                });

                return await _context.Get<UserGridModel>(queryGetUser, parameters);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<UserGridModel>> GetUserGridAll()
        {
            try
            {
                string queryGetUserGridAll = @"SELECT US.UserId [UserId]
		                                            ,US.IsAdmin [IsAdmin]
		                                            ,US.FullName [FullName]
		                                            ,US.PhoneNumber [PhoneNumber]
		                                            ,US.Email [Email]
		                                            ,US.IsActive [IsActive]
		                                            ,US.CreatedBy [CreatedBy]
		                                            ,US.CreatedDate [CreatedDate]
		                                            ,US.LastUpdatedBy [LastUpdatedBy]
		                                            ,US.LastUpdatedDate [LastUpdatedDate]
                                            FROM dbo.[User] US 
                                            WHERE US.IsDeleted != 1
                                            ORDER BY US.CreatedDate ASC";

                var resultUserGridAll = await _context.GetAll<UserGridModel>(queryGetUserGridAll);

                return resultUserGridAll;
            }
            catch
            {
                throw;
            }
        }

        public async Task<UserModel?> InsertUser(UserInsertModel userInsert)
        {
            try
            {
                string queryInsertUser = @"DECLARE @DateNow DATETIME = GETDATE();
                                            INSERT INTO dbo.[User]
                                                       (Username
                                                       ,Password
                                                       ,IsAdmin
                                                       ,FullName
                                                       ,PhoneNumber
                                                       ,Email
                                                       ,IsActive
                                                       ,CreatedBy
                                                       ,CreatedDate
                                                       ,LastUpdatedBy
                                                       ,LastUpdatedDate
                                                       ,IsDeleted)
                                            OUTPUT INSERTED.*
                                            VALUES
                                                (@Username
                                                ,@Password
                                                ,@IsAdmin
                                                ,@FullName
                                                ,@PhoneNumber
                                                ,@Email
                                                ,1
                                                ,@CreatedBy
                                                ,@DateNow
                                                ,@LastUpdatedBy
                                                ,@DateNow
                                                ,0)";

                parameters = new DynamicParameters(new
                {
                    Username = userInsert.Username,
                    Password = userInsert.Password,
                    IsAdmin = userInsert.IsAdmin,
                    FullName = userInsert.FullName,
                    PhoneNumber = userInsert.PhoneNumber,
                    Email = userInsert.Email,
                    CreatedBy = userInsert.CreatedBy,
                    LastUpdatedBy = userInsert.LastUpdatedBy
                });

                return await _context.Insert<UserModel>(queryInsertUser, parameters);
            }
            catch
            {
                throw;
            }
        }

        public async Task<UserModel?> UpdateUser(long userId, UserUpdateModel userInsert)
        {
            try
            {
                string queryUpdateUser = @"UPDATE [dbo].[User]
                                            SET [Password] = @Password
                                                ,[IsAdmin] = @IsAdmin
                                                ,[FullName] = @FullName
                                                ,[PhoneNumber] = @PhoneNumber
                                                ,[Email] = @Email
                                                ,[IsActive] = @IsActive
                                                ,[LastUpdatedBy] = @LastUpdatedBy
                                                ,[LastUpdatedDate] = GETDATE()
                                            OUTPUT INSERTED.*
                                            WHERE UserId = @UserId";

                parameters = new DynamicParameters(new
                {
                    UserId = userId,
                    Password = userInsert.Password,
                    IsAdmin = userInsert.IsAdmin,
                    FullName = userInsert.FullName,
                    PhoneNumber = userInsert.PhoneNumber,
                    Email = userInsert.Email,
                    IsActive = userInsert.IsActive,
                    LastUpdatedBy = userInsert.LastUpdatedBy
                });

                return await _context.Update<UserModel>(queryUpdateUser, parameters);
            }
            catch
            {
                throw;
            }
        }

        public async Task<string?> DeleteUser(long userId)
        {
            try
            {
                string queryDeleteUser = @"UPDATE [dbo].[User]
                                            SET [IsDeleted] = 1
                                            WHERE UserId = @UserId

                                            IF @@ROWCOUNT > 0
                                            BEGIN
                                                SELECT 'User have been deleted !';
                                            END
                                            ELSE
                                            BEGIN
                                                SELECT 'Failed to delete user !';
                                            END";

                parameters = new DynamicParameters(new
                {
                    UserId = userId,
                });

                return await _context.Delete<string>(queryDeleteUser, parameters);
            }
            catch
            {
                throw;
            }
        }
    }
}
