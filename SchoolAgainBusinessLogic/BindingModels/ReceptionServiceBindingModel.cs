using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SchoolAgainBusinessLogic.BindingModels
{
    [DataContract]
    public class ReceptionServiceBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int ServiceId { get; set; }
        [DataMember]
        public string ServiceName { get; set; }
        [DataMember]
        public int ReceptionId { get; set; }
        [DataMember]
        public int Price { get; set; }
        [DataMember]
        public int Count { get; set; }
    }
}
