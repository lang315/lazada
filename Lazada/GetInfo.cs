using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using xNet.Net;

namespace Lazada
{
    class GetInfo
    {
        HtmlDocument _doc = new HtmlDocument();
        public string Day { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }

        public GetInfo()
        {
            HttpRequest request = new HttpRequest();

            request.Cookies = new CookieDictionary();
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";
            request.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            request.AddHeader("Accept-Language", "vi,en-US;q=0.7,en;q=0.3");
            request.AddHeader("Encoding", "gzip, deflate");
            string html = request.Get("https://www.fakenamegenerator.com/").ToString();
            _doc.LoadHtml(html);

            string birthday = _doc.DocumentNode.SelectSingleNode("/html/body/div[2]/div/div/div[1]/div/div[3]/div[2]/div[2]/div/div[2]/dl[6]/dd").InnerText;
            DateTime date = Convert.ToDateTime(birthday);
            Day = date.Day.ToString();
            Month = date.Month.ToString();
            Year = date.Year.ToString();
        }

        public string FullName()
        {
            string fullname = _doc.DocumentNode.SelectSingleNode("/html/body/div[2]/div/div/div[1]/div/div[3]/div[2]/div[2]/div/div[1]/h3").InnerText;
            fullname = fullname.Split(' ').First() + " " + fullname.Split(' ').Last();
            return fullname;
        }

        //public string Password()
        //{
        //    return _doc.DocumentNode.SelectSingleNode("/html/body/div[2]/div/div/div[1]/div/div[3]/div[2]/div[2]/div/div[2]/dl[11]/dd").InnerText;
        //}


    }
}
