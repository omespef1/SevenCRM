using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SevenCRMApi.Models
{
    [Serializable, DataContract(IsReference = true)]
    public class CR_ACTIV
    {
        [DataMember]
        public string ACT_CODI { get; set; }
        [DataMember]
        public string ACT_NOMB { get; set; }
    }
}