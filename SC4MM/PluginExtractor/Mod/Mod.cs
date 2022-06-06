using System.IO;
using System.Collections.Generic;

namespace SC4MM
{
    public class Mod
    {
        readonly ModFolders folders;
        readonly ModFiles files;
        readonly IFileMover fileMover;

        public Mod(ModFolders paths, ModFiles files, IFileMover fileMover)
        {
            this.folders = paths;
            this.files = files;
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
        
        public LinkedListNode<string> DisableFile(LinkedListNode<string> file)
        {
            if (file.List == files.Disabled) return file;

            files.Enabled.Remove(file);
            file = files.Disabled.AddLast(file.Value);

            if (Enabled)
            {
                MoveFile(file.Value, folders.Enabled, folders.Disabled);
            }
            
            return file;
        }

        public LinkedListNode<string> EnableFile(LinkedListNode<string> file)
        {
            if (file.List == files.Enabled) return file;

            files.Disabled.Remove(file);
            file = files.Enabled.AddLast(file.Value);

            if (Enabled)
            {
                MoveFile(file.Value, folders.Disabled, folders.Enabled);
            }

            return file;
        }
        
        private void MoveEnabledFiles(string destination)
        {
            foreach (string file in files.Enabled)
            {
                MoveFile(file, folders.Enabled, destination);
            }
        }
        
        private void MoveFile(string file, string source, string destination)
        {
            string CurrentFilePath = Path.Combine(source, file);
            fileMover.Move(CurrentFilePath, destination);
        }
    }
}