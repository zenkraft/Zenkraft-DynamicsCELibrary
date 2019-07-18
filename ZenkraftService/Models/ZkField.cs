using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenkraftService.Models
{
    public class ZkField
    {
        public string name { get; set; }
        public bool required { get; set; }
        public string slug { get; set; }
        public string type { get; set; }
        public bool ui { get; set; }
        public List<ZkField> collection { get; set; }
        public string code { get; set; }
        public int maxlength { get; set; }
        public int minlength { get; set; }
        public string node { get; set; }
        public List<string> sizes { get; set; }
        public bool required_weight { get; set; }
        public string value { get; set; }
    }
}
