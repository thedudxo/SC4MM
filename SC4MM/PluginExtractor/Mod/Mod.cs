using System.IO;

namespace SC4MM
{
    public class Mod : IMod
    {
        readonly IFileMover fileMover;
        public ModFolders Folders { get; }
        public Dictionary<string, bool> ToggleByFileName { get; } = new();
        public List<string> Readmes { get; init; } = new();
        public string Name { get; set; }

        public Mod(ModFolders folders, IFileMover fileMover)
        {
            this.Folders = folders;
            this.fileMover = fileMover;
        }

        public bool Enabled { get; private set; } = true;

        public void Enable()
        {
            if (Enabled) return;

            MoveEnabledFiles(Folders.Enabled);

            Enabled = true;
        }


        public void Disable()
        {
            if (Enabled == false) return;

            MoveEnabledFiles(Folders.Disabled);

            Enabled = false;
        }

        public void DisableFile(string filename)
        {
            if(ToggleByFileName[filename] == false) return;

            ToggleByFileName[filename] = false;

            if (Enabled)
            {
                MoveFile(filename, Folders.Enabled, Folders.Disabled);
            }
        }

        public void EnableFile(string filename)
        {
            if (ToggleByFileName[filename] == true) return;

            ToggleByFileName[filename] = true;

            if (Enabled)
            {
                MoveFile(filename, Folders.Disabled, Folders.Enabled);
            }
        }

        private void MoveEnabledFiles(string destination)
        {
            foreach (var item in ToggleByFileName)
            {
                if(item.Value == true)
                    MoveFile(item.Key, Folders.Enabled, destination);
            }
        }

        private void MoveFile(string filename, string source, string destination)
        {
            string CurrentFilePath = Path.Combine(source, filename);
            fileMover.Move(CurrentFilePath, destination);
        }
    }
}