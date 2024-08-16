using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTO;

namespace Backend.Services
{
    public interface IChampion
    {
        Task<Dictionary<string, ChampionDTO>> GetChampions();
        Task<ChampionDTO> GetChampion(string championName);
    }
}