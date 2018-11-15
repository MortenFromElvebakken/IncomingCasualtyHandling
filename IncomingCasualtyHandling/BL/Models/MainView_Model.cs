using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IncomingCasualtyHandling.DAL;

namespace IncomingCasualtyHandling.BL.Models
{
    public class MainView_Model : ObservableObject
    {

        
        public MainView_Model()
        {
            //DateTime d;
            //d = DateTime.Now;
            //string day = d.Day.ToString().PadLeft(2, '0');
            //string month = d.ToString("MMM", _culture);
            //string year = d.Year.ToString();
            //string hour = d.Hour.ToString().PadLeft(2, '0');
            //string minute = d.Minute.ToString().PadLeft(2, '0');

            //CurrentDateTime = day + ". " + month + ". " + year + "\t" + hour + ":" + minute;
            _timer.Tick += new EventHandler(Timer_Click);
            //_timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Start();
            
        }
        // Timer made with inspiration from:
        // https://stackoverflow.com/a/5410783

        readonly System.Windows.Threading.DispatcherTimer _timer = new System.Windows.Threading.DispatcherTimer();

        private string _currentDateTime;
        // Property for binding 
        public string CurrentDateTime
        {
            get => _currentDateTime;
            set
            {
                if (_currentDateTime != value)
                {
                    _currentDateTime = value;
                    OnPropertyChanged();
                }
            }
        }

        private readonly CultureInfo _culture = CultureInfo.CurrentCulture;

        private void Timer_Click(object sender, EventArgs e)
        {
            DateTime d;

            d = DateTime.Now;
            string day = d.Day.ToString().PadLeft(2, '0');
            string month = d.ToString("MMM", _culture);
            string year = d.Year.ToString();
            string hour = d.Hour.ToString().PadLeft(2, '0');
            string minute = d.Minute.ToString().PadLeft(2, '0');

            CurrentDateTime = day + ". " + month + ". " + year + "\t" + hour + ":" + minute;
            //OnPropertyChanged("CurrentDateTime");
        }

    }
}
