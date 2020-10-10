using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using SchoolAgainBusinessLogic.Enums;

namespace SchoolAgainBusinessLogic.BindingModels
{
    [DataContract]
    public class ReceptionBindingModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public int ClientId { get; set; }
        [DataMember]
        public DateTime DateCreate { get; set; } 
        [DataMember]
        public int TotalSum { get; set; }
        [DataMember]
        public int LeftSum { get; set; }
        [DataMember]
        public DateTime? Date { get; set; }
        [DataMember]
        public DateTime? DateTo { get; set; }
        [DataMember]
        public ReceptionStatus ReceptionStatus { get; set; }
        [DataMember]
        public virtual List<ReceptionServiceBindingModel> ReceptionServices { get; set; }
    }
}
