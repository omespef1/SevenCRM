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
    public class ActividadesController : ApiController
    {
        BOActividades bo = new BOActividades();

        [HttpGet]
        [Route("api/Actividades/ListarEmpleados")]
        public List<GN_USUAR> ListarEmpleados(string usu_codi)
        {
            return bo.ListarEmpleados(usu_codi);
        }

        [HttpGet]
        [Route("api/Actividades/ListarActividades")]
        public List<CR_ACTIV> ListarActividades(short emp_codi)
        {
            return bo.ListarActividades(emp_codi);
        }

        [HttpGet]
        [Route("api/Actividades/ListarEtapas")]
        public List<CR_ETAPA> ListarEtapas(short emp_codi)
        {
            return bo.ListarEtapas(emp_codi);
        }

        [HttpGet]
        [Route("api/Actividades/cargarClientes")]
        public ActionResult<List<CR_PROSP>, List<CR_DPROS>, List<CR_CONPR>> cargarClientes(short emp_codi, string filter)
        {
            return bo.cargarClientes(emp_codi, filter);
        }

        [HttpGet]
        [Route("api/Actividades/cargarDClientes")]
        public ActionResult<List<CR_DPROS>, List<CR_CONPR>> cargarDClientes(string pro_cont)
        {
            return bo.cargarDClientes(pro_cont);
        }

        [HttpGet]
        [Route("api/Actividades/cargarContactos")]
        public ActionResult<List<CR_CONPR>> cargarContactos(string pro_cont, string dpr_codi)
        {
            return bo.cargarContactos(pro_cont, dpr_codi);
        }

        [HttpPost]
        [Route("api/Actividades/guardarActividad")]
        public ActionResult GuardarActividad(TOAGENDA agenda)
        {
            return bo.GuardarActividad(agenda);
        }

        [HttpPost]
        [Route("api/Actividades/invitarActividad")]
        public ActionResult invitarActividad(TOAGENDA agenda)
        {
            return bo.invitarActividad(agenda);
        }
    }
}
