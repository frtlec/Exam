using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject.Dtos
{
    public class wiredModel
    {
        public wiredModel()
        {
            wiredItems = new List<wiredItem>();
        }
        public List<wiredItem> wiredItems { get; set; }
    }
    public class wiredItem
    {
        public string header { get; set; }
        public string article { get; set; }

    }
}
