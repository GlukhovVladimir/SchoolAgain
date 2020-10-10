using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SchoolAgainBusinessLogic.BindingModels
{
    [DataContract]
    public class KidBindingModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public string KidName { get; set; }
        [DataMember]
        public int ClientId { get; set; }
        [DataMember]
        public string Height { get; set; } //1
        [DataMember]
        public string Free { get; set; } //1
        [DataMember]
        public int Age { get; set; }
        [DataMember]
        public string Gender { get; set; } //пол
        public List<ClientKidBindingModel> ClientKids { get; set; }
    }
}
