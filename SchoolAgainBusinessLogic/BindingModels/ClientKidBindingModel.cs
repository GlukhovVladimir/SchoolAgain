using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SchoolAgainBusinessLogic.BindingModels
{
    [DataContract]
    public class ClientKidBindingModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public int ClientId { get; set; }
        [DataMember]
        public int KidId { get; set; }
        [DataMember]
        public int KidName { get; set; }
        [DataMember]
        public string ClientFIO { get; set; }
        [DataMember]
        public int Count { get; set; }
    }
}
