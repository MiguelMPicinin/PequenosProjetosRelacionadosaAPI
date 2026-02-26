using PequenosProjetosRelacionadosaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PequenosProjetosRelacionadosaAPI.Services
{
    public interface IQuizService
    {
        Task<List<QuizQuestion>> GetQuestionsAsync(int amount);
        Task<QuizResult> CheckAnswersAsync(List<QuizAnswer> answers);
    }
}
