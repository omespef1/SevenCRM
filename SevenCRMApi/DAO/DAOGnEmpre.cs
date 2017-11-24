using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using Ophelia;
using Ophelia.DataBase;
using SevenCRMApi.Models;
using Ophelia.Comun;

namespace SevenCRMApi.DAO
{
    public class DAOGnEmpre
    {
        public List<GN_EMPRE> ConsultarEmpresas()
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT * FROM GN_EMPRE ");

                OTOContext pTOContext = new OTOContext();
                var conection = DBFactory.GetDB(pTOContext);
                var objeto = conection.ReadList(pTOContext, sql.ToString(), Make, null);
                return objeto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Func<IDataReader, GN_EMPRE> Make = reader => new GN_EMPRE
        {
            Emp_Codi = reader["EMP_CODI"].AsInt16(),
            Emp_Nomb = reader["EMP_NOMB"].AsString()
        };
    }
}