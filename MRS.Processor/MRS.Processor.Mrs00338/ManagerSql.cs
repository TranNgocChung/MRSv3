using Inventec.Core;
using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRS.MANAGER.Base;
using MOS.EFMODEL;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisCashierRoom;
using MRS.MANAGER.Config;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisTransaction;
using MOS.MANAGER.HisTreatment;

namespace MRS.Processor.Mrs00338
{
    public partial class ManagerSql : BusinessBase
    {
        public List<HIS_TRANSACTION> Get(HisTransactionFilterQuery filter)
        {
            List<HIS_TRANSACTION> result = new List<HIS_TRANSACTION>();
            try
            {
                string query = "";
                query += "SELECT ";
                query += "TRAN.* ";
                query += "FROM HIS_RS.HIS_TRANSACTION TRAN ";
                query += "JOIN HIS_RS.HIS_TREATMENT TREA ON TREA.ID=TRAN.TREATMENT_ID ";
                query += "WHERE 1=1 ";
                query += "AND TRAN.TRANSACTION_TYPE_ID=3 AND TRAN.IS_CANCEL IS NULL AND not exists (select 1 from HIS_RS.his_transaction where is_cancel is null and transaction_type_id =3 and treatment_id = tran.treatment_id and id<>tran.id and (transaction_time>tran.transaction_time or (transaction_time=tran.transaction_time and id>tran.id)))  ";
                query += "AND TREA.TDL_PATIENT_TYPE_ID <>1 ";
                if (filter.TRANSACTION_TIME_TO != null)
                {
                    query += string.Format("AND TRAN.TRANSACTION_TIME < {0} ", filter.TRANSACTION_TIME_TO);
                }
                if (filter.TRANSACTION_TIME_FROM != null)
                {
                    query += string.Format("AND TRAN.TRANSACTION_TIME >= {0} ", filter.TRANSACTION_TIME_FROM);
                }
                Inventec.Common.Logging.LogSystem.Info("SQL: " + query);
                var rs = new MOS.DAO.Sql.SqlDAO().GetSql<HIS_TRANSACTION>(query);

                if (rs != null)
                {
                    result = rs;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }

            return result;
        }

        public List<HIS_TREATMENT> Get(HisTreatmentFilterQuery filter)
        {
            List<HIS_TREATMENT> result = new List<HIS_TREATMENT>();
            try
            {
                string query = "";
                query += "SELECT ";
                query += "TREA.* ";
                query += "FROM HIS_RS.HIS_TREATMENT TREA ";
                query += "JOIN HIS_RS.HIS_HEIN_APPROVAL HAP ON TREA.ID=HAP.TREATMENT_ID ";
                query += "WHERE 1=1 ";
                if (filter.FEE_LOCK_TIME_TO != null)
                {
                    query += string.Format("AND HAP.EXECUTE_TIME < {0} ", filter.FEE_LOCK_TIME_TO);
                }
                if (filter.FEE_LOCK_TIME_FROM != null)
                {
                    query += string.Format("AND HAP.EXECUTE_TIME >= {0} ", filter.FEE_LOCK_TIME_FROM);
                }

                Inventec.Common.Logging.LogSystem.Info("SQL: " + query);
                var rs = new MOS.DAO.Sql.SqlDAO().GetSql<HIS_TREATMENT>(query);

                if (rs != null)
                {
                    result = rs.GroupBy(o => o.ID).Select(p => p.First()).ToList();
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }

            return result;
        }
        
    }
}
