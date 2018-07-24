using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using xNet.Net;

namespace Lazada
{
    class InteractProduct
    {
        private Product Product { get; set; }

        public InteractProduct()
        {

        }

        public InteractProduct(Product product)
        {
            Product = product;
        }

        public bool AddToCart(ref HttpRequest request)
        {
            string contentPost = "[{\"itemId\":\"" + Product.ItemID + "\",\"skuId\":\"" + Product.SKUID + "\",\"quantity\":1}]";
            string html = request.Post("https://cart.lazada.vn/cart/api/add", contentPost, "application/json").ToString();
            if (html.Contains("\"success\":true"))
                return true;
            return false;
        }

        public bool AddToWishList(ref HttpRequest request)
        {

            string contentPost = "{}";
            var configXNet = new ConfigXNet();
            //request.Proxy = HttpProxyClient.Parse("127.0.0.1:8888");
            configXNet.AddHeaderXCSRFToken(ref request);
            //request.AddHeader("Accept", "application/json, text/plain, */*");
            //request.AddHeader("Accept Encoding", "gzip, deflate, br");
            //request.AddHeader(HttpHeader.AcceptLanguage, "vi,en-US;q=0.7,en;q=0.3");
            //request.AddHeader("X-Requested-With", "XMLHttpRequest");

            string link = "https://my.lazada.vn/wishlist/api/addItem?itemId=" + Product.ItemID + "&skuId=" + Product.SKUID;
            string html = request.Post("https://my.lazada.vn/wishlist/api/addItem?itemId=" + Product.ItemID + "&skuId=" + Product.SKUID, contentPost, "application/json;charset=utf-8").ToString();
            if (html.Contains("\"success\":true"))
                return true;
            return false;
        }

        public void ViewProduct(ref HttpRequest request, string linkProduct)
        {
            string html = request.Get(linkProduct).ToString();

            string pItem = FindSKUInfo("_p_item[^&]+", 1, html).Replace("_p_item=", "");
            string regCategoryId = FindSKUInfo("\"regCategoryId\":\"[^\"]+", 0, html).Replace("\"regCategoryId\":\"", "");
            string sellerId = FindSKUInfo("&seller_id=[^&]+", 0, html).Replace("&seller_id=", "");
            string gmkey = "EXP";
            string gokey = "_g_encode=utf-8&_p_item=" + pItem + "&_p_prod=" + Product.ItemID + "&_p_sku=" + Product.SKUID + "&spm=a2o4n.pdp.main_page.d1&cfgver=1.0&lzd_pg_type=pdp&_p_lang=vi&lzd_layout=desktop&_p_usertype=new&_d_cookie=true&_e_cookie=true&_p_version=0.9.26&_d_cookie2=true&_p_typ=pdp&_p_ispdp=1";
            gokey += "&_p_item=" + pItem + "&_p_prod=" + Product.ItemID + "&_p_sku=" + Product.SKUID + "&_p_slr=" + sellerId + "&_p_voya=1&_p_reg_cateId=" + regCategoryId;
            gokey = gokey.Replace("=", "%3D").Replace("&", "%26");
            string linkView = "https://sg.mmstat.com/Lazada_PDP.PDP_Page.pdp_sku_exposure?cache=&gmkey=" + gmkey + "&gokey=" + gokey + "&cna=&_slog=0&spm-cnt&logtype=2";
            request.Referer = "https://www.lazada.vn/";
            html = request.Get(linkView).ToString();
        }

        private string FindSKUInfo(string patternRegex, int indexSelect, string html)
        {
            var regex = new Regex(patternRegex, RegexOptions.IgnoreCase);
            return regex.Matches(html)[indexSelect].Value;
        }
    }
}
