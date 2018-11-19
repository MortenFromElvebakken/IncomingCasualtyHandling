using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IncomingCasualtyHandling.BL.Object_classes;

namespace IncomingCasualtyHandling.BL.Interfaces
{
    public interface IDetailView_Model
    {
        List<PatientModel> ETAPatients { get; set; }
        List<Specialty> ListOfSpecialties { get; set; }
        List<List<PatientModel>> ListOfSpecialtiesPatientLists { get; set; }
    }
}
