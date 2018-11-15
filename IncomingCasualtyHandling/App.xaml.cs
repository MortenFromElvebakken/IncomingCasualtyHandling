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
            //MVVM.WorldsSmallest view = new MVVM.WorldsSmallest();
            //MVVM.WorldsSmallestViewModel viewmodel = new MVVM.WorldsSmallestViewModel();
            //MVVM.WorldsSmallestModel model = new MVVM.WorldsSmallestModel();
            //view.DataContext = viewmodel;
            //view.Show();


            // Create DAL
            //Data layer Initialize
            ILoadConfigurationSettings lcs = new LoadConfigurationSettingsFromXMLDocument();
            ISerializeToPatient isp = new SerialiseToPatient();
            var fhirCommands = new GetPatientsFromFhir(lcs,isp);

            // Create BLL
            // First Models
            // Then Logics
            DetailView_Model _detailViewModel = new DetailView_Model();
            OverviewView_Model _overviewViewModel = new OverviewView_Model();
            MainView_Model _mainViewModel = new MainView_Model();


            IGetPatientsFromFHIR IGetPatientsFromFhir = fhirCommands;
            ITimer timer = new Timer(_mainViewModel, _overviewViewModel);
            var sortETA = new SortETA(_overviewViewModel,_detailViewModel, timer, IGetPatientsFromFhir);
            var sortSpecialty = new SortSpecialty(lcs, _overviewViewModel, _detailViewModel, IGetPatientsFromFhir);
            var sortTriage = new SortTriage(lcs, _overviewViewModel, _detailViewModel, IGetPatientsFromFhir);
            fhirCommands.GetAllPatients();


            //PatientHandlingLogic patientHandlingLogic = new PatientHandlingLogic(sortETA, sortSpecialty, sortTriage);
            //fhirCommands.Attach(patientHandlingLogic);
            //TestSubScriptionClass test = new TestSubScriptionClass();
            //test.SetupSubscription();
            //fhirCommands.GetAllPatients();

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
