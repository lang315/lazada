using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xNet.Net;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.IO;

namespace Lazada
{
    class RemoveProduct
    {
        private string HTML { get; set; }
        private JObject JProducts { get; set; }


        public RemoveProduct()
        {

        }

        public void Remove(HttpRequest request)
        {
            string html = request.Get("https://www.lazada.vn/cart/").ToString();
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var listProduct = doc.DocumentNode.SelectNodes("//span[@class='productlink']");
            foreach (var product in listProduct)
            {
                string linkRemove = product.SelectSingleNode(".//a").Attributes["href"].Value;
                html = request.Get("https://www.lazada.vn" + linkRemove).ToString();
            }
        }

        public List<string> ListItemID(JObject json)
        {
            List<string> lstItemID = new List<string>();

            var jsonData = json["module"]["data"].ToString();

            Regex regexText = new Regex("item_i[^\"]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            foreach (var item in regexText.Matches(jsonData))
            {
                lstItemID.Add(item.ToString());
            }
            return lstItemID;
        }

        public void Remove(ref HttpRequest request)
        {
            string htmlPageCart = request.Get("https://cart.lazada.vn/cart").ToString();
            HTML = htmlPageCart;
            HTML = Regex.Unescape(HTML);
            string stringJson = Regex.Match(HTML, "window.__initData__ = [^;]+").Value.Replace("window.__initData__ = ", "");
            JProducts = JObject.Parse(stringJson);

            var valueLinkage = ValueLinkage();
            var valueHierarchy = ValueHierarchy();

            List<string> listItemID = ListItemID(JProducts);

            foreach (var itemID in listItemID)
            {
                var config = new ConfigXNet();
                config.AddHeaderXCSRFToken(ref request);

                var jProduct = new JObject();
                var valueData = ValueData(itemID);
                jProduct.Add("operator", itemID);
                jProduct.Add("data", valueData);
                jProduct.Add("linkage", valueLinkage);
                jProduct.Add("hierarchy", valueHierarchy);
                var contentPost = jProduct.ToString();
                
                string html = request.Post("https://cart.lazada.vn/cart/api/async", contentPost, "application/json").ToString();
            }

        }

        private JObject ValueData(string itemID)
        {
            var jData = new JObject();
            string itemIDJson = JProducts["module"]["data"][itemID]["fields"].ToString();

            JObject jObject = JObject.Parse(itemIDJson);
            jObject.Add("operation", "delete");

            JToken jFields = jObject;
            JToken jType = JProducts["module"]["data"][itemID]["type"];
            itemIDJson = JProducts["module"]["data"][itemID].ToString();

            JObject jItemID = JObject.Parse(itemIDJson);

            jItemID.Remove("fields");
            jItemID.Remove("type");

            jItemID.Add("fields", jFields);
            jItemID.Add("type", jType);

            var jItem = new JObject();
            jItem.Add(itemID, (JToken)jItemID);
            return jItem;

        }

        private JObject ValueLinkage()
        {
            string linkageString = JProducts["module"]["linkage"].ToString();

            JObject jLinkage = JObject.Parse(linkageString);
            jLinkage.Remove("input");
            jLinkage.Remove("request");
            string a = jLinkage.ToString();
            return jLinkage;
        }

        private JObject ValueHierarchy()
        {
            string hierarchyString = JProducts["module"]["hierarchy"].ToString();

            var jHierachy = JObject.Parse(hierarchyString);
            jHierachy.Remove("component");
            jHierachy.Remove("root");
            return jHierachy;
        }
    }
}
