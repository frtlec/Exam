using System;
using System.Collections.Generic;

namespace ExamProject.Models
{
    public partial class Exams
    {
        public long ExamId { get; set; }
        public string Header { get; set; }
        public string Paragraph { get; set; }
        public string UniqueId { get; set; }
    }
}
