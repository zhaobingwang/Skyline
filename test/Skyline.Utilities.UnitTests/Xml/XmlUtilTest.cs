using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Xunit;

namespace Skyline.Utilities.UnitTests.Xml
{
    [Trait("工具类", "Xml")]
    public class XmlUtilTest
    {
        [Fact(DisplayName = "Xml转对象-正常入参")]
        public void ToObject_ShouldSuccess_WithExpectedParameters()
        {
            string xml = "<?xml version=\"1.0\"?><catalog><book id=\"bk101\"><author>Gambardella, Matthew</author><title>XML Developer's Guide</title><genre>Computer</genre><price>44.95</price><publish_date>2000-10-01</publish_date><description>An in-depth look at creating applications with XML.</description></book><book id=\"bk102\"><author>Ralls, Kim</author><title>Midnight Rain</title><genre>Fantasy</genre><price>5.95</price><publish_date>2000-12-16</publish_date><description>A former architect battles corporate zombies, an evil sorceress, and her own childhood to become queen of the world.</description></book></catalog>";

            var result = XmlUtil.ToObject<BookCatalog>(xml);

            Assert.NotNull(result);
            Assert.True(result.Books.Count == 2);
        }

        [Fact(DisplayName = "Xml转对象-空值入参")]
        public void ToObject_ReturnDefault_WithNull()
        {
            string xml = null;

            var result = XmlUtil.ToObject<BookCatalog>(xml);

            Assert.True(result == default(BookCatalog));
        }

        [Fact(DisplayName = "对象转Xml-正常入参")]
        public void ToXml_ShouldSuccess_WithExpectedParameters()
        {
            string expected = "<catalog><book id=\"bk101\"><author>Gambardella, Matthew</author><title>XML Developer's Guide</title><genre>Computer</genre><price>44.95</price><publish_date>2000-10-01</publish_date><description>An in-depth look at creating applications with XML.</description></book><book id=\"bk102\"><author>Ralls, Kim</author><title>Midnight Rain</title><genre>Fantasy</genre><price>5.95</price><publish_date>2000-12-16</publish_date><description>A former architect battles corporate zombies, an evil sorceress, and her own childhood to become queen of the world.</description></book></catalog>";
            var bookCatalog = new BookCatalog();
            var books = new List<Book> {
                new Book{
                    Id="bk101",
                    Author= "Gambardella, Matthew",
                    Title="XML Developer's Guide",
                    Genre="Computer",
                    Price=44.95M,
                    PublishDate=new DateTime(2000,10,1).ToString("yyyy-MM-dd"),
                    Description="An in-depth look at creating applications with XML.",
                },
                new Book{
                    Id="bk102",
                    Author= "Ralls, Kim",
                    Title="Midnight Rain",
                    Genre="Fantasy",
                    Price=5.95M,
                    PublishDate=new DateTime(2000,12,16).ToString("yyyy-MM-dd"),
                    Description="A former architect battles corporate zombies, an evil sorceress, and her own childhood to become queen of the world.",
                },
            };
            bookCatalog.Books = books;

            var actual = XmlUtil.ToXml(bookCatalog);

            Assert.Equal(expected, actual);
        }
    }


    [XmlRoot(ElementName = "catalog")]
    public class BookCatalog
    {
        [XmlElement(ElementName = "book")]
        public List<Book> Books { get; set; }
    }
    public class Book
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("author")]
        public string Author { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("genre")]
        public string Genre { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }

        [XmlElement("publish_date")]
        public string PublishDate { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }
    }
}
