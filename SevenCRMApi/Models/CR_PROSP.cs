using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SevenCRMApi.Models
{
    [Serializable, DataContract(IsReference = true)]
    public class CR_PROSP
    {
        [DataMember]
        public string PRO_CONT { get; set; }
        [DataMember]
        public string PRO_NOMB { get; set; }
    }
}