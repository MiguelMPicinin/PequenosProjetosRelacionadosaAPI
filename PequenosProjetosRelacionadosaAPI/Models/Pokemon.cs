using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PequenosProjetosRelacionadosaAPI.Models
{
    public class Pokemon
    {
        public string Name { get; set; }
        public List<string> Types { get; set; }
        public List<string> Abilities { get; set; }
        public Dictionary<string, int> Stats { get; set; }
    }
}
