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
using Microsoft.AspNetCore.Mvc;

namespace ExamProject.Controllers
{
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
            List<string> hrefs=_myHtmlAgilityRepository.getHrefs(documentHomePage, htmlClass);

            #region getPost
           

            wiredModel wiredModel = new wiredModel();
            wiredItem wiredItem = new wiredItem();
            //going to posts
            foreach (var item in hrefs)
            {
                HtmlDocument documentPost = _myHtmlAgilityRepository.getDocument(link+item);
                wiredItem.header = _myHtmlAgilityRepository.getHeaders(documentPost, "h1");
                wiredItem.article = "";
                wiredModel.wiredItems.Add(wiredItem);
            }
            #endregion





            return View();
        }
    }
  
}