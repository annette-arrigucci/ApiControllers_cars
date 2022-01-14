using System.Collections.Generic;

namespace ApiControllers.Models
{
    public class MakeSearchResult
    {
        public int Count { get; set; }
        public string Message { get; set; }
        public string SearchCriteria { get; set; }
        public List<VehicleObject> Results { get; set; }
    }
}
