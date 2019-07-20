using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenkraftService.Models
{
    public class ZkSuccess
    {
        public string error_message { get; set; }
        public string detail { get; set; }
        public bool success { get; set; }
    }
}
