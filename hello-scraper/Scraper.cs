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
        public string siteName { get; set; }

        public abstract void changePage(int pageNum);
        public abstract IOffer convertElementToOffer(IWebElement element);

        public void resetPage() { driver.Url = pageUrl; }

        public List<IOffer> GetOfferListFromPage(int pageNum) // zwraca oferty z konkretnej strony
        {
            changePage(pageNum);
            List<IOffer> offerList = new List<IOffer>();
            var offerIWebElementList = driver.FindElements(By.ClassName(this.offerClassName));
            foreach (IWebElement element in offerIWebElementList)
            {
                IOffer o = convertElementToOffer(element);
                offerList.Add(o);
            }

            return offerList;
        }
        
        public List <IOffer> GetOfferList() // zwraca oferty ze wszystkich stron
        {
            var offerList = new List<IOffer>();
            for (int i = 1; i < this.pageCount + 1; i++ )
            {
                offerList.AddRange(GetOfferListFromPage(i));
            }

            return offerList;
        }

        /*public IEnumerable<object> getMajorScores(List<Offer> offerList)
        {
            return offerList
                .GroupBy(offer => offer.Major)
                .Select(group => new { key = group.Key, val = group.Count() }) ;
        }

        public void exportToJson(string filepath = "scores.json")
        {
            string json = JsonConvert.SerializeObject(getMajorScores(GetOfferList()), Formatting.Indented);
            File.WriteAllText(filepath, json);
        }*/

    }





    class KonsyliumScraper : Scraper
    {
        public KonsyliumScraper()
        {
            this.pageUrl = "https://konsylium24.pl/kompendium24/praca/oferty-pracy/lista?gad_source=1&page=1#custom-list";
            this.offerClassName = "list-group-offer";
            this.pageCount = getPageCount();
            this.siteName = "Konsylium";
        }

        public override IOffer convertElementToOffer(IWebElement element)
        {
            return new KonsyliumOffer(element);
        }

        public int getPageCount()
        {
            changePage(1);
            var max = driver.FindElement(By.ClassName("page-away-9")).Text;
            return Convert.ToInt32(max);
        }

        public override void changePage(int pageNum) { this.driver.Url = $"https://konsylium24.pl/kompendium24/praca/oferty-pracy/lista?page={Convert.ToString(pageNum)}#custom-list"; }


    }

    class KlinikaScraper : Scraper
    {
        public KlinikaScraper()
        {
            this.pageUrl = "https://klinikaofert.pl/";
            this.offerClassName = "ogl-box";
            this.pageCount = 1;
            this.siteName = "KlinikaOfert";
        }
        public override IOffer convertElementToOffer(IWebElement element)
        {
            return new KlinikaOffer(element);
        }
        public override void changePage(int page)
        { 
            if (page != 1)
            {
                Console.WriteLine("This site has only one page");
            }

            driver.Url = this.pageUrl; 
        }



    }
}
