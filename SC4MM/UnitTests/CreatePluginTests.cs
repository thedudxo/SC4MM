using NUnit.Framework;
using SC4MM.ZipFileIntake;
using System.Collections;
using System.Collections.Generic;

namespace UnitTests
{
    public class CreatePluginTests
    {
        string DummyPlugin = "..\\..\\..\\TestFiles\\DummyPlugin";

        class ExpectedFile 
        { 
            public string name; 
            public FileType type;
        }

        ExpectedFile[] ExpectedPluginFiles =
        {
            new ExpectedFile{ name = "1234.SC4Desc", type = FileType.SC4Plugin },
            new ExpectedFile{ name = "ATestLot.SC4Lot", type = FileType.SC4Plugin },
            new ExpectedFile{ name = "Another.TestLot.SC4Lot", type = FileType.SC4Plugin },
            new ExpectedFile{ name = "Model.Mod.SC4Model", type = FileType.SC4Plugin },
            new ExpectedFile{ name = "SomeBat.bat", type = FileType.SC4Plugin },
        };

        ExpectedFile[] ExpectedReadmeFiles =
        {
            new ExpectedFile{ name = "3FramesofAGif.gif", type = FileType.Readme },
            new ExpectedFile{ name = "DummyWebpage.html", type = FileType.Readme },
            new ExpectedFile{ name = "LowQaulityjpg.jpg", type = FileType.Readme },
            new ExpectedFile{ name = "SomeBat.bat", type = FileType.Readme },
        };

        Plugin plugin;

        private class MockPathReader : IFilePathReader
        {
            int count = -1;
            int PluginFiles = 5;

            public FileType GetFileType(string extension)
            {
                count++;
                if (count < PluginFiles)
                    return FileType.SC4Plugin;
                else
                    return FileType.Readme;
            }

            public string ReadExtension(string filePath) => ".mock";
            public string ReadName(string filePath) => "mockname";
        }

        [SetUp]
        public void Setup()
        {
            PluginFactory factory = new(new MockPathReader());
            plugin = factory.Open(DummyPlugin);
        }

        bool NotEmpty(ICollection c) => c.Count > 0;

        [Test]
        public void PluginHasListOfFiles()
        {
            Assert.That(NotEmpty(plugin.PluginFiles));
            Assert.That(NotEmpty(plugin.ReadmeFiles));
        }

        [Test]
        public void PluginHasCorrectNumberOfFiles()
        {
            Assert.That(plugin.PluginFiles.Count == ExpectedPluginFiles.Length);
            Assert.That(plugin.ReadmeFiles.Count == ExpectedReadmeFiles.Length);
        }


    }
}