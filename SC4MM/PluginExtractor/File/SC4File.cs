namespace SC4MM.ZipFileIntake
{
    public class SC4File
    {
        public SC4FileType Type { get; private set; }
        public SC4File(string path)
        {
            GetFileType(path);
        }

        private void GetFileType(string path)
        {
            Type = FileExtensionReader.GetSC4FileType(path);
            if (Type == SC4FileType.unrecognised)
                throw new System.ArgumentException("Unrecognised File Type");
        }
    }
}