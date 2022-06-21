namespace SC4MM.Tests.ModCollections
{
    internal class DesiredModFilesTests
    {
        DesiredModFiles desiredModFiles;
        List<string> IncludedFiles => desiredModFiles.IncludedFiles;
        List<string> ExcludedFiles => desiredModFiles.ExcludedFiles;
        
        MockMod mod;
        Dictionary<string, bool> toggleByFilename;

        [SetUp]
        public void Setup()
        {
            toggleByFilename = new Dictionary<string, bool>();
            mod = new(toggleByFilename);

            desiredModFiles = new(mod);
        }

        void ParamsAction<t>(Action<t> action, t required, params t[] extras)
        {
            action(required);
            foreach (t extra in extras)
                action(extra);
        }

        void AddEnabledFilesToMod(string file, params string[] files)
        {
            ParamsAction(f => toggleByFilename.Add(f, true), file, files);
        }

        void AddDisabledFilesToMod(string file, params string[] files)
        {
            ParamsAction(f => toggleByFilename.Add(f, false), file, files);
        }

        void AddIncludedFiles(string file, params string[] files)
        {
            ParamsAction(f => IncludedFiles.Add(f), file, files);
        }

        void AddExcludedFiles(string file, params string[] files)
        {
            ParamsAction(f => ExcludedFiles.Add(f), file, files);
        }

        [Test]
        public void DisabledFilesIncluded_Apply_BecomeEnabled()
        {
            AddIncludedFiles("file1","file2");
            AddDisabledFilesToMod("file1", "file2");

            desiredModFiles.Apply();

            Assert.IsTrue(toggleByFilename["file1"]);
            Assert.IsTrue(toggleByFilename["file2"]);
        }

        [Test]
        public void DisabledFilesExcluded_Apply_StillDisabled()
        {
            AddExcludedFiles("file1", "file2");
            AddDisabledFilesToMod("file1", "file2");

            desiredModFiles.Apply();

            Assert.IsFalse(toggleByFilename["file1"]);
            Assert.IsFalse(toggleByFilename["file2"]);
        }

        [Test]
        public void EnabledFilesIncluded_Apply_StillEnabled()
        {
            AddIncludedFiles("file1", "file2");
            AddEnabledFilesToMod("file1", "file2");

            desiredModFiles.Apply();

            Assert.IsTrue(toggleByFilename["file1"]);
            Assert.IsTrue(toggleByFilename["file2"]);
        }

        [Test]
        public void EnabledFilesExcluded_Apply_BecomeDisabled()
        {
            AddExcludedFiles("file1", "file2");
            AddEnabledFilesToMod("file1", "file2");

            desiredModFiles.Apply();

            Assert.IsFalse(toggleByFilename["file1"]);
            Assert.IsFalse(toggleByFilename["file2"]);
        }
    }
}
