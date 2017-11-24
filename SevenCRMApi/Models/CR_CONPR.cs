using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SevenCRMApi.Models
{
    [Serializable, DataContract(IsReference = true)]
    public class CR_CONPR
    {
        [DataMember]
        public string CON_CODI { get; set; }
        [DataMember]
        public string CON_NOMB { get; set; }
        [DataMember]
        public string CON_DIRE { get; set; }
        [DataMember]
        public string CON_TELE { get; set; }
        [DataMember]
        public string CON_CELU { get; set; }
        [DataMember]
        public string CON_DEPA { get; set; }
        [DataMember]
        public string CON_EMAI { get; set; }
        [DataMember]
        public string CON_CARG { get; set; }
        [DataMember]
        public string TITULO { get; set; }
    }
}