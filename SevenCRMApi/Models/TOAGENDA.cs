using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SevenCRMApi.Models
{
    [Serializable, DataContract(IsReference = true)]
    public class TOAGENDA
    {
        [DataMember]
        public string Usu_Codi { get; set; }
        [DataMember]
        public short Emp_codi { get; set; }
        [DataMember]
        public string Usu_Ejec { get; set; }
        //[DataMember]
        //public string Usu_Plan { get; set; }
        [DataMember]
        public int Act_Codi { get; set; }
        [DataMember]
        public string Age_Asun { get; set; }
        [DataMember]
        public int Pro_Cont { get; set; }
        //[DataMember]
        //public DateTime Age_Feje { get; set; }
        //[DataMember]
        //public DateTime Age_Fini { get; set; }
        //[DataMember]
        //public DateTime Age_FFin { get; set; }
        //[DataMember]
        //public DateTime Age_Tiem { get; set; }
        [DataMember]
        public int Eta_Codi { get; set; }
        [DataMember]
        public int Dpr_Codi { get; set; }
        [DataMember]
        public int Con_Codi { get; set; }
        //[DataMember]
        //public DateTime Age_Hini { get; set; }
        [DataMember]
        public DateTime Age_Fech { get; set; }
        [DataMember]
        public DateTime Age_Tiem { get; set; }
        [DataMember]
        public int Age_Dura { get; set; }
        [DataMember]
        public string Inv_Codi { get; set; }//código del invitado
    }
}