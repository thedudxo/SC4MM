using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace SC4MM_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly List<Viewmodels.Mod> Mods = new();
        ModList modlist = new();

        public Viewmodels.Mod SelectedMod
        {
            get { return (Viewmodels.Mod)GetValue(SelectedModProperty); }
            set { SetValue(SelectedModProperty, value); }
        }

        public static readonly DependencyProperty SelectedModProperty =
            DependencyProperty.Register("SelectedMod", typeof(Viewmodels.Mod), typeof(MainWindow));



        readonly ObservableCollection<Viewmodels.ModList> openedModlistTabs = new();
        public MainWindow()
        {
            InitializeComponent();

            Viewmodels.Mod mod1 = CreateTestmod1();
            CreateTestmod2();
            CreateTestSublist();

            SelectedMod = mod1;

            //ModDetailsView.DataContext = SelectedMod;
            ModsList.ItemsSource = Mods;

            modlist.Name = "Default";

            var modlistVM = new Viewmodels.ModList(modlist);

            openedModlistTabs.Add(modlistVM);

            ModListsTabs.ItemsSource = openedModlistTabs;
        }

        void SublistSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count <= 0) return;

            if (e.AddedItems[0] is not Viewmodels.ModList selectedItem) return;

            if (openedModlistTabs.Contains(selectedItem) == false)
                openedModlistTabs.Add(selectedItem);

            ModListsTabs.SelectedItem = selectedItem;
        }

        void CreateTestSublist()
        {
            ModList sublist = new();
            sublist.Name = "ShantyTown";
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
