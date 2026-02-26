using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PequenosProjetosRelacionadosaAPI.Models
{
    public class QuizAnswer
    {
        public int QuestionId { get; set; }
        public int SelectedOptionIndex { get; set; }
    }
}
