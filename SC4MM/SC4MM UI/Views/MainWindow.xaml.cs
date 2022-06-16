using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SC4MM_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Viewmodels.Mod> Mods = new();
        public Viewmodels.Mod SelectedMod { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Viewmodels.Mod mod1 = new( new Mod(
                new ModFolders("Enabled/Awesome Hotel", "Disabled/Awesome Hotel", "Readme/Awesome Hotel"),
                new DummyFileMover())
            {
                Name = "Awesome Hotel",
            });

            mod1.ToggleByFileName.Add("AwesomeHotel4x3.bat", true);
            mod1.ToggleByFileName.Add("AwesomeHotel4x4.bat", true);
            mod1.ToggleByFileName.Add("DNight AwesomeHotel4x3.bat", false);
            mod1.ToggleByFileName.Add("DNight AwesomeHotel4x4.bat", false);

            mod1.ReadmeFiles.Add("Readme.txt");
            mod1.ReadmeFiles.Add("hotel.jpg");

            Viewmodels.Mod mod2 = new ( new Mod(
                new ModFolders("Enabled/Green Parks", "Disabled/Green Parks", "Readme/Green Parks"),
                new DummyFileMover())
            {
                Name = "Green Parks",
            });

            mod2.ToggleByFileName.Add("GreenPark.bat", true);
            mod2.ReadmeFiles.Add("GreenparksREADME.html");

            Mods.Add(mod1);
            Mods.Add(mod2);

            SelectedMod = mod1;

            ModDetailsView.DataContext = SelectedMod;

            ModsList.ItemsSource = Mods;
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
