using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IncomingCasualtyHandling.BL.Models;
using IncomingCasualtyHandling.BL.Object_classes;

namespace IncomingCasualtyHandling.BL.Interfaces
{
    public interface IDetailView_Model
    {
        List<Triage> ListOfTriages { get; set; }
        List<List<ICHPatient>> ListOfTriagePatientLists { get; set; }
        List<Specialty> ListOfSpecialties { get; set; }
        List<List<ICHPatient>> ListOfSpecialtiesPatientLists { get; set; }
        List<ICHPatient> ETAPatients { get; set; }
        string IconPath { get; }
        int SelectedTabIndex { get; set; }
        string StringFromChangeViewCommandParameter { get; set; }
        event PropertyChangedEventHandler PropertyChanged;
        void ChangeTabsAllowed(string s);
        void GridViewColumnHeaderClicked(string s);
        bool ChangedFromMain { set; }
        ObservableCollection<TabControl> ObservableCollectionTabs { get; }
    }
}
