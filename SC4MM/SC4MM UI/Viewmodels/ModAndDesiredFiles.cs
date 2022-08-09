namespace SC4MM_UI.Viewmodels
{
    public class ModAndDesiredFiles : IModAndDesiredFiles
    {
        public IMod? Mod { get; set; }
        public IDesiredModFiles? DesiredFiles { get; set; }

        public IModAndDesiredFiles? Model;

        public ModAndDesiredFiles() { }
        public ModAndDesiredFiles(IModAndDesiredFiles model)
        {
            Mod = new Mod(model.Mod);
            DesiredFiles = new DesiredModFiles(model.DesiredFiles);
            Model = model;
        }

        public void Apply()
        {
            _ = Model ?? throw new InvalidOperationException("No underlying model set");
            Model.Apply();
        }
    }
}
