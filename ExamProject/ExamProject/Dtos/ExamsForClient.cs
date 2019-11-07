using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject.Dtos
{
    public class ExamsForClient
    {
        public long ExamId { get; set; }
        public string Header { get; set; }
        public string Paragraph { get; set; }
        public string UniqueId { get; set; }
        public string CreationDate { get; set; }
    }
}
