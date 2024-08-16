using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTO;

namespace Backend.Services
{
    public interface ISummoner
    {
        Task<CombinedSummonerDTO> GetSummonerInformationAsync(string summonerName, string tag);
        Task<AccountDTO> GetAccountInfo(string summonerName, string tag);
        Task<SummonerDTO> GetSummonerInfo(string puuid);
        Task<List<LeagueDTO>> GetSummonerLeagueInfo(string summonerId);
    }
}