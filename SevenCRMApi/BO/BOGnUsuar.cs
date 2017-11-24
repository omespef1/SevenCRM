using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using KBOGn;
using KTO;
using Ophelia.Seven;
using SevenCRMApi.DAO;
using SevenCRMApi.Models;

namespace SevenCRMApi.BO
{
    public class BOGnUsuar
    {
        DAOGnUsuar dao = new DAOGnUsuar();

        public ActionResult<GN_USUAR> ValidarUsuario(string usuario, string password)
        {

            try
            {
                KTOGnUsuar TOusuar = new KTOGnUsuar();
                KBOGeneral BOGeneral = new KBOGeneral();

                TOusuar = BOGeneral.SEBOConsultarUsuPas(usuario, password);
                if (TOusuar.cod_usua.Trim() != "")
                {
                    KTODComLogin pTODComLogin = new KTODComLogin();

                    pTODComLogin.user = usuario;
                    pTODComLogin.password = password;
                    //pTODComLogin.sessionID = Session.SessionID;
                    pTODComLogin.alias = ConfigurationManager.AppSettings["alias"];
                    pTODComLogin.programa = "SWfSvrcn";
                    pTODComLogin.host = "localhost";
                    pTODComLogin.ip = "132.147.150.50";
                    pTODComLogin.ftp_Server = "localhost";

                    BOGeneral.BOInstanciaDcomLoginPrograma(pTODComLogin);
                    GN_USUAR user = dao.ConsultarUsuario(usuario);
                    user.Solo_Flujos = ConfigurationManager.AppSettings.Get("soloFlujos");
                    return new ActionResult<GN_USUAR>(true, user, ""); ;
                }
                else
                {
                    return new ActionResult<GN_USUAR>(false, null, "Verifique su usuario y contraseña.");
                }
            }
            catch (Exception ex)
            {
                return new ActionResult<GN_USUAR>(false, null, "Ocurrió un error validando el usuario.");
            }






            //GN_USUAR user = dao.ConsultarUsuario(usuario);
            //if (user == null)
            //    return new ActionResult<GN_USUAR>(false, null, "El usuario indicado no existe.");
            //if (password != Encrypta.DesencriptaClave(user.Usu_Idpk))
            //    return new ActionResult<GN_USUAR>(false, null, "Usuario o clave no válidos");

            //user.Solo_Flujos = ConfigurationManager.AppSettings.Get("soloFlujos");
            //return new ActionResult<GN_USUAR>(true, user, ""); ;
        }

        public List<GN_USUAR> CargarFlujos(string usu_codi)
        {
            if (dao.SEBOEsVendedorCmVende(usu_codi))
            {
                //drVende.Enabled = true;
                //drVende.Items.Clear();
                return this.SEBOListadeVendedorCmVende(usu_codi);
            }
            else
            {
                //drVende.Enabled = true;
                //drVende.Items.Clear();
                return dao.DAOSEListadeSinVendedorCmVende();
            }
        }

        public List<GN_USUAR> SEBOListadeVendedorCmVende(string pUsuario)
        {
            if ((pUsuario.ToUpper() == "CAMILOB") || (pUsuario.ToUpper() == "APRESIDENCIA"))
            {
                return dao.DAOSEListadeVendedorCmVende("");
                //pDdlVende.Items.Clear();
                //pDdlVende.DataTextField = "USU_NOMB";
                //pDdlVende.DataValueField = "USU_CODI";
                //pDdlVende.DataSource = set.Tables[0].DefaultView;
                //pDdlVende.DataBind();
                //pDdlVende.Items.Add("Seleccione");
                //pDdlVende.SelectedValue = pUsuario;
            }
            else
            {
                return dao.DAOSEListadeVendedorCmVende("CAMILOB");
                //pDdlVende.Items.Clear();
                //pDdlVende.DataTextField = "USU_NOMB";
                //pDdlVende.DataValueField = "USU_CODI";
                //pDdlVende.DataSource = set2.Tables[0].DefaultView;
                //pDdlVende.DataBind();
                //pDdlVende.Items.Add("Seleccione");
                //pDdlVende.SelectedValue = pUsuario;
            }
        }
    }
}