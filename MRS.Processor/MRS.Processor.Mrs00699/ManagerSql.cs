using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MRS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Processor.Mrs00699
{
    partial class ManagerSql : BusinessBase
    {
        public List<V_HIS_BABY> GetListBaby(Mrs00699Filter filter) {
            List<V_HIS_BABY> result = new List<V_HIS_BABY>();
            try
            {
                string query = "";
                query += "SELECT * FROM V_HIS_BABY \n";
                query += "WHERE \n";
                query += string.Format("BORN_TIME BETWEEN {0} AND {1}\n", filter.TIME_FROM, filter.TIME_TO);
                query += "AND IS_DELETE = 0 \n";
                Inventec.Common.Logging.LogSystem.Info("SQL: " + query);
                result = new MOS.DAO.Sql.SqlDAO().GetSql<V_HIS_BABY>(query);
            }
            catch (Exception ex)
            {
                result = null;
                LogSystem.Error(ex);
            }
            return result;
        }
    }
}
