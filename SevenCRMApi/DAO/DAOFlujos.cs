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
    public class DAOFlujos
    {
        public List<WF_SEGUI> DAOSEListaFlujos(short pEMP_CODI, string pCOD_EMPL)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine(" SELECT WF_SEGUI.EMP_CODI, WF_SEGUI.CAS_CONT, WF_SEGUI.SEG_CONT, WF_SEGUI.SEG_CONA, ");
                builder.AppendLine("        WF_SEGUI.FLU_CONT, WF_SEGUI.ETA_CONT, WF_SEGUI.SEG_SUBJ, WF_SEGUI.SEG_PRIO, ");
                builder.AppendLine("        WF_SEGUI.SEG_FREC, WF_SEGUI.SEG_HREC, WF_SEGUI.SEG_FLIM, WF_SEGUI.SEG_HLIM, ");
                builder.AppendLine("        WF_SEGUI.SEG_DIAE, WF_SEGUI.SEG_FCUL, WF_SEGUI.SEG_HCUL, WF_SEGUI.SEG_DIAR, ");
                builder.AppendLine("        WF_SEGUI.SEG_DIAD, WF_SEGUI.SEG_ESTC, WF_SEGUI.SEG_ABRE, WF_SEGUI.SEG_UORI, ");
                builder.AppendLine("        WF_SEGUI.SEG_UENC, WF_SEGUI.SEG_COME, WF_SEGUI.SEG_ESTE, WF_SEGUI.SEG_RECO, ");
                builder.AppendLine("        WF_SEGUI.AUD_ESTA, WF_SEGUI.AUD_USUA, WF_SEGUI.AUD_UFAC, GN_USUAR.USU_NOMB, ");
                builder.AppendLine("        WF_CASOS.CAS_DESC, WF_ETAPA.ETA_SSQL ");
                builder.AppendLine(" FROM WF_SEGUI, GN_USUAR, WF_CASOS, WF_ETAPA ");
                builder.AppendLine(" WHERE WF_SEGUI.EMP_CODI = WF_CASOS.EMP_CODI ");
                builder.AppendLine("   AND WF_SEGUI.CAS_CONT = WF_CASOS.CAS_CONT ");
                builder.AppendLine("   AND WF_SEGUI.SEG_UORI = GN_USUAR.USU_CODI ");
                builder.AppendLine("   AND WF_SEGUI.EMP_CODI = WF_ETAPA.EMP_CODI ");
                builder.AppendLine("   AND WF_SEGUI.FLU_CONT = WF_ETAPA.FLU_CONT ");
                builder.AppendLine("   AND WF_SEGUI.ETA_CONT = WF_ETAPA.ETA_CONT ");
                builder.AppendLine("   AND WF_SEGUI.SEG_ESTE = 'P' ");
                builder.AppendLine("   AND WF_SEGUI.EMP_CODI = " + pEMP_CODI + " ");
                builder.AppendLine("   AND UPPER(WF_SEGUI.SEG_UENC) = '" + pCOD_EMPL + "' ");
                builder.AppendLine(" ORDER BY WF_SEGUI.EMP_CODI, WF_SEGUI.SEG_PRIO, WF_SEGUI.CAS_CONT ");

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

        public string DAOSEConsultarAdicionales(short pEMP_CODI, int pCAS_CONT, string pSQL)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                pSQL = pSQL.ToUpper().Replace(":P_EMP_CODI", pEMP_CODI.AsString());
                pSQL = pSQL.ToUpper().Replace(":P_CAS_CONT", "'" + pCAS_CONT + "'");

                OTOContext pTOContext = new OTOContext();
                var conection = DBFactory.GetDB(pTOContext);
                var objeto = conection.GetDataSet(pTOContext, pSQL.ToString(), null);
                List<string> lData = new List<string>();
                for (int j = 0; j < objeto.Tables[0].Rows.Count; j++)
                {
                    for (int k = 0; k < objeto.Tables[0].Columns.Count; k++)
                    {
                        DataColumn col = objeto.Tables[0].Columns[k];
                        lData.Add(string.Format("{0}: {1}", col.ColumnName, objeto.Tables[0].Rows[j][k].ToString()));
                    }
                }
                return string.Join("<br />", lData);
            }
            catch (Exception)
            {
                //this.BOException.Throw("KDAOGeneral", "DAOSEListaFlujos", exception);
                return "";
            }
        }

        public List<WF_SEGUI> SEListaFlujosDetalle(short pEMP_CODI, int pCAS_CONT, string pCOD_EMPL, int pSEG_CONT)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine(" SELECT WF_SEGUI.EMP_CODI, WF_SEGUI.CAS_CONT, WF_SEGUI.SEG_CONT, WF_SEGUI.SEG_CONA, ");
                builder.AppendLine("        WF_SEGUI.FLU_CONT, WF_SEGUI.ETA_CONT, WF_SEGUI.SEG_SUBJ, WF_SEGUI.SEG_PRIO, ");
                builder.AppendLine("        WF_SEGUI.SEG_FREC, WF_SEGUI.SEG_HREC, WF_SEGUI.SEG_FLIM, WF_SEGUI.SEG_HLIM, ");
                builder.AppendLine("        WF_SEGUI.SEG_DIAE, WF_SEGUI.SEG_FCUL, WF_SEGUI.SEG_HCUL, WF_SEGUI.SEG_DIAR, ");
                builder.AppendLine("        WF_SEGUI.SEG_DIAD, WF_SEGUI.SEG_ESTC, WF_SEGUI.SEG_ABRE, WF_SEGUI.SEG_UORI, ");
                builder.AppendLine("        WF_SEGUI.SEG_UENC, WF_SEGUI.SEG_COME, WF_SEGUI.SEG_ESTE, WF_SEGUI.SEG_RECO, ");
                builder.AppendLine("        WF_SEGUI.AUD_ESTA, WF_SEGUI.AUD_USUA, WF_SEGUI.AUD_UFAC, GN_USUAR.USU_NOMB, ");
                builder.AppendLine("        WF_CASOS.CAS_DESC, WF_SEGUI.SEG_COME, WF_ETAPA.ETA_SSQL ");
                builder.AppendLine(" FROM WF_SEGUI, GN_USUAR, WF_CASOS, WF_ETAPA ");
                builder.AppendLine(" WHERE WF_SEGUI.EMP_CODI = WF_CASOS.EMP_CODI ");
                builder.AppendLine("   AND WF_SEGUI.CAS_CONT = WF_CASOS.CAS_CONT ");
                builder.AppendLine("   AND WF_SEGUI.SEG_UORI = GN_USUAR.USU_CODI ");
                builder.AppendLine("   AND WF_SEGUI.EMP_CODI = WF_ETAPA.EMP_CODI ");
                builder.AppendLine("   AND WF_SEGUI.FLU_CONT = WF_ETAPA.FLU_CONT ");
                builder.AppendLine("   AND WF_SEGUI.ETA_CONT = WF_ETAPA.ETA_CONT ");
                builder.AppendLine("   AND WF_SEGUI.SEG_ESTE = 'P' ");
                builder.AppendLine("   AND WF_SEGUI.EMP_CODI = " + pEMP_CODI + " ");
                builder.AppendLine("   AND WF_SEGUI.CAS_CONT = " + pCAS_CONT + " ");
                builder.AppendLine("   AND UPPER(WF_SEGUI.SEG_UENC) = '" + pCOD_EMPL.ToUpper().Trim() + "' ");
                builder.AppendLine("   AND WF_SEGUI.SEG_CONT = " + pSEG_CONT + " ");
                builder.AppendLine(" ORDER BY WF_SEGUI.EMP_CODI, WF_SEGUI.SEG_PRIO, WF_SEGUI.CAS_CONT ");

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

        internal FA_CLIEN DAOSEGetNombreSolicitante(short emp_codi, int cas_cont)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine("SELECT FA_CLIEN.CLI_NOCO AS CLI_NOCO, FA_DCLIE.DCL_NOMB AS DCL_NOMB FROM TS_ANTIC, FA_CLIEN, FA_DCLIE ");
                builder.AppendLine("WHERE TS_ANTIC.EMP_CODI = FA_CLIEN.EMP_CODI ");
                builder.AppendLine("  AND TS_ANTIC.CLI_CODI = FA_CLIEN.CLI_CODI ");
                builder.AppendLine("  AND TS_ANTIC.EMP_CODI = FA_DCLIE.EMP_CODI ");
                builder.AppendLine("  AND TS_ANTIC.CLI_CODI = FA_DCLIE.CLI_CODI ");
                builder.AppendLine("  AND TS_ANTIC.DCL_CODD = FA_DCLIE.DCL_CODD ");
                builder.AppendLine("  AND TS_ANTIC.EMP_CODI = @EMP_CODI ");
                builder.AppendLine("  AND TS_ANTIC.CAS_CONT = @CAS_CONT ");
                builder.AppendLine("  AND TS_ANTIC.ANT_TIPO = 'C' ");
                builder.AppendLine("UNION ALL ");
                builder.AppendLine("SELECT PO_PVDOR.PVR_NOCO AS CLI_NOCO, PO_DEPRO.DEP_NOMB AS DCL_NOMB  FROM TS_ANTIC, PO_PVDOR, PO_DEPRO ");
                builder.AppendLine("WHERE TS_ANTIC.EMP_CODI = PO_PVDOR.EMP_CODI ");
                builder.AppendLine("  AND TS_ANTIC.PVD_CODI = PO_PVDOR.PVD_CODI ");
                builder.AppendLine("  AND TS_ANTIC.EMP_CODI = PO_DEPRO.EMP_CODI ");
                builder.AppendLine("  AND TS_ANTIC.PVD_CODI = PO_DEPRO.PVD_CODI ");
                builder.AppendLine("  AND TS_ANTIC.DEP_CODD = PO_DEPRO.DEP_CODD ");
                builder.AppendLine("  AND TS_ANTIC.EMP_CODI = @EMP_CODI ");
                builder.AppendLine("  AND TS_ANTIC.CAS_CONT = @CAS_CONT ");
                builder.AppendLine("  AND TS_ANTIC.ANT_TIPO = 'P' ");
                builder.AppendLine("UNION ALL ");
                builder.AppendLine("SELECT CR_PROSP.PRO_NOCO collate Latin1_General_Bin AS CLI_NOCO, CR_DPROS.DPR_NOMB AS DCL_NOMB  FROM TS_ANTIC, CR_PROSP, CR_DPROS ");
                builder.AppendLine("WHERE TS_ANTIC.EMP_CODI = CR_PROSP.EMP_CODI ");
                builder.AppendLine("  AND TS_ANTIC.PRO_CONT = CR_PROSP.PRO_CONT ");
                builder.AppendLine("  AND TS_ANTIC.EMP_CODI = CR_DPROS.EMP_CODI ");
                builder.AppendLine("  AND TS_ANTIC.PRO_CONT = CR_DPROS.PRO_CONT ");
                builder.AppendLine("  AND TS_ANTIC.DPR_CODI = CR_DPROS.DPR_CODI ");
                builder.AppendLine("  AND TS_ANTIC.EMP_CODI = @EMP_CODI ");
                builder.AppendLine("  AND TS_ANTIC.CAS_CONT = @CAS_CONT ");
                builder.AppendLine("  AND TS_ANTIC.ANT_TIPO <> 'C' ");
                builder.AppendLine("  AND TS_ANTIC.ANT_TIPO <> 'P' ");

                Parameter[] param = new Parameter[] { 
                    new Parameter("EMP_CODI", emp_codi),
                    new Parameter("CAS_CONT", cas_cont)
                };

                OTOContext pTOContext = new OTOContext();
                var conection = DBFactory.GetDB(pTOContext);
                var objeto = conection.Read(pTOContext, builder.ToString(), MkFaClien, param);
                return objeto;
            }
            catch (Exception)
            {
                return null;
            }
        }

        internal List<WF_ACCIO> DAOSEListaAccionesdeUnaEtapa(short emp_codi, int flu_cont, short eta_cont)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine(" SELECT WF_ACCIO.ACC_CONT,WF_ACCIO.ACC_NOMB,WF_ACCIO.ACC_ESTT ");
                builder.AppendLine(" FROM WF_ACCIO,WF_RUTAS ");
                builder.AppendLine(" WHERE WF_ACCIO.EMP_CODI=WF_RUTAS.EMP_CODI ");
                builder.AppendLine(" AND WF_ACCIO.FLU_CONT=WF_RUTAS.FLU_CONT ");
                builder.AppendLine(" AND WF_ACCIO.ETA_CONT=WF_RUTAS.ETA_CONT ");
                builder.AppendLine(" AND WF_ACCIO.ACC_CONT=WF_RUTAS.ACC_CONT ");
                builder.AppendLine(" AND WF_ACCIO.EMP_CODI= " + emp_codi + " ");
                builder.AppendLine(" AND WF_ACCIO.FLU_CONT= " + flu_cont + " ");
                builder.AppendLine(" AND WF_ACCIO.ETA_CONT= " + eta_cont + " ");

                OTOContext pTOContext = new OTOContext();
                var conection = DBFactory.GetDB(pTOContext);
                var objeto = conection.ReadList(pTOContext, builder.ToString(), MkAcc, null);
                return objeto;
            }
            catch (Exception exception)
            {
                //this.BOException.Throw("KDAOGeneral", "DAOSEListaAccionesdeUnaEtapa", exception);
                return null;
            }
        }

        public Func<IDataReader, WF_ACCIO> MkAcc = reader => new WF_ACCIO
        {
            ACC_CONT = reader["ACC_CONT"].AsInt16(),
            ACC_ESTT = reader["ACC_ESTT"].AsString(),
            ACC_NOMB = reader["ACC_NOMB"].AsString()
        };

        public Func<IDataReader, FA_CLIEN> MkFaClien = reader => new FA_CLIEN
        {
            CLI_NOCO = reader["CLI_NOCO"].AsString(),
            DCL_NOMB = reader["DCL_NOMB"].AsString(),
        };

        public Func<IDataReader, WF_SEGUI> Make = reader => new WF_SEGUI
        {
            AUD_ESTA = reader["AUD_ESTA"].AsString(),
            AUD_UFAC = reader["AUD_UFAC"].AsDateTime(),
            AUD_USUA = reader["AUD_USUA"].AsString(),
            CAS_CONT = reader["CAS_CONT"].AsInt(),
            CAS_DESC = reader["CAS_DESC"].AsString(),
            EMP_CODI = reader["EMP_CODI"].AsInt16(),
            ETA_CONT = reader["ETA_CONT"].AsInt16(),
            ETA_SSQL = reader["ETA_SSQL"].AsString(),
            FLU_CONT = reader["FLU_CONT"].AsInt(),
            SEG_ABRE = reader["SEG_ABRE"].AsString(),
            SEG_COME = reader["SEG_COME"].AsString(),
            SEG_CONA = reader["SEG_CONA"].AsInt(),
            SEG_CONT = reader["SEG_CONT"].AsInt(),
            SEG_DIAD = reader["SEG_DIAD"].AsDouble(),
            SEG_DIAE = reader["SEG_DIAE"].AsDouble(),
            SEG_DIAR = reader["SEG_DIAR"].AsDouble(),
            SEG_ESTC = reader["SEG_ESTC"].AsString(),
            SEG_ESTE = reader["SEG_ESTE"].AsString(),
            SEG_FCUL = reader["SEG_FCUL"].AsDateTime(),
            SEG_FLIM = reader["SEG_FLIM"].AsDateTime(),
            SEG_FREC = reader["SEG_FREC"].AsDateTime(),
            SEG_HCUL = reader["SEG_HCUL"].AsDateTime(),
            SEG_HLIM = reader["SEG_HLIM"].AsDateTime(),
            SEG_HREC = reader["SEG_HREC"].AsDateTime(),
            SEG_PRIO = reader["SEG_PRIO"].AsString(),
            SEG_RECO = reader["SEG_RECO"].AsString(),
            SEG_SUBJ = reader["SEG_SUBJ"].AsString(),
            SEG_UENC = reader["SEG_UENC"].AsString(),
            SEG_UORI = reader["SEG_UORI"].AsString(),
            USU_NOMB = reader["USU_NOMB"].AsString()
        };
    }
}