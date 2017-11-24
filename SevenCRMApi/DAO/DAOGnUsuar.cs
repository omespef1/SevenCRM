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
using KTO;
using System.Data.Common;
using KDBGn;
using KBOGn;

namespace SevenCRMApi.DAO
{
    public class DAOGnUsuar
    {
        /// <summary>
        /// Connection Factory
        /// </summary>
        protected KDBGnLocator dbConnectionFactory;
        /// <summary>
        /// BO Exception
        /// </summary>
        protected KBOGnException BOException;
        /// <summary>
        /// Transaccion Factory
        /// </summary>
        protected DbCommand cmdTransaccionFactory;
        /// <summary>
        /// Conector Factory
        /// </summary>
        protected DbDataAdapter adapConectorFactory;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public DAOGnUsuar()
        {
            dbConnectionFactory = new KDBGn.KDBGnLocator();
            BOException = new KBOGn.KBOGnException();
        }

        public GN_USUAR ConsultarUsuario(string usu_codi)
        {
            String cadenaSQL = "";
            GN_USUAR TOusuar = new GN_USUAR();
            DbConnection cnConectar = dbConnectionFactory.dbProveedorFactory.CreateConnection();
            cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand();
            DbDataReader resultado;
            StringBuilder cmd = new StringBuilder();
            try
            {
                cmd.AppendLine("SELECT USU_CODI,USU_NOMB, EMP_CODI, USU_IDPK, USU_FOTO");
                cmd.AppendLine("FROM GN_USUAR ");
                cmd.AppendFormat("{0}{1}", " WHERE USU_ESTA = ", " 'A' ");
                cmd.AppendFormat("{0}{1}", " AND USU_CODI = ", "@codusua");
                cadenaSQL = cmd.ToString();
                if (dbConnectionFactory.ValOracle())
                {
                    cadenaSQL = cadenaSQL.Replace("= @", "= :");
                }
                cnConectar = dbConnectionFactory.ObtenerConexion();
                cnConectar.Open();
                DbParameter parametro0 = cmdTransaccionFactory.CreateParameter();
                if (dbConnectionFactory.ValOracle())
                {
                    parametro0.ParameterName = ":codusua";
                }
                else
                {
                    parametro0.ParameterName = "@codusua";
                }
                parametro0.Value = usu_codi;
                cmdTransaccionFactory.Parameters.Add(parametro0);
                cmdTransaccionFactory.Connection = cnConectar;
                cmdTransaccionFactory.CommandText = cadenaSQL;
                resultado = cmdTransaccionFactory.ExecuteReader();
                if (resultado.Read())
                {
                    if (resultado.GetValue(0) != DBNull.Value)
                    {
                        TOusuar.Usu_Codi = resultado.GetString(0);
                    }
                    if (resultado.GetValue(1) != DBNull.Value)
                    {
                        TOusuar.Usu_Nomb = resultado.GetString(1);
                    }
                    if (resultado.GetValue(2) != DBNull.Value)
                    {
                        TOusuar.Emp_Codi = Convert.ToInt16(resultado.GetValue(2));
                    }
                    if (resultado.GetValue(3) != DBNull.Value)
                    {
                        TOusuar.Usu_Idpk = resultado.GetValue(3).ToString();
                    }
                    if (resultado.GetValue(4) != DBNull.Value)
                    {
                        TOusuar.Usu_Foto = resultado.GetValue(4) as byte[];
                    }
                    if (resultado.Read())
                    {
                        throw new KBOGnExcepcion("Error de Usuario o Contraseña, Verifique por favor.");
                    }
                    return TOusuar;
                }
                else
                {
                    throw new KBOGnExcepcion("Error de Usuario o Contraseña, verifiqué por favor.");
                }
            }
            catch (Exception ex)
            {
                BOException.Throw("KDAOGeneral", "DAOSEConsultarUsuPas", ex);
                return null;
            }
            finally
            {
                dbConnectionFactory.CerrarConexionBaseDatos(cnConectar);
            }

            //try
            //{
            //    List<Parameter> listAux = new List<Parameter>();
            //    StringBuilder sql = new StringBuilder();
            //    sql.Append(" SELECT * FROM GN_USUAR WHERE USU_CODI = @USU_CODI ");

            //    listAux.Add(new Parameter("USU_CODI", usu_codi));

            //    Parameter[] oParameter = listAux.ToArray();
            //    OTOContext pTOContext = new OTOContext();

            //    var conection = DBFactory.GetDB(pTOContext);
            //    var objeto = conection.Read(pTOContext, sql.ToString(), Make, oParameter);
            //    return objeto;
            //}
            //catch (Exception ex)
            //{
            //    return null;
            //}
        }

