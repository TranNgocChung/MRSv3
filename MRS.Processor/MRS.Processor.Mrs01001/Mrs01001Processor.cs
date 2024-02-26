using Inventec.Common.FlexCellExport;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MRS.MANAGER.Base;
using MRS.MANAGER.Core.MrsReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Common.DateTime;
using MRS.MANAGER.Config;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisIcd;
using MOS.MANAGER.HisIcdGroup;

namespace MRS.Processor.Mrs01001
{
    public partial class Mrs01001Processor : AbstractProcessor
    {
        private Mrs01001Filter filter;
        private CommonParam paramGet = new CommonParam();
        private List<RdoGet> listRdoGet = new List<RdoGet>();
        private List<Mrs01001RDO> listRdo = new List<Mrs01001RDO>();

        List<long> DepartmentIdExam = null;
        public Mrs01001Processor(CommonParam param, string reportTypeCode)
            : base(param, reportTypeCode)
        {
        }

        public override Type FilterType()
        {
            return typeof(Mrs01001Filter);
        }

        protected override bool GetData()///
        {
            var result = true;
            filter = (Mrs01001Filter)reportFilter;
            try
            {
                DepartmentIdExam = HisDepartmentCFG.HIS_DEPARTMENT_ID__EXAM ?? new List<long>();
                var listRdoGetEarlier = new ManagerSql().GetRdo(filter, string.Join("','", DepartmentIdExam), filter.EARLIER_TIME_FROM, filter.EARLIER_TIME_TO, ManagerSql.STT_DATAS.First);
                if (IsNotNullOrEmpty(listRdoGetEarlier)) listRdoGet.AddRange(listRdoGetEarlier);
                var listRdoGetLater = new ManagerSql().GetRdo(filter, string.Join("','", DepartmentIdExam), filter.LATER_TIME_FROM, filter.LATER_TIME_TO, ManagerSql.STT_DATAS.Second);
                if (IsNotNullOrEmpty(listRdoGetLater)) listRdoGet.AddRange(listRdoGetLater);

                this.listIcd = GetIcd();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        protected override bool ProcessData()
        {
            var result = true;
            try
            {
                Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData("listRdoGet", listRdoGet));
                Inventec.Common.Logging.LogSystem.Debug("listRdoGet.Count: " + listRdoGet.Count);
                var listRdoGroup = listRdoGet.GroupBy(o => new { o.ICD_CODE, o.ICD_NAME, o.ATTACH_ICD_CODES, o.ICD_GROUP_ID, o.ICD_GROUP_CODE, o.ICD_GROUP_NAME, o.HEIN_MEDI_ORG_CODE, o.TDL_TREATMENT_TYPE_ID});
                Inventec.Common.Logging.LogSystem.Debug("listRdoGroup.Count: " + listRdoGroup.Count());
                foreach (var itemGroup in listRdoGroup)
                {
                    Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData("itemGroup", itemGroup.ToList()));
                    Mrs01001RDO rdo = new Mrs01001RDO();
                    rdo.ICD_CODE = itemGroup.First().ICD_CODE;
                    rdo.ICD_NAME = itemGroup.First().ICD_NAME;
                    rdo.ATTACH_ICD_CODES = itemGroup.First().ATTACH_ICD_CODES;
                    rdo.ATTACH_ICD_NAMES = GetIcdNamesByCodes(rdo.ATTACH_ICD_CODES);
                    rdo.ICD_GROUP_ID = itemGroup.First().ICD_GROUP_ID;
                    rdo.ICD_GROUP_CODE = itemGroup.First().ICD_GROUP_CODE;
                    rdo.ICD_GROUP_NAME = itemGroup.First().ICD_GROUP_NAME;
                    rdo.HEIN_MEDI_ORG_CODE = itemGroup.First().HEIN_MEDI_ORG_CODE;
                    rdo.TDL_TREATMENT_TYPE_ID = itemGroup.First().TDL_TREATMENT_TYPE_ID;
                    foreach (var item in itemGroup)
                    {
                        if (item.STT_DATAS == (int)ManagerSql.STT_DATAS.First)
                        {
                            rdo.EARLIER_TOTAL_COUNT_TREATMENT += item.TOTAL_COUNT_TREATMENT;
                            rdo.EARLIER_TOTAL_COUNT_TREATMENT_ABNORMAL += item.TOTAL_COUNT_TREATMENT_ABNORMAL;
                            rdo.EARLIER_TOTAL_HEIN_PRICE += item.TOTAL_HEIN_PRICE;
                        }
                        else if (item.STT_DATAS == (int)ManagerSql.STT_DATAS.Second)
                        {
                            rdo.LATER_TOTAL_COUNT_TREATMENT += item.TOTAL_COUNT_TREATMENT;
                            rdo.LATER_TOTAL_COUNT_TREATMENT_ABNORMAL += item.TOTAL_COUNT_TREATMENT_ABNORMAL;
                            rdo.LATER_TOTAL_HEIN_PRICE += item.TOTAL_HEIN_PRICE;
                        }
                    }

                    listRdo.Add(rdo);
                }
            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        protected override void SetTag(Dictionary<string, object> dicSingleTag, ProcessObjectTag objectTag, Store store)
        {
            dicSingleTag.Add("EARLIER_TIME_FROM", Inventec.Common.DateTime.Convert.TimeNumberToDateString(filter.EARLIER_TIME_FROM));
            dicSingleTag.Add("EARLIER_TIME_TO", Inventec.Common.DateTime.Convert.TimeNumberToDateString(filter.EARLIER_TIME_TO));
            dicSingleTag.Add("LATER_TIME_FROM", Inventec.Common.DateTime.Convert.TimeNumberToDateString(filter.LATER_TIME_FROM));
            dicSingleTag.Add("LATER_TIME_TO", Inventec.Common.DateTime.Convert.TimeNumberToDateString(filter.LATER_TIME_TO));

            objectTag.AddObjectData(store, "Report", listRdo);
        }

    }
}
