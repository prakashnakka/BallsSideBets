using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBalls.Security;
using Microsoft.Extensions.Configuration;
using MyBalls.Repository;
using MyBalls.DataAccess;

namespace MyBalls.Controllers
{
    [AdminAuthorize(MyCustomMode.Enforce)]
    public class AdminController : BaseSiteController<AdminController>
    {
        private readonly int _gameId;
        

        private readonly IAdminRepository _adminRepository;
        public AdminController(ICookieRepository cookieRepository, IAdminRepository adminRepository, IConfiguration config) : base(cookieRepository, config) 
        {
            _adminRepository = adminRepository;
            _gameId = this.GameId; //Convert.ToInt32(_config["GameId"].ToString());
        }

        public async Task<IActionResult> AddResults()
        {
            var resultPicks = await _adminRepository.Get(_gameId);

            return View(resultPicks);
        }

        [HttpPost]
        public async Task<IActionResult> targetScore()
        {
            var combinedScore = Convert.ToInt32(Request.Form["targetScore"]);

            var isSuccess = await _adminRepository.Update(CategoryType.CombinedScore, _gameId, combinedScore);
            var resultPicks = await _adminRepository.Get(_gameId);
            return View("AddResults", resultPicks);
        }

        [HttpPost]
        public async Task<IActionResult> firstinningwickets()
        {
            var firstInningWickets = Convert.ToInt32(Request.Form["firstInningWickets"]);

            var isSuccess = await _adminRepository.Update(CategoryType.FirstInningWickets, _gameId, firstInningWickets);
            var resultPicks = await _adminRepository.Get(_gameId);
            return View("AddResults", resultPicks);
        }

        [HttpPost]
        public async Task<IActionResult> secondinningwickets()
        {
            var secondInningWickets = Convert.ToInt32(Request.Form["secondInningWickets"]);

            var isSuccess = await _adminRepository.Update(CategoryType.SecondInningWickets, _gameId, secondInningWickets);
            var resultPicks = await _adminRepository.Get(_gameId);
            return View("AddResults", resultPicks);
        }

        [HttpPost]
        public async Task<IActionResult> overschase()
        {
            var oversChase = Convert.ToDouble(Request.Form["oversChase"]);

            var isSuccess = await _adminRepository.Update(CategoryType.OversChase, _gameId, null, oversChase);
            var resultPicks = await _adminRepository.Get(_gameId);
            return View("AddResults", resultPicks);
        }

        [HttpPost]
        public async Task<IActionResult> highestscore()
        {
            var highestScore = Convert.ToInt32(Request.Form["highestScore"]);

            var isSuccess = await _adminRepository.Update(CategoryType.HighestScore, _gameId, highestScore);
            var resultPicks = await _adminRepository.Get(_gameId);
            return View("AddResults", resultPicks);
        }

        [HttpPost]
        public async Task<IActionResult> highestwickets()
        {
            var highestWickets = Convert.ToInt32(Request.Form["highestWickets"]);

            var isSuccess = await _adminRepository.Update(CategoryType.HighestWickets, _gameId, highestWickets);
            var resultPicks = await _adminRepository.Get(_gameId);
            return View("AddResults", resultPicks);
        }

        [HttpPost]
        public async Task<IActionResult> total4s()
        {
            var total4s = Convert.ToInt32(Request.Form["total4s"]);

            var isSuccess = await _adminRepository.Update(CategoryType.Total4s, _gameId, total4s);
            var resultPicks = await _adminRepository.Get(_gameId);
            return View("AddResults", resultPicks);
        }

        [HttpPost]
        public async Task<IActionResult> total6s()
        {
            var total6s = Convert.ToInt32(Request.Form["total6s"]);

            var isSuccess = await _adminRepository.Update(CategoryType.Total6s, _gameId, total6s);
            var resultPicks = await _adminRepository.Get(_gameId);
            return View("AddResults", resultPicks);
        }

        [HttpPost]
        public async Task<IActionResult> teamPick()
        {
            var teamPick = Convert.ToInt32(Request.Form["teamPick"]);

            var isSuccess = await _adminRepository.UpdateTeamPick(_gameId, teamPick);
            var resultPicks = await _adminRepository.Get(_gameId);
            return View("AddResults", resultPicks);
        }

        [HttpPost]
        public async Task<IActionResult> maxSingleOverScore()
        {
            var maxSingleOverScore = Convert.ToInt32(Request.Form["maxSingleOverScore"]);

            var isSuccess = await _adminRepository.Update(CategoryType.MaxSingleOverScore, _gameId, maxSingleOverScore);
            var resultPicks = await _adminRepository.Get(_gameId);
            return View("AddResults", resultPicks);
        }
        
    }
}