using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace SchoolAgainBusinessLogic.ViewModels
{
    [DataContract]
    public class ClientViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DisplayName("ФИО клиента")]
        [DataMember]
        public string ClientFIO { get; set; }
        [DisplayName("Логин")]
        public string Login { get; set; }
        [DataMember]
        [DisplayName("Почта")]
        public string Email { get; set; }
        [DataMember]
        [DisplayName("Пароль")]
        public string Password { get; set; }
        [DataMember]
        [DisplayName("Номер телефона")]
        public string Phone { get; set; }
        [DataMember]
        [DisplayName("Блокировка")]
        public bool Block { get; set; }
    }
}
