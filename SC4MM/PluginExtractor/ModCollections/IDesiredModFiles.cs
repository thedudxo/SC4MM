namespace SC4MM
{
    public interface IDesiredModFiles
    {
        List<string> IncludedFiles { get; }
        List<string> ExcludedFiles { get; }
        void Apply();
    }
}