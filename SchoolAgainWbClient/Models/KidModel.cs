using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAgainWbClient.Models
{
    public class KidModel
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите имя")]
        public string KidName { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите 1")]
        public string Height { get; set; } //1
        [Required(ErrorMessage = "Пожалуйста, введите породу")]
        public string Free { get; set; } //1
        [Required(ErrorMessage = "Пожалуйста, введите возраст")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите пол")]
        public string Gender { get; set; } //пол
        public Dictionary<int, int> ClientKids { get; set; }
    }
}
