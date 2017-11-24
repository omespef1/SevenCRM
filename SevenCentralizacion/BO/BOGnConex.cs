using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ophelia.Seven;
using SevenCentralizacion.DAO;
using SevenCentralizacion.Models;

namespace SevenCentralizacion.BO
{
    public class BOGnConex
    {
        DAOGnConex dao = new DAOGnConex();

        public ActionResult<List<GN_CONEX>> GetConnections()
        {
            List<GN_CONEX> conexiones = dao.GetConnections();
            if (conexiones == null || !conexiones.Any())
                return new ActionResult<List<GN_CONEX>>(false, null, "No se encontraron conexiones.");

            foreach (var item in conexiones)
            {
                item.CNX_IPSR = item.CNX_IPSR;
            }
            return new ActionResult<List<GN_CONEX>>(true, conexiones, "");
        }
    }
}