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
    public class GnUsuarController : ApiController
    {
        BOGnUsuar bo = new BOGnUsuar();

        [HttpGet]
        [Route("api/GnUsuar/ValidateUser")]
        public ActionResult<GN_USUAR> ValidateUser( string user, string pass)
        {
            return bo.ValidarUsuario(user, pass);
        }

        [HttpGet]
        [Route("api/GnUsuar/GetUsuariosFlujo")]
        public List<GN_USUAR> GetUsuariosFlujo(string usu_codi)
        {
            return bo.CargarFlujos(usu_codi);
        }
    }
}
