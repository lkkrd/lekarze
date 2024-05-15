using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V122.Network;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hello_scraper
{
    public abstract class Scraper
    {
        public ChromeDriver driver = new ChromeDriver();
        public string offerClassName { get; set; }
        public int pageCount { get; set; } // ile stron można zescrapować
        public string pageUrl { get; set; }

        public abstract void changePage(int pageNum);

        public int setPageCount()
        {
            changePage(1);
            var max = driver.FindElement(By.ClassName("page-away-9")).Text;
            return Convert.ToInt32(max);
        }

        public void resetPage() { driver.Url = pageUrl; }

        public List<Offer> GetOfferListFromPage(int pageNum) // zwraca oferty z konkretnej strony
        {
            changePage(pageNum);
            var offerList = new List<Offer>();
            var offerIWebElementList = driver.FindElements(By.ClassName(this.offerClassName));
            foreach (IWebElement element in offerIWebElementList)
            {
                Offer o = new Offer(element);
                offerList.Add(o);
            }

            return offerList;
        }
        
        public List <Offer> GetOfferList() // zwraca oferty ze wszystkich stron
        {
            var offerList = new List<Offer>();
            for (int i = 1; i < this.pageCount + 1; i++ )
            {
                offerList.AddRange(GetOfferListFromPage(i));
            }

            return offerList;
        }

        public IEnumerable<object> getMajorScores(List<Offer> offerList)
        {
            return offerList
                .GroupBy(offer => offer.Major)
                .Select(group => new { key = group.Key, val = group.Count() }) ;
        }

        public void exportToJson(string filepath = "scores.json")
        {
            string json = JsonConvert.SerializeObject(getMajorScores(GetOfferList()), Formatting.Indented);
            File.WriteAllText(filepath, json);
        }

    }





    class KonsyliumScraper : Scraper
    {
        public KonsyliumScraper()
        {
        this.pageUrl = "https://konsylium24.pl/kompendium24/praca/oferty-pracy/lista?gad_source=1&page=1#custom-list";
        this.offerClassName = "list-group-offer";
        this.pageCount = setPageCount();
        }



        public override void changePage(int pageNum) { this.driver.Url = $"https://konsylium24.pl/kompendium24/praca/oferty-pracy/lista?page={Convert.ToString(pageNum)}#custom-list"; }

        public IWebElement returnClass(string className)
        {
            return driver.FindElement(By.ClassName(className));
        }
    }
}
