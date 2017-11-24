using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SevenCRMApi.BO;
using SevenCRMApi.Models;

namespace SevenCRMApi.Controllers
{
    public class FlujosController : ApiController
    {
        BOFlujos bo = new BOFlujos();

        #region Flujo
        [HttpGet]
        [Route("api/Flujos/FlujosAdm")]
        public List<WF_SEGUI> Get(short emp_codi, string usu_codi)
        {
            return bo.CargarFlujos(emp_codi, usu_codi);
        }

        [HttpPost]
        [Route("api/Flujos/FlujosAdm")]
        public ActionResult Post(WF_SEGUI segui)
        {
            var res = bo.AprobarFlujo(segui);
            return new ActionResult(res.State, res.Message);
        }

        [HttpPut]
        [Route("api/Flujos/FlujosAdm")]
        public ActionResult<WF_SEGUI> Put(WF_SEGUI p)
        {
            var res = bo.RechazarFlujo(p);
            return new ActionResult<WF_SEGUI>(res.State, p, res.Message);
        }

        [HttpGet]
        [Route("api/Flujos/cantidadFlujos")]
        public int cantidadFlujos(short emp_codi, string usu_codi)
        {
            return bo.cantidadFlujos(emp_codi, usu_codi);
        }        
        #endregion

        #region Detalle de Flujo
        [HttpGet]
        [Route("api/Flujos/FlujoDetalle")]
        public ActionResult<WF_SEGUI> GetFlujoDetalle(short emp_codi, int cas_cont, string usu_codi, int seg_cont)
        {
            return bo.ListaFlujosDetalle(emp_codi, cas_cont, usu_codi, seg_cont);
        }

        [HttpPost]
        [Route("api/Flujos/FlujoDetalle")]
        public ActionResult<WF_SEGUI, List<WF_ACCIO>> AprobarFlujo(ActionResult<WF_SEGUI> p)
        {
            var res = bo.AprobarFlujo(p.ObjResult);
            return new ActionResult<WF_SEGUI, List<WF_ACCIO>>(res.State, p.ObjResult, res.ObjResult, res.Message);
        }

        [HttpPut]
        [Route("api/Flujos/FlujoDetalle")]
        public ActionResult<WF_SEGUI> RechazarFlujo(ActionResult<WF_SEGUI> p)
        {
            return bo.RechazarFlujo(p.ObjResult);
        }
        #endregion
    }
}
