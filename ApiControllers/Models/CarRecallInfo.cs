using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiControllers.Models
{
    public class CarRecallInfo
    {
        public int Count { get; set; }
        public string Message { get; set; }
        public Dictionary<string, List<CarRecallItem>> Results { get; set; }
        
    }
}
