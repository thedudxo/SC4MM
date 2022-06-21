using System.Windows;
using System.Windows.Controls;

namespace SC4MM_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly List<Viewmodels.Mod> Mods = new();
        ModList modlist = new();
        public Viewmodels.Mod SelectedMod { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Viewmodels.Mod mod1 = CreateTestmod1();
            CreateTestmod2();
            CreateTestSublist();

            SelectedMod = mod1;

            ModDetailsView.DataContext = SelectedMod;
            ModsList.ItemsSource = Mods;

            ModListsView.DataContext = new Viewmodels.ModList(modlist);
        }

        void CreateTestSublist()
        {
            ModList sublist = new();
            modlist.AddSublist(sublist);

            var mod = new Mod(
                            new ModFolders("Enabled/ShantyTown/Residential", "Disabled/ShantyTown/Residential", "Readme/ShantyTown/Residential"),
                            new DummyFileMover())
            {
                Name = "ShantyTown Residential",
            };

            mod.ToggleByFileName.Add("SHTHouse1x1.bat", true);
            mod.ToggleByFileName.Add("SHTHouse1x2.bat", true);
            mod.ToggleByFileName.Add("SHTHouse2x2.bat", true);
            mod.ToggleByFileName.Add("SHTHouse2x1.bat", true);
            mod.ToggleByFileName.Add("SHTHouse3x1.bat", true);
            mod.ToggleByFileName.Add("SHTHouse3x2.bat", true);

            mod.Readmes.Add("SHTHouses.pdf");

            var df = new DesiredModFiles(mod);
            var mdf = new ModAndDesiredFiles(mod, df);

            sublist.Add(mdf);
        }

        private void CreateTestmod2()
        {
            var mod = new Mod(
                            new ModFolders("Enabled/Green Parks", "Disabled/Green Parks", "Readme/Green Parks"),
                            new DummyFileMover())
            {
                Name = "Green Parks",
            };

            Viewmodels.Mod mod2 = new(mod);

            mod2.ToggleByFileName.Add("GreenPark.bat", true);
            mod2.Readmes.Add("GreenparksREADME.html");


            Mods.Add(mod2);

            var desired = new DesiredModFiles(mod);
            desired.IncludedFiles.Add("GreenPark.bat");
            modlist.Add(new ModAndDesiredFiles(mod, desired));
        }

        private Viewmodels.Mod CreateTestmod1()
        {
            var mod = new Mod(
                            new ModFolders("Enabled/Awesome Hotel", "Disabled/Awesome Hotel", "Readme/Awesome Hotel"),
                            new DummyFileMover())
            {
                Name = "Awesome Hotel",
            };

            Viewmodels.Mod mod1 = new(mod);

            mod1.ToggleByFileName.Add("AwesomeHotel4x3.bat", true);
            mod1.ToggleByFileName.Add("AwesomeHotel4x4.bat", true);
            mod1.ToggleByFileName.Add("DNight AwesomeHotel4x3.bat", false);
            mod1.ToggleByFileName.Add("DNight AwesomeHotel4x4.bat", false);

            mod1.Readmes.Add("Readme.txt");
            mod1.Readmes.Add("hotel.jpg");

            Mods.Add(mod1);

            var desired = new DesiredModFiles(mod);
            desired.IncludedFiles.Add("AwesomeHotel4x3.bat");
            desired.IncludedFiles.Add("AwesomeHotel4x4.bat");
            desired.ExcludedFiles.Add("DNight AwesomeHotel4x3.bat");
            desired.ExcludedFiles.Add("DNight AwesomeHotel4x4.bat");
            modlist.Add(new ModAndDesiredFiles(mod, desired));

            return mod1;
        }

        private void ModsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var newSelection = (Viewmodels.Mod?)e.AddedItems[0];
            if (newSelection == null) return;

            SelectedMod = newSelection;
            ModDetailsView.DataContext = SelectedMod;
        }
    }
}
