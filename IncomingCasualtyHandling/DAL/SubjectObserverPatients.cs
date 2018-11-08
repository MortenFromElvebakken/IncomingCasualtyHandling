using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IncomingCasualtyHandling.BL.Models;
using IncomingCasualtyHandling.DAL.Interface;

namespace IncomingCasualtyHandling.DAL
{
    public abstract class SubjectObserverPatients
    {
        List<IObserver> ObserverList;
        public SubjectObserverPatients()
        {
            ObserverList = new List<IObserver>();
        }



        public void Attach(IObserver o)
        {
            ObserverList.Add(o);
        }

        public void Detach(IObserver o)
        {
            ObserverList.Remove(o);
        }

        public void Notify(List<PatientModel> s)
        {
            foreach (var item in ObserverList)
            {
                item.Update(s);
            }
        }

    }
}
