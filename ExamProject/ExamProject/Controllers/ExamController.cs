using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ExamProject.Data;
using ExamProject.Dtos;
using ExamProject.Models;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamProject.Controllers
{
   [Authorize]
    public class ExamController : Controller
    {
        private IMyHtmlAgilityRepository _myHtmlAgilityRepository;
        private IAppRepository _appRepository;
        private examDBContext _context;
        private string link= "https://www.wired.com"; //Website link
        public ExamController(IMyHtmlAgilityRepository myHtmlAgilityRepository,IAppRepository appRepository)
        {
            _myHtmlAgilityRepository = myHtmlAgilityRepository;
            _appRepository = appRepository;
            _context = new examDBContext();
        }
        public IActionResult Index()
        {
            string htmlClass = "card-component__description";//we are looking for members of this class in the document
            HtmlDocument documentHomePage = _myHtmlAgilityRepository.getDocument(link);
            IEnumerable<WiredLink> wiredLinks=_myHtmlAgilityRepository.getHrefs(documentHomePage, htmlClass);//collecting links for last 5 posts
            ViewBag.option = wiredLinks;
            return View();
        }

        [HttpPost]
        public IActionResult GetWiredPostParagraph([FromBody]WiredLink wiredLink)
        {
            HtmlDocument documentPost = _myHtmlAgilityRepository.getDocument(link + wiredLink.href);//source codes are downloaded using HtmlAgilityPack
            string paragraph = _myHtmlAgilityRepository.getParagraph(documentPost, "p");
            return Json(paragraph);
        }

     
        [HttpPost]
        public IActionResult ExamCreate([FromBody] ExamCreateModel createModel)
        {
            /*
                action to create questions
             
             */


            string uniqueId = String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000);

            Exams exams = new Exams
            {
                Header = createModel.header,
                Paragraph = createModel.paragraph,
                UniqueId = uniqueId,
                CreationDate = DateTime.Now.ToShortDateString()
            };

            try
            {
                _appRepository.Add(exams);
                bool examsSave = _appRepository.SaveAll();
                if (!examsSave)
                {
                    return Json(new { message = "error" });
                }
                var data = _context.Exams.Select(f => new { f.ExamId, f.UniqueId }).Where(f => f.UniqueId == uniqueId).FirstOrDefault();
                var examId = data.ExamId;
             

                foreach (var item in createModel.questions)
                {
                    ExamQuestions examQuestions = new ExamQuestions();
                    examQuestions.ExamId = examId;
                    examQuestions.Answer = item.answer;
                    examQuestions.Question = item.question;
                    if (item.options.Length!=4)
                    {
                        //If item.option.length is not equal to four. error
                        return BadRequest("missing option");
                    }
                    examQuestions.OptionA = item.options[0];
                    examQuestions.OptionB = item.options[1];
                    examQuestions.OptionC = item.options[2];
                    examQuestions.OptionD = item.options[3];
                    _appRepository.Add(examQuestions);
                }
               
                bool EquestionSave = _appRepository.SaveAll();
                if (!EquestionSave)
                {
                    return Json(new { message = "error" });
                }

                //int id=_context.Exams.Where(f=>f.)

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return Json(new {  ok=true });
        }

        

    }
  
}