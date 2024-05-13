using System;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V122.CSS;

namespace hello_scraper;

class Program
{
    public static void Main()
    {
        var scraper = new KonsyliumScraper();
        var collectList = new List<Offer>();

        for (int i = 0; i < 5; i++)
        {
            scraper.UpdateDiverUrl();
            var offers = scraper.GetOffersFromPage();
            collectList.AddRange(offers);
        }
    }


}
