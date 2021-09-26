using Application.Services.Match;
using AutoMapper;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class TeamStatisticsDto : IComparable<TeamStatisticsDto>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int MatchesPlayed { get; set; }

        public int Points { get; set; }

        public int GoalsHome { get; set; }

        public int GoalsAway { get; set; }

        public int GoalsLostHome { get; set; }

        public int GoalsLostAway { get; set; }

        public int WonHome { get; set; }

        public int DrawnHome { get; set; }

        public int LostHome { get; set; }

        public int WonAway { get; set; }

        public int DrawnAway { get; set; }

        public int LostAway { get; set; }

        public int CompareTo(TeamStatisticsDto other)
        {
            if (this.Points < other.Points)
                return 1;
            else if (this.Points > other.Points)
                return -1;

            int goalsScored = this.GoalsAway + this.GoalsHome;
            int goalsLost = this.GoalsLostAway - this.GoalsLostHome;

            int otherGoalsScored = other.GoalsAway + other.GoalsHome;
            int otherGoalsLost = other.GoalsLostAway + other.GoalsLostHome;

            if (goalsScored - goalsLost < otherGoalsScored - otherGoalsLost)
                return 1;
            else if (goalsScored - goalsLost > otherGoalsScored - otherGoalsLost)
                return -1;

            if (goalsScored < otherGoalsScored)
                return 1;
            else if (goalsScored > otherGoalsScored)
                return -1;

            return 0;
        }
    }
}
