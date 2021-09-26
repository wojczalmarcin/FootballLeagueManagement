using Application.DTO;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Match
{
    public class MatchLogic
    {
        public async Task<List<TeamStatisticsDto>> CreateSeasonTableAsync(IEnumerable<TeamStatisticsDto> teamsStatistics, IEnumerable<MatchDto> matches)
        {
            List<Task> listOfTasks = new List<Task>();

            var matchesList = matches.ToList();
            var teamsStatisticsList = teamsStatistics.ToList();

            foreach(var team in teamsStatisticsList)
            {
                team.Points = 0;
                team.WonHome = 0;
                team.DrawnHome = 0;
                team.LostHome = 0;
                team.WonAway = 0;
                team.DrawnAway = 0;
                team.LostAway = 0;

                listOfTasks.Add(CalculateStatistics(team, matchesList));
            }

            await Task.WhenAll(listOfTasks);

            teamsStatisticsList.Sort();
            return teamsStatisticsList;
        }

        public enum MatchScore
        {
            Win = 3,
            Draw = 1,
            Defeat = 0
        }

        private Task CalculateStatistics(TeamStatisticsDto team, IEnumerable<MatchDto> matches)
        {
            return Task.Run(() =>
            {
                foreach (var match in matches)
                {
                    if (match.TeamHome.Id == team.Id)
                    {
                        StatisticsHome(team, match);
                    }
                    else if (match.TeamAway.Id == team.Id)
                    {
                        StatisticsAway(team, match);
                    }
                }
            });
        }
            
        private void StatisticsHome(TeamStatisticsDto team, MatchDto match)
        {
            team.GoalsHome += match.MatchScore.HomeGoals;
            team.GoalsLostHome += match.MatchScore.AwayGoals;

            if (match.MatchScore.HomeGoals > match.MatchScore.AwayGoals)
            {
                team.Points += 3;
                team.WonHome += 1;
            }
            else if (match.MatchScore.HomeGoals == match.MatchScore.AwayGoals)
            {
                team.Points += 1;
                team.DrawnHome += 1;
            }
            else
            {
                team.LostHome += 1;
            }
        }

        private void StatisticsAway(TeamStatisticsDto team, MatchDto match)
        {
            team.GoalsAway += match.MatchScore.AwayGoals;
            team.GoalsLostAway += match.MatchScore.HomeGoals;

            if (match.MatchScore.HomeGoals < match.MatchScore.AwayGoals)
            {
                team.Points += 3;
                team.WonAway += 1;
            }
            else if (match.MatchScore.HomeGoals == match.MatchScore.AwayGoals)
            {
                team.Points += 1;
                team.DrawnAway += 1;
            }
            else
            {
                team.LostAway += 1;
            }
        }


    }
}
