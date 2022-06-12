namespace SC4MM.Tests.ModCollections
{
    class MockModListItem : IModAndDesiredFiles
    {
        public bool Applied = false;
        public void Apply()
        {
            Applied = true;
        }

        public IMod Mod { get; set; }

        public IDesiredModFiles DesiredFiles => throw new NotImplementedException();
    }
}