        public bool SEBOEsVendedorCmVende(string pUsuario)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine("SELECT * FROM CA_VENDE WHERE VEN_ESTA = 'A' AND USU_CODI = @Usuario ");

                Parameter[] oParameter = new Parameter[] {
                    new Parameter("Usuario", pUsuario)
                };

                OTOContext pTOContext = new OTOContext();
                var conection = DBFactory.GetDB(pTOContext);
                DataSet objeto = conection.GetDataSet(pTOContext, builder.ToString(), oParameter);
                if (objeto == null || objeto.Tables.Count == 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<GN_USUAR> DAOSEListadeVendedorCmVende(string pUsuario)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine(" SELECT DISTINCT(USU.USU_CODI), USU.USU_NOMB ");
                builder.AppendLine(" FROM GN_USUAR USU WHERE USU.USU_ESTA = 'A' ");
                if (pUsuario.Trim() != "")
                {
                    builder.AppendLine(" AND USU.USU_CODI NOT IN ('" + pUsuario + "') ");
                }
                builder.AppendLine(" ORDER BY  USU.USU_NOMB ");
                OTOContext pTOContext = new OTOContext();
                var conection = DBFactory.GetDB(pTOContext);
                var objeto = conection.ReadList(pTOContext, builder.ToString(), Make, null);
                return objeto;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<GN_USUAR> DAOSEListadeSinVendedorCmVende()
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine(" SELECT DISTINCT(USU.USU_CODI), USU.USU_NOMB ");
                builder.AppendLine(" FROM GN_USUAR USU WHERE USU.USU_ESTA = 'A' AND ");
                builder.AppendLine(" USU.USU_CODI NOT IN (SELECT DISTINCT(USU.USU_CODI) ");
                builder.AppendLine("                      FROM CA_VENDE VEN, GN_USUAR USU ");
                builder.AppendLine("                      WHERE VEN.USU_CODI = USU.USU_CODI AND ");
                builder.AppendLine("                      VEN.VEN_ESTA = 'A' AND USU.USU_ESTA = 'A') ");
                builder.AppendLine("                      ORDER BY  USU.USU_NOMB ");

                OTOContext pTOContext = new OTOContext();
                var conection = DBFactory.GetDB(pTOContext);
                var objeto = conection.ReadList(pTOContext, builder.ToString(), Make, null);
                return objeto;
            }
            catch (Exception)
            {
                //this.BOException.Throw("KDAOGeneral", "DAOListadoCmQuereCerrados", exception);
                return null;
            }
        }

        public Func<IDataReader, GN_USUAR> Make = reader => new GN_USUAR
        {
            Usu_Codi = reader["USU_CODI"].AsString(),
            Usu_Nomb = reader["USU_NOMB"].AsString(),
            Emp_Codi = reader["EMP_CODI"] == DBNull.Value ? (short?)null : reader["EMP_CODI"].AsInt16(),
            Usu_Idpk = reader["USU_IDPK"] == DBNull.Value ? null : reader["USU_IDPK"].AsString(),
            Usu_Foto = reader["USU_FOTO"] == DBNull.Value ? null : reader["USU_FOTO"] as byte[],
        };
    }
}