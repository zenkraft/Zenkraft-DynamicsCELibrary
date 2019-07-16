using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenkraftService.Models
{
    public class PackageCheckpoint
    {
        public List<Checkpoint> checkpoints { get; set; }
        public string tracking_number { get; set; }
    }
}
