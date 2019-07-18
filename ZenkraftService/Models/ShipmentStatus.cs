using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenkraftService.Models
{
    public class ShipmentStatus
    {
        public string carrier { get; set; }
        public string status { get; set; }
        public  List<Checkpoint> checkpoints{ get; set; }
        public bool delivered { get; set; }
        public List<PackageCheckpoint> packages { get; set; }
        public Shipment shipment { get; set; }
        public string tracking_stage { get; set; }

    }
}
