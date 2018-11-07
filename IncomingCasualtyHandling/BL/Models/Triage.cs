using System.Windows;

namespace IncomingCasualtyHandling.BL.Models
{
    public class Triage
    {
        public string Name { get; set; }
        public string Colour { get; set; }
        public int Amount { get; set; }
        public Visibility ShowAs { get; set; }
    }
}
