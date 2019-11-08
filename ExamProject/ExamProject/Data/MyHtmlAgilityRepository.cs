using ExamProject.Dtos;
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
        public IEnumerable<WiredLink> getHrefs(HtmlDocument document, string ParentClassForA)
        {
            var allElementsWithClass = document.DocumentNode.SelectNodes("//*[contains(@class,'" + ParentClassForA + "')]"); //searches by card-component__image class 
            List<WiredLink> hrefs = new List<WiredLink>();
            foreach (var item in allElementsWithClass)
            {
                var nodesForhref = item.SelectNodes("a");//searches by a tag
                var i = 0;
                foreach (var node in nodesForhref)
                {
                    if (i%2==1)
                    {
                        WiredLink wiredLink = new WiredLink();
                        var href = node.Attributes.Where(f => f.Name == "href").FirstOrDefault();//searches by Href attribute
                        var h2 = node.FirstChild;
                        wiredLink.href = href.Value;
                        wiredLink.header = h2.InnerText;
                        hrefs.Add(wiredLink);
                    }
               
                    i++;

                }
            }
            var x= hrefs.Take(5); 
            return x;//return attr hrefs (string)
        }

        public string getHeaders(HtmlDocument document, string element)
        {
            var tags = document.DocumentNode.SelectNodes("//"+element); //searches by h1 tag

            return tags[0].InnerHtml;
        }

        public string getParagraph(HtmlDocument document, string element)
        {
            string paragraphs = "";

            var paragraph = document.DocumentNode.SelectNodes("//" + element); //searches by p tag
            var i = 0;
            foreach (var item in paragraph)
            {
                if (i>4)
                {
                    break;
                }
                paragraphs+=item.InnerHtml;
                i++;
            }
            return paragraphs;
        }

        public static WiredModel wiredModel { get; set; }
    }
}
