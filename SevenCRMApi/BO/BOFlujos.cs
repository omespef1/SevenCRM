using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using KBOGn;
using Ophelia.Seven;
using SevenCRMApi.DAO;
using SevenCRMApi.Models;
using SWfSvrcn;

namespace SevenCRMApi.BO
{
    public class BOFlujos
    {
        DAOFlujos dao = new DAOFlujos();
        string usuario = ConfigurationManager.AppSettings["usuario"].ToString();
        string password = ConfigurationManager.AppSettings["password"].ToString();
        string alias = ConfigurationManager.AppSettings["alias"].ToString();

        internal List<WF_SEGUI> CargarFlujos(short emp_codi, string usu_codi)
        {
            string sourceDBPrevius = System.Configuration.ConfigurationManager.AppSettings.Get("sourceDB");
            //System.Configuration.ConfigurationManager.AppSettings.Set("sourceDB", "DwOXml/ODBConfigOphelia.xml");
            var flujos = dao.DAOSEListaFlujos(emp_codi, usu_codi);
            //System.Configuration.ConfigurationManager.AppSettings.Set("sourceDB", sourceDBPrevius);
            if (flujos.Any() && flujos.Find(o => o.ETA_SSQL != null && o.ETA_SSQL != "") != null)
            {
                foreach (WF_SEGUI item in flujos)
                {
                    item.MOSTRARDETALLE = false;
                    if (item.ETA_SSQL != null && item.ETA_SSQL != "")
                        item.ADICIO = dao.DAOSEConsultarAdicionales(emp_codi, item.CAS_CONT, item.ETA_SSQL);

                    List<WF_ACCIO> vValor = dao.DAOSEListaAccionesdeUnaEtapa(item.EMP_CODI, item.FLU_CONT,
                        item.ETA_CONT);
                    if (vValor != null && vValor.Any())
                    {
                        if (vValor.Count == 1)
                        {
                            item.ACC_CONT = vValor.FirstOrDefault().ACC_CONT.ToString();
                        }
                        else
                        {
                            if (item.ACC_CONT == null || item.ACC_CONT == "")
                            {
                                item.ACCIONES = vValor;
                                //return new ActionResult<List<WF_ACCIO>>(false, vValor, "Seleccione una acción.");
                            }
                        }
                    }
                    else
                    {
                        item.ACC_CONT = " ";
                    }
                }
            }
            return flujos;
        }

        internal ActionResult<WF_SEGUI> ListaFlujosDetalle(short emp_codi, int cas_cont, string usu_codi, int seg_cont)
        {
            List<WF_SEGUI> fl = dao.SEListaFlujosDetalle(emp_codi, cas_cont, usu_codi, seg_cont);
            if (fl != null)
            {
                foreach (WF_SEGUI item in fl)
                {
                    item.SOLICITADO = "";
                    FA_CLIEN rr = dao.DAOSEGetNombreSolicitante(item.EMP_CODI, item.CAS_CONT);
                    if (rr != null)
                    {
                        item.SOLICITADO = string.Concat(rr.CLI_NOCO, " - ", rr.DCL_NOMB);
                    }
                }
            }
            else
            {
                return new ActionResult<WF_SEGUI>(false, null, "");
            }
            return new ActionResult<WF_SEGUI>(true, fl.FirstOrDefault(), "");
        }

        internal ActionResult<WF_SEGUI> RechazarFlujo(WF_SEGUI pSWfSvrcn)
        {
            try
            {
                object varSali;
                string txterror = "";

                SWfSvrcnDmr _SWfSvrcn = new SWfSvrcnDmr();
                object[] varEntr = { usuario, Encrypta.EncriptarClave(password), alias, "SWfSvrcn", "", "", "", "", "", "N" };
                if (_SWfSvrcn.ProgramLogin(varEntr, out varSali, out txterror) != 0)
                {
                    throw new Exception("Error al ingresar a SEVEN-ERP, " + txterror);
                }

                _SWfSvrcn.emp_codi = pSWfSvrcn.EMP_CODI;
                _SWfSvrcn.usu_codi = pSWfSvrcn.USU_CODI;
                _SWfSvrcn.cas_cont = pSWfSvrcn.CAS_CONT;
                _SWfSvrcn.seg_cont = pSWfSvrcn.SEG_CONT;
                _SWfSvrcn.seg_come = pSWfSvrcn.COMENTARIOS;
                _SWfSvrcn.SelExec = "";
                _SWfSvrcn.WebServer = ConfigurationManager.AppSettings["webServer"].ToString();
                _SWfSvrcn.AppServer = ConfigurationManager.AppSettings["appServer"].ToString();
                _SWfSvrcn.FtpServer = ConfigurationManager.AppSettings["ftpServer"].ToString();
                _SWfSvrcn.FtpUser = "";
                _SWfSvrcn.FtpPassword = "";
                //_SWfSvrcn.WebServer = pSWfSvrcn.webServer;
                //_SWfSvrcn.AppServer = pSWfSvrcn.appServer;
                //_SWfSvrcn.FtpServer = pSWfSvrcn.ftpServer;
                //_SWfSvrcn.FtpUser = pSWfSvrcn.ftpUser;
                //_SWfSvrcn.FtpPassword = pSWfSvrcn.ftpPassword;
                _SWfSvrcn.seg_subj = pSWfSvrcn.SEG_SUBJ;
                int num = _SWfSvrcn.InvalidarSeguimiento();
                if (num == 0)
                    return new ActionResult<WF_SEGUI>(true, pSWfSvrcn, "");
                return new ActionResult<WF_SEGUI>(false, null, _SWfSvrcn.txterror);
            }
            catch (Exception)
            {
                return null;
            }
        }

