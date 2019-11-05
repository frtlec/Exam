using System;
using System.Collections.Generic;

namespace ExamProject.Models
{
    public partial class Users
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public string UserPassword { get; set; }
    }
}
