using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace SchoolAgainBusinessLogic.ViewModels
{
    [DataContract]
    public class ReceptionServiceViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int ServiceId { get; set; }
        [DataMember]
        public int ReceptionId { get; set; }
        [DataMember]
        public int Price { get; set; }
        [DataMember]
        [DisplayName("Название услуги")]
        public string ServiceName { get; set; }
        [DataMember]
        [DisplayName("Количество")]
        public int Count { get; set; }
    }
}