        internal ActionResult<List<WF_ACCIO>> AprobarFlujo(WF_SEGUI pSWfSvrcn)
        {
            try
            {
                List<WF_ACCIO> vValor = dao.DAOSEListaAccionesdeUnaEtapa(pSWfSvrcn.EMP_CODI, pSWfSvrcn.FLU_CONT,
                    pSWfSvrcn.ETA_CONT);

                if (vValor != null && vValor.Any())
                {
                    if (vValor.Count == 1)
                    {
                        pSWfSvrcn.ACC_CONT = vValor.FirstOrDefault().ACC_CONT.ToString();
                    }
                    else
                    {
                        if (pSWfSvrcn.ACC_CONT == null || pSWfSvrcn.ACC_CONT == "")
                        {
                            return new ActionResult<List<WF_ACCIO>>(false, vValor, "Seleccione una acción.");
                        }
                    }
                }
                else
                {
                    pSWfSvrcn.ACC_CONT = " ";
                }

                object varSali;
                string txterror = "";

                SWfSvrcnDmr _SWfSvrcn = new SWfSvrcnDmr();
                object[] varEntr = { usuario, Encrypta.EncriptarClave(password), alias, "SWfSvrcn", "", "", "", "", "", "N" };
                if (_SWfSvrcn.ProgramLogin(varEntr, out varSali, out txterror) != 0)
                {
                    throw new Exception("Error al ingresar a SEVEN-ERP, " + txterror);
                }
                _SWfSvrcn.emp_codi = pSWfSvrcn.EMP_CODI;
                _SWfSvrcn.usu_codi = pSWfSvrcn.USU_CODI.ToUpper();
                _SWfSvrcn.cas_cont = pSWfSvrcn.CAS_CONT;
                _SWfSvrcn.seg_cont = pSWfSvrcn.SEG_CONT;
                _SWfSvrcn.seg_come = pSWfSvrcn.COMENTARIOS;
                _SWfSvrcn.acc_cont = pSWfSvrcn.ACC_CONT;
                _SWfSvrcn.SelExec = "";
                _SWfSvrcn.WebServer = ConfigurationManager.AppSettings["webServer"].ToString();
                _SWfSvrcn.AppServer = ConfigurationManager.AppSettings["appServer"].ToString();
                _SWfSvrcn.FtpServer = ConfigurationManager.AppSettings["ftpServer"].ToString();
                _SWfSvrcn.FtpUser = "";
                _SWfSvrcn.FtpPassword = "";
                //_SWfSvrcn.WebServer = pSWfSvrcn.webServer;
                //_SWfSvrcn.AppServer = pSWfSvrcn.appServer;
                //_SWfSvrcn.FtpServer = pSWfSvrcn.ftpServer;
                //_SWfSvrcn.FtpUser = pSWfSvrcn.ftpUser;
                //_SWfSvrcn.FtpPassword = pSWfSvrcn.ftpPassword;
                _SWfSvrcn.seg_subj = pSWfSvrcn.SEG_SUBJ;


                object din = new object();
                object dout;
                string txterr;

                //int num = _SWfSvrcn.TerminarSeguimiento(din, out dout, out txterr);
                int num = _SWfSvrcn.TerminarSeguimiento();
                if (num == 0)
                    return new ActionResult<List<WF_ACCIO>>(true, null, "");
                return new ActionResult<List<WF_ACCIO>>(false, null, _SWfSvrcn.txterror);
            }
            catch (Exception exception)
            {
                //base.BOException.Throw("KBOSWfSvrcn", "RechazarFlujo", exception);
                return new ActionResult<List<WF_ACCIO>>(false, null, exception.Message);
            }
        }

        internal int cantidadFlujos(short emp_codi, string usu_codi)
        {
            KBOGeneral BOGeneral = new KBOGeneral();
            string r = BOGeneral.SEBOCantidadFlujos(emp_codi.ToString(), usu_codi);
            return Convert.ToInt32(r);
        }
    }
}