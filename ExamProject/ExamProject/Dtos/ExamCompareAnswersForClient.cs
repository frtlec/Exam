using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject.Dtos
{
    public class ExamCompareAnswersForClient
    {
        public int QuestionId { get; set; }
        public string UserAnswer { get; set; }
        public bool CorrectAnswer { get; set; }

    }
}
