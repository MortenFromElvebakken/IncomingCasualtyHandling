using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IncomingCasualtyHandling.BL.Models;

namespace IncomingCasualtyHandling.DAL.Interface
{
    public interface IObserver
    {
        void Update(List<PatientModel> s); // Indsæt at den notify'er med en liste af aircrafts
    }
}
