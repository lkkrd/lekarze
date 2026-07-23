using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Json.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenQA.Selenium.Internal;
using System.Text.RegularExpressions;
using OpenQA.Selenium.DevTools.V123.Page;
using System.Linq.Expressions;
using OpenQA.Selenium.DevTools.V124.Storage;

namespace hello_scraper
{

    public interface IOffer
    {
        string major { get; set; }
        string? date { get; set; }
        string location { get; set; }
        decimal? salary { get; set; }

    }
    public abstract class Offer : IOffer
    {
        public string major { get; set; }
        public string? date { get; set; }
        public string location { get; set; }
        public decimal? salary { get; set; }
        public string id { get; set; }
        public string website { get; set; }

        protected IWebElement parentElement;
        public Offer(IWebElement parentElement)
        {
            this.parentElement = parentElement;
            this.date = getDate();
            this.location = getLocation();
            this.major = getMajor();
            this.salary = getSalary();
            this.id = getId();
        }
        public abstract string getMajor();
        protected abstract string? getDate();
        protected abstract string getLocation();
        protected abstract decimal? getSalary();
        protected abstract string getId();
        
    }

    public class KonsyliumOffer : Offer
    {
        public KonsyliumOffer(IWebElement parentElement) : base(parentElement)
        {
            this.website = "Konsulium24.pl";
        }
        public override string getMajor() { return parentElement.FindElement(By.ClassName("spec")).Text; }
        protected override string getLocation() { return parentElement.FindElement(By.ClassName("workplace")).Text; }
        protected override string? getDate() { return parentElement.FindElement(By.ClassName("time-ago")).GetAttribute("data-time-ago"); }
        protected override decimal? getSalary() { return null; }
        protected override string getId()
        {
            var href = parentElement.FindElement(By.CssSelector("a")).GetAttribute("href");
            Match match = Regex.Match(href, $"\\d+$");
            return "konsylium-" + match.Value;
        }
    }



    public class KlinikaOffer : Offer
    {
        public KlinikaOffer(IWebElement parentElement) : base(parentElement)
        {
            this.website = "Klinikaofert.pl";
        }
        
        public override string getMajor() { return parentElement.FindElement(By.XPath("div/div[1]/h2/span[2]")).Text; }
        protected override string getLocation() { return parentElement.FindElement(By.XPath("div/div[4]/div/p[2]")).Text; }

        protected override string? getDate() { return null; }
        protected override decimal? getSalary()
        {
            try
            {
                string str_salary = parentElement.FindElement(By.XPath("div/div[3]/div/p[2]")).Text;
                Match match = Regex.Match(str_salary, @"[\d.,]+");
                if (match.Success)
                {
                    return decimal.Parse(match.Value);
                }
                else
                {
                    return null;
                }
            }
            catch { return null; }
        }
        protected override string getId()
        {
            IWebElement a = parentElement.FindElement(By.XPath(".."));
            string url = a.GetAttribute("href");
            Match match = Regex.Match(url, $"/([^/]+)/?$");
            if (match.Success) { return "klinika-" + match.Value.Replace("/", ""); }
            else { return ""; }
        }
    }
}
