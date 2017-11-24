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
    public class DAOCrAgend
    {
        public DataSet DAOSEListaActividadesDs(string pCOD_RESP, DateTime pFEC_DESD, DateTime pFEC_HAST)
        {
            StringBuilder builder = new StringBuilder();
            try
            {
                builder.AppendLine("SELECT DISTINCT ");
                builder.AppendLine("CON.CON_NOMB, CON.CON_APEL, ACT.ACT_NOMB, CON.CON_NOMB, ");
                builder.AppendLine("CON.CON_APEL, AGE.EMP_CODI, AGE.PRO_CONT, AGE.ACT_CODI, ");
                builder.AppendLine("AGE.USU_EJEC, AGE.AGE_FREG, AGE.AGE_FINI,AGE.USU_PLAN, ");
                builder.AppendLine("AGE.AGE_FFIN, AGE.AGE_TIEM, AGE.AGE_FEJE, PRO.PRO_NOMB, ");
                builder.AppendLine("AGE.AGE_ESTA, ACT.ACT_OBSE, ACT.ACT_NOMB, AGE.AGE_ASUN, CON.CON_CARG, ");
                builder.AppendLine("(SELECT ITE_NOMB FROM GN_ITEMS WHERE ITE_CONT = CON.ITE_TITU) AS ITE_TITU ");
                builder.AppendLine("FROM CR_AGEND AGE, CR_CONPR CON, CR_PROSP PRO, CR_ACTIV ACT ");
                builder.AppendLine("WHERE AGE.PRO_CONT = PRO.PRO_CONT ");
                builder.AppendLine("AND AGE.EMP_CODI = PRO.EMP_CODI ");
                builder.AppendLine("AND AGE.PRO_CONT = CON.PRO_CONT ");
                builder.AppendLine("AND AGE.DPR_CODI = CON.DPR_CODI ");
                builder.AppendLine("AND AGE.EMP_CODI = CON.EMP_CODI ");
                builder.AppendLine("AND AGE.CON_CODI = CON.CON_CODI ");
                builder.AppendLine("AND AGE.ACT_CODI = ACT.ACT_CODI ");
                builder.AppendLine("AND AGE.EMP_CODI = ACT.EMP_CODI ");
                builder.AppendLine("AND AGE.AGE_ESTA NOT IN ('C')");
                builder.AppendLine("AND AGE.USU_EJEC = @CODRESP ");
                builder.AppendLine("AND AGE.AGE_FINI >= @FECDESD ");
                builder.AppendLine("AND AGE.AGE_FINI <= @FECHAST");
                builder.AppendLine("ORDER BY AGE.AGE_FINI ");

                Parameter[] param = new Parameter[] { 
                    new Parameter("CODRESP", pCOD_RESP),
                    new Parameter("FECDESD", pFEC_DESD),
                    new Parameter("FECHAST", pFEC_HAST),
                };

                OTOContext pTOContext = new OTOContext();
                var conection = DBFactory.GetDB(pTOContext);
                var objeto = conection.GetDataSet(pTOContext, builder.ToString(), param);
                return objeto;
            }
            catch (Exception exception)
            {
                //this.BOException.Throw("KDAOGeneral", "DAOSEListaActividades", exception);
                return null;
            }
        }

        public List<CR_AGEND> DAOSEListaActividades(string pCOD_RESP, DateTime pFEC_DESD, DateTime pFEC_HAST)
        {
            StringBuilder builder = new StringBuilder();
            try
            {
                builder.AppendLine("SELECT DISTINCT ");
                builder.AppendLine("CON.CON_NOMB, CON.CON_APEL, ACT.ACT_NOMB, CON.CON_NOMB, ");
                builder.AppendLine("CON.CON_APEL, AGE.EMP_CODI, AGE.PRO_CONT, AGE.ACT_CODI, ");
                builder.AppendLine("AGE.USU_EJEC, AGE.AGE_FREG, AGE.AGE_FINI,AGE.USU_PLAN, ");
                builder.AppendLine("AGE.AGE_FFIN, AGE.AGE_TIEM, AGE.AGE_FEJE, PRO.PRO_NOMB, ");
                builder.AppendLine("AGE.AGE_ESTA, ACT.ACT_OBSE, ACT.ACT_NOMB, AGE.AGE_ASUN, CON.CON_CARG, ");
                builder.AppendLine("(SELECT ITE_NOMB FROM GN_ITEMS WHERE ITE_CONT = CON.ITE_TITU) AS ITE_TITU ");
                builder.AppendLine("FROM CR_AGEND AGE, CR_CONPR CON, CR_PROSP PRO, CR_ACTIV ACT ");
                builder.AppendLine("WHERE AGE.PRO_CONT = PRO.PRO_CONT ");
                builder.AppendLine("AND AGE.EMP_CODI = PRO.EMP_CODI ");
                builder.AppendLine("AND AGE.PRO_CONT = CON.PRO_CONT ");
                builder.AppendLine("AND AGE.DPR_CODI = CON.DPR_CODI ");
                builder.AppendLine("AND AGE.EMP_CODI = CON.EMP_CODI ");
                builder.AppendLine("AND AGE.CON_CODI = CON.CON_CODI ");
                builder.AppendLine("AND AGE.ACT_CODI = ACT.ACT_CODI ");
                builder.AppendLine("AND AGE.EMP_CODI = ACT.EMP_CODI ");
                builder.AppendLine("AND AGE.AGE_ESTA NOT IN ('C')");
                builder.AppendLine("AND AGE.USU_EJEC = @CODRESP ");
                builder.AppendLine("AND AGE.AGE_FINI >= @FECDESD ");
                builder.AppendLine("AND AGE.AGE_FINI <= @FECHAST");
                builder.AppendLine("ORDER BY AGE.AGE_FINI ");

                Parameter[] param = new Parameter[] { 
                    new Parameter("CODRESP", pCOD_RESP),
                    new Parameter("FECDESD", pFEC_DESD),
                    new Parameter("FECHAST", pFEC_HAST),
                };

                OTOContext pTOContext = new OTOContext();
                var conection = DBFactory.GetDB(pTOContext);
                var objeto = conection.ReadList(pTOContext, builder.ToString(), Make, param);
                return objeto;
            }
            catch (Exception exception)
            {
                //this.BOException.Throw("KDAOGeneral", "DAOSEListaActividades", exception);
                return null;
            }
        }

        public Func<IDataReader, CR_AGEND> Make = reader => new CR_AGEND
        {
            ACT_CODI = reader["ACT_CODI"].AsInt(),
            ACT_NOMB = reader["ACT_NOMB"].AsString(),
            ACT_OBSE = reader["ACT_OBSE"].AsString(),
            AGE_ASUN = reader["AGE_ASUN"].AsString(),
            AGE_ESTA = reader["AGE_ESTA"].AsString(),
            AGE_FEJE = reader["AGE_FEJE"].AsDateTime(),
            AGE_FFIN = reader["AGE_FFIN"].AsDateTime(),
            AGE_FINI = reader["AGE_FINI"].AsDateTime(),
            AGE_FREG = reader["AGE_FREG"].AsDateTime(),
            AGE_TIEM = reader["AGE_TIEM"].AsDateTime(),
            CON_APEL = reader["CON_APEL"].AsString(),
            CON_CARG = reader["CON_CARG"].AsString(),
            CON_NOMB = reader["CON_NOMB"].AsString(),
            EMP_CODI = reader["EMP_CODI"].AsInt16(),
            ITE_TITU = reader["ITE_TITU"].AsString(),
            PRO_CONT = reader["PRO_CONT"].AsInt(),
            PRO_NOMB = reader["PRO_NOMB"].AsString(),
            USU_EJEC = reader["USU_EJEC"].AsString(),
            USU_PLAN = reader["USU_PLAN"].AsString()
        };
    }
}