using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IncomingCasualtyHandling.DAL.Interface;
using IncomingCasualtyHandling.Models;

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

        public void Notify(List<OurPatient> s)
        {
            foreach (var item in ObserverList)
            {
                item.Update(s);
            }
        }

    }
}
