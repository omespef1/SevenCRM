using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SevenCRMApi.Models
{
    [Serializable, DataContract(IsReference = true)]
    public class CR_AGEND
    {
        [DataMember]
        public string CON_NOMB { get; set; }
        [DataMember]
        public string CON_APEL { get; set; }
        [DataMember]
        public string ACT_NOMB { get; set; }
        [DataMember]
        public short EMP_CODI { get; set; }
        [DataMember]
        public int PRO_CONT { get; set; }
        [DataMember]
        public int ACT_CODI { get; set; }
        [DataMember]
        public string USU_EJEC { get; set; }
        [DataMember]
        public DateTime AGE_FREG { get; set; }
        [DataMember]
        public DateTime AGE_FINI { get; set; }
        [DataMember]
        public string USU_PLAN { get; set; }
        [DataMember]
        public DateTime AGE_FFIN { get; set; }
        [DataMember]
        public DateTime AGE_TIEM { get; set; }
        [DataMember]
        public DateTime AGE_FEJE { get; set; }
        [DataMember]
        public string PRO_NOMB { get; set; }
        [DataMember]
        public string AGE_ESTA { get; set; }
        [DataMember]
        public string ACT_OBSE { get; set; }
        [DataMember]
        public string AGE_ASUN { get; set; }
        [DataMember]
        public string CON_CARG { get; set; }
        [DataMember]
        public string ITE_TITU { get; set; }
    }
}