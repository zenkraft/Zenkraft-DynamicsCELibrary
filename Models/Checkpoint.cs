using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenkraftService.Models
{
    public class Checkpoint
    {
        public string description { get; set; }
        public Location location { get; set; }
        public DateTime time { get; set; }
        public string status { get; set; }
        public string tracking_stage { get; set; }
    }
}
