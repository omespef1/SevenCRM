using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SevenCRMApi.Models
{
    [Serializable, DataContract(IsReference = true)]
    public class CR_DPROS
    {
        [DataMember]
        public string DPR_NOMB { get; set; }
        [DataMember]
        public string DPR_CODI { get; set; }
        [DataMember]
        public string DPR_DIRE { get; set; }
        [DataMember]
        public string DPR_MAIL { get; set; }
        [DataMember]
        public string DPR_NTEL { get; set; }
    }
}