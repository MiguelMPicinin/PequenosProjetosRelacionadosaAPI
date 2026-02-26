using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PequenosProjetosRelacionadosaAPI.Models
{
    public class ConversionResult
    {
        public string From { get; set; }
        public string To { get; set; }
        public decimal Amount { get; set; }
        public decimal Converted { get; set; }
        public decimal Rate { get; set; }
    }
}
