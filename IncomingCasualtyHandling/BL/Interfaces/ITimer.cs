﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IncomingCasualtyHandling.BL.Object_classes;

namespace IncomingCasualtyHandling.BL.Interfaces
{
    public interface ITimer
    {
        void FindRelativeTime(List<PatientModel> sortedEtas);
        //void CompareETATimeToCurrentTime(DateTime nextEta);

    }
}
