using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xNet.Net;

namespace Lazada
{
    class ConfigXNet
    {
        public void ConfigDefault(ref HttpRequest request)
        {
            request.Cookies = new CookieDictionary();
            //request.Proxy = HttpProxyClient.Parse("127.0.0.1:8888");
            request.AddHeader("Accept", "application/json, text/plain, */*");
            request.AddHeader("Accept Encoding", "gzip, deflate, br");
            request.AddHeader(HttpHeader.AcceptLanguage, "vi,en-US;q=0.7,en;q=0.3");
            request.AddHeader("X-Requested-With", "XMLHttpRequest");
        }

        public void AddUserAgent(ref HttpRequest request, string userAgent)
        {
            request.UserAgent = userAgent;
        }

        public void AddHeaderXCSRFToken(ref HttpRequest request)
        {
            string xCSRFToken = request.Cookies["_tb_token_"];
            request.AddHeader("X-CSRF-TOKEN", xCSRFToken);
        }

        public void AddHeaderXCSRFToken(ref HttpRequest request, string html)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            string xCSRFToken = doc.DocumentNode.SelectSingleNode("//id[@id='X-CSRF-TOKEN']").Attributes["content"].Value;
            request.AddHeader("X-CSRF-TOKEN", xCSRFToken);
        }
    }
}
