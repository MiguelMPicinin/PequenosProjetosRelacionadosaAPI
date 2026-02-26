using System.Collections.Generic;
using System.Threading.Tasks;

namespace PequenosProjetosRelacionadosaAPI.Services
{
    public interface IJokeService
    {
        Task<string> GetRandomJokeAsync();
        Task<List<string>> GetAllJokesAsync();   // plural
        Task<int> GetJokeCountAsync();
    }
}