using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenkraftService.Models
{
    public class Package
    {
        public int height { get; set; }
        public string label { get; set; }
        public string label_type { get; set; }
        public int length { get; set; }
        public string tracking_number { get; set; }
        public decimal value { get; set; }
        public decimal weight { get; set; }
        public int width { get; set; }
        public string carrier_specific { get; set; }
        public string description { get; set; }
      
    }
}
