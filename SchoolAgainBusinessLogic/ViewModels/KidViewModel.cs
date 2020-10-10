using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace SchoolAgainBusinessLogic.ViewModels
{
    [DataContract]
    public class KidViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int ClientId { get; set; }
        [DisplayName("Кличка питомца")]
        [DataMember]
        public string KidName { get; set; }
        [DataMember]
        [DisplayName("Вид")]
        public string Height { get; set; } //1
        [DataMember]
        [DisplayName("Порода")]
        public string Free { get; set; } //1
        [DisplayName("Возраст")]
        [DataMember]
        public int Age { get; set; }
        [DisplayName("Пол")]
        [DataMember]
        public string Gender { get; set; } //пол
        [DataMember]
        public List<ClientKidViewModel> ClientKids { get; set; }
    }
}
