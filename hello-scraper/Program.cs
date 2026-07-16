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
        Scraper konsylium = new KonsyliumScraper();
        var konsyliumList = konsylium.GetOfferList();



        //var db_handler = new DbHandler(File.ReadAllText(@"C:\Users\Guest\Desktop\lekarze\connection_string.txt"));

        //foreach ( IOffer offer in klinikaList )
        //{
        //    db_handler.SendOffer(offer);
        //}

    }
}