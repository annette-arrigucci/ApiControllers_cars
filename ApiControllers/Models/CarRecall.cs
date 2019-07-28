using System.Collections.Generic;

namespace ApiControllers.Models
{
    public class CarRecall
    {
        public CarInfoModel model { get; set; }
        public string imageUrl { get; set; }
        //public string recallList { get; set; }
        //public List<CarRecallInfo> recallList { get; set; }
        public List<CarRecallItem> recallList { get; set; }
    }
}
