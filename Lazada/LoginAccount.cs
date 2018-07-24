using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xNet.Net;

namespace Lazada
{
    class LoginAccount
    {
        public bool IsSuccessful(ref HttpRequest request, Account account)
        {
            var config = new ConfigXNet();
            config.ConfigDefault(ref request);
            string html = request.Get("https://member.lazada.vn/user/login").ToString();
            config.AddHeaderXCSRFToken(ref request);
            string contentPost = "{\"email\":\""+ account.Email +"\",\"password\":\"" + account.Password +"\"}";
            html = request.Post("https://member.lazada.vn/user/api/login", contentPost, "application/json").ToString();
            if (html.Contains("\"success\":true"))
                return true;
            return false;
        }
    }
}
