using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace SchoolAgainBusinessLogic.BindingModels
{
    [DataContract]
    public class PaymentBindingModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public int ReceptionId { get; set; }
        [DataMember]
        public int ClientId { get; set; }
        [DataMember]
        public DateTime DatePayment { get; set; }
        [DataMember]
        public int Sum { get; set; }
    }
}
