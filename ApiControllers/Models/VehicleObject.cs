﻿namespace ApiControllers.Models
{
    public class VehicleObject
    {
        public int Make_ID { get; set; }
        public string Make_Name { get; set; }
        public int Model_ID { get; set; }
        public string Model_Name { get; set; }
        public int VehicleTypeId { get; set; }
        public string VehicleTypeName { get; set; }
    }
}