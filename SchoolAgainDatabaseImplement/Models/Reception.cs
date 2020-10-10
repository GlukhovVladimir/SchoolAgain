using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;
using SchoolAgainBusinessLogic.Enums;

namespace SchoolAgainDatabaseImplement.Models
{
    public class Reception
    {
        public int Id { get; set; }

        public int ClientId { get; set; }


        [Required]
        public int TotalSum { get; set; }

        public ReceptionStatus ReceptionStatus { get; set; }

        [Required]
        public DateTime DateCreate { get; set; }

        public Client Client { get; set; }

        [ForeignKey("ReceptionId")]
        public virtual List<ReceptionService> ReceptionServices { get; set; }
        [Required]
        [ForeignKey("ReceptionId")]
        public List<Payment> Payments { get; set; }
    }
}
