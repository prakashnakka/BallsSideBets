using BL.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace MyBalls.DataAccess
{

    public interface IAdminRepository
    {
        Task<bool> Update(CategoryType columnName, int gameId, int? intVal = null, double? floatVal = null);
        Task<ResultPick> Get(int gameId);
        Task<bool> UpdateTeamPick(int gameId, int intVal);
    }

    public class AdminRepository : IAdminRepository
    {
        private readonly IConfiguration _config;

        public AdminRepository(IConfiguration config)
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

        public async Task<bool> Update(CategoryType columnName, int gameId, int? intVal = null, double? floatVal = null )
        {
            var isSuccess = false;
            var p = new DynamicParameters();
            p.Add("@categoryId", columnName);
            p.Add("@gameId", gameId);
            p.Add("@intVal", intVal);
            p.Add("@floatVal", floatVal);
            
            try
            {
                using (IDbConnection conn = Connection)
                {
                    conn.Execute("UpdateResultsPick", p, commandType: CommandType.StoredProcedure);
                }
                isSuccess = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return await Task.FromResult(isSuccess);
        }

        public async Task<ResultPick> Get(int gameId)
        {
            using (IDbConnection conn = Connection)
            {
                var sQuery = $"SELECT * from results where gameId = '{gameId}'";

                var result = await conn.QueryAsync<ResultPick>(sQuery);

                return result.FirstOrDefault();
            }
        }

        public async Task<bool> UpdateTeamPick(int gameId, int intVal)
        {
            var isSuccess = false;
            var p = new DynamicParameters();
            p.Add("@gameId", gameId);
            p.Add("@intVal", intVal);
            
            try
            {
                using (IDbConnection conn = Connection)
                {
                    conn.Execute("UpdateTeamPick", p, commandType: CommandType.StoredProcedure);
                }
                isSuccess = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return await Task.FromResult(isSuccess);
        }
    }

    public enum CategoryType
    {
        CombinedScore,
        FirstInningWickets,
        SecondInningWickets,
        HighestScore,
        HighestWickets,
        OversChase,
        Total4s,
        Total6s,
        TeamPick,
        MaxSingleOverScore
    }
}
