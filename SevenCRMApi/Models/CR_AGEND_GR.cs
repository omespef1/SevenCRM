using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SevenCRMApi.Models
{
    [Serializable, DataContract(IsReference = true)]
    public class CR_AGEND_GR
    {
        [DataMember]
        public string GrupoHora { get; set; }
        [DataMember]
        public List<CR_AGEND> Agenda { get; set; }
    }
}