using Dapper;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Data.SqlClient;
using System.Security.AccessControl;
using System.Text;
using static Dapper.SqlMapper;

namespace WeddingInvitation.Web.API.Dapper
{
    public class DapperContext : IDapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString = string.Empty;
        private readonly ILogger<DapperContext> _logger;
        private readonly string objectName = nameof(DapperContext);

        public DapperContext(IConfiguration configuration, ILogger<DapperContext> logger)
        {
            _configuration = configuration;
            _logger = logger;
            string? connectionString = _configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                _logger.LogError("DefaultConnection is null");
            }
            else
            {
                _connectionString = connectionString;
            }
            
        }

        public IDbConnection CreateConnection() 
            => new SqlConnection(_connectionString);

        public async Task<List<T>> GetAll<T>(string queryString, DynamicParameters? parms = null, CommandType commandType = CommandType.Text)
        {
            if (string.IsNullOrEmpty(queryString))
            {
                throw new ArgumentException($"'{nameof(queryString)}' cannot be null or empty.", nameof(queryString));
            }

            using IDbConnection db = CreateConnection();

            try
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                using var tran = db.BeginTransaction();

                try
                {
                    var resultDataAll = (await db.QueryAsync<T>(queryString, parms, transaction: tran, commandType: commandType)).ToList();
                    tran.Commit();
                    return resultDataAll;
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                {
                    db.Close();
                }
            }
        }

        public async Task<T?> Get<T>(string queryString, DynamicParameters? parms = null, CommandType commandType = CommandType.Text)
        {
            if (string.IsNullOrEmpty(queryString))
            {
                throw new ArgumentException($"'{nameof(queryString)}' cannot be null or empty.", nameof(queryString));
            }

            using IDbConnection db = CreateConnection();

            try
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                using var tran = db.BeginTransaction();

                try
                {
                    var resultData = await db.QuerySingleOrDefaultAsync<T>(queryString, parms, transaction: tran, commandType: commandType);
                    tran.Commit();
                    return resultData;
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                {
                    db.Close();
                }
            }
        }

        public async Task<T?> Insert<T>(string queryString, DynamicParameters? parms = null, CommandType commandType = CommandType.Text)
        {
            return await Get<T>(queryString, parms, commandType);
            //if (string.IsNullOrEmpty(queryString))
            //{
            //    throw new ArgumentException($"'{nameof(queryString)}' cannot be null or empty.", nameof(queryString));
            //}


            //using IDbConnection db = CreateConnection();

            //try
            //{
            //    if (db.State == ConnectionState.Closed)
            //    {
            //        db.Open();
            //    }

            //    using var tran = db.BeginTransaction();

            //    try
            //    {
            //        var resultData = await db.QuerySingleOrDefaultAsync<T>(queryString, parms, transaction: tran, commandType: commandType);
            //        tran.Commit();
            //        return resultData;
            //    }
            //    catch
            //    {
            //        tran.Rollback();
            //        throw;
            //    }
            //}
            //catch
            //{
            //    throw;
            //}
            //finally
            //{
            //    if (db.State == ConnectionState.Open)
            //    {
            //        db.Close();
            //    }
            //}
        }

        public async Task<T?> Update<T>(string queryString, DynamicParameters? parms = null, CommandType commandType = CommandType.Text)
        {
            return await Get<T>(queryString, parms, commandType);
        }

        public async Task<T?> Delete<T>(string queryString, DynamicParameters? parms = null, CommandType commandType = CommandType.Text)
        {
            return await Get<T>(queryString, parms, commandType);
        }
    }
}
