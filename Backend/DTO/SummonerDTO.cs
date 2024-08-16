using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTO
{
    public class AccountDTO
    {
        public string puuid { get; set; }
        public string gameName { get; set; }
        public string tagLine { get; set; }
    }
    public class SummonerDTO
    {
        public string id { get; set; }
        public string accountId { get; set; }
        public int profileIconId { get; set; }
        public int summonerLevel { get; set; }
    }

    public class LeagueDTO
    {
        public string leagueId { get; set; }
        public string queueType { get; set; }
        public string tier { get; set; }
        public string rank { get; set; }
        public int leaguePoints { get; set; }
        public int wins { get; set; } = 0;
        public int losses { get; set; } = 0;
    }

    public class TopChampionMastery
    {
        public int championId { get; set; }
        public int championLevel { get; set; }
        public int championPoints { get; set; }
    }
    public class CombinedSummonerDTO
    {
        public AccountDTO Account { get; }
        public SummonerDTO Summoner { get; }
        public List<LeagueDTO> League { get; }

        public ChampionDTO TopChampion { get; }

        public CombinedSummonerDTO(AccountDTO account, SummonerDTO summoner, List<LeagueDTO> league, ChampionDTO topChampion)
        {
            Account = account ?? throw new ArgumentNullException(nameof(account));
            Summoner = summoner ?? throw new ArgumentNullException(nameof(summoner));
            League = league ?? throw new ArgumentNullException(nameof(league));
            TopChampion = topChampion ?? throw new ArgumentNullException(nameof(topChampion));
        }
    }
}