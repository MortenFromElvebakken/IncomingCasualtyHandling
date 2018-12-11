using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
namespace IncomingCasualtyHandling.BL.Models
{
    public class TabControl: ObservableObject
    {
        public string Name { get; set; }

        private ObservableCollection<ICHPatient> _data;
        public ObservableCollection<ICHPatient> Data
        {
            get => _data;
            set
            {
                _data = value;
                OnPropertyChanged();
            }
        }

        public Visibility isVisible { get; set; }
        public string PatientsInThisTab
        {
            get
            {
                if (Data != null)
                {
                    return string.Format(Data.Count + " patient(s)");
                }
                else
                {
                    return "";
                }
            }
        }
    }
}
