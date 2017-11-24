using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SevenCRMApi.Models
{
    [Serializable, DataContract(IsReference = true)]
    public class WF_ACCIO
    {
        [DataMember]
        public short ACC_CONT { get; set; }
        [DataMember]
        public string ACC_NOMB { get; set; }
        [DataMember]
        public string ACC_ESTT { get; set; }
    }
}