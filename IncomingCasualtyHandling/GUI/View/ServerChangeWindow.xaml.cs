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
using System.Windows.Shapes;
using Hl7.Fhir.Rest;

namespace IncomingCasualtyHandling.GUI.View
{
    /// <summary>
    /// Interaction logic for ServerChangeWindow.xaml
    /// </summary>
    public partial class ServerChangeWindow : Window
    {
        public ServerChangeWindow()
        {
            InitializeComponent();
        }
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {

            //Check if given URL is valid? 
            if (ServerName.Text != "")
            {
                //Lav fhirclient kald og se om den giver en valid endpoint, hvis ja, sæt dialogresult true
                
                try
                {
                    FhirClient testGivenEndpoint = new FhirClient(ServerName.Text);
                    testGivenEndpoint.CapabilityStatement();
                    DialogResult = true;
                        
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message + ". Try another endpoint");
                    DialogResult = false;
                }
                
            }
            else
            {
                MessageBox.Show("Type in URL of endpoint before pressing OK");
                DialogResult = false;
            }
            //DialogResult = true;
        }
    }
}
