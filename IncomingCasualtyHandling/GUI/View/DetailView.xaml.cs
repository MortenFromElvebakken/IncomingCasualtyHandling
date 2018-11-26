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

namespace IncomingCasualtyHandling.GUI.View
{
    /// <summary>
    /// Interaction logic for DetailView.xaml
    /// </summary>
    public partial class DetailView : UserControl
    {
        public DetailView()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var headerClicked = e.OriginalSource;
            System.Windows.Controls.GridViewColumnHeader clickedHeader = (GridViewColumnHeader)e.OriginalSource;
            var headerName = clickedHeader.Column.Header;

            //headerClicked.Content;
            //var columnBinding = headerClicked.Column.DisplayMemberBinding;
            //var sortBy = columnBinding?.Path.Path ?? headerClicked.Column.Header;
            MessageBox.Show("You clicked" + e.OriginalSource.ToString());
        }


    }
}
