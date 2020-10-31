using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BL.Models
{
    public class PlayerPick
    {
        [Required(ErrorMessage = "Target score is required"), DisplayName("Target Score")]
        public int CombinedScore { get; set; }
        [Required(ErrorMessage = "First innings wickets are required"), DisplayName("First innings wickets")]
        public int FirstInningWickets { get; set; }
        [Required(ErrorMessage = "Chasing wickets are required"), DisplayName("Chasing wickets")]
        public int SecondInningWickets { get; set; }
        [Required(ErrorMessage = "Highest single score is required"), DisplayName("Highest Single Score")]
        public int HighestScore { get; set; }
        [Required(ErrorMessage = "Highest single wickets is required"), DisplayName("Highest Single Wickets")]
        public int HighestWickets { get; set; }
        [Required(ErrorMessage = "Overs to chase is required"), DisplayName("Overs to chase")]
        public double OversChase { get; set; }
        [Required(ErrorMessage = "Total 4s both innings is required"), DisplayName("Total 4's (both innings combined)")]
        public int Total4s { get; set; }
        [Required(ErrorMessage = "Total 6s both innings is required"), DisplayName("Total 6's (both innings combined)")]
        public int Total6s { get; set; }
        [Required(ErrorMessage = "Picking the winning team is required"), DisplayName("Team winner 1 - RCB or 2 - SRH")]
        public int TeamPick { get; set; }
        public int UserId { get; set; }
        public DateTime AddDt { get; set; }
        public DateTime UpdateDt { get; set; }
    }

    public class LeaderBoardPick : PlayerPick
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }
    }

    public class ResultRow : PlayerPick
    {
        public string DisplayName { get; set; }
        public new int OversChase { get; set; }
        public int TotalPoints { get; set; }
    }

    public class ResultPick : PlayerPick
    {
        public int GameId { get; set; }
    }
}
