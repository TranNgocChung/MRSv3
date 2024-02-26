using MOS.EFMODEL.DataModels;
using MRS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Processor.Mrs00202
{
    partial class ManagerSql : BusinessBase
    {
        public List<V_HIS_HEIN_APPROVAL> GetHeinApprovalView(Mrs00202Filter filter)
        {
            List<V_HIS_HEIN_APPROVAL> result = new List<V_HIS_HEIN_APPROVAL>();
            string query = "";
            query += "SELECT \n";
            query += "HEAP.* \n";
            //
            query += "FROM HIS_RS.V_HIS_HEIN_APPROVAL HEAP \n";
            query += "JOIN HIS_RS.HIS_TREATMENT TREA ON TREA.ID = HEAP.TREATMENT_ID\n";
            //
            query += "WHERE 1=1\n";
            query += "AND HEAP.IS_DELETE = 0\n";
            query += string.Format("AND HEAP.BRANCH_ID = {0} \n", filter.BRANCH_ID);
            query += string.Format("AND HEAP.EXECUTE_TIME BETWEEN {0} and {1} \n", filter.TIME_FROM, filter.TIME_TO);

            if (filter.IN_DEPARTMENT_IDs != null)
            {
                query += string.Format("AND TREA.IN_DEPARTMENT_ID IN ({0}) \n", string.Join(",", filter.IN_DEPARTMENT_IDs));
            }
            if (filter.LAST_DEPARTMENT_IDs != null)
            {
                query += string.Format("AND TREA.LAST_DEPARTMENT_ID IN ({0}) \n", string.Join(",", filter.LAST_DEPARTMENT_IDs));
            }
            if (filter.END_DEPARTMENT_IDs != null)
            {
                query += string.Format("AND TREA.END_DEPARTMENT_ID IN ({0}) \n", string.Join(",", filter.END_DEPARTMENT_IDs));
            }
            query += "ORDER BY HEAP.EXECUTE_TIME ASC\n";
            Inventec.Common.Logging.LogSystem.Info("SQL: " + query);

            {
                result = new MOS.DAO.Sql.SqlDAO().GetSql<V_HIS_HEIN_APPROVAL>(query);
            }
            if (IsNotNullOrEmpty(result)) result = result.GroupBy(o => o.ID).Select(p => p.First()).ToList();

            Inventec.Common.Logging.LogSystem.Info("Finish Query ");

            return result;
        }
    }
}
