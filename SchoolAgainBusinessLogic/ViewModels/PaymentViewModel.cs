using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace SchoolAgainBusinessLogic.ViewModels
{
    [DataContract]
    public class PaymentViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int ClientId { get; set; }
        [DataMember]
        public int ReceptionId { get; set; }
        [DataMember]
        [DisplayName("Дата оплаты")]
        public DateTime DatePayment { get; set; }
        [DataMember]
        [DisplayName("Сумма")]
        public int Sum { get; set; }
    }
}
