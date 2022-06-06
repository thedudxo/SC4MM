namespace SC4MM
{
    public interface IMod
    {
        bool Enabled { get; }

        void Disable();
        void DisableFile(string filename);
        void Enable();
        void EnableFile(string filename);
    }
}