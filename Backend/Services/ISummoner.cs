using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTO;

namespace Backend.Services
{
    public interface ISummoner
    {
        Task<CombinedSummonerDTO> GetSummonerInformation(string summonerName, string tag);
        Task<AccountDTO> GetAccountInfo(string summonerName, string tag);
        Task<SummonerDTO> GetSummonerInfo(string puuid);
        Task<List<LeagueDTO>> GetSummonerLeagueInfo(string summonerId);
        Task<List<string>> GetSummonerMatches(string puuid, int startTime, int endTime, int count, string typeQueue);
    }
}