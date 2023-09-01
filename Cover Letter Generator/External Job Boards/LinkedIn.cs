using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cover_Letter_Generator.External_Job_Boards
{
    public class LinkedIn : JobBoard
    {
        public override async Task<JobInfo?> GetInfo(string url)
        {
            var html= await GetHtmlFromUrl(url);
            if (html == null)
                return null;
            try
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                // Select the element with the specified query selector
                //var selectedElement = getElement(doc, "//h1[contains(@class, 'top-card-layout__title')]");
                var jobTitle = getNodeText(getElement(doc, "//h1[contains(@class, 'top-card-layout__title')]"));
                var company = getNodeText(getElement(doc, "//a[contains(@class,'topcard__org-name-link')]"));
                var jobDescription = getNodeText(getElement(doc, "//section//div[contains(@class, 'show-more-less-html__markup')]"));
                Console.WriteLine(jobTitle);
                Console.WriteLine(company);
                Console.WriteLine(jobDescription);
                return new JobInfo()
                {
                    JobTitle = jobTitle,
                    JobDescription = jobDescription,
                    Company = company,
                };
            }
            catch
            {
                return null;
            }
        }

        public override bool isJobBoard(string url) => new Regex(@"https://www.linkedin.com/jobs/view/\d+").IsMatch(url);
    }
}
