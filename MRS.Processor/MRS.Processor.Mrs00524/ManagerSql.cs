using MOS.EFMODEL.DataModels;
using MRS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Processor.Mrs00524
{
    public partial class ManagerSql : BusinessBase
    {
        public List<V_HIS_IMP_MEST_MEDICINE> GetImpMestMedicineView(List<long> expMestMedicineIds)
        {
            List<V_HIS_IMP_MEST_MEDICINE> result = new List<V_HIS_IMP_MEST_MEDICINE>();
            try
            {
                StringBuilder query = new StringBuilder(" --Cac phieu hoan tra THUOC \n");
                query.Append("SELECT \n");
                query.Append("IMM.* \n");
                query.Append("FROM HIS_RS.V_HIS_IMP_MEST_MEDICINE IMM \n");
                query.Append("WHERE 1=1 \n");
                query.Append("AND IMM.IMP_MEST_STT_ID=5 AND IMM.IS_DELETE=0 \n");
                query.AppendFormat("AND IMM.TH_EXP_MEST_MEDICINE_ID IN ({0}) \n", String.Join(",", expMestMedicineIds));
                Inventec.Common.Logging.LogSystem.Info("SQL: " + query);
                result = new MOS.DAO.Sql.SqlDAO().GetSql<V_HIS_IMP_MEST_MEDICINE>(query.ToString()) ?? new List<V_HIS_IMP_MEST_MEDICINE>();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }

            return result;
        }

    }
}
