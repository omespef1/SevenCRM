using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using Ophelia;
using Ophelia.DataBase;
using SevenCentralizacion.Models;
using Ophelia.Comun;

namespace SevenCentralizacion.DAO
{
    public class DAOGnConex
    {
        public List<GN_CONEX> GetConnections()
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine(" SELECT * FROM GN_CONEX ");

                OTOContext pTOContext = new OTOContext();
                var conection = DBFactory.GetDB(pTOContext);
                var objeto = conection.ReadList(pTOContext, builder.ToString(), Make, null);
                return objeto;
            }
            catch (Exception)
            {
                //this.BOException.Throw("KDAOGeneral", "DAOSEListaFlujos", exception);
                return null;
            }
        }

        public Func<IDataReader, GN_CONEX> Make = reader => new GN_CONEX
        {
            CNX_IPSR = reader["CNX_IPSR"].AsString(),
            CNX_NOMB = reader["CNX_NOMB"].AsString()
        };
    }
}