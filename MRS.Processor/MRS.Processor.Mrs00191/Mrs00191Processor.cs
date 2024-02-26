using MOS.MANAGER.HisPatientType;
using MOS.MANAGER.HisCare;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisDepartment;
using MOS.MANAGER.HisTreatmentEndType;
using MOS.MANAGER.HisTranPatiReason;
using MOS.MANAGER.HisTranPatiForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Common.FlexCellExport;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MRS.MANAGER.Base;
using MRS.MANAGER.Core.MrsReport;

using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisDepartmentTran;
using MOS.MANAGER.HisPatientTypeAlter;
using AutoMapper;
using MRS.MANAGER.Config;
using FlexCel.Report;
using ACS.Filter;

using ACS.EFMODEL.DataModels;
using MOS.MANAGER.HisCareer;

namespace MRS.Processor.Mrs00191
{
    public class Mrs00191Processor : AbstractProcessor
    {
        CommonParam paramGet = new CommonParam();
        Mrs00191Filter CastFilter = null;
        private List<Mrs00191RDO> ListRdo = new List<Mrs00191RDO>();
        private List<V_HIS_TREATMENT> listTreatments = new List<V_HIS_TREATMENT>();
        private List<V_HIS_PATIENT_TYPE_ALTER> listPatienttypeAlters = new List<V_HIS_PATIENT_TYPE_ALTER>();
        Dictionary<string, ACS_USER> dicAcsUser = new Dictionary<string, ACS_USER>();
        public Mrs00191Processor(CommonParam param, string reportTypeCode)
            : base(param, reportTypeCode)
        {
        }

        public override Type FilterType()
        {
            return typeof(Mrs00191Filter);
        }

        protected override bool GetData()
        {
            CastFilter = ((Mrs00191Filter)reportFilter);
            var result = true;
            try
            {
                var HisTreatmentFilterQuery = new HisTreatmentViewFilterQuery()
                {
                    OUT_TIME_FROM = CastFilter.OUT_TIME_FROM, //lay thoi gian duyet khoa
                    OUT_TIME_TO = CastFilter.OUT_TIME_TO,  //lay thoi gian duyet khoa
                    FEE_LOCK_TIME_FROM = CastFilter.FEE_LOCK_TIME_FROM, //lay thoi gian duyet khoa
                    FEE_LOCK_TIME_TO = CastFilter.FEE_LOCK_TIME_TO,  //lay thoi gian duyet khoa
                    TREATMENT_END_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__CHUYEN, //lay BN chuyển viên
                };
                listTreatments = new HisTreatmentManager(paramGet).GetView(HisTreatmentFilterQuery);
                if (CastFilter.TREATMENT_TYPE_IDs != null)
                {
                    listTreatments = listTreatments.Where(o => CastFilter.TREATMENT_TYPE_IDs.Contains(o.TDL_TREATMENT_TYPE_ID ?? 0)).ToList();
                }
                var treatmentIDs = listTreatments.Select(s => s.ID).ToList();
                if (IsNotNullOrEmpty(treatmentIDs))
                {
                    var skip = 0;
                    while (treatmentIDs.Count() - skip > 0)
                    {
                        var ListDSs = treatmentIDs.Skip(skip).Take(ManagerConstant.MAX_REQUEST_LENGTH_PARAM).ToList();
                        skip = skip + ManagerConstant.MAX_REQUEST_LENGTH_PARAM;
                        var patienttypeAlterFilters = new HisPatientTypeAlterViewFilterQuery()
                        {
                            TREATMENT_IDs = ListDSs,
                        };
                        var listPatienttypeAlter = new HisPatientTypeAlterManager(paramGet).GetView(patienttypeAlterFilters);
                        listPatienttypeAlters.AddRange(listPatienttypeAlter);
                    }
                }
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
                ListRdo.Clear();
                //lay nhung doi tuong chuyen vien (02 la chyen vien)
                listTreatments = listTreatments.Where(s => HisTreatmentEndTypeCFG.TREATMENT_END_TYPE_ID__CV.Contains(s.TREATMENT_END_TYPE_ID ?? 0)).ToList();
                foreach (var tranPatiTreatment in listTreatments)
                {
                    var rdo = new Mrs00191RDO(tranPatiTreatment);
                    if (tranPatiTreatment.TDL_PATIENT_GENDER_ID == IMSys.DbConfig.HIS_RS.HIS_GENDER.ID__MALE)
                    {
                        rdo.AGE_MALE = Inventec.Common.DateTime.Calculation.AgeCaption(tranPatiTreatment.TDL_PATIENT_DOB);
                    }
                    else if (tranPatiTreatment.TDL_PATIENT_GENDER_ID == IMSys.DbConfig.HIS_RS.HIS_GENDER.ID__FEMALE)
                    {
                        rdo.AGE_FEMALE = Inventec.Common.DateTime.Calculation.AgeCaption(tranPatiTreatment.TDL_PATIENT_DOB);
                    }
                    ListRdo.Add(rdo);
                }
            }
            catch (Exception ex)
            {
                result = false;
                LogSystem.Error(ex);
            }
            return result;
        }

        protected override void SetTag(Dictionary<string, object> dicSingleTag, ProcessObjectTag objectTag, Store store)
        {
            dicSingleTag.Add("TIME_FROM", Inventec.Common.DateTime.Convert.TimeNumberToTimeString(CastFilter.OUT_TIME_FROM ?? CastFilter.FEE_LOCK_TIME_FROM ?? 0));

            dicSingleTag.Add("TIME_TO", Inventec.Common.DateTime.Convert.TimeNumberToTimeString(CastFilter.OUT_TIME_TO ?? CastFilter.FEE_LOCK_TIME_TO ?? 0));

            objectTag.AddObjectData(store, "Report", ListRdo.ToList());
        }

    }


}
