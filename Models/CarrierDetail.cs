using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenkraftService.Models
{
    public class CarrierDetail
    {
        public List<ZkField> auth { get; set; }
        public List<ZkField> billing_fields { get; set; }
        public List<ZkField> custom_fields { get; set; }
        public List<ZkField> customs { get; set; }
        public string display_name { get; set; }
        public List<ZkField> label_types { get; set; }
        public bool multi_package { get; set; }
        public List<ZkField> notifications { get; set; }
        public List<ZkField> package_special_services { get; set; }
        public List<ZkField> packaging_types { get; set; }
        public CarrierRate rate { get; set; }
        public List<ZkField> references { get; set; }
        public bool servicetype_availability { get; set; }
        public List<ZkField> service_types { get; set; }
        public List<ZkField> special_services { get; set; }


    }
}
