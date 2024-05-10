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
        var driver = new ChromeDriver();
        var offerList = new List<Offer>();
        int offerIterator = 1;
        string xpath = "//*[@id=\"custom-list\"]/li[" + Convert.ToString(offerIterator) + "]";

        var majorDict = new Dictionary<string, int>();
        var locationDict = new Dictionary<string, int>();
    
        driver.Url = "https://konsylium24.pl/kompendium24/praca/oferty-pracy/lista/";

        var offerIWebElementList = driver.FindElements(By.ClassName("list-group-offer"));

        foreach(IWebElement element in offerIWebElementList)
        {
            Offer o = new Offer(element);
            offerList.Add(o);

            if (!majorDict.TryGetValue(o.Major, out int _))
            {
                majorDict.Add(o.Major, 0);
            }
            majorDict[o.Major]++;
        }
    }

    public Offer getOffers(ChromeDriver driver)
    {
        
        throw new NotImplementedException();
    }
}
