using NUnit.Framework;
using SC4MM.ZipFileIntake;
using System;

namespace UnitTests
{
    public class Tests
    {
        const string testDirectory = "..\\..\\..\\TestFiles\\";

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("SomeFile3982598.SC4Desc", SC4File.Extension.SC4Desc)]
        [TestCase("AnotherFile222443.SC4Lot", SC4File.Extension.SC4Lot)]
        [TestCase("AnotherFile222443.SC4Model", SC4File.Extension.SC4Model)]
        [TestCase("AnotherFile222443.bat", SC4File.Extension.bat)]
        [TestCase("AnotherFile222443.txt", SC4File.Extension.txt)]
        [TestCase("AnotherFile222443.html", SC4File.Extension.html)]
        [TestCase("AnotherFile222443.jpg", SC4File.Extension.jpg)]
        [TestCase("AnotherFile222443.gif", SC4File.Extension.gif)]
        //aditionalExtensions
        [TestCase("AnotherFile222443.jpg.txt", SC4File.Extension.txt)]
        [TestCase("AnotherFile222443.psd.SC4Desc", SC4File.Extension.SC4Desc)]
        [TestCase("txt.AnotherFile222443.gif", SC4File.Extension.gif)]
        [TestCase("Another.txt.File222443.html", SC4File.Extension.html)]
        [TestCase("Another.gifFile222443.SC4Lot", SC4File.Extension.SC4Lot)]
        public void GetFileExtension(string filePath, SC4File.Extension expected)
        {
            SC4File file = new(filePath);

            Assert.That(file.GetExtension() == expected);
        }

        [Test]
        [TestCase("somefile.mp4")]
        [TestCase("SomeNonsense.k28dLol")]
        [TestCase("somefile.mp4")]
        [TestCase("somefile.sc4lot")]
        [TestCase("somefile.HTML")]
        [TestCase("somefile.Html")]
        [TestCase(".SC4Lot.somefile")]
        public void GetFileExtension_Exceptions(string filePath)
        {
            void makeNew()
            {
                try
                {
                    new SC4File(filePath);
                }

                catch (ArgumentException)
                {
                    throw;
                }
            }
            Assert.That(makeNew, Throws.ArgumentException);
        }
    }
}