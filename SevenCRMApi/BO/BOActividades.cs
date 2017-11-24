using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using KBOGn;
using SevenCRMApi.Models;

namespace SevenCRMApi.BO
{
    public class BOActividades
    {
        private KBOGeneral BOGeneral = new KBOGeneral();

        public List<GN_USUAR> ListarEmpleados(string usu_codi)
        {
            DropDownList drVende = new DropDownList();
            if (BOGeneral.SEBOEsVendedorCmVende(usu_codi))
            {
                drVende.Items.Clear();
                BOGeneral.SEBOListadeVendedorCmVende(drVende, usu_codi);
            }
            else
            {
                drVende.Enabled = true;
                BOGeneral.SEBOListadeSinVendedorCmVende(drVende, usu_codi);
            }
            DataView dv = (DataView)drVende.DataSource;
            DataTable newTable = dv.ToTable("GN_USUAR", true, "USU_CODI", "USU_NOMB");
            List<GN_USUAR> empleados = new List<GN_USUAR>();
            if (newTable != null && newTable.Rows.Count > 0)
            {
                foreach (DataRow item in newTable.Rows)
                {
                    empleados.Add(new GN_USUAR()
                    {
                        Usu_Codi = item["USU_CODI"].ToString(),
                        Usu_Nomb = item["USU_NOMB"].ToString()
                    });
                }
            }
            return empleados;
        }

        internal List<CR_ACTIV> ListarActividades(short emp_codi)
        {
            DropDownList DpActiv = new DropDownList();
            BOGeneral.SEBOListadeActividades(DpActiv, emp_codi);

            List<CR_ACTIV> actividades = new List<CR_ACTIV>();
            DataSet ds = (DataSet)DpActiv.DataSource;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    actividades.Add(new CR_ACTIV()
                    {
                        ACT_CODI = row["ACT_CODI"].ToString(),
                        ACT_NOMB = row["ACT_NOMB"].ToString()
                    });
                }
            }
            else
            {
                actividades.Add(new CR_ACTIV()
                {
                    ACT_CODI = "-1",
                    ACT_NOMB = "NULO"
                });
            }
            return actividades;
        }

        internal List<CR_ETAPA> ListarEtapas(short emp_codi)
        {
            DropDownList DpEtapa = new DropDownList();
            BOGeneral.SEBOListadeEtapas(DpEtapa, emp_codi);
            List<CR_ETAPA> etapas = new List<CR_ETAPA>();
            DataView dv = (DataView)DpEtapa.DataSource;
            DataTable newTable = dv.ToTable("CR_ETAPA", true, "ETA_CODI", "ETA_NOMB", "ETA_OBSE");
            if (newTable != null && newTable.Rows.Count > 0)
            {
                foreach (DataRow row in newTable.Rows)
                {
                    etapas.Add(new CR_ETAPA()
                    {
                        ETA_CODI = row["ETA_CODI"].ToString(),
                        ETA_NOMB = row["ETA_NOMB"].ToString(),
                        ETA_OBSE = row["ETA_OBSE"].ToString()
                    });
                }
            }
            else
            {
                etapas.Add(new CR_ETAPA()
                {
                    ETA_CODI = "-1",
                    ETA_NOMB = "NULO"
                });
            }
            return etapas;
        }

        internal ActionResult<List<CR_PROSP>, List<CR_DPROS>, List<CR_CONPR>> cargarClientes(short emp_codi, string filter)
        {
            DropDownList DpClient = new DropDownList();
            BOGeneral.SEBOListadeClientes(DpClient, filter, emp_codi);

            List<CR_PROSP> clients = new List<CR_PROSP>();

            DataView dv = (DataView)DpClient.DataSource;
            DataTable newTable = null;
            if (dv != null)
                newTable = dv.ToTable("CR_PROSP", true, "PRO_CONT", "PRO_NOMB");
            if (newTable != null && newTable.Rows.Count > 0)
            {
                foreach (DataRow row in newTable.Rows)
                {
                    clients.Add(new CR_PROSP()
                    {
                        PRO_CONT = row["PRO_CONT"].ToString(),
                        PRO_NOMB = row["PRO_NOMB"].ToString()
                    });
                }
            }
            else
            {
                clients.Add(new CR_PROSP()
                {
                    PRO_CONT = "-1",
                    PRO_NOMB = "NULO"
                });
            }
            List<CR_DPROS> dpros = CargarListaDetalles(clients.FirstOrDefault().PRO_CONT);
            List<CR_CONPR> conpr = CargarListaContactoCliente(clients.FirstOrDefault().PRO_CONT, dpros.FirstOrDefault().DPR_CODI);
            return new ActionResult<List<CR_PROSP>, List<CR_DPROS>, List<CR_CONPR>>(true, clients, dpros, conpr, "");
        }

        public List<CR_DPROS> CargarListaDetalles(string cliente)
        {
            DropDownList DpDetalles = new DropDownList();
            BOGeneral.SEBOListaDetalleCliente(DpDetalles, Convert.ToInt32(cliente));

            List<CR_DPROS> d = new List<CR_DPROS>();
            DataView dv = (DataView)DpDetalles.DataSource;
            DataTable newTable = null;
            if (dv != null)
                newTable = dv.ToTable("CR_DPROS", true, "DPR_CODI", "DPR_DIRE", "DPR_MAIL", "DPR_NOMB", "DPR_NTEL");
            if (newTable != null && newTable.Rows.Count > 0)
            {
                foreach (DataRow row in newTable.Rows)
                {
                    d.Add(new CR_DPROS()
                    {
                        DPR_CODI = row["DPR_CODI"].ToString(),
                        DPR_DIRE = row["DPR_DIRE"].ToString(),
                        DPR_MAIL = row["DPR_MAIL"].ToString(),
                        DPR_NOMB = row["DPR_NOMB"].ToString(),
                        DPR_NTEL = row["DPR_NTEL"].ToString()
                    });
                }
            }
            else
            {
                d.Add(new CR_DPROS()
                {
                    DPR_CODI = "-1",
                    DPR_NOMB = "NULO"
                });
            }
            return d;
        }

        public List<CR_CONPR> CargarListaContactoCliente(string cliente, string detalle)
        {
            DropDownList DpConta = new DropDownList();
            BOGeneral.SEBOListadeContactoCliente(DpConta, Convert.ToInt32(cliente), Convert.ToInt32(detalle));

            List<CR_CONPR> d = new List<CR_CONPR>();
            DataView dv = (DataView)DpConta.DataSource;
            DataTable newTable = null;
            if (dv != null)
                newTable = dv.ToTable("CR_CONPR", true, "CON_CARG", "CON_CELU", "CON_CODI",
                "CON_DEPA", "CON_DIRE", "CON_EMAI", "CON_NOMB", "CON_TELE", "TITULO");
            if (newTable != null && newTable.Rows.Count > 0)
            {
                foreach (DataRow row in newTable.Rows)
                {
                    d.Add(new CR_CONPR()
                    {
                        CON_CARG = row["CON_CARG"].ToString(),
                        CON_CELU = row["CON_CELU"].ToString(),
                        CON_CODI = row["CON_CODI"].ToString(),
                        CON_DEPA = row["CON_DEPA"].ToString(),
                        CON_DIRE = row["CON_DIRE"].ToString(),
                        CON_EMAI = row["CON_EMAI"].ToString(),
                        CON_NOMB = row["CON_NOMB"].ToString(),
                        CON_TELE = row["CON_TELE"].ToString(),
                        TITULO = row["TITULO"].ToString()
                    });
                }
            }
            else
            {
                d.Add(new CR_CONPR()
                {
                    CON_CODI = "-1",
                    CON_NOMB = "NULO"
                });
            }
            return d;
        }

        internal ActionResult<List<CR_DPROS>, List<CR_CONPR>> cargarDClientes(string pro_cont)
        {
            List<CR_DPROS> dpros = CargarListaDetalles(pro_cont);
            List<CR_CONPR> conpr = CargarListaContactoCliente(pro_cont, dpros.FirstOrDefault().DPR_CODI);
            return new ActionResult<List<CR_DPROS>, List<CR_CONPR>>(true, dpros, conpr, "");
        }

        internal ActionResult<List<CR_CONPR>> cargarContactos(string pro_cont, string dpr_codi)
        {
            List<CR_CONPR> conpr = CargarListaContactoCliente(pro_cont, dpr_codi);
            return new ActionResult<List<CR_CONPR>>(true, conpr, "");
        }

        internal ActionResult GuardarActividad(TOAGENDA agenda)
        {

            //try
            //{
            //    KTOCm.KTOCmActcl TOCmActcl = new KTOCm.KTOCmActcl();
            //    TOCmActcl.fec_prog = DateTime.Now;
            //    TOCmActcl.act_usua = agenda.Usu_Codi;
            //    TOCmActcl.cod_asig = agenda.Usu_Codi;
            //    TOCmActcl.cod_resp = agenda.Usu_Ejec;
            //    TOCmActcl.cod_acti = agenda.Act_Codi;
            //    TOCmActcl.cod_clie = agenda.Pro_Cont;
            //    TOCmActcl.rmt_cont = agenda.Con_Codi;
            //    TOCmActcl.asu_acti = agenda.Age_Asun;
            //    TOCmActcl.fec_ejec = new DateTime(agenda.Age_Fech.Year, agenda.Age_Fech.Month, agenda.Age_Fech.Day, agenda.Age_Tiem.Hour, agenda.Age_Tiem.Minute, 00);
            //    TOCmActcl.fec_term = TOCmActcl.fec_ejec.AddHours(Convert.ToDouble(agenda.Age_Dura));
            //    TOCmActcl.tie_acti = Convert.ToInt64(agenda.Age_Dura);

            //    BOGeneral.BOInsertarCmActcl(TOCmActcl);
            //    return new ActionResult(true, "");
            //}
            //catch (KBOGnExcepcion ex)
            //{
            //    return new ActionResult(false, ex.Message);

            //}
            //catch (Exception ex)
            //{
            //    return new ActionResult(false, ex.Message);
            //}
            try
            {
                DigitalWare.Seven.TO.SETOAgenda newAgenda = new DigitalWare.Seven.TO.SETOAgenda();

                newAgenda.aud_usua = agenda.Usu_Codi;
                newAgenda.emp_codi = agenda.Emp_codi;
                newAgenda.aud_ufac = DateTime.Now;
                newAgenda.aud_esta = "A";
                newAgenda.usu_ejec = agenda.Usu_Ejec;
                newAgenda.usu_plan = agenda.Usu_Codi;
                newAgenda.act_codi = agenda.Act_Codi;
                newAgenda.age_freg = DateTime.Now;
                newAgenda.age_asun = agenda.Age_Asun;
                newAgenda.pro_cont = agenda.Pro_Cont;
                newAgenda.age_feje = new DateTime(agenda.Age_Fech.Year, agenda.Age_Fech.Month, agenda.Age_Fech.Day, agenda.Age_Tiem.Hour, agenda.Age_Tiem.Minute, 00);
                newAgenda.age_fini = newAgenda.age_feje;


                newAgenda.age_ffin = newAgenda.age_fini.AddHours(Convert.ToDouble(agenda.Age_Dura));
                newAgenda.age_tiem = new DateTime(1899, 12, 30, (newAgenda.age_ffin.Hour - newAgenda.age_fini.Hour), 0, 0);
                newAgenda.age_esta = "P";
                newAgenda.eta_codi = agenda.Eta_Codi;
                newAgenda.dpr_codi = agenda.Dpr_Codi;
                newAgenda.con_codi = agenda.Con_Codi;
                newAgenda.age_hini = new DateTime(1899, 12, 30, agenda.Age_Tiem.Hour, agenda.Age_Tiem.Minute, 0);

                BOGeneral.SEBOInsertarCmActcl(newAgenda);
                return new ActionResult(true, "");
            }
            catch (KBOGnExcepcion ex)
            {
                return new ActionResult(false, ex.Message);
                //lblError.Text = ex.Message;
                //lblError.Visible = true;
            }
            catch (Exception ex)
            {
                return new ActionResult(false, ex.Message);
            }
        }

        internal ActionResult invitarActividad(TOAGENDA agenda)
        {
            try
            {
                Int16 pEmpr = agenda.Emp_codi;
                Int32 pClie = agenda.Pro_Cont;
                Int16 pActi = (short)agenda.Act_Codi;
                String pResp = agenda.Usu_Codi;
                String pAsig = agenda.Usu_Codi;
                DateTime pFecE = agenda.Age_Fech;//.AddHours(agenda.Age_Tiem.Hour).AddMinutes(agenda.Age_Tiem.Minute); //new DateTime(Convert.ToInt32(Request.QueryString["pFecEA"].ToString()), Convert.ToInt32(Request.QueryString["pFecEM"].ToString()), Convert.ToInt32(Request.QueryString["pFecED"].ToString()), Convert.ToInt32(Request.QueryString["pHorEH"].ToString()), Convert.ToInt32(Request.QueryString["pHorEM"].ToString()), 00);

                KBOGeneral BOGeneral = new KBOGeneral();
                BOGeneral.SEBOInsertarInvitadoAActividad(pEmpr, pClie, pActi, pResp, pAsig, pFecE, agenda.Usu_Codi, agenda.Inv_Codi);
                //BOGeneral.SEBOListaActividadesInv(drVende, pEmpr, pClie, pActi, pAsig, pFecE);
                //txtInvitados.Text = string.Empty;
                //txtInvitados.Text = BOGeneral.SEBOActividadesInvitados(pEmpr, pClie, pActi, pAsig, pFecE);
                //lblError.Text = "";
                //lblError.Visible = false;
                return new ActionResult(true, "");
            }
            catch (KBOGnExcepcion ex)
            {
                return new ActionResult(false, ex.Message);
            }
            catch (Exception ex)
            {
                return new ActionResult(false, ex.Message);
            }
        }
    }
}