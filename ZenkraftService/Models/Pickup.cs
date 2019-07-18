using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenkraftService.Models
{
    public class Pickup
    {
        public string carrier { get; set; }
        public Int64 shipping_account { get; set; }
        public ZkTime time { get; set; }
        public Address location { get; set; }
        public List<Shipment> shipments { get; set; }
        public bool test { get; set; }
        public bool debug { get; set; }
        public string weight_units { get; set; }
        public string dim_units { get; set; }
        public string description { get; set; }
        public string id { get; set;}
        public string confirmation_number { get; set; }
        public string pickup_location_id { get; set; }
    }
}
