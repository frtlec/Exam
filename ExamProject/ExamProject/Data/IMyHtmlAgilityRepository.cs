using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject.Data
{
    
    public interface IMyHtmlAgilityRepository
    {
        HtmlDocument getDocument(string link);
        List<string> getHrefs(HtmlDocument document,string ParentClassForA);
        string getHeaders(HtmlDocument document,string AttrID );


    }
}
