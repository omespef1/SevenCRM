using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SevenCRMApi.Models
{
    [Serializable, DataContract(IsReference = true)]
    public class WF_SEGUI
    {
        [DataMember]
        public short EMP_CODI { get; set; }
        [DataMember]
        public int CAS_CONT { get; set; }
        [DataMember]
        public int SEG_CONT { get; set; }
        [DataMember]
        public int SEG_CONA { get; set; }
        [DataMember]
        public int FLU_CONT { get; set; }
        [DataMember]
        public short ETA_CONT { get; set; }
        [DataMember]
        public string SEG_SUBJ { get; set; }
        [DataMember]
        public string SEG_PRIO { get; set; }
        [DataMember]
        public DateTime SEG_FREC { get; set; }
        [DataMember]
        public DateTime SEG_HREC { get; set; }
        [DataMember]
        public DateTime SEG_FLIM { get; set; }
        [DataMember]
        public DateTime SEG_HLIM { get; set; }
        [DataMember]
        public double SEG_DIAE { get; set; }
        [DataMember]
        public DateTime SEG_FCUL { get; set; }
        [DataMember]
        public DateTime SEG_HCUL { get; set; }
        [DataMember]
        public double SEG_DIAR { get; set; }
        [DataMember]
        public double SEG_DIAD { get; set; }
        [DataMember]
        public string SEG_ESTC { get; set; }
        [DataMember]
        public string SEG_ABRE { get; set; }
        [DataMember]
        public string SEG_UORI { get; set; }
        [DataMember]
        public string SEG_UENC { get; set; }
        [DataMember]
        public string SEG_COME { get; set; }
        [DataMember]
        public string SEG_ESTE { get; set; }
        [DataMember]
        public string SEG_RECO { get; set; }
        [DataMember]
        public string AUD_ESTA { get; set; }
        [DataMember]
        public string AUD_USUA { get; set; }
        [DataMember]
        public DateTime AUD_UFAC { get; set; }

        [DataMember]
        public string USU_NOMB { get; set; }
        [DataMember]
        public string CAS_DESC { get; set; }
        [DataMember]
        public string ETA_SSQL { get; set; }

        [DataMember]
        public string ADICIO { get; set; } //datos adicionales

        [NotMapped]
        [DataMember]
        public string SOLICITADO { get; set; }
        [DataMember]
        public string USU_CODI { get; set; }
        [NotMapped]
        [DataMember]
        public string COMENTARIOS { get; set; }
        [NotMapped]
        [DataMember]
        public string ACC_CONT { get; set; }
        [NotMapped]
        [DataMember]
        public List<WF_ACCIO> ACCIONES { get; set; }
        [NotMapped]
        [DataMember]
        public bool MOSTRARDETALLE { get; set; }
    }
}