using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace SchoolAgainBusinessLogic.ViewModels
{
    [DataContract]
    public class ServiceViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DisplayName("Название услуги")]
        [DataMember]
        public string ServiceName { get; set; }
        [DisplayName("Цена услуги")]
        [DataMember]
        public int Price { get; set; }
    }
}
