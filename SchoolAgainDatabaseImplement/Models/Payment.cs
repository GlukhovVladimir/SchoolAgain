using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SchoolAgainDatabaseImplement.Models
{
    public class Payment
    {
        public int Id { get; set; }
        [Required]
        public int ReceptionId { get; set; }
        public int ClientId { get; set; }
        [Required]
        public DateTime DatePayment { get; set; }
        [Required]
        public int Sum { get; set; }
        public virtual Client Client { get; set; }
        public virtual Reception Reception { get; set; }
    }
}
