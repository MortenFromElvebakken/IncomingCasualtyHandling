﻿using System;
using System.Collections.Generic;
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
        List<List<PatientModel>> ListOfTriagePatientLists { get; set; }
        List<Specialty> ListOfSpecialties { get; set; }
        List<List<PatientModel>> ListOfSpecialtiesPatientLists { get; set; }
        List<PatientModel> ETAPatients { get; set; }
        List<TabControl> ListOfTabs { get; set; }
        string IconPath { get; }
        int SelectedTabIndex { get; set; }
        string StringFromChangeViewCommandParameter { get; set; }
        string PatientsInList { get; set; }
        event PropertyChangedEventHandler PropertyChanged;
    }
}