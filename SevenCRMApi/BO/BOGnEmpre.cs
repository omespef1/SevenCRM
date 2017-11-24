using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SevenCRMApi.DAO;
using SevenCRMApi.Models;

namespace SevenCRMApi.BO
{
    public class BOGnEmpre
    {
        DAOGnEmpre dao = new DAOGnEmpre();

        public ActionResult<List<GN_EMPRE>> ConsultarEmpresas()
        {
            var empresas = dao.ConsultarEmpresas();
            if (empresas == null)
                return new ActionResult<List<GN_EMPRE>>(false, null, "No se encontraron empresas.");
            return new ActionResult<List<GN_EMPRE>>(true, empresas, "");
        }
    }
}