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
/*    public class Offer
    {
        public string major;
        public string? date;
        public string location;
        [JsonIgnore]
        public IWebElement parentElement;

        public Offer(IWebElement parentElement)
        {
            this.parentElement = parentElement;
        }


        public string Desc()
        {
            return JsonConvert.SerializeObject(this);
        }
    }*/

/*    public class KonsyliumOffer1 : Offer
    {
        public KonsyliumOffer1(IWebElement parentElement)
        {
            this.major = parentElement.FindElement(By.ClassName("spec")).Text;
            this.location = parentElement.FindElement(By.ClassName("workplace")).Text;
            this.date = parentElement.FindElement(By.ClassName("time-ago")).GetAttribute("data-time-ago");

        }
    }*/

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
}
