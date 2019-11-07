using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject.Dtos
{
    public class ExamsQuestionsForClient
    {
        public long QuestionId { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string Question { get; set; }
        public long? ExamId { get; set; }
    }
}
