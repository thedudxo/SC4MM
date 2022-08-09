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

namespace SC4MM_UI.Views
{
    /// <summary>
    /// Interaction logic for ModDetails.xaml
    /// </summary>
    public partial class ModDetails : UserControl
    {
        //public Mod? mod { get; set; }



        public Viewmodels.ModList ModList
        {
            get { return (Viewmodels.ModList)GetValue(ModListProperty); }
            set { SetValue(ModListProperty, value); }
        }

        public static readonly DependencyProperty ModListProperty =
            DependencyProperty.Register("ModList", typeof(Viewmodels.ModList), typeof(ModDetails), new PropertyMetadata(null));




        public ModDetails()
        {
            InitializeComponent();
        }

        private void RemoveFromList_Button_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is not Viewmodels.ModAndDesiredFiles) return;
            var mod = (Viewmodels.ModAndDesiredFiles)DataContext;

        }
    }
}
