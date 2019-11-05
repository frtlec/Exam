using ExamProject.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject.Data
{
    public interface IAuthRepository
    {
        UsersForClient Login(string userName, string password);
        Task<bool> UserExists(string userName);
    }
}
