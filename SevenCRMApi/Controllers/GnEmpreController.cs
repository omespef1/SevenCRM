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
    public class GnEmpreController : ApiController
    {
        BOGnEmpre bo = new BOGnEmpre();

        [HttpGet]
        [Route("api/GnEmpre/GetEmpresas")]
        public ActionResult<List<GN_EMPRE>> GetEmpresas()
        {
            return bo.ConsultarEmpresas();
        }
    }
}
