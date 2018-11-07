using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Hl7.Fhir.Model;
using IncomingCasualtyHandling.BL.Models;

namespace IncomingCasualtyHandling.GUI.ViewModels
{

    public class Patient
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class TabElement
    {
        public string TabTitle { get; set; }

        public ObservableCollection<Patient> Data { get; set; } = new ObservableCollection<Patient>();

    }

public class DetailViewViewModel : WorkspaceViewModel
    {


        //private List<TabElement> _tabs = new List<TabElement>();

        //public List<TabElement> Tabs => _tabs;

        private ObservableCollection<TabElement> _tabs = new ObservableCollection<TabElement>();
        public ObservableCollection<TabElement> Tabs => _tabs;


        public string TabTitle => "TabTitleText";

        public string Text => "TestText";


        public DetailViewViewModel()
        {
            ObservableCollection<Patient> _patientList = new ObservableCollection<Patient>();
            Patient patient1 = new Patient
            {
                Name = "Patient1",
                Age = 5
            };
            Patient patient2 = new Patient
            {
                Name = "Patient2",
                Age = 10
            };
            _patientList.Add(patient1);
            _patientList.Add(patient2);
            TabElement firstTab = new TabElement
            {
                TabTitle = "FirstTab",
                Data = _patientList
                
            };
            TabElement secondTab = new TabElement
            {
                TabTitle = "SecondTab"
            };
            TabElement thirdTab = new TabElement
            {
                TabTitle = "ThirdTab"
            };

            _tabs.Add(firstTab);
            _tabs.Add(secondTab);
            string text = _tabs[1].ToString();
            
            _tabs.Add(thirdTab);
        }

    }
}
