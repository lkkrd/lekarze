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

    public interface IOffer
    {
        string major { get; set; }
        string? date { get; set; }
        string location { get; set; }

    }

    public class KonsyliumOffer : IOffer
    {
        public string major { get; set; }
        public string? date { get; set; }
        public string location { get; set; }
        [JsonIgnore]
        public IWebElement parentElement { get; set; }

        public KonsyliumOffer(IWebElement parentElement)
        {
            this.parentElement = parentElement;
            this.major = parentElement.FindElement(By.ClassName("spec")).Text;
            this.location = parentElement.FindElement(By.ClassName("workplace")).Text;
            this.date = parentElement.FindElement(By.ClassName("time-ago")).GetAttribute("data-time-ago");
        }
    }

    public class KlinikaOffer : IOffer
    {
        public string major { get; set; }
        public string? date { get; set; }
        public string location { get; set; }
        [JsonIgnore]
        public IWebElement parentElement { get; set; }

        public KlinikaOffer(IWebElement parentElement)
        {
            this.parentElement = parentElement;
            this.major = parentElement.FindElement(By.XPath("div/div[1]/h2/span[2]")).Text;
            this.location = parentElement.FindElement(By.XPath("div/div[4]/div/p[2]")).Text;
        }




    }
}
