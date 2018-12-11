using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using IncomingCasualtyHandling.BL.Object_classes;
// **********************************
// Group: 2018E73
// Anni Lykke Wilhelmsen, 201509504
// Morten From Elvebakken, 201509095
// **********************************
namespace IncomingCasualtyHandling.BL.Interfaces
{
    public interface IMainView_Model
    {
        List<Triage> ListOfTriages { get; set; }
        string CurrentDateTime { get; set; }
        Specialty Specialty1 { get; set; }
        Triage Triage1 { get; set; }
        Triage Triage2 { get; set; }
        Triage Triage3 { get; set; }
        Triage Triage4 { get; set; }
        Triage Triage5 { get; set; }
        Triage Triage6 { get; set; }
        ETA ETA { get; set; }
        string ServerName { get; set; }
        Visibility ConnectionToInternet { get; set; }
        string NoConnectionString { get;}
        event PropertyChangedEventHandler PropertyChanged;
    }
}
