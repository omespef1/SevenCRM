using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SevenCRMApi.Models
{
    [Serializable, DataContract(IsReference = true)]
    public class CR_ETAPA
    {
        [DataMember]
        public string ETA_CODI { get; set; }
        [DataMember]
        public string ETA_NOMB { get; set; }
        [DataMember]
        public string ETA_OBSE { get; set; }
    }
}