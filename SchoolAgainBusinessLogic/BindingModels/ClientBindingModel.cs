using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SchoolAgainBusinessLogic.BindingModels
{
    [DataContract]
    public class ClientBindingModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public string ClientFIO { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Login { get; set; }
        [DataMember]
        public bool Block { get; set; }
        [DataMember]
        public string Phone { get; set; }
        public List<ClientKidBindingModel> ClientKids { get; set; }
    }
}
