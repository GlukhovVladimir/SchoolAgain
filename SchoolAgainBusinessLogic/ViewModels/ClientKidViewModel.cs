using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace SchoolAgainBusinessLogic.ViewModels
{
    [DataContract]
    public class ClientKidViewModel
    {
        [DataMember]
        public int Id { get; set; }
        
        [DataMember]
        public int ClientId { get; set; }
        [DataMember]
        public int KidId { get; set; }
        [DisplayName("Имя питомца")]
        [DataMember]
        public int KidName { get; set; }
        [DisplayName("ФИО клиента")]
        [DataMember]
        public string ClientFIO { get; set; }
        [DataMember]
        public int Count { get; set; }
    }
}
