using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace hello_scraper
{
    internal class KonsyliumScraper : Scraper
    {
        public int pageNum = 0;
        public string page;
        public ChromeDriver driver;

        public KonsyliumScraper()
        {
            this.page = "https://konsylium24.pl/kompendium24/praca/oferty-pracy/lista?page=0#custom-list";
            this.driver = new ChromeDriver();
        }
        
        public void UpdateDiverUrl()
        {
            this.pageNum++;
            this.driver.Url = "https://konsylium24.pl/kompendium24/praca/oferty-pracy/lista?page=" + Convert.ToString(this.pageNum) + "#custom-list";
        }

        public Dictionary<string, int> OfferListToMajorDict(List<Offer> offerList)
        {
            var majorDict = new Dictionary<string, int>();

            foreach (Offer offer in offerList)
            {
                var element = offer.ParentElement;

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
