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
    /// Interaction logic for ModList.xaml
    /// </summary>
    public partial class ModList : UserControl
    {
        public Action<object, SelectionChangedEventArgs> OnSublistsSelectionChanged
        {
            get { return (Action<object, SelectionChangedEventArgs>)GetValue(OnSublistsSelectionChangedProperty); }
            set { SetValue(OnSublistsSelectionChangedProperty, value); }
        }

        public static readonly DependencyProperty OnSublistsSelectionChangedProperty =
            DependencyProperty.Register("OnSublistsSelectionChanged", 
                typeof(Action<object, SelectionChangedEventArgs>),
                typeof(ModList));



        public ModList()
        {
            InitializeComponent();
        }

        private void Sublists_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OnSublistsSelectionChanged(sender, e);
        }
    }
}
