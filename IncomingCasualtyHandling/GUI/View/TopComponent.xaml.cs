using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IncomingCasualtyHandling.GUI.View
{
    /// <summary>
    /// Interaction logic for TopComponent.xaml
    /// </summary>
    public partial class TopComponent : UserControl
    {
        //// Timer made with inspiration from:
        //// https://stackoverflow.com/a/5410783

        //System.Windows.Threading.DispatcherTimer _timer = new System.Windows.Threading.DispatcherTimer();

        //// Property for binding 
        //private string _currentDateTime;
        //public string CurrentDateTime
        //{
        //    get => _currentDateTime;
        //    set => _currentDateTime = value;
        //}

        public TopComponent()
        {
            InitializeComponent();
            //DateTimeLabel.Content = DateTime.Now.Hour + " : " + DateTime.Now.Minute;
            //DateTime d;
            //d = DateTime.Now;
            //string day = d.Day.ToString().PadLeft(2, '0');
            //string month = d.ToString("MMM", culture);
            //string year = d.Year.ToString();
            //string hour = d.Hour.ToString().PadLeft(2, '0');
            //string minute = d.Minute.ToString().PadLeft(2, '0');
            //DateTimeLabel.Content = day + ". " + month + ". " + year + "\t" + hour + ":" + minute;

            //CurrentDateTime = day + ". " + month + ". " + year + "\t" + hour + ":" + minute;
            //_timer.Tick += new EventHandler(Timer_Click);
            ////_timer.Interval = new TimeSpan(0, 0, 1);
            //_timer.Interval = TimeSpan.FromSeconds(1);
            //_timer.Start();
        }

        //private CultureInfo culture = System.Globalization.CultureInfo.CurrentCulture;

        //private void Timer_Click(object sender, EventArgs e)
        //{
        //    DateTime d;

        //    d = DateTime.Now;
        //    string day = d.Day.ToString().PadLeft(2, '0');
        //    string month = d.ToString("MMM", culture);
        //    string year = d.Year.ToString();
        //    string hour = d.Hour.ToString().PadLeft(2, '0');
        //    string minute = d.Minute.ToString().PadLeft(2, '0');

        //    CurrentDateTime = day + ". " + month + ". " + year + "\t" + hour + ":" + minute;

        //}
    }
}
