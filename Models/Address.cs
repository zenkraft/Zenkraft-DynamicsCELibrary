using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenkraftService.Models
{
    public class Address
    {
        public string city { get; set; }
        public string company { get; set; }
        public string country { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string postal_code { get; set; }
        public string state { get; set; }
        public string street1 { get; set; }
        public bool residential { get; set; }
       
    }
}
