using System;

namespace ApiControllers.Models
{
    public class RecallResultObject
    {
        public string Manufacturer { get; set; }
        public string NHTSACampaignNumber { get; set; }
        public string NHTSAActionNumber { get; set; }
        public string ReportReceivedDate { get; set; }
        public string Component { get; set; }
        public string Summary { get; set; }
        public string Conequence { get; set; }
        public string Remedy { get; set; }
        public string Notes { get; set; }
        public string ModelYear { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
    }
}
