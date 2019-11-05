using ExamProject.Dtos;
using ExamProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject.Data
{
 
    public class AuthRepository:IAuthRepository
    {
          private examDBContext _context;

        public AuthRepository(examDBContext context)
        {
            _context = context;
        }

        public  UsersForClient Login(string userName, string password)
        {
            var user = _context.Users.FirstOrDefault(f => f.Username == userName);
            if (user == null)
            {
                return null;
            }
            return  new UsersForClient
            {
                Username=user.Username,
                UserPassword=user.UserPassword
            };
        }


        public Task<bool> UserExists(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
