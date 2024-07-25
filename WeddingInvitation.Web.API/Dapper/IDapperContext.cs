using Dapper;
using System.Data;

namespace WeddingInvitation.Web.API.Dapper
{
    public interface IDapperContext
    {
        Task<T?> Get<T>(string queryString, DynamicParameters? parms = null, CommandType commandType = CommandType.Text);
        Task<List<T>> GetAll<T>(string queryString, DynamicParameters? parms = null, CommandType commandType = CommandType.Text);
        Task<T?> Insert<T>(string queryString, DynamicParameters? parms = null, CommandType commandType = CommandType.Text);
        Task<T?> Update<T>(string queryString, DynamicParameters? parms = null, CommandType commandType = CommandType.Text);
        Task<T?> Delete<T>(string queryString, DynamicParameters? parms = null, CommandType commandType = CommandType.Text);
    }
}
