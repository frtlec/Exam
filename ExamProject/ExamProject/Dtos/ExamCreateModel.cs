using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject.Dtos
{
    public class ExamCreateModel
    {
        public ExamCreateModel()
        {
            questions = new List<QuestionModel>();
        }
        public string header { get; set; }
  
        public string paragraph { get; set; }

       
        public int id { get; set; }


        public List<QuestionModel> questions { get; set; }
    }
    public class QuestionModel
    {
        public string question { get; set; }
        public string[] options { get; set; }
        public string answer { get; set; }
    }
}
