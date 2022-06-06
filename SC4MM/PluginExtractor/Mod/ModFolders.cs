namespace SC4MM
{
    public class ModFolders
    {
        public string Enabled { get; init; }
        public string Disabled { get; init; }
        public string Readme { get; init; }
        
        public ModFolders(string enabled, string disabled, string readme)
        {
            Enabled = enabled;
            Disabled = disabled;
            Readme = readme;
        }
    }
}