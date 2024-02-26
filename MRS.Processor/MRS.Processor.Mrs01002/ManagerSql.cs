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
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.DAO.Sql;
using System.Data;
using MRS.MANAGER.Config;

namespace MRS.Processor.Mrs01002
{
    public class ManagerSql : BusinessBase
    {
        public List<V_HIS_TREATMENT> GetHoSoRavien(Mrs01002Filter filter)
        {
            List<V_HIS_TREATMENT> result = new List<V_HIS_TREATMENT>();
            CommonParam paramGet = new CommonParam();
            string query = "";
            query += string.Format("-- tong so benh nhan ra vien, chuyen vien, tu vong\n");
            query += string.Format("select\n");
            query += string.Format("trea.*\n");
            query += string.Format("from V_HIS_TREATMENT trea\n");
            query += string.Format("where 1=1\n");
            if (filter.DEPARTMENT_IDs != null)
                query += string.Format("and trea.END_DEPARTMENT_ID in ({0})\n", string.Join(",", filter.DEPARTMENT_IDs));
            if (filter.ICD_CODEs != null)
                query += string.Format("and trea.ICD_CODE in('{0}') \n", string.Join("','", filter.ICD_CODEs));
            query += string.Format("and trea.OUT_TIME between {0} and {1}\n", filter.TIME_FROM, filter.TIME_TO);
            Inventec.Common.Logging.LogSystem.Info("SQL: " + query);
            result = new MOS.DAO.Sql.SqlDAO().GetSql<V_HIS_TREATMENT>(paramGet, query);
            if (paramGet.HasException)
                throw new NullReferenceException("Co exception xay ra tai DAOGET trong qua trinh lay du lieu Mrs01002");

            return result;
        }

        public List<V_HIS_TREATMENT> GetHoSoChuyenKhoa(Mrs01002Filter filter)
        {
            List<V_HIS_TREATMENT> result = new List<V_HIS_TREATMENT>();
            CommonParam paramGet = new CommonParam();
            string query = "";
            query += string.Format("-- tong so benh nhan chuyen khoa\n");
            query += string.Format("select\n");
            query += string.Format("trea.*\n");
            query += string.Format("from V_HIS_TREATMENT trea\n");
            query += string.Format("JOIN HIS_DEPARTMENT_TRAN DETR ON DETR.TREATMENT_ID = TREA.ID\n");
            query += string.Format("LEFT JOIN HIS_DEPARTMENT_TRAN PRDT ON DETR.PREVIOUS_ID = PRDT.ID\n");
            query += string.Format("LEFT JOIN HIS_DEPARTMENT PRDE ON PRDT.DEPARTMENT_ID = PRDE.ID\n");
            query += string.Format("where 1=1\n");
            if (filter.DEPARTMENT_IDs != null)
                query += string.Format("and PRDE.ID in ({0})\n", string.Join(",", filter.DEPARTMENT_IDs));
            if (filter.ICD_CODEs != null)
                query += string.Format("and trea.ICD_CODE in('{0}') \n", string.Join("','", filter.ICD_CODEs));
            query += string.Format("and DETR.DEPARTMENT_IN_TIME between {0} and {1}\n", filter.TIME_FROM, filter.TIME_TO);
            Inventec.Common.Logging.LogSystem.Info("SQL: " + query);
            result = new MOS.DAO.Sql.SqlDAO().GetSql<V_HIS_TREATMENT>(paramGet, query);
            if (paramGet.HasException)
                throw new NullReferenceException("Co exception xay ra tai DAOGET trong qua trinh lay du lieu Mrs01002");
            if (IsNotNullOrEmpty(result))
                result = result.GroupBy(o=>o.ID).Select(group=>group.First()).ToList();
            return result;
        }

    }
}
