using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace SchoolAgainDatabaseImplement.Models
{
    public class Kid
    {
        public int Id { get; set; }
        [Required]
        public string KidName { get; set; }
       [Required]
        public int ClientId { get; set; }
        [Required]
        public string Height { get; set; } //1
        [Required]
        public string Free { get; set; } //1
        [Required]
        public int Age { get; set; }
        [Required]
        public string Gender { get; set; } //пол

    }
}
