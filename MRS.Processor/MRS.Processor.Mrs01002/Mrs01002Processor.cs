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

namespace MRS.Processor.Mrs01002
{
    public partial class Mrs01002Processor : AbstractProcessor
    {
        private Mrs01002Filter filter;
        private CommonParam paramGet = new CommonParam();
        private List<Mrs01002RDO> listRdo = new List<Mrs01002RDO>();

        List<long> DepartmentIdExam = null;
        public Mrs01002Processor(CommonParam param, string reportTypeCode)
            : base(param, reportTypeCode)
        {
        }

        public override Type FilterType()
        {
            return typeof(Mrs01002Filter);
        }

        protected override bool GetData()///
        {
            var result = true;
            filter = (Mrs01002Filter)reportFilter;
            try
            {
                GetTreatments();

                this.listIcds = GetIcd();
                this.listIcdGroups = GetIcdGroup();
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
                var timeFrom = filter.TIME_FROM;
                var timeTo = filter.TIME_TO;
                var lisTreatments = listOuts.Union(listDepaTrans).ToList() ?? new List<V_HIS_TREATMENT>();
                List<string> icdCodes = lisTreatments.Select(o => o.ICD_CODE).ToList() ?? new List<string>();
                //List<string> icdCauseCodes = lisTreatments.Select(o => o.ICD_CAUSE_CODE).ToList() ?? new List<string>();
                //listIcds = listIcds.Where(o => icdCodes.Exists(s => s == o.ICD_CODE) || icdCauseCodes.Exists(s => s == o.ICD_CODE)).ToList();
                listIcds = listIcds.Where(o => icdCodes.Exists(s => s == o.ICD_CODE)).ToList();
                var listIcdGroupbyIcdGroups = listIcds.Distinct().GroupBy(s => s.ICD_GROUP_ID).ToList();
                foreach (var listIcdGroupbyIcdGroup in listIcdGroupbyIcdGroups)
                {
                    var icdGroup = listIcdGroups.Where(s => s.ID == listIcdGroupbyIcdGroup.Key).ToList();
                    foreach (var hisIcd in listIcdGroupbyIcdGroup)
                    {
                        Mrs01002RDO rdo = new Mrs01002RDO();
                        if (icdGroup != null && icdGroup.Count > 0)
                        {
                            rdo.ICD_GROUP_CODE = icdGroup.First().ICD_GROUP_CODE ?? " ";
                            rdo.ICD_GROUP_NAME = icdGroup.First().ICD_GROUP_NAME;
                        }
                        rdo.ICD_CODE = hisIcd.ICD_CODE;
                        rdo.ICD_NAME = hisIcd.ICD_NAME;
                        if (rdo.ICD_CODE.Length >= 3)
                        {
                            rdo.ICD_CODE_SUB = hisIcd.ICD_CODE.Substring(0, 3);
                        }
                        rdo.TOTAL_TONG = lisTreatments.Where(x => x.ICD_CODE == hisIcd.ICD_CODE).Count();
                        rdo.TOTAL_VAO_VIEN = lisTreatments.Where(x => x.ICD_CODE == hisIcd.ICD_CODE && x.IN_TIME >= timeFrom && x.IN_TIME <= timeTo).Count();
                        rdo.TOTAL_TU_THANG_TRUOC = rdo.TOTAL_TONG - rdo.TOTAL_VAO_VIEN;
                        rdo.TOTAL_GIOITINH_NU = lisTreatments.Where(x => x.ICD_CODE == hisIcd.ICD_CODE && x.TDL_PATIENT_GENDER_ID == IMSys.DbConfig.HIS_RS.HIS_GENDER.ID__FEMALE).Count();
                        rdo.TOTAL_GIOITINH_NAM = lisTreatments.Where(x => x.ICD_CODE == hisIcd.ICD_CODE && x.TDL_PATIENT_GENDER_ID == IMSys.DbConfig.HIS_RS.HIS_GENDER.ID__MALE).Count();
                        
                        var chuyenVien = listOuts.Where(x => x.ICD_CODE == hisIcd.ICD_CODE && x.TREATMENT_END_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__CHUYEN).Select(x=>x.ID);
                        rdo.TOTAL_CHUYEN_VIEN = chuyenVien.Count();
                        var tuVong = listDeaths.Where(x => x.ICD_CODE == hisIcd.ICD_CODE).Select(x=>x.ID);
                        rdo.TOTAL_TU_VONG = tuVong.Count();
                        var raVien = listOuts.Where(x => x.ICD_CODE == hisIcd.ICD_CODE && !chuyenVien.Contains(x.ID)&& !tuVong.Contains(x.ID)).Select(x=>x.ID);
                        rdo.TOTAL_RA_VIEN = raVien.Count();
                        rdo.TOTAL_CHUYEN_KHOA = listDepaTrans.Where(x => x.ICD_CODE == hisIcd.ICD_CODE).Count();
                        rdo.TOTAL_TREATMENT_DAY = lisTreatments.Where(x => x.ICD_CODE == hisIcd.ICD_CODE).Sum(x => x.TREATMENT_DAY_COUNT ?? 0);

                        rdo.TOTAL_DEATH_LESS_THAN_15_AGE = listDeaths.Where(x => x.ICD_CODE == hisIcd.ICD_CODE && Calculation.Age(x.TDL_PATIENT_DOB) < 15).Count();
                        rdo.TOTAL_LESS_THAN_15_AGE = lisTreatments.Where(x => x.ICD_CODE == hisIcd.ICD_CODE && Calculation.Age(x.TDL_PATIENT_DOB) < 15).Count();
                        rdo.TOTAL_TREATMENT_DAY_LESS_THAN_15_AGE = lisTreatments.Where(x => x.ICD_CODE == hisIcd.ICD_CODE && Calculation.Age(x.TDL_PATIENT_DOB) < 15).Sum(x => x.TREATMENT_DAY_COUNT);

                        rdo.TOTAL_DEATH_LESS_THAN_6_AGE = listDeaths.Where(x => x.ICD_CODE == hisIcd.ICD_CODE && Calculation.Age(x.TDL_PATIENT_DOB) < 6).Count();
                        rdo.TOTAL_LESS_THAN_6_AGE = lisTreatments.Where(x => x.ICD_CODE == hisIcd.ICD_CODE && Calculation.Age(x.TDL_PATIENT_DOB) < 6).Count();
                        rdo.TOTAL_TREATMENT_DAY_LESS_THAN_6_AGE = lisTreatments.Where(x => x.ICD_CODE == hisIcd.ICD_CODE && Calculation.Age(x.TDL_PATIENT_DOB) < 6).Sum(x => x.TREATMENT_DAY_COUNT);

                        rdo.DIC_DEATH_24H_AMOUNT = listDeaths.Where(x => x.ICD_CODE == hisIcd.ICD_CODE).GroupBy(x => x.DEATH_WITHIN_ID ?? 0).ToDictionary(x => x.Key.ToString(), y => y.Count());
                        rdo.DIC_DEATH_FEMALE_24H_AMOUNT = listDeaths.Where(x => x.ICD_CODE == hisIcd.ICD_CODE && x.TDL_PATIENT_GENDER_ID == IMSys.DbConfig.HIS_RS.HIS_GENDER.ID__FEMALE).GroupBy(x => x.DEATH_WITHIN_ID ?? 0).ToDictionary(x => x.Key.ToString(), y => y.Count());
                        listRdo.Add(rdo);
                    }
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
            dicSingleTag.Add("TIME_FROM", Inventec.Common.DateTime.Convert.TimeNumberToDateString(filter.TIME_FROM));
            dicSingleTag.Add("TIME_TO", Inventec.Common.DateTime.Convert.TimeNumberToDateString(filter.TIME_TO));

            objectTag.AddObjectData(store, "Report", listRdo);
        }

    }
}
