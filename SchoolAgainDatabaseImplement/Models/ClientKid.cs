using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace SchoolAgainDatabaseImplement.Models
{
    public class ClientKid
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int KidId { get; set; }
        public string ClientFIO { get; set; }
        public int Count { get; set; }
        public virtual Kid Kid { get; set; }
        public virtual Client Client { get; set; }
    }
}
