using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiControllers.Models
{
    public class CarRecallItem
    {
        //Manufacturer
        public string Manufacturer { get; set; }
        //NHTSACampaignNumber
        public string NHTSACampaignNumber { get; set; }
        //ReportReceivedDate
        public string ReportReceivedDate { get; set; }
        //Component
        public string Component { get; set; }
        //Summary
        public string Summary { get; set; }
        //Conequence
        public string Conequence { get; set; }
        //Remedy
        public string Remedy { get; set; }
        //Notes
        public string Notes { get; set; }
        //ModelYear
        public string ModelYear { get; set; }
        //Make
        public string Make { get; set; }
        //Model
        public string Model { get; set; }
    }
}
