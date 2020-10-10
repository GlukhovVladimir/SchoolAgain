using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using SchoolAgainBusinessLogic.BindingModels;
using SchoolAgainBusinessLogic.Enums;

namespace SchoolAgainBusinessLogic.ViewModels
{
    [DataContract]
    public class ReceptionViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int ClientId { get; set; }
        [DataMember]
        [DisplayName("ФИО Клиента")]
        public string ClientFIO { get; set; }
        [DataMember]
        [DisplayName("Сумма")]
        public int TotalSum { get; set; }
        [DataMember]
        [DisplayName("Статус приема")]
        public ReceptionStatus ReceptionStatus { get; set; }
        [DataMember]

        [DisplayName("Дата создания записи на прием")]
        public DateTime DateCreate { get; set; }
        [DataMember]
        [DisplayName("Оплачено")]
        public int LeftSum { get; set; }
        [DataMember]
        public virtual List<ReceptionServiceViewModel> ReceptionServices { get; set; }
    }
}
