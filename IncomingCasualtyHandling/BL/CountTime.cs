using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Threading;
using IncomingCasualtyHandling.BL.Interfaces;
using IncomingCasualtyHandling.BL.Models;
using IncomingCasualtyHandling.BL.Object_classes;

namespace IncomingCasualtyHandling.BL
{
    public class CountTime : ICountTime
    {

        private IMainView_Model _mainViewModel;
        private IOverviewView_Model _overviewViewModel;

        // CountTime made with inspiration from:
        // https://stackoverflow.com/a/5410783

        //readonly DispatcherTimer _currentDateTimeTimer = new DispatcherTimer();
        readonly DispatcherTimer _etaTimer = new DispatcherTimer();

        private System.Timers.Timer _currentDateTimeTimer;

        public CountTime(IMainView_Model mainViewModel, IOverviewView_Model overviewViewModel)
        {
            // Set Models
            _mainViewModel = mainViewModel;
            _overviewViewModel = overviewViewModel;

            _currentDateTimeTimer = new Timer();
            _currentDateTimeTimer.Elapsed += new System.Timers.ElapsedEventHandler(CurrentDateTime_TimerTick);
            _currentDateTimeTimer.Interval = 1000;
            _currentDateTimeTimer.AutoReset = true;
            _currentDateTimeTimer.Enabled = true;
        }

        // Set the culture to be the systems culture:
        private readonly CultureInfo _culture = CultureInfo.CurrentCulture;

        // CountTime-event that keeps track of current time and updates MainView_Model
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

        // List to contain ETAs
        private List<PatientModel> _listOfEtas;
        
        // Method to find the next coming ETA
        public void FindRelativeTime(List<PatientModel> sortedEtas)
        {
            _listOfEtas = sortedEtas;
            // Check the ETAs to see, if they are in the future
            foreach (var patient in _listOfEtas)
            {

                if (patient.ETA > DateTime.Now)
                {
                    CompareETATimeToCurrentTime(patient.ETA);
                    return;
                }
            }

            // If the compiler gets to here, there are no next coming ETAs
            _nextEta = new ETA
            {
                AbsoluteTime = "--:--",
                RelativeTime = ""
            };
            _overviewViewModel.Eta = _nextEta;

        }

        // Relative time calculations done with inspiration from:
        // https://stackoverflow.com/a/1248 and
        // https://stackoverflow.com/a/628203

        // Constants for relative time method
        private const int Second = 1;
        private const int MinuteInSeconds = 60 * Second;
        private const int HourInMinutes = 60 * MinuteInSeconds;

        // Prefix for relative time
        private string _prefix;

        private string _relativeTime;
        private TimeSpan _timeSpan;
        private TimeSpan _positiveTimeSpan;
        private double _timeDifference;
        private DateTime _dateTimeEta = new DateTime();
        ETA _nextEta = new ETA();

        // Compares the parameter time with the current time
        private void CompareETATimeToCurrentTime(DateTime nextEta)
        {
            _dateTimeEta = nextEta;
            _prefix = "+";
            _relativeTime = "NN:NN";

            // Find the timespan between now and the ETA
            _timeSpan = new TimeSpan(DateTime.Now.Ticks - nextEta.Ticks);
            _timeDifference = Math.Abs(_timeSpan.TotalSeconds);

            CalculateRelativeTime(_timeDifference, _timeSpan);

            // Prepare ETA timer and start it
            _etaTimer.Tick += (sender, e) =>
            {
                ETATime_TimerTick(sender, e, nextEta);
            };
            _etaTimer.Interval = TimeSpan.FromSeconds(1);
            _etaTimer.Start();

        }


        private void CalculateRelativeTime(double timeDifference, TimeSpan timeSpan)
        {
            // Check whether the timespan is negative and create a positive timespan to use for minutes and seconds
            // Change the prefix to a minus
            _positiveTimeSpan = timeSpan;
            if (timeSpan.CompareTo(TimeSpan.Zero) < 0)
            {
                _prefix = "-";
                _positiveTimeSpan = timeSpan.Negate();
            }

            // Time less than 99 hours but more than a minute:
            if (timeDifference < 999 * MinuteInSeconds && _timeDifference > 0)
            {
                // Ceil the total minute count, as seconds aren't shown. E.g. 41 minutes and 20 seconds becomes 42 minutes.
                _relativeTime = Math.Ceiling(_positiveTimeSpan.TotalMinutes).ToString().PadLeft(2, '0');
            }

            if (timeDifference > 999* MinuteInSeconds)
            {
                _prefix = ">";
                _relativeTime = "999";
            }

            // Check if minutes contains more than 2 characters
            // If it does, the prefix should be right next to the first number
            // If not, a space is placed between the prefix and the number
            if (_positiveTimeSpan.TotalMinutes > 99)
            {
                _nextEta = new ETA
                {
                    AbsoluteTime = _dateTimeEta.ToShortTimeString(),
                    RelativeTime = "(" + _prefix + _relativeTime + " minutes)"
                };
            }
            else
            {
                _nextEta = new ETA
                {
                    AbsoluteTime = _dateTimeEta.ToShortTimeString(),
                    RelativeTime = "(" + _prefix + " " + _relativeTime + " minutes)"
                };
            }


            

            _overviewViewModel.Eta = _nextEta;
            _mainViewModel.Eta = _nextEta;

        }

        // CountTime-event that keeps track of relative time until ETA and updates OverviewView_Model
        private void ETATime_TimerTick(object sender, EventArgs e, DateTime nextEta)
        {
            // Find the current timespan
            _timeSpan = new TimeSpan(DateTime.Now.Ticks - nextEta.Ticks);
            // Remove a second
            _timeSpan = _timeSpan.Subtract(new TimeSpan(0, 0, 1));
            // Find the time difference
            _timeDifference = Math.Abs(_timeSpan.TotalSeconds);

            // OBS - skal vi have denne if eller ej?
            // Check if ETA is NOW, because then a new ETA should be found
            if (_timeSpan.TotalSeconds > -Second)
            {
                // Stop the timer
                DispatcherTimer timer = (DispatcherTimer)sender;
                timer.Stop();
                // Find the next ETA in the list
                FindRelativeTime(_listOfEtas);
                return;
            }
            CalculateRelativeTime(_timeDifference, _timeSpan);
        }
    }
}
