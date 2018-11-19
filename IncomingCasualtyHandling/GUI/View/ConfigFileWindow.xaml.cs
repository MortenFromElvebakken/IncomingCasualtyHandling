using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;
using System.Xml;

namespace IncomingCasualtyHandling.GUI.View
{
    /// <summary>
    /// Interaction logic for ConfigFileWindow.xaml
    /// </summary>
    public partial class ConfigFileWindow : Window
    {
        public ConfigFileWindow()
        {
            InitializeComponent();
            
        }

        private bool shouldIClose = false;
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            shouldIClose = true;
            DialogResult = false;
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if(shouldIClose == false)
            {
                try
                {
                    XmlDocument test = new XmlDocument();
                    test.Load(ServerTextBoxName.Text);
                    e.Cancel = false;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    e.Cancel = true;
                    MessageBox.Show("Invalid configuration file URL");
                }
            }
            else
            {
                e.Cancel = false;
            }
        }
    }
}
