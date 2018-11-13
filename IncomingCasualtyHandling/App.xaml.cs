using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using IncomingCasualtyHandling.BL;
using IncomingCasualtyHandling.BL.Models;
using IncomingCasualtyHandling.DAL;
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


            //Opret detail
            DetailViewModel _detailViewModel = new DetailViewModel();
            DetailViewViewModel _detailViewViewModel = new DetailViewViewModel(_detailViewModel);

            OverviewViewModel _overviewViewModel = new OverviewViewModel();
            OverviewViewViewModel _overviewViewViewModel = new OverviewViewViewModel(_overviewViewModel);

            MainViewModel _mainViewModel = new MainViewModel();
            MainViewViewModel _mainViewViewModel = new MainViewViewModel(_mainViewModel, _overviewViewViewModel,_detailViewViewModel);
            MainView _mainView = new MainView();
            _mainView.DataContext = _mainViewViewModel;

            //Data layer Initialize
            var lcs = new LoadConfigurationSettingsFromXMLDocument();
            var fhirCommands = new GetPatientsFromFhir(lcs);

            //Business layer initialize, attach sortpatients as an observer for the pattern that sends the first full
            //list of patients up to be sorted. 
            var timer = new Timer(_mainViewModel, _overviewViewModel);
            var sortETA = new SortETA(_overviewViewModel,_detailViewModel, timer);
            var sortSpecialty = new SortSpecialty(lcs, _overviewViewModel, _detailViewModel);
            var sortTriage = new SortTriage(lcs, _overviewViewModel, _detailViewModel);
            PatientHandlingLogic patientHandlingLogic = new PatientHandlingLogic(sortETA, sortSpecialty, sortTriage);
            fhirCommands.Attach(patientHandlingLogic);
            TestSubScriptionClass test = new TestSubScriptionClass();
            //test.SetupSubscription();
            fhirCommands.GetAllPatients();
            _mainView.Show();
        }
    }
}
