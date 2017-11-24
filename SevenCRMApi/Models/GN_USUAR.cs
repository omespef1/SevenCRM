using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SevenCRMApi.Models
{
    [Serializable, DataContract(IsReference = true)]
    public class GN_USUAR
    {
        [DataMember]
        public string Usu_Codi { get; set; }
        [DataMember]
        public string Usu_Nomb { get; set; }
        [DataMember]
        public string Usu_Idpk { get; set; }
        [DataMember]
        public byte[] Usu_Foto { get; set; }
        [DataMember]
        public short? Emp_Codi { get; set; }


        public string Act_esta { get; set; }
        public DateTime Act_hora = Convert.ToDateTime("01/01/1900");
        public string Act_usua { get; set; }
        public string Blo_usua { get; set; }
        public string Cam_pass { get; set; }
        public string Cod_empl { get; set; }
        public int Cod_empr;
        public int Cod_gusu = 0;
        public string Cod_usua { get; set; }
        public string Est_usua { get; set; }
        public DateTime Fec_acti = Convert.ToDateTime("01/01/1900");
        public DateTime Fec_expi = Convert.ToDateTime("01/01/1900");
        public DateTime Hfi_domi = Convert.ToDateTime("01/01/1900");
        public DateTime Hfi_fest = Convert.ToDateTime("01/01/1900");
        public DateTime Hfi_juev = Convert.ToDateTime("01/01/1900");
        public DateTime Hfi_lune = Convert.ToDateTime("01/01/1900");
        public DateTime Hfi_mart = Convert.ToDateTime("01/01/1900");
        public DateTime Hfi_mier = Convert.ToDateTime("01/01/1900");
        public DateTime Hfi_saba = Convert.ToDateTime("01/01/1900");
        public DateTime Hfi_vier = Convert.ToDateTime("01/01/1900");
        public DateTime Hin_domi = Convert.ToDateTime("01/01/1900");
        public DateTime Hin_fest = Convert.ToDateTime("01/01/1900");
        public DateTime Hin_juev = Convert.ToDateTime("01/01/1900");
        public DateTime Hin_lune = Convert.ToDateTime("01/01/1900");
        public DateTime Hin_mart = Convert.ToDateTime("01/01/1900");
        public DateTime Hin_mier = Convert.ToDateTime("01/01/1900");
        public DateTime Hin_saba = Convert.ToDateTime("01/01/1900");
        public DateTime Hin_vier = Convert.ToDateTime("01/01/1900");
        public string Mod_cadm { get; set; }
        public string Nom_usua { get; set; }
        public string Pas_mbox { get; set; }
        public string Pas_usua { get; set; }
        public int Per_bloq = 0;
        public string Per_domi { get; set; }
        public string Per_fest { get; set; }
        public string Per_juev { get; set; }
        public string Per_lune { get; set; }
        public string Per_mart { get; set; }
        public string Per_mier { get; set; }
        public int Per_pass = 0;
        public string Per_saba { get; set; }
        public string Per_vier { get; set; }
        public string Tip_usua { get; set; }
        public DateTime Ult_camb = Convert.ToDateTime("01/01/1900");
        public DateTime Ult_entr = Convert.ToDateTime("01/01/1900");
        public string Usu_emai { get; set; }
        public string Usu_mbox { get; set; }
        public string Usu_wind { get; set; }
        public string Ver_actu { get; set; }
        public string Ver_cump { get; set; }




        public string Solo_Flujos { get; set; }
    }
}