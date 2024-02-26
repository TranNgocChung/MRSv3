using MRS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MRS.Processor.Mrs01001
{
    public class ManagerSql : BusinessBase
    {
        public enum STT_DATAS
        {
            First = 1,
            Second =2,
        }
        public List<RdoGet> GetRdo(Mrs01001Filter filter, string examDepartmentIds, long timeFrom, long timeTo, STT_DATAS stt)
        {
            List<RdoGet> result = null;
            try
            {
                string query = "";
                query += string.Format("--du lieu tong hop luot kham chua benh theo ma benh\n");
                query += string.Format("select {0} STT_DATAS,\n", (int)stt);
                query += string.Format("icd.icd_code,\n");
                query += string.Format("icd.icd_name,\n");
                query += string.Format("icd.attach_icd_codes,\n");
                query += string.Format("nvl(icdgr.id,0) icd_group_id,\n");
                query += string.Format("icdgr.icd_group_code,\n");
                query += string.Format("icdgr.icd_group_name,\n");
                query += string.Format("bra.hein_medi_org_code,\n");
                query += string.Format("trea.tdl_treatment_type_id,\n");
                query += string.Format("trea.in_date IN_DATE,\n");

                query += string.Format("count(distinct(trea.id)) TOTAL_COUNT_TREATMENT,\n");
                query += string.Format("count(distinct(case when (EXISTS(select 1 from his_treatment where id!=trea.id and in_date=trea.in_date and patient_id=trea.patient_id and in_time>trea.in_time and clinical_in_time is not null and floor(clinical_in_time/1000000)=floor(in_time/1000000))) then trea.id else null end)) TOTAL_COUNT_TREATMENT_ABNORMAL,\n");
                query += string.Format("sum(nvl(ss.vir_total_hein_price,0)) TOTAL_HEIN_PRICE\n");

                query += string.Format("from his_service_req sr\n");
                query += string.Format("left join his_sere_serv ss on sr.id=ss.service_req_id\n");
                query += string.Format("join his_treatment trea on trea.id=sr.treatment_id\n");
                query += string.Format("join his_branch bra on bra.id=trea.branch_id\n");
                query += string.Format("join his_icd icd on icd.icd_code=sr.icd_code\n");
                query += string.Format("left join his_icd_group icdgr on icdgr.id=icd.icd_group_id\n");
                query += string.Format("where 1=1\n");
                query += string.Format("and sr.is_no_execute is null\n");
                if (filter.INPUT_DATA_ID_STT_TYPE.HasValue)
                {
                    //đang điều trị
                    if (filter.INPUT_DATA_ID_STT_TYPE == 1)
                    {
                        query += string.Format("and trea.in_time >= {0}\n", timeFrom);
                        query += string.Format("and trea.in_time <= {0}\n", timeTo);
                        query += string.Format("and trea.is_pause is null\n");
                    }
                    //đã kết thúc điều trị
                    else if (filter.INPUT_DATA_ID_STT_TYPE == 2)
                    {
                        query += string.Format("and trea.out_time >= {0}\n", timeFrom);
                        query += string.Format("and trea.out_time <= {0}\n", timeTo);
                        query += string.Format("and trea.is_pause=1\n");
                    }
                    //đã khóa viện phí
                    else if (filter.INPUT_DATA_ID_STT_TYPE == 3)
                    {
                        query += string.Format("and trea.fee_lock_time >= {0}\n", timeFrom);
                        query += string.Format("and trea.fee_lock_time <= {0}\n", timeTo);
                        query += string.Format("and (trea.IS_ACTIVE is null or trea.IS_ACTIVE = 0)\n");
                    }
                    //đã khóa bảo hiểm
                    else if (filter.INPUT_DATA_ID_STT_TYPE == 4)
                    {
                        query += string.Format("and trea.fee_lock_time >= {0}\n", timeFrom);
                        query += string.Format("and trea.fee_lock_time <= {0}\n", timeTo);
                        query += string.Format("and trea.IS_LOCK_HEIN = 1\n");
                    }
                    else
                    {
                        query += string.Format("and trea.fee_lock_time >= {0}\n", timeFrom);
                        query += string.Format("and trea.fee_lock_time <= {0}\n", timeTo);
                        query += string.Format("and trea.IS_ACTIVE is null or trea.IS_ACTIVE = 0\n");
                    }
                }
                else
                {
                    query += string.Format("and trea.fee_lock_time >= {0}\n", timeFrom);
                    query += string.Format("and trea.fee_lock_time <= {0}\n", timeTo);
                    query += string.Format("and trea.IS_ACTIVE is null or trea.IS_ACTIVE = 0\n");
                }
                if (IsNotNullOrEmpty(filter.ICD_CODEs))
                {
                    query += string.Format("AND icd.icd_code IN('{0}') \n", string.Join("','", filter.ICD_CODEs));
                }
                query += string.Format("group by\n");
                query += string.Format("icd.icd_code,\n");
                query += string.Format("icd.icd_name,\n");
                query += string.Format("icd.attach_icd_codes,\n");
                query += string.Format("icdgr.id ,\n");
                query += string.Format("icdgr.icd_group_code,\n");
                query += string.Format("icdgr.icd_group_name,\n");
                query += string.Format("bra.hein_medi_org_code,\n");
                query += string.Format("trea.tdl_treatment_type_id,\n");
                query += string.Format("trea.in_date\n");
               
                Inventec.Common.Logging.LogSystem.Info("SQL: " + query);
                result = new MOS.DAO.Sql.SqlDAO().GetSql<RdoGet>(query);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

       
    }

    public class RdoGet
    {
        public int STT_DATAS { get; set; }

        public string HEIN_MEDI_ORG_CODE { get; set; }
        public long TDL_TREATMENT_TYPE_ID { get; set; }
        public long ICD_GROUP_ID { get; set; }
        public string ICD_GROUP_CODE { get; set; }
        public string ICD_GROUP_NAME { get; set; }
        public string ICD_CODE { get; set; }
        public string ICD_NAME { get; set; }
        public string ATTACH_ICD_CODES { get; set; }
        public long IN_DATE { get; set; }

        public int TOTAL_COUNT_TREATMENT { get; set; }//tổng số ca khám chữa bệnh
        public int TOTAL_COUNT_TREATMENT_ABNORMAL { get; set; }//tổng số ca khám chữa bệnh bất thường (Đến khám và điều trị sinh 2 hồ sơ điều trị trong ngày nhưng lần sau nhập viện, thì tính lần 1 khám là 1 lượt bất thường)
        public decimal TOTAL_HEIN_PRICE { get; set; }//Tiền BHTT
    }
   
}
