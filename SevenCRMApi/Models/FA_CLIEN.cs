using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SevenCRMApi.Models
{
    [Serializable, DataContract(IsReference = true)]
    public class FA_CLIEN
    {
        [DataMember]
        public string CLI_NOCO { get; set; }
        [DataMember]
        public string DCL_NOMB { get; set; }
    }
}