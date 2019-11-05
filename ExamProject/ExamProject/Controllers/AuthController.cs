using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ExamProject.Data;
using ExamProject.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ExamProject.Controllers
{
    public class AuthController : Controller
    {
        private IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        public IActionResult Login()
        {


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromForm]LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            UsersForClient user = _authRepository.Login(loginModel.Username,loginModel.Password);
            if (user == null)
            {
                ViewBag.message = "Wrong username or password";
                return View();
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginModel.Username)
            };

            var userIdentity = new ClaimsIdentity(claims, "login");

            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
             await HttpContext.SignInAsync(principal);

                //Just redirect to our index after logging in. 
            return RedirectToAction("Index", "Exam");
           
        }

    }
}