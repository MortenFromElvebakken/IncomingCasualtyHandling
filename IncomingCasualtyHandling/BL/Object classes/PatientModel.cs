﻿using System;
using Hl7.Fhir.Model;

namespace IncomingCasualtyHandling.BL.Object_classes
{
    public class PatientModel
    {
        public string PatientId { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public AdministrativeGender Gender { get; set; }
        public string Triage { get; set; }
        public string Specialty { get; set; }
        public string ToHospital { get; set; }
        public DateTime ETA { get; set; }
    }
}
