using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SevenCentralizacion.Models
{
    [Serializable, DataContract(IsReference = true)]
    public class GN_CONEX
    {
        [DataMember]
        public string CNX_NOMB { get; set; }
        [DataMember]
        public string CNX_IPSR { get; set; }
    }
}