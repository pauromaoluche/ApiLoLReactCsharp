using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTO;

namespace Backend.Services
{
    public interface IMatch
    {
        Task<List<MatchResponse>> GetMatcheInfo(string[] matchIds);
    }
}