using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAgainWbClient.Models
{
    public class ClientKidModel
    {
        public string ClientFIO { get; set; }
        public string KidName { get; set; }
        public string Height { get; set; } //1
        public string Free { get; set; } //1
        public int Age { get; set; }
        public string Gender { get; set; } //пол
        public int Count { get; set; }
        
    }
}
