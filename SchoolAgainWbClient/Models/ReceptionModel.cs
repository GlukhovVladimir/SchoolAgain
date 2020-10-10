using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolAgainBusinessLogic.Enums;

namespace SchoolAgainWbClient.Models
{
    public class ReceptionModel
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int KidId { get; set; }
        public string ClientFIO { get; set; }
        public string KidName { get; set; }
        public decimal TotalSum { get; set; }
        public int LeftSum { get; set; }
        public ReceptionStatus ReceptionStatus { get; set; }
        public DateTime DateCreate { get; set; }
        public virtual List<ReceptionServiceModel> ReceptionServices { get; set; }
    }
}
