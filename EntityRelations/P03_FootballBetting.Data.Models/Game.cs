using System;
using System.Collections.Generic;

namespace P03_FootballBetting.Data.Models
{
    public class Game
    {
        public int GameId { get; set; }

        public DateTime DateTime { get; set; }

        public int AwayTeamId{ get; set; }
        public Team AwayTeam { get; set; }
        public float AwayTeamBetRate{ get; set; }
        public byte AwayTeamGoals{ get; set; }


        public decimal DrawBetRate{ get; set; }

        public  int HomeTeamId { get; set; }
        public Team HomeTeam { get; set; }
        public float HomeTeamBetRate{ get; set; }
        public byte HomeTeamGoals{ get; set; }

        public GameResult Result { get; set; }

        public ICollection<PlayerStatistic> PlayerStatistics { get; set; } = new HashSet<PlayerStatistic>();

        public ICollection<Bet> Bets { get; set; } = new HashSet<Bet>();
    }
}