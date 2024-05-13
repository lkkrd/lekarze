using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V122.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hello_scraper
{
    internal abstract class Scraper
    {
        ChromeDriver driver { get; }
        string offerClassName { get; }
        int pageCount { get; set; } // ile stron można zescrapować
        string pageUrl { get; }

        private void changePage(int pageNum) { driver.Url = "https://konsylium24.pl/kompendium24/praca/oferty-pracy/lista?page={Convert.ToString(pageNum)}#custom-list";}
        private void resetPage() { driver.Url = pageUrl; }

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

            resetPage();
            return offerList;
        }
        

        public List <Offer> GetOfferList() // zwraca oferty ze wszystkich stron
        {
            var offerList = new List<Offer>();
            for (int i = 0; i < this.pageCount; i++ )
            {
                offerList.AddRange(GetOfferListFromPage(i));
            }

            return offerList;
        }

        public Dictionary<string, int> GetMajorDictionary(List<Offer> offerList)
        {
            var majorDict = new Dictionary<string, int>();

            foreach (Offer offer in offerList)
            {

                if (!majorDict.TryGetValue(offer.Major, out int _))
                {
                    majorDict.Add(offer.Major, 0);
                }
                majorDict[offer.Major]++;

            }

            return majorDict;
        }

    }
}
