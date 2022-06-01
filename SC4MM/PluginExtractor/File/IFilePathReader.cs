namespace SC4MM.ZipFileIntake
{
    public interface IFilePathReader
    {
        string ReadName(string filePath);
        string ReadExtension(string filePath);
        FileType GetFileType(string extension);
    }
}