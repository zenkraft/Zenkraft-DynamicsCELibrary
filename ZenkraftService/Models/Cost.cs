using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenkraftService.Models
{
    public class Cost
    {
        public string base_charge { get; set; }
        public string discounts { get; set; }
        public string net_charge { get; set; }
        public string surcharges { get; set; }
        public string taxes { get; set; }
        public string currency { get; set; }
    }
}
