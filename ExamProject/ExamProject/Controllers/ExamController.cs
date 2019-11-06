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
using HtmlAgilityPack;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamProject.Controllers
{
    [Authorize]
    public class ExamController : Controller
    {
        private IMyHtmlAgilityRepository _myHtmlAgilityRepository;
       
        public ExamController(IMyHtmlAgilityRepository myHtmlAgilityRepository)
        {
            _myHtmlAgilityRepository = myHtmlAgilityRepository;
        }
        public IActionResult Index()
        {
            string link = "https://www.wired.com";  //link değişkenine çekeceğimiz web sayafasının linkini yazıyoruz.
            string htmlClass = "card-component__image";
            HtmlDocument documentHomePage = _myHtmlAgilityRepository.getDocument(link);
            IEnumerable<string> hrefs=_myHtmlAgilityRepository.getHrefs(documentHomePage, htmlClass);

            #region getPost
           

            wiredModel wiredModel = new wiredModel();

            //going to posts
            int i = 0;
            foreach (var item in hrefs)
            {
                wiredItem wiredItem = new wiredItem();
                HtmlDocument documentPost = _myHtmlAgilityRepository.getDocument(link+item);
                wiredItem.wiredId = i;
                wiredItem.header = _myHtmlAgilityRepository.getHeaders(documentPost, "h1");
                wiredItem.paragraph = _myHtmlAgilityRepository.getParagraph(documentPost,"p");
                wiredModel.wiredItems.Add(wiredItem);
                i++;
            }
            #endregion


            MyHtmlAgilityRepository.wiredModel = wiredModel;


            return View(wiredModel);
        }
        [HttpGet]
        public IActionResult ExamCreate(int id)
        {
            wiredModel wiredModel = MyHtmlAgilityRepository.wiredModel;
            if (wiredModel!=null)
            {
                var exam = wiredModel.wiredItems.Where(f => f.wiredId == id).FirstOrDefault();
                ViewBag.exam = exam;
                ViewBag.id = id;
                return View();
            }
            return RedirectToAction("Index");

        }
        [HttpPost]
        public IActionResult ExamCreate([FromBody] ExamCreateModel createModel)
        {
            return Json(createModel);
        }
    }
  
}