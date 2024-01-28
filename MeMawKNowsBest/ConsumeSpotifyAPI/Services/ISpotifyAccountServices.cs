using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumeSpotifyAPI.Services
{
    public interface ISpotifyAccountServices
    {
        Task<string> GetToken(string clientId, string clientSecret);

    }
}
