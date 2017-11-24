using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SevenCentralizacion.BO;
using SevenCentralizacion.DAO;
using SevenCentralizacion.Models;

namespace SevenCentralizacion.Controllers
{
    public class GnConexController : ApiController
    {
        BOGnConex bo = new BOGnConex();

        // GET api/gnconex
        [HttpGet]
        [Route("api/GnConex/GetConnections")]
        public ActionResult<List<GN_CONEX>> GetConnections()
        {
            return bo.GetConnections();
        }
    }
}
