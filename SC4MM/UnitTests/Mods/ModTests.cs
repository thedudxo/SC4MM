using System.IO;

namespace SC4MM.Tests.Mods
{
    class MockFileMover : IFileMover
    {
        public struct Command
        {
            public string source;
            public string destination;

            public override string ToString()
            {
                return $"Source: {source}, Destination: {destination}";
            }
        }

        public readonly List<Command> commands = new();

        public void Move(string source, string destination)
        {
            commands.Add(new Command { source = source, destination = destination });
        }
    }

    class ModTests
    {

        Mod mod;
        Dictionary<string, bool> toggleByFilename;
        ModFolders folders;
        MockFileMover fileMover;

        [SetUp]
        public void Setup()
        {
            fileMover = new();
            toggleByFilename = new();
            folders = new("mod/enabled", "mod/disabled", "mod/readme");

            mod = new(folders, toggleByFilename, fileMover);
        }

        private void AddTwoEnabledModFiles()
        {
            toggleByFilename.Add("mod1", true);
            toggleByFilename.Add("mod2", true);
        }

        [Test]
        public void EnabledMod_Disable_MovesEnabledFilesToDisabledPath()
        {
            AddTwoEnabledModFiles();

            mod.Disable();

            MockFileMover.Command[] expectedCommands =
            {
                new()
                {
                    source = Path.Combine(folders.Enabled, "mod1"),
                    destination = folders.Disabled
                },

                new()
                {
                    source = Path.Combine(folders.Enabled, "mod2"),
                    destination = folders.Disabled
                }
            };

            Assert.That(fileMover.commands, Is.EquivalentTo(expectedCommands));
        }

        [Test]
        public void EnabledMod_Enable_DoesNotMoveFiles()
        {
            AddTwoEnabledModFiles();
            mod.Disable();
            mod.Enable();
            fileMover.commands.Clear();

            mod.Enable();

            Assert.That(fileMover.commands, Is.Empty);
        }

        [Test]
        public void DisabledMod_Enable_DoesNotMoveDisabledFilesToEnabledPath()
        {
            AddTwoEnabledModFiles();
            toggleByFilename.Add("mod3", false);
            toggleByFilename.Add("mod4", false);
            mod.Disable();
            fileMover.commands.Clear();

            mod.Enable();

            MockFileMover.Command[] expectedCommands =
            {
                new()
                {
                    source = Path.Combine(folders.Enabled, "mod1"),
                    destination = folders.Enabled
                },

                new()
                {
                    source = Path.Combine(folders.Enabled, "mod2"),
                    destination = folders.Enabled
                }
            };

            Assert.That(fileMover.commands, Is.EquivalentTo(expectedCommands));
        }

        [Test]
        public void DisabledMod_Enable_MovesEnabledFilesToEnabledPath()
        {
            AddTwoEnabledModFiles();
            mod.Disable();
            fileMover.commands.Clear();

            mod.Enable();

            MockFileMover.Command[] expectedCommands =
            {
                new()
                {
                    source = Path.Combine(folders.Enabled, "mod1"),
                    destination = folders.Enabled
                },

                new()
                {
                    source = Path.Combine(folders.Enabled, "mod2"),
                    destination = folders.Enabled
                }
            };

            Assert.That(fileMover.commands, Is.EquivalentTo(expectedCommands));
        }

        [Test]
        public void DisabledMod_Disable_DoesNotMoveFiles()
        {
            AddTwoEnabledModFiles();
            mod.Enable();
            mod.Disable();
            fileMover.commands.Clear();

            mod.Disable();

            Assert.That(fileMover.commands, Is.Empty);
        }

        [Test]
        public void EnabledModDisabledFile_DisableFile_DoesNotMoveFile()
        {
            toggleByFilename.Add("mod1", false);

            mod.DisableFile("mod1");

            Assert.That(fileMover.commands, Is.Empty);
        }

        [Test]
        public void EnabledModEnabledFile_EnableFile_DoesNotMoveFile()
        {
            toggleByFilename.Add("mod1", true);

            mod.EnableFile("mod1");

            Assert.That(fileMover.commands, Is.Empty);
        }
        
        [Test]
        public void DisabledModEnabledFile_EnableFile_DoesNotMoveFile()
        {
            toggleByFilename.Add("mod1", true);
            mod.Disable();
            fileMover.commands.Clear();
            
            mod.EnableFile("mod1");

            Assert.That(fileMover.commands, Is.Empty);
        }

        [Test]
        public void DisabledModDisabledFile_DisableFile_DoesNotMoveFile()
        {
            toggleByFilename.Add("mod1", false);
            mod.Disable();
            fileMover.commands.Clear();

            mod.DisableFile("mod1");

            Assert.That(fileMover.commands, Is.Empty);
        }

        [Test]
        public void DisabledModDisabledFile_EnableFile_DoesNotMoveFile()
        {
            toggleByFilename.Add("mod1", false);
            mod.Disable();
            fileMover.commands.Clear();

            mod.EnableFile("mod1");

            Assert.That(fileMover.commands, Is.Empty);
        }

        [Test]
        public void EnabledModDisabledFile_EnableFile_MovesFileToEnabledPath()
        {
            toggleByFilename.Add("mod1", false);

            mod.EnableFile("mod1");

            CollectionAssert.Contains(fileMover.commands, new MockFileMover.Command
            {
                source = Path.Combine(folders.Disabled, "mod1"),
                destination = folders.Enabled
            });
        }

        [Test]
        public void DisabledFile_EnableFileTwice_DoesNotMoveFileTwice()
        {
            toggleByFilename.Add("mod1", false);
            mod.EnableFile("mod1");
            fileMover.commands.Clear();

            mod.EnableFile("mod1");

            Assert.That(fileMover.commands, Is.Empty);
        }

        [Test]
        public void EnabledFile_DisableFileTwice_DoesNotMoveFileTwice()
        {
            toggleByFilename.Add("mod1", true);
            mod.DisableFile("mod1");
            fileMover.commands.Clear();

            mod.DisableFile("mod1");

            Assert.That(fileMover.commands, Is.Empty);
        }

        [Test]
        public void EnabledFile_DisableFile_MovesFileToEnabledPath()
        {
            toggleByFilename.Add("mod1", true);

            mod.DisableFile("mod1");

            CollectionAssert.Contains(fileMover.commands, new MockFileMover.Command
            {
                source = Path.Combine(folders.Enabled, "mod1"),
                destination = folders.Disabled
            });
        }
    }        
}
