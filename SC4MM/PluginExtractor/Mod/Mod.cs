using System.IO;

namespace SC4MM
{
    public class Mod : IMod
    {
        readonly IFileMover fileMover;
        public readonly ModFolders folders;
        public Dictionary<string, bool> ToggleByFileName { get; } = new();
        public List<string> Readme { get; init; } = new();
        public string Name { get; set; }

        public Mod(ModFolders folders, IFileMover fileMover)
        {
            this.folders = folders;
            this.fileMover = fileMover;
        }

        public bool Enabled { get; private set; } = true;

        public void Enable()
        {
            if (Enabled) return;

            MoveEnabledFiles(folders.Enabled);

            Enabled = true;
        }


        public void Disable()
        {
            if (Enabled == false) return;

            MoveEnabledFiles(folders.Disabled);

            Enabled = false;
        }

        public void DisableFile(string filename)
        {
            if(ToggleByFileName[filename] == false) return;

            ToggleByFileName[filename] = false;

            if (Enabled)
            {
                MoveFile(filename, folders.Enabled, folders.Disabled);
            }
        }

        public void EnableFile(string filename)
        {
            if (ToggleByFileName[filename] == true) return;

            ToggleByFileName[filename] = true;

            if (Enabled)
            {
                MoveFile(filename, folders.Disabled, folders.Enabled);
            }
        }

        private void MoveEnabledFiles(string destination)
        {
            foreach (var item in ToggleByFileName)
            {
                if(item.Value == true)
                    MoveFile(item.Key, folders.Enabled, destination);
            }
        }

        private void MoveFile(string filename, string source, string destination)
        {
            string CurrentFilePath = Path.Combine(source, filename);
            fileMover.Move(CurrentFilePath, destination);
        }
    }
}