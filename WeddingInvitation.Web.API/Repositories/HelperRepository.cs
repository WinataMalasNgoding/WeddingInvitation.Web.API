using Dapper;
using System.Data;
using WeddingInvitation.Web.API.Contract;
using WeddingInvitation.Web.API.Dapper;
using WeddingInvitation.Web.API.Models;

namespace WeddingInvitation.Web.API.Repositories
{
    public class HelperRepository : IHelperRepository
    {
        private readonly IDapperContext _context;
        private DynamicParameters parameters = new DynamicParameters();

        public HelperRepository(IDapperContext context)
        {
            _context = context;
        }

        public async Task<bool> ValidateIsFieldExists<T>(string tableName, string columnName, T value)
        {
            try
            {
                string query = $"SELECT COUNT(1) FROM dbo.[{tableName}] WHERE {columnName} = @Value";
                parameters = new DynamicParameters(new
                {
                    Value = value
                });

                return await _context.Get<bool>(query, parameters, commandType: CommandType.Text);
            }
            catch
            {
                throw;
            }
        }
    }
}
