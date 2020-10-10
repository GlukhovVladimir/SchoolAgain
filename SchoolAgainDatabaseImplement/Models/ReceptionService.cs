using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace SchoolAgainDatabaseImplement.Models
{
    public class ReceptionService
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int ReceptionId { get; set; }
        public int Count { get; set; }
        public virtual Service Service { get; set; }
        public virtual Reception Reception { get; set; }
    }
}
