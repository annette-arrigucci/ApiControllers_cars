using System.Collections.Generic;

namespace ApiControllers.Models
{
    public class RecallObject
    {
        public int Count { get; set; }
        public string Message { get; set; }
        public List<RecallResultObject> Results { get; set; }
    }
}
