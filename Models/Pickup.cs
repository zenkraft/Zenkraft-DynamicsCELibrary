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
        public Person person { get; set; }
    }
}
