﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IncomingCasualtyHandling.BL.Interfaces;
using IncomingCasualtyHandling.BL.Models;
using IncomingCasualtyHandling.BL.Object_classes;
using IncomingCasualtyHandling.DAL;
using IncomingCasualtyHandling.DAL.Interface;

namespace IncomingCasualtyHandling.BL
{
    public class SortETA
    {
        private OverviewView_Model _overviewViewModel;
        private DetailView_Model _detailView_Model;
        private MainView_Model _mainView_Model;
        private ITimer _timer;


        // Constructor
        public SortETA(OverviewView_Model overviewView_Model, DetailView_Model detailView_Model, MainView_Model mainView_Model, ITimer timer, IGetPatientsFromFHIR RecievePatientsFromFhir)
        {
            
            RecievePatientsFromFhir.PatientDataReady += SortForETA;
            _overviewViewModel = overviewView_Model;
            _detailView_Model = detailView_Model;
            _mainView_Model = mainView_Model;
            _timer = timer;
        }

        // Method that sorts ETAs
        // Adds a sorted patientList to DetailView_Model
        public void SortForETA(List<PatientModel> listOfPatients)
        {
            List<PatientModel> SortedETAList = listOfPatients.OrderBy(o => o.ETA).ToList();
            _detailView_Model.ETAPatients = SortedETAList;

            // When the ETA's are sorted, they are send to the Timer-class to work on relative time
            _timer.FindRelativeTime(SortedETAList);

        }
    }
}
