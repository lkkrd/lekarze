using System;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace hello_scraper;

class Program
{
    public static void Main()
    {
        Scraper konsylium = new KonsyliumScraper(2);
        var konsyliumList = konsylium.GetOfferList();

        var db_handler = new DbHandler(File.ReadAllText(@"C:\Users\mrm\Desktop\lekarze\connection_string.txt"));

        foreach (Offer offer in konsyliumList)
        {
            db_handler.SendOffer(offer);
        }

    }
}