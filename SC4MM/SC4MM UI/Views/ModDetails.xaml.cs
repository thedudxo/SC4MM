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
        public Mod? mod { get; set; }

        public ModDetails()
        {
            InitializeComponent();
            //DataContext = mod;
        }
    }
}
