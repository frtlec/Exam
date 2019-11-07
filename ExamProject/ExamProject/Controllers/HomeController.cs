using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExamProject.Models;
using Microsoft.AspNetCore.Authorization;
using ExamProject.Dtos;
using ExamProject.Data;

namespace ExamProject.Controllers
{
   [Authorize]
    public class HomeController : Controller
    {
        private examDBContext _context;
        private IAppRepository _appRepository;
        public HomeController(IAppRepository appRepository)
        {
            _context = new examDBContext();
            _appRepository = appRepository;
        }
        public IActionResult Index()
        {
            List<ExamsForClient> examsForClient = _context.Exams.Select(f=>new ExamsForClient {
                ExamId=f.ExamId,
                Header=f.Header,
                Paragraph=f.Paragraph,
                UniqueId=f.UniqueId,
                CreationDate=f.CreationDate
            }).ToList();
            return View(examsForClient);
        }
        [HttpGet]
        public IActionResult ExamSolve(int id)
        {
            ExamsForClient examsForClient = _context.Exams
            .Select(f=> new ExamsForClient {
                ExamId=f.ExamId,
                Header=f.Header,
                Paragraph=f.Paragraph,
                UniqueId=f.UniqueId
            }).FirstOrDefault(f=>f.ExamId==id);
            List<ExamsQuestionsForClient> examsQuestionsForClient = _context.ExamQuestions.Select(f => new ExamsQuestionsForClient
            {
                ExamId=f.ExamId,
                QuestionId=f.QuestionId,
                OptionA=f.OptionA,
                OptionB=f.OptionB,
                OptionC=f.OptionC,
                OptionD=f.OptionD,
                Question=f.Question

            }).Where(f=>f.ExamId==id).ToList();

            //
            ViewBag.Exam = examsForClient;
            ViewBag.Questions = examsQuestionsForClient;
            return View();
        }
        [HttpPost]
        public IActionResult ExamCompareAnswers([FromBody] List<ExamCompareAnswersModel> model)
        {
            List<ExamQuestions> examQuestions = _context.ExamQuestions.ToList();
         
            if (examQuestions.Count<1)
            {
                return null;
            }

            List<ExamCompareAnswersForClient> compares = new List<ExamCompareAnswersForClient>();
            foreach (var item in model)
            {
                //comparing user answers
                ExamCompareAnswersForClient compare = new ExamCompareAnswersForClient();
                compare.QuestionId = item.QuestionId;
                compare.UserAnswer = item.UserAnswer;
                compare.CorrectAnswer = examQuestions.Where(f => f.QuestionId == item.QuestionId && f.Answer.ToLower() == item.UserAnswer.ToLower()).Any();

                compares.Add(compare);
            }

            return Json(compares);

        }

        [HttpGet]
        public IActionResult ExamDelete(int id)
        {
            try
            {
                Exams exams = _context.Exams.Where(f => f.ExamId == id).FirstOrDefault();
                List<ExamQuestions> examQuestions = _context.ExamQuestions.Where(f => f.ExamId == id).ToList();
                _appRepository.Delete(exams);
                foreach (var item in examQuestions)
                {
                    _appRepository.Delete(item);
                }
                _appRepository.SaveAll();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                return RedirectToAction("Index");
            }


        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
