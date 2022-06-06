using System;
using System.Collections.Generic;
using SC4MM;
using NUnit.Framework;
using System.IO;

namespace SC4MM.Tests
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
        ModFiles files;
        ModFolders folders;
        MockFileMover fileMover;

        [SetUp]
        public void Setup()
        {
            fileMover = new();
            files = new();
            folders = new("mod/enabled", "mod/disabled", "mod/readme");

            mod = new(folders, files, fileMover);
        }

        private void AddTwoEnabledModFiles()
        {
            files.Enabled.AddLast("mod1");
            files.Enabled.AddLast("mod2");
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
            files.Disabled.AddLast("mod3");
            files.Disabled.AddLast("mod4");
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
            var modFile = files.Disabled.AddLast("mod1");

            mod.DisableFile(modFile);

            Assert.That(fileMover.commands, Is.Empty);
        }

        [Test]
        public void EnabledModEnabledFile_EnableFile_DoesNotMoveFile()
        {
            var modFile = files.Enabled.AddLast("mod1");

            mod.EnableFile(modFile);

            Assert.That(fileMover.commands, Is.Empty);
        }
        
        [Test]
        public void DisabledModEnabledFile_EnableFile_DoesNotMoveFile()
        {
            var modFile = files.Enabled.AddLast("mod1");
            mod.Disable();
            fileMover.commands.Clear();
            
            mod.EnableFile(modFile);

            Assert.That(fileMover.commands, Is.Empty);
        }

        [Test]
        public void DisabledModDisabledFile_DisableFile_DoesNotMoveFile()
        {
            var modFile = files.Disabled.AddLast("mod1");
            mod.Disable();
            fileMover.commands.Clear();

            mod.DisableFile(modFile);

            Assert.That(fileMover.commands, Is.Empty);
        }

        [Test]
        public void DisabledModDisabledFile_EnableFile_DoesNotMoveFile()
        {
            var modFile = files.Disabled.AddLast("mod1");
            mod.Disable();
            fileMover.commands.Clear();

            mod.EnableFile(modFile);

            Assert.That(fileMover.commands, Is.Empty);
        }

        [Test]
        public void EnabledModDisabledFile_EnableFile_MovesFileToEnabledPath()
        {
            var modFile = files.Disabled.AddLast("mod1");

            mod.EnableFile(modFile);

            CollectionAssert.Contains(fileMover.commands, new MockFileMover.Command
            {
                source = Path.Combine(folders.Disabled, "mod1"),
                destination = folders.Enabled
            });
        }

        [Test]
        public void DisabledFile_EnableFileTwice_DoesNotMoveFileTwice()
        {
            var modFile = files.Disabled.AddLast("mod1");
            modFile = mod.EnableFile(modFile);
            fileMover.commands.Clear();

            mod.EnableFile(modFile);

            Assert.That(fileMover.commands, Is.Empty);
        }

        [Test]
        public void EnabledFile_DisableFileTwice_DoesNotMoveFileTwice()
        {
            var modFile = files.Enabled.AddLast("mod1");
            modFile = mod.DisableFile(modFile);
            fileMover.commands.Clear();

            mod.DisableFile(modFile);

            Assert.That(fileMover.commands, Is.Empty);
        }

        [Test]
        public void EnabledFile_DisableFile_MovesFileToEnabledPath()
        {
            var modFile = files.Enabled.AddLast("mod1");

            mod.DisableFile(modFile);

            CollectionAssert.Contains(fileMover.commands, new MockFileMover.Command
            {
                source = Path.Combine(folders.Enabled, "mod1"),
                destination = folders.Disabled
            });
        }
    }        
}
