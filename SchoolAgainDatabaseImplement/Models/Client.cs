using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace SchoolAgainDatabaseImplement.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Required]
        public string ClientFIO { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public bool Block { get; set; }
        [ForeignKey("ClientId")]
        public virtual List<Reception> Receptions { get; set; }
        [ForeignKey("ClientId")]
        public virtual List<Payment> Payments { get; set; }
        [ForeignKey("ClientId")]
        public virtual List<ClientKid> ClientKids { get; set; }
    }
}
