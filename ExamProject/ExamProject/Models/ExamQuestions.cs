using System;
using System.Collections.Generic;

namespace ExamProject.Models
{
    public partial class ExamQuestions
    {
        public long QuestionId { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string Answer { get; set; }
        public long? ExamId { get; set; }
    }
}
