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
        KlinikaScraper scraper = new KlinikaScraper();
        var list = scraper.GetOfferList();
    }
}