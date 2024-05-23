using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
    public abstract class Offer : IOffer
    {
        public string major { get; set; }
        public string? date { get; set; }
        public string location { get; set; }

        protected IWebElement parentElement;
        public Offer(IWebElement parentElement)
        {
            this.parentElement = parentElement;
            this.date = getDate();
            this.location = getLocation();
            this.major = getMajor();
        }

        public abstract string getMajor();
        protected abstract string? getDate();
        protected abstract string getLocation();
    }

    public class KonsyliumOffer : Offer
    {
        public KonsyliumOffer(IWebElement parentElement) : base(parentElement) { }
        public override string getMajor() { return parentElement.FindElement(By.ClassName("spec")).Text; }
        protected override string getLocation() { return parentElement.FindElement(By.ClassName("workplace")).Text; }
        protected override string getDate() { return parentElement.FindElement(By.ClassName("time-ago")).GetAttribute("data-time-ago"); }
    }



    public class KlinikaOffer : Offer
    {
        public KlinikaOffer(IWebElement parentElement) : base(parentElement) { }
        public override string getMajor() { return parentElement.FindElement(By.XPath("div/div[1]/h2/span[2]")).Text;}
        protected override string getLocation() { return parentElement.FindElement(By.XPath("div/div[4]/div/p[2]")).Text; }
        protected override string? getDate() { return null; }
    }
}
