using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using xNet.Net;

namespace Lazada
{
    class RegisterAccount
    {
        public bool IsSuccessful(ref HttpRequest request, Account account)
        {
            var config = new ConfigXNet();
            config.ConfigDefault(ref request);
            if (EmailWasUsed(ref request, account.Email))
            {
                Debug.WriteLine(account.Email, "Trùng");
                return false;
            }
            Thread.Sleep(3000);
            config.AddHeaderXCSRFToken(ref request);
            string contentPost = "{\"email\":\"" + account.Email + "\",\"password\":\"" + account.Password + "\",\"re-password\":\"" + account.Password + "\",\"name\":\"" + account.FullName + "\",\"enableNewsletter\":true,\"month\":\"" + account.Month + "\",\"day\":" + account.Day + ",\"year\":" + account.Year + ",\"birthday\":\"" + account.BirthDay + "\",\"gender\":\"" + account.Gender + "\",\"loading\":\"false\"}";
            //request.Proxy = HttpProxyClient.Parse("127.0.0.1:8888");
            string html = request.Post("https://member.lazada.vn/user/api/register", contentPost, "application/json").ToString();
            if (html.Contains("\"success\":true"))
                return true;
            return false;
        }

        public bool EmailWasUsed(ref HttpRequest request, string email)
        {
            string html = request.Get("https://member.lazada.vn/user/api/checkEmailUsage?email=" + email).ToString();
            if (html.Contains("NONE"))
                return false;
            return true;
        }
    }
}
