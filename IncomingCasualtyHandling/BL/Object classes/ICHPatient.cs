using System;
using Hl7.Fhir.Model;

namespace IncomingCasualtyHandling.BL.Object_classes
{
    public class ICHPatient : ObservableObject
    {
        public string CPR { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public AdministrativeGender Gender { get; set; }
        public Triage Triage { get; set; }
        public string Specialty { get; set; }
        public string ToHospital { get; set; }
        public string FromDestination { get; set; }
        public DateTimeOffset LastUpdated { get; set; }
        public DateTime ETA { get; set; }
    }
}
