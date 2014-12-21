using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;


namespace DmhySourceParser
{
    [ServiceContract]
    public class Service
    {
        [OperationContract]
        [WebGet]
        public List<RawResource> Test()
        {
            List<RawResource> list = Parse();
            return list;
        }

        public List<RawResource> Parse()
        {
            var matchedItems = new List<RawResource>();
            WebClient client = new WebClient();
            Console.WriteLine("Start loading dmhy page: " + DateTime.Now.ToString());
            byte[] contentData = client.DownloadData("http://share.dmhy.org/topics/list/sort_id/2");
            Console.WriteLine("End loading dmhy page: " + DateTime.Now.ToString());
            string html = Encoding.UTF8.GetString(contentData);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            HtmlNode rootNode = doc.DocumentNode.SelectSingleNode("//table[@id='topic_list']/tbody");
            if (rootNode != null)
            {
                HtmlNodeCollection selectedNodes = rootNode.SelectNodes("tr");
                if (selectedNodes != null)
                {
                    //Item lists
                    foreach (HtmlNode node in selectedNodes)
                    {
                        try
                        {
                            HtmlNode publishDateNode = node.SelectSingleNode("td[1]/span");
                            string publishDateStr = publishDateNode.InnerText;
                            DateTime publishDate = DateTime.Parse(publishDateStr);

                            HtmlNode titleNode = node.SelectSingleNode("td[@class='title']/a");
                            string title = titleNode.InnerText.Trim();

                            HtmlNode linkNode = node.SelectSingleNode("td/a[@class='download-arrow arrow-torrent']");
                            string link = "http://share.dmhy.org" + linkNode.Attributes["href"].Value;
                            matchedItems.Add(new RawResource() { Title = title, Link = link, PublishDate = publishDate });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
            return matchedItems;
        }
    }
}
