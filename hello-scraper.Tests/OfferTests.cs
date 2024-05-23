using FakeItEasy;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V123.DOM;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace hello_scraper.Tests
{
    [TestFixture]
    public class Tests
    {
        IWebElement fakeWebElement;
        IWebElement fakeMajorElement;
        IWebElement fakeLocationElement;
        IWebElement fakeDateElement;

        [SetUp]
        public void Setup()
        {

            fakeWebElement = A.Fake<IWebElement>();
            fakeMajorElement = A.Fake<IWebElement>();
            fakeLocationElement = A.Fake<IWebElement>();
            fakeDateElement = A.Fake<IWebElement>();

            // fakeWebElements setup
            A.CallTo(() => fakeMajorElement.Text).Returns("important-major");
            A.CallTo(() => fakeLocationElement.Text).Returns("important-location");
            A.CallTo(() => fakeDateElement.GetAttribute("data-time-ago")).Returns("important-date");

            // konsylium
            A.CallTo(() => fakeWebElement.FindElement(By.ClassName("spec"))).Returns(fakeMajorElement);
            A.CallTo(() => fakeWebElement.FindElement(By.ClassName("workplace"))).Returns(fakeLocationElement);
            A.CallTo(() => fakeWebElement.FindElement(By.ClassName("time-ago"))).Returns(fakeDateElement);
            // klinika
            A.CallTo(() => fakeWebElement.FindElement(By.XPath("div/div[1]/h2/span[2]"))).Returns(fakeMajorElement);
            A.CallTo(() => fakeWebElement.FindElement(By.XPath("div/div[4]/div/p[2]"))).Returns(fakeLocationElement);
        }


        [Test]
        public void Konsylium_GetLocation_ReturnsString()
        {
            var offer = new KonsyliumOffer(fakeWebElement);
            Assert.That(offer.getMajor(), Is.EqualTo("important-major"));
        }
        [Test]
        public void Konsylium_OnInitialize_SetMajorToString()
        {
            //act
            var offer = new KonsyliumOffer(fakeWebElement);
            //assert
            Assert.That(offer.major, Is.EqualTo("important-major"));
        }
        [Test]
        public void KlinikaOffer_OnInit_DateIsNull()
        {
            var offer = new KlinikaOffer(fakeWebElement);
            Assert.That(offer.date, Is.Null);
        }
    }
}