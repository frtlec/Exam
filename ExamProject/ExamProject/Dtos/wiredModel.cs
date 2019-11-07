using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject.Dtos
{
    public class WiredModel
    {
        public WiredModel()
        {
            wiredItems = new List<wiredItem>();
        }
        public List<wiredItem> wiredItems { get; set; }
    }
    public class wiredItem
    {
        [Key]
        public int wiredId { get; set; }
        public string header { get; set; }
        public string paragraph { get; set; }

    }
}
