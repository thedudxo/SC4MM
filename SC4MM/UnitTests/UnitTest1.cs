using NUnit.Framework;
using SC4MM.ZipFileIntake;
using System;

namespace UnitTests
{
    public class SC4FileTests
    {
        [Test]
        #region Cases
        [TestCase("SomeFile3982598.SC4Desc", SC4FileType.SC4Desc)]
        [TestCase("AnotherFile222443.SC4Lot", SC4FileType.SC4Lot)]
        [TestCase("AnotherFile222443.SC4Model", SC4FileType.SC4Model)]
        [TestCase("AnotherFile222443.bat", SC4FileType.bat)]
        //readmes
        [TestCase("AnotherFile222443.txt", SC4FileType.txt)]
        [TestCase("AnotherFile222443.html", SC4FileType.html)]
        [TestCase("AnotherFile222443.jpg", SC4FileType.jpg)]
        [TestCase("AnotherFile222443.gif", SC4FileType.gif)]
        //EdgeCases
        [TestCase("AnotherFile222443.jpg.txt", SC4FileType.txt)]
        [TestCase("AnotherFile222443.psd.SC4Desc", SC4FileType.SC4Desc)]
        [TestCase("txt.AnotherFile222443.gif", SC4FileType.gif)]
        [TestCase("Another.txt.File222443.html", SC4FileType.html)]
        [TestCase("Another.gifFile222443.SC4Lot", SC4FileType.SC4Lot)]
        #endregion
        public void GetFileExtension(string filePath, SC4FileType expected)
        {
            SC4File file = new(filePath);

            Assert.That(file.Type == expected);
        }

        [Test]
        #region Cases
        [TestCase("somefile.mp4")]
        [TestCase("SomeNonsense.k28dLol")]
        [TestCase("somefile.mp4")]
        [TestCase("somefile.sc4lot")]
        [TestCase("somefile.HTML")]
        [TestCase("somefile.Html")]
        [TestCase(".SC4Lot.somefile")]
        #endregion
        public void GetFileExtension_Exceptions(string filePath)
        {
            Assert.Throws<ArgumentException>(() => new SC4File(filePath));
        }
    }

}