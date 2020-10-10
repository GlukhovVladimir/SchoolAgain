using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAgainWbClient.Models
{
    public class ClientModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string ClientFIO { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Block { get; set; }
    }
}
