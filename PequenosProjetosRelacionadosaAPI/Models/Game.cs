using System.Collections.Generic;

namespace PequenosProjetosRelacionadosaAPI.Models
{
    public class Game
    {
        public string Name { get; set; }
        public string Released { get; set; }   // com 'd'
        public string Metacritic { get; set; }
        public List<string> Platforms { get; set; }
    }
}