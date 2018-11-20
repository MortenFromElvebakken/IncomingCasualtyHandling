using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using IncomingCasualtyHandling.BL;
using IncomingCasualtyHandling.BL.Interfaces;
using IncomingCasualtyHandling.BL.Models;
using IncomingCasualtyHandling.DAL;
using IncomingCasualtyHandling.DAL.Interface;
using IncomingCasualtyHandling.GUI.View;
using IncomingCasualtyHandling.GUI.ViewModels;

namespace IncomingCasualtyHandling
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        // Bootstrapper
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            

            // Create DAL
            //Data layer Initialize
            ILoadConfigurationSettings lcs;

            try
            {
                lcs = new LoadConfigurationSettingsFromXMLDocument();
            }
            catch (Exception)
            {
                //Initializing class as default, which returns null if nothing else has been done
                //This is to be able to check if it creates exceptions when reading from server, and if so, open up a new window
                //Where the user is prompted to give a URL to the configuration.xml file
                lcs = default(LoadConfigurationSettingsFromXMLDocument);
                ConfigFileWindow configFileWindow = new ConfigFileWindow();
                Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
                
                    if (configFileWindow.ShowDialog() == true)
                    {
                        
                        try
                        {
                            //new constructor that takes the newly specified Configuration file location
                            lcs = new LoadConfigurationSettingsFromXMLDocument(configFileWindow.ServerTextBoxName.Text);
                            ////test.Close();
                            
                            
                            //configFileWindow.shouldIClose = true;
                            //configFileWindow.Close();
                            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception);
                            MessageBox.Show("Invalid configuration file URL");
                        }
                    }
                    else
                    {
                        Environment.Exit(0);
                        
                    }
            }
            
            ISerializeToPatient isp = new SerialiseToPatient();
            var fhirCommands = new GetPatientsFromFhir(lcs,isp);

            // Create BLL
            // First Models
            // Then Logics
            IDetailView_Model _detailViewModel = new DetailView_Model();
            IOverviewView_Model _overviewViewModel = new OverviewView_Model();
            //IMainView_Model _mainViewModel = new MainView_Model();


            IGetPatientsFromFHIR IGetPatientsFromFhir = fhirCommands;
            IMainView_Model _mainViewModel = new MainView_Model(IGetPatientsFromFhir);
            ITimer timer = new Timer(_mainViewModel, _overviewViewModel);
            //var sortETA = new SortETA(_overviewViewModel,_detailViewModel, _mainViewModel, timer, IGetPatientsFromFhir);
            //var sortSpecialty = new SortSpecialty(lcs, _overviewViewModel, _detailViewModel, _mainViewModel, IGetPatientsFromFhir);
            //var sortTriage = new SortTriage(lcs, _overviewViewModel, _detailViewModel, _mainViewModel, IGetPatientsFromFhir);
            var sortETA = new SortETA(_overviewViewModel, _detailViewModel, _mainViewModel, timer, IGetPatientsFromFhir);
            var sortSpecialty = new SortSpecialty(lcs, _overviewViewModel, _detailViewModel, _mainViewModel, sortETA);
            var sortTriage = new SortTriage(lcs, _overviewViewModel, _detailViewModel, _mainViewModel, sortETA);
            fhirCommands.GetAllPatients();

          
            //Create viewmodels
            DetailView_ViewModel _detailViewViewModel = new DetailView_ViewModel(_detailViewModel);
            OverviewView_ViewModel _overviewViewViewModel = new OverviewView_ViewModel(_overviewViewModel);
            MainView_ViewModel _mainViewViewModel = new MainView_ViewModel(_mainViewModel, _overviewViewViewModel, _detailViewViewModel);

            // Create GUI
            MainView _mainView = new MainView();
            _mainView.DataContext = _mainViewViewModel;
            
            _mainView.Show();
        }
    }
}
