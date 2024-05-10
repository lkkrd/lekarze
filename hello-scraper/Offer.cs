using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V122.DOM;
using Json.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace hello_scraper
{
    internal class Offer
    {
        public string Xpath;
        public string Major;
        public string Date;
        public string Location;
        [JsonIgnore]
        public IWebElement ParentElement;
        public Offer(IWebElement parentElement)
        {
            this.ParentElement = parentElement;
            this.Major = ParentElement.FindElement(By.ClassName("spec")).Text;
            this.Location = ParentElement.FindElement(By.ClassName("workplace")).Text;
            this.Date = ParentElement.FindElement(By.ClassName("time-ago")).GetAttribute("data-time-ago");
        }

        public string Desc()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
