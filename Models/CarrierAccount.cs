using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenkraftService.Models
{
    public class CarrierAccount
    {
        public string name { get; set; }
        public Int64 id { get; set; }
        public string carrier { get; set; }
        public string dim_units { get; set; }
        public string weight_units { get; set; }
        public string currency { get; set; }
        public string auth { get; set; }
    }
}
