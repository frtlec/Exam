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
        [Required]
        public string header { get; set; }
        [Required]
        public string paragraph { get; set; }
        public int id { get; set; }
        [Required]
        public List<QuestionModel> questions { get; set; }
    }
    public class QuestionModel
    {
        [Required]
        public string question { get; set; }
        [Required]
        public string[] options { get; set; }
        [Required]
        public string answer { get; set; }
    }
}
