using BL.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MyBalls.DataAccess
{

    public interface IUserRepository
    {
        Task<UserAccount> ValidateLoginByEmail(string email, string pwd);

        Task<UserAccount> Insert(UserAccount ua);
        Task<bool> ExistingEmail(string email, int? userId = null);

    }

    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _config;

        public UserRepository(IConfiguration config)
        {
            _config = config;
        }

        private IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DbConnect"));
            }
        }

        public async Task<UserAccount> ValidateLoginByEmail(string email, string pwd)
        {
            using (IDbConnection conn = Connection)
            {
                var sQuery = $"SELECT * from useraccount where Email = '{email}'";
                var result = await conn.QueryAsync<UserAccount>(sQuery);
                var userAccount = result.FirstOrDefault();

                if (userAccount != null && userAccount.Pwd == pwd)
                    return userAccount;
                else
                    return null;
            }
        }

        public async Task<UserAccount> Insert(UserAccount user)
        {
            var p = new DynamicParameters();
            p.Add("@Email", user.Email);
            p.Add("@pwd", user.Pwd);
            p.Add("@displayname", user.DisplayName);
            
            using (IDbConnection conn = Connection)
            {
                var result = await conn.QueryAsync<UserAccount>("InsertUserAccount", p, commandType: CommandType.StoredProcedure);

                return result.FirstOrDefault();
            }
            //return await Task.FromResult<int>(p.Get<int>("@UserID"));
        }

        public async Task<bool> ExistingEmail(string email, int? userId = null)
        {
            var affectedRows = 0;
            var sql = (userId == null) ? $"SELECT count(email) as emailCount from UserAccount where Email = '{email}'"
            : $"SELECT count(email) as emailCount from UserAccount where Email = '{email}' and UserId <> {userId}";

            using (IDbConnection conn = Connection)
            {
                affectedRows = conn.Query<int>(sql).First();
            }

            return await Task.FromResult<bool>((affectedRows == 1) ? true : false);
        }
    }
}
