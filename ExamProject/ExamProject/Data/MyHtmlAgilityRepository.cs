﻿using ExamProject.Dtos;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ExamProject.Data
{
    public class MyHtmlAgilityRepository : IMyHtmlAgilityRepository
    {

        public HtmlDocument getDocument(string link)
        {
            Uri url = new Uri(link);

            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;

            string html = client.DownloadString(url);

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);
            return document;
        }
        public IEnumerable<string> getHrefs(HtmlDocument document, string ParentClassForA)
        {
            var allElementsWithClass = document.DocumentNode.SelectNodes("//*[contains(@class,'" + ParentClassForA + "')]"); //searches by card-component__image class 
            List<string> hrefs = new List<string>();
            foreach (var item in allElementsWithClass)
            {
                var nodesForhref = item.SelectNodes("a");//searches by a tag
                foreach (var node in nodesForhref)
                {
                    var href = node.Attributes.Where(f => f.Name == "href").FirstOrDefault();//searches by Href attribute
                    hrefs.Add(href.Value);
                }
            }
            return hrefs.Take(5);//return attr hrefs (string)
        }

        public string getHeaders(HtmlDocument document, string element)
        {
            var tags = document.DocumentNode.SelectNodes("//"+element); //searches by h1 tag

            return tags[0].InnerHtml;
        }

        public IEnumerable<string> getParagraph(HtmlDocument document, string element)
        {
            List<string> paragraphs = new List<string>();

            var paragrap = document.DocumentNode.SelectNodes("//" + element); //searches by p tag
            foreach (var item in paragrap)
            {
                paragraphs.Add(item.InnerHtml);
            }
            return paragraphs.Take(4).ToList();
        }

        public static wiredModel wiredModel { get; set; }
    }
}
