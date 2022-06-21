namespace SC4MM_UI.Viewmodels
{
    internal class DesiredModFiles : IDesiredModFiles
    {
        public List<string> IncludedFiles { get; set; } = new();
        public List<string> ExcludedFiles { get; set; } = new();

        public IDesiredModFiles? Model;

        public DesiredModFiles(IDesiredModFiles model)
        {
            Model = model;
            IncludedFiles = model.IncludedFiles;
            ExcludedFiles = model.ExcludedFiles;
        }

        public void Apply()
        {
            _ = Model ?? throw new InvalidOperationException("No underlying model set");
            Model.Apply();
        }
    }
}
