using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cover_Letter_Generator.External_Job_Boards
{
    public abstract class JobBoard
    {
        public abstract bool isJobBoard(string url);


        internal string getNodeText(HtmlNode? node)
        {
            if(node!=null)
                return WebUtility.HtmlDecode(node.InnerText).Trim();
            return null;
        }
        internal HtmlNode? getElement(HtmlDocument doc, string selector)
        {
            return doc.DocumentNode.SelectSingleNode(selector);
        }

        internal async Task<string> GetHtmlFromUrl(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        Console.WriteLine($"HTTP request failed with status code {response.StatusCode}");
                        return null;
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"HTTP request error: {e.Message}");
                    return null;
                }
            }
        }

        public virtual async Task<JobInfo?> GetInfo(string url)
        {
            throw new Exception("JobBoard.GetInfo Must Be Overwritten");
            return null;
        }
    }
}
