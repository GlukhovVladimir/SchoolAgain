using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace SchoolAgainDatabaseImplement.Models
{
    public class Service
    {
        public int Id { get; set; }
        [Required]
        public string ServiceName { get; set; }
        [Required]
        public int Price { get; set; }
    }
}
