using System;

namespace IncomingCasualtyHandling.BL.Models
{
    public class OurPatient
    {
        public string PatientId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Triage { get; set; }
        public string Specialty { get; set; }
        public string ToHospital { get; set; }
        public DateTime ETA { get; set; }
    }
}
