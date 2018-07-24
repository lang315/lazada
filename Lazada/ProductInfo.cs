using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Lazada
{
    class ProductInfo
    {

        private string Html { get; set; }
        HtmlDocument Doc = new HtmlDocument();
        private HtmlNode ProductNode { get; set; }

        public ProductInfo(string html)
        {
            Html = html;
            Doc.LoadHtml(Html);
            ProductNode = Doc.DocumentNode.SelectSingleNode("//*[@id=\"prodinfo\"]/div[1]/div/div[2]/div[1]/div");
        }


        public string ConfigSKU()
        {
            return ProductNode.Attributes["data-config-sku"].Value;
        }

        public string SimpleSKU()
        {
            return ProductNode.Attributes["data-simple-sku"].Value;
        }

        public string ConfigID()
        {
            return Doc.DocumentNode.SelectSingleNode("//input[@id='config_id']").Attributes["value"].Value;
        }

        public string FormToken()
        {
            return Doc.DocumentNode.SelectSingleNode("//input[@name='FORM_TOKEN']").Attributes["value"].Value;
        }


    }
}
