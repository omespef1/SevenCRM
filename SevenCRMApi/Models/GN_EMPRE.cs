using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SevenCRMApi.Models
{
    [Serializable, DataContract(IsReference = true)]
    public class GN_EMPRE
    {
        [DataMember]
        public short Emp_Codi { get; set; }
        [DataMember]
        public string Emp_Nomb { get; set; }
    }
}