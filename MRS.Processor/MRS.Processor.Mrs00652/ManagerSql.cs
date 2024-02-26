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
using MRS.MANAGER.Config;
using MRS.Processor.Mrs00652;

namespace MRS.Processor.Mrs00652
{
    public partial class ManagerSql : BusinessBase
    {
        public List<V_HIS_SERE_SERV_3> GetVSereServ3(List<long> heinApprovalIds, long? patientTypeId, long? requestDepartmentId)
        {
            List<V_HIS_SERE_SERV_3> result = new List<V_HIS_SERE_SERV_3>();
            try
            {

                string query = "SELECT SS.*";
                query += "FROM V_HIS_SERE_SERV_3 SS WHERE 1 = 1 ";
                if (IsNotNullOrEmpty(heinApprovalIds))
                {
                    string idStr = string.Join(",", heinApprovalIds);
                    query += "AND HEIN_APPROVAL_ID IN (" + idStr + ")";
                }
                if (patientTypeId.HasValue)
                {
                    query += "AND PATIENT_TYPE_ID = " + patientTypeId.Value.ToString();
                }
                if (requestDepartmentId.HasValue)
                {
                    query += "AND TDL_REQUEST_DEPARTMENT_ID = " + requestDepartmentId.Value.ToString();
                }
                LogSystem.Info("SQL: " + query);
                var rs = new MOS.DAO.Sql.SqlDAO().GetSql<V_HIS_SERE_SERV_3>(query);

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
    }
}
