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

    public interface IPickRepository
    {
        Task<bool> Insert(PlayerPick pick);
        Task<PlayerPick> GetByUser(int userId);
        Task<ICollection<PlayerPick>> GetAuditByUser(int userId);
        Task<ICollection<LeaderBoardPick>> GetAllPicks();
        Task<ICollection<ResultRow>> GetAllPoints();
        Task<ResultPick> GetResults(int gameId);
    }

    public class PickRepository : IPickRepository
    {
        private readonly IConfiguration _config;

        public PickRepository(IConfiguration config)
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

        public async Task<bool> Insert(PlayerPick pick)
        {
            var isSuccess = false;
            var p = new DynamicParameters();
            p.Add("@combinedscore", pick.CombinedScore);
            p.Add("@firstInningWickets", pick.FirstInningWickets);
            p.Add("@secondinningwickets", pick.SecondInningWickets);
            p.Add("@HighestScore", pick.HighestScore);
            p.Add("@highestWickets", pick.HighestWickets);
            p.Add("@overschase", pick.OversChase);
            p.Add("@Total4s", pick.Total4s);
            p.Add("@Total6s", pick.Total6s);
            p.Add("@TeamPick", pick.TeamPick);
            p.Add("@userId", pick.UserId);

            try
            {
                using (IDbConnection conn = Connection)
                {
                    conn.Execute("InsertPlayerPick", p, commandType: CommandType.StoredProcedure);
                }
                isSuccess = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return await Task.FromResult(isSuccess);
        }

        public async Task<PlayerPick> GetByUser(int userId)
        {
            var p = new DynamicParameters();
            p.Add("@userId", userId);

            using (IDbConnection conn = Connection)
            {
                var result = await conn.QueryAsync<PlayerPick>("GetPicksByUserId", p, commandType: CommandType.StoredProcedure);

                return result.FirstOrDefault();
            }
        }

        public async Task<ICollection<PlayerPick>> GetAuditByUser(int userId)
        {
            var p = new DynamicParameters();
            p.Add("@userId", userId);
            using (IDbConnection conn = Connection)
            {
                var result = await conn.QueryAsync<PlayerPick>("GetAuditPicksByUserId", p, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }

        public async Task<ICollection<LeaderBoardPick>> GetAllPicks()
        {
            using (IDbConnection conn = Connection)
            {
                var sQuery = $"SELECT p.userId, displayname, teampick, combinedscore, firstinningwickets, secondinningwickets, highestscore, " +
                    $"highestwickets, overschase, total4s, total6s, p.addDt from playerpicks p inner join useraccount u on p.userId = u.userId order by p.addDt, u.displayname ";

                var result = await conn.QueryAsync<LeaderBoardPick>(sQuery);

                return result.ToList();
            }
        }

        public async Task<ICollection<ResultRow>> GetAllPoints()
        {
            var p = new DynamicParameters();
            
            using (IDbConnection conn = Connection)
            {
                var result = await conn.QueryAsync<ResultRow>("GetAllPoints", p, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }

        public async Task<ResultPick> GetResults(int gameId)
        {
            using (IDbConnection conn = Connection)
            {
                var sQuery = $"SELECT * from results where gameId = '{gameId}'";

                var result = await conn.QueryAsync<ResultPick>(sQuery);

                return result.FirstOrDefault();
            }
        }
    }
}
