using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using SevenCRMApi.DAO;
using SevenCRMApi.Models;

namespace SevenCRMApi.BO
{
    public class BOCrAgend
    {
        DAOCrAgend dao = new DAOCrAgend();

        public string SEBOListaActividadesString(string pCOD_RESP, DateTime pFEC_DESD, DateTime pFEC_HAST)
        {
            try
            {
                string str = "";
                string str2 = "";
                DataSet set = new DataSet();
                set = dao.DAOSEListaActividadesDs(pCOD_RESP, pFEC_DESD, pFEC_HAST);
                for (int i = 0; i < set.Tables[0].Rows.Count; i++)
                {
                    if (str2 != this.DiaSemana((DateTime)set.Tables[0].Rows[i]["AGE_FINI"]))
                    {
                        str = (str + "<div class='pagina'>" + "<div class='linha'>") + "<div class='tile btn mui-Verde'> " + "<span class='titulo'>";
                        str = ((str + " <b>" + this.DiaSemana((DateTime)set.Tables[0].Rows[i]["AGE_FINI"]).ToUpper() + "</b>") + "</span>" + "</div>") + "</div>" + "</div> ";
                    }
                    else
                    {
                        str = str + " <br /> ";
                    }
                    DateTime time = (DateTime)set.Tables[0].Rows[i]["AGE_FINI"];
                    str = (((str + "<div class='pagina'>") + "<div class='linha'>" + "<div class='tile btn mui-Azul'> ") + "<span class='titulo'>" + "<b>") + time.ToShortTimeString() + " - ";
                    if (set.Tables[0].Rows[i]["AGE_FFIN"].ToString() != "")
                    {
                        str = str + ((DateTime)set.Tables[0].Rows[i]["AGE_FFIN"]).ToShortTimeString();
                    }
                    else
                    {
                        str = str + " ? ";
                    }
                    str = str + "</b>   " + set.Tables[0].Rows[i]["PRO_NOMB"].ToString();
                    str = ((((str + " - " + set.Tables[0].Rows[i]["CON_NOMB"].ToString() + " " + set.Tables[0].Rows[i]["CON_APEL"].ToString()) + " - " + set.Tables[0].Rows[i]["ACT_NOMB"].ToString()) + " - " + set.Tables[0].Rows[i]["AGE_ASUN"].ToString()) + "</span>" + "</div>") + "</div>" + "</div> ";
                }
                if (str.Trim() != "")
                {
                    return (str + " <br /> ");
                }
                str = "";
                str = (str + "<div class='pagina'>" + "<div class='linha'>") + "<div class='tile btn mui-Verde'> " + "<span class='titulo'>";
                string[] textArray2 = new string[] { str, " No Tiene Actividades Pendientes, Para las Fechas: ", pFEC_DESD.ToShortDateString(), " - ", pFEC_HAST.AddDays(-1.0).ToShortDateString() };
                return ((string.Concat(textArray2) + "</span>" + "</div>") + "</div>" + "</div> ");
            }
            catch (Exception exception)
            {
                //base.BOException.Throw("KBOGeneral", "SEBOListaActividadesT", exception);
                return (" Er. No Tiene Actividades Pendientes. - Ms:" + exception.Message);
            }
        }

        public List<CR_AGEND_GR> SEBOListaActividadesT(string pCOD_RESP, DateTime pFEC_DESD, DateTime pFEC_HAST)
        {
            List<CR_AGEND> lista = dao.DAOSEListaActividades(pCOD_RESP, pFEC_DESD, pFEC_HAST);
            if (lista != null && lista.Any())
            {
                List<CR_AGEND_GR> agenda = new List<CR_AGEND_GR>();
                foreach (DateTime item in lista.Select(o => o.AGE_FINI.Date).Distinct())
                {
                    agenda.Add(new CR_AGEND_GR()
                    {
                        GrupoHora = item.ToString("dddd d MMM  yyyy", 
                        System.Globalization.CultureInfo.CreateSpecificCulture("es-ES")),
                        Agenda = lista.FindAll(o => o.AGE_FINI.Date == item)
                    });
                }
                return agenda;
            }
            return null;
        }

        protected string DiaSemana(DateTime pValor)
        {
            string str = "";
            try
            {
                if (pValor.DayOfWeek == DayOfWeek.Monday)
                {
                    str = ((str + "Lunes ") + pValor.Day + "/") + pValor.Month;
                }
                else if (pValor.DayOfWeek == DayOfWeek.Tuesday)
                {
                    str = ((str + "Martes ") + pValor.Day + "/") + pValor.Month;
                }
                else if (pValor.DayOfWeek == DayOfWeek.Wednesday)
                {
                    str = ((str + "Miercoles ") + pValor.Day + "/") + pValor.Month;
                }
                else if (pValor.DayOfWeek == DayOfWeek.Thursday)
                {
                    str = ((str + "Jueves ") + pValor.Day + "/") + pValor.Month;
                }
                else if (pValor.DayOfWeek == DayOfWeek.Friday)
                {
                    str = ((str + "Viernes ") + pValor.Day + "/") + pValor.Month;
                }
                else if (pValor.DayOfWeek == DayOfWeek.Saturday)
                {
                    str = ((str + "Sabado ") + pValor.Day + "/") + pValor.Month;
                }
                else if (pValor.DayOfWeek == DayOfWeek.Sunday)
                {
                    str = ((str + "Domingo ") + pValor.Day + "/") + pValor.Month;
                }
                return str;
            }
            catch
            {
                return " - ";
            }
        }
    }
}