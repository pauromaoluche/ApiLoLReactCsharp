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
        public string accountId { get; set; }
        public int profileIconId { get; set; }
        public int summonerLevel { get; set; }
    }

    public class CombinedSummonerDTO
    {
        public AccountDTO Account { get; }
        public SummonerDTO Summoner { get; }

        public CombinedSummonerDTO(AccountDTO account, SummonerDTO summoner)
        {
            Account = account ?? throw new ArgumentNullException(nameof(account));
            Summoner = summoner ?? throw new ArgumentNullException(nameof(summoner));
        }
    }
}