using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using IncomingCasualtyHandling.BL.Interfaces;
using IncomingCasualtyHandling.BL.Models;
using IncomingCasualtyHandling.BL.Object_classes;

namespace IncomingCasualtyHandling.BL
{
    public class Timer:ITimer
    {

        private IMainView_Model _mainViewModel;
        private IOverviewView_Model _overviewViewModel;

        // Timer made with inspiration from:
        // https://stackoverflow.com/a/5410783

        readonly DispatcherTimer _currentDateTimeTimer = new DispatcherTimer();
        readonly DispatcherTimer _etaTimer = new DispatcherTimer();


        public Timer(IMainView_Model mainViewModel, IOverviewView_Model overviewViewModel)
        {
            // Set Models
            _mainViewModel = mainViewModel;
            _overviewViewModel = overviewViewModel;

            // Prepare current datetime timer and start it
            _currentDateTimeTimer.Tick += new EventHandler(CurrentDateTime_TimerTick);
            _currentDateTimeTimer.Interval = TimeSpan.FromSeconds(1);
            _currentDateTimeTimer.Start();
        }

        // Set the culture to be the systems culture:
        private readonly CultureInfo _culture = CultureInfo.CurrentCulture;

        // Timer-event that keeps track of current time and updates MainView_Model
        private void CurrentDateTime_TimerTick(object sender, EventArgs e)
        {
            DateTime d;

            d = DateTime.Now;
            string day = d.Day.ToString().PadLeft(2, '0');
            string month = d.ToString("MMM", _culture);
            string year = d.Year.ToString();
            string hour = d.Hour.ToString().PadLeft(2, '0');
            string minute = d.Minute.ToString().PadLeft(2, '0');

            _mainViewModel.CurrentDateTime = day + ". " + month + ". " + year + "\t" + hour + ":" + minute;
        }

        // Relative time calculations done with inspiration from:
        // https://stackoverflow.com/a/1248 and
        // https://stackoverflow.com/a/628203

        // Constant for relative time method
        private const int Second = 1;
        private const int MinuteInSeconds = 60 * Second;
        private const int HourInMinutes = 60 * MinuteInSeconds;

        private string _prefix;
        private string _relativeTime;
        private TimeSpan _timeSpan;
        private TimeSpan _positiveTimeSpan;
        private double _timeDifference;
        private DateTime _dateTimeEta = new DateTime();
        ETA _nextEta = new ETA();

        // Compares the parameter time with the current time
        // Returns the relative time in minutes
        public void CompareETATimeToCurrentTime(DateTime nextEta)
        {
            _dateTimeEta = nextEta;
            _prefix = "+";
            _relativeTime = "NN:NN";

            _timeSpan = new TimeSpan(DateTime.Now.Ticks - nextEta.Ticks);
            _timeDifference = Math.Abs(_timeSpan.TotalSeconds);

            FindRelativeTime(_timeDifference, _timeSpan);

            // Prepare ETA timer and start it
            _etaTimer.Tick += (sender, e) =>
            {
                ETATime_TimerTick(sender, e, nextEta);
            };
            _etaTimer.Interval = TimeSpan.FromSeconds(1);
            _etaTimer.Start();

        }


        private void FindRelativeTime(double timeDifference, TimeSpan timeSpan)
        {
            // Check whether the timespan is negative
            // If it is, make it positive and change the prefix to a minus
            _positiveTimeSpan = timeSpan;
            if (timeSpan.CompareTo(TimeSpan.Zero) < 0)
            {
                _prefix = "-";
                _positiveTimeSpan = _timeSpan.Negate();
            }

            // Time less than a minute:
            if (_timeDifference < 1 * MinuteInSeconds)
            {
                //Set relative time to show no missing minutes
                _relativeTime = _positiveTimeSpan.Minutes.ToString().PadLeft(2, '0') + ":" + _positiveTimeSpan.Seconds.ToString().PadLeft(2, '0');
            }
            // Time less than 99 hours but more than a minute:
            if (_timeDifference < 99 * HourInMinutes && _timeDifference > 1 * MinuteInSeconds)
            {
                _relativeTime = _positiveTimeSpan.Minutes.ToString().PadLeft(2, '0') + ":" + _positiveTimeSpan.Seconds.ToString().PadLeft(2, '0');
            }

            if (_timeDifference > 99 * HourInMinutes)
            {
                _prefix = ">";
                _relativeTime = "99:59";
            }

            _nextEta = new ETA
            {
                AbsoluteTime = _dateTimeEta.ToShortTimeString(),
                RelativeTime = "(" + _prefix + _relativeTime + ")"
            };

            _overviewViewModel.Eta = _nextEta;
            _mainViewModel.Eta = _nextEta;

        }

        // Timer-event that keeps track of relative time until ETA and updates OverviewView_Model
        private void ETATime_TimerTick(object sender, EventArgs e, DateTime nextEta)
        {
            _timeSpan = new TimeSpan(DateTime.Now.Ticks - nextEta.Ticks);
            _timeSpan = _timeSpan.Subtract(new TimeSpan(0, 0, 1));
            _timeDifference = Math.Abs(_timeSpan.TotalSeconds);

            // OBS - skal vi have denne if eller ej?
            if (_timeSpan.TotalSeconds > -Second)
            {
                DispatcherTimer timer = (DispatcherTimer)sender;
                timer.Stop();
            }
            FindRelativeTime(_timeDifference, _timeSpan);
        }
    }
}
