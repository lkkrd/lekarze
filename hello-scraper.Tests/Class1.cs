using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FakeItEasy;
using NUnit.Framework.Internal;

namespace hello_scraper.Tests
{
    [TestFixture]
    internal class ExporterTests
    {
        List<IOffer> offers;
        IOffer o1;
        IOffer o2;
        [SetUp]
        public void SetUp()
        {
            o1 = A.Fake<IOffer>();
            o2 = A.Fake<IOffer>();

            A.CallTo(() => o1.major).Returns("Chirurgia");
            A.CallTo(() => o1.location).Returns("Poznań");
            A.CallTo(() => o2.major).Returns("Okulistyka");
            A.CallTo(() => o2.location).Returns("Poznań");

            offers = new List<IOffer> { o1, o2 };
            
        }
        [Test]
        public void ToJson_WhenListNotNull_ReturnsString()
        {
            Console.WriteLine(DictExporter.ToJson(offers));
            Assert.That(DictExporter.ToJson(offers), Is.TypeOf<string>());
        }

        [Test]
        public void ToMajorCounter_WhenListNotNull_ReturnsString()
        {
            Console.WriteLine(DictExporter.ToMajorCounter(offers));
            Assert.That(DictExporter.ToMajorCounter(offers), Is.TypeOf<string>());
        }

        [Test]
        public void ToLocationCounter_WhenListnotNull_ReturnsString()
        {
            Console.WriteLine(DictExporter.ToLocationCounter(offers));
            Assert.That(DictExporter.ToLocationCounter(offers), Is.TypeOf<string>());
        }
    }
}
