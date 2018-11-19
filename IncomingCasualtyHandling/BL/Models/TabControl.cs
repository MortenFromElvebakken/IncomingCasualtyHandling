using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IncomingCasualtyHandling.BL.Object_classes;

namespace IncomingCasualtyHandling.BL.Models
{
    public class TabControl
    {
        public string Name { get; set; }
        public List<PatientModel> Data { get; set; }
    }
}
