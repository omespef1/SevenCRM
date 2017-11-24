using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KBOGn;
using SevenCRMApi.BO;
using SevenCRMApi.Models;

namespace SevenCRMApi.Controllers
{
    public class CrAgendController : ApiController
    {
        BOCrAgend bo = new BOCrAgend();
        // GET api/cragend
        [HttpGet]
        [Route("api/CrAgend/ListaActividades")]
        public ActionResult<List<CR_AGEND_GR>> ListaActividades(string usu_codi, DateTime fini, DateTime fina)
        {
            return new ActionResult<List<CR_AGEND_GR>>(true, bo.SEBOListaActividadesT(usu_codi, fini, fina), "");
        }

        // GET api/cragend/5
        [HttpGet]
        [Route("api/CrAgend/CancelarActividad")]
        public ActionResult CancelarActividad(short emp_codi, int pro_cont, short act_codi, string usu_eject, 
            string usu_plan, DateTime age_fini, string usu_codi)
        {
            KBOGeneral BOGeneral = new KBOGeneral();
            bool res = BOGeneral.SEBOCancelarActividades(emp_codi, pro_cont, act_codi, usu_eject, usu_plan, age_fini, usu_codi);
            return new ActionResult(res, "Ocurrió un error al cancelar la actividad.");
        }
    }
}
