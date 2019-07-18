using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenkraftService.Models
{
    public class Shipment
    {
        public string carrier { get; set; }
        public Cost costs { get; set; }
        public string currency { get; set; }
        public string dim_units { get; set; }
        public bool debug { get; set; }
        public string id { get; set; }
        public string label_type { get; set; }
        public List<Package> packages { get; set; }
        public Address recipient { get; set; }
        public Address sender { get; set; }
        public string service { get; set; }
        public DateTime ship_date { get; set; }
        public Int64 shipping_account { get; set; }
        public List<ZkField> special_services { get; set; }
        public string tracking_number { get; set; }
        public string type { get; set; }
        public string weight_units { get; set; }
        public bool test { get; set; }
        public string packaging { get; set; }
        public string additional_fields { get; set; }
        public List<ZkField> shipping_documents { get; set; }
        public List<ZkField> references { get; set; }



        



    }
}
