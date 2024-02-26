using MOS.MANAGER.HisPatientType;
using MOS.MANAGER.HisService;
using Inventec.Common.FlexCellExport;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisDepartment;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTreatment;
using MRS.MANAGER.Base;
using MRS.MANAGER.Config;
using MRS.MANAGER.Core.MrsReport;
using MRS.MANAGER.Core.MrsReport.RDO;
using MRS.Processor.Mrs00058;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Processor.Mrs00058
{
    public class Mrs00058Processor : AbstractProcessor
    {
        Mrs00058Filter castFilter = null;
        List<Mrs00058RDO> ListRdo = new List<Mrs00058RDO>();
        List<long> listServiceId = new List<long>();

        Dictionary<long, List<HIS_SERVICE_MACHINE>> dicServiceMachine = new Dictionary<long, List<HIS_SERVICE_MACHINE>>();
        List<HIS_MACHINE> ListMachine = new List<HIS_MACHINE>();

        CommonParam paramGet = new CommonParam();
        public Mrs00058Processor(CommonParam param, string reportTypeCode)
            : base(param, reportTypeCode)
        {
        }

        public override Type FilterType()
        {
            return typeof(Mrs00058Filter);
        }

        protected override bool GetData()
        {
            bool result = false;
            try
            {
                castFilter = (Mrs00058Filter)this.reportFilter;

                ListRdo = new ManagerSql().GetRdo(castFilter, new List<long>() { IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__SA });

                //máy
                GetMachine();

                //dịch vụ máy
                GetServiceMachine();

                result = true;
                
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);

                result = false;
            }
            return result;
        }

        private void GetMachine()
        {
            string query = "select * from his_machine where is_delete=0";
            ListMachine = new MOS.DAO.Sql.SqlDAO().GetSql<HIS_MACHINE>(query) ?? new List<HIS_MACHINE>();
        }

        private void GetServiceMachine()
        {
            string query = "select * from his_service_machine where is_delete=0";
            var ListServiceMachine = new MOS.DAO.Sql.SqlDAO().GetSql<HIS_SERVICE_MACHINE>(query);
            if (ListServiceMachine != null && ListServiceMachine.Count > 0)
            {
                dicServiceMachine = ListServiceMachine.GroupBy(o => o.SERVICE_ID).ToDictionary(p => p.Key, q => q.ToList());
            }
        }

        protected override bool ProcessData()
        {
            bool result = false;
            try
            {
                if (ListRdo != null && ListRdo.Count > 0)
                {
                    ListRdo = ListRdo.ToList().OrderBy(d => d.TDL_PATIENT_ID).ToList();

                    if (ListRdo != null && ListRdo.Count > 0)
                    {
                        CommonParam param = new CommonParam();
                        int start = 0;
                        int count = ListRdo.Count;
                        while (count > 0)
                        {
                            int limit = (count <= ManagerConstant.MAX_REQUEST_LENGTH_PARAM ? count : ManagerConstant.MAX_REQUEST_LENGTH_PARAM);
                            List<Mrs00058RDO> ListServiceReqLimit = ListRdo.Skip(start).Take(limit).ToList();
                            ProcessOneSereServFromList(ListServiceReqLimit);
                            start += ManagerConstant.MAX_REQUEST_LENGTH_PARAM;
                            count -= ManagerConstant.MAX_REQUEST_LENGTH_PARAM;
                        }

                        ListRdo = ListRdo.OrderBy(o => o.PATIENT_CODE).ToList();


                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                ListRdo.Clear();
            }
            return result;
        }

        private void ProcessOneSereServFromList(List<Mrs00058RDO> sereServs)
        {
            try
            {
                foreach (var rdo in sereServs)
                {
                    rdo.EXECUTE_DATE_STR = Inventec.Common.DateTime.Convert.TimeNumberToDateString(rdo.INTRUCTION_TIME_NUM ?? 0);
                    rdo.INTRUCTION_TIME = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(rdo.INTRUCTION_TIME_NUM??0);
                    rdo.FINISH_TIME_STR = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(rdo.FINISH_TIME ?? 0);
                    var patientType = HisPatientTypeCFG.PATIENT_TYPEs.FirstOrDefault(o => o.ID == rdo.PATIENT_TYPE_ID) ?? new HIS_PATIENT_TYPE();
                    var patientType2 = HisPatientTypeCFG.PATIENT_TYPEs.FirstOrDefault(o => o.ID == rdo.TDL_PATIENT_TYPE_ID) ?? new HIS_PATIENT_TYPE();
                    rdo.PATIENT_TYPE_NAME = patientType.PATIENT_TYPE_NAME;
                    rdo.PATIENT_TYPE_CODE = patientType.PATIENT_TYPE_CODE;
                    rdo.PATIENT_TYPE_NAME_1 = patientType2.PATIENT_TYPE_NAME;
                    rdo.REQUEST_ROOM = (HisRoomCFG.HisRooms.FirstOrDefault(o => rdo.REQUEST_ROOM_ID == o.ID) ?? new V_HIS_ROOM()).ROOM_NAME;
                    rdo.EXECUTE_DEPARTMENT_NAME = (HisRoomCFG.HisRooms.FirstOrDefault(o => rdo.EXECUTE_ROOM_ID == o.ID) ?? new V_HIS_ROOM()).DEPARTMENT_NAME;
                    rdo.REQUEST_DEPARTMENT_NAME = (HisRoomCFG.HisRooms.FirstOrDefault(o => rdo.REQUEST_ROOM_ID == o.ID) ?? new V_HIS_ROOM()).DEPARTMENT_NAME;
                    //if (serviceReq != null)
                    {
                        rdo.ICD_SUIM = rdo.ICD_NAME + ";" + rdo.ICD_TEXT;
                    }

                    CalcuatorAge(rdo);
                    IsBhyt( rdo);

                    var serviceMachine = dicServiceMachine.ContainsKey(rdo.SERVICE_ID) ? dicServiceMachine[rdo.SERVICE_ID] : null;
                    if (serviceMachine != null && serviceMachine.Count > 0)
                    {
                        var machine = ListMachine.Where(p => serviceMachine.Exists(o => p.ID == o.MACHINE_ID)).ToList();
                        if (machine.Count > 0)
                        {
                            rdo.MACHINE_NAME = string.Join(";", machine.Where(o => !string.IsNullOrWhiteSpace(o.MACHINE_NAME)).Select(p => p.MACHINE_NAME).ToList());
                            rdo.MACHINE_CODE = string.Join(";", machine.Where(o => !string.IsNullOrWhiteSpace(o.MACHINE_CODE)).Select(p => p.MACHINE_CODE).ToList());
                        }
                    }
                    var machineExt = ListMachine.Where(p => p.ID == rdo.MACHINE_ID).ToList();
                    if (machineExt.Count > 0)
                    {
                        rdo.EXECUTE_MACHINE_NAME = string.Join(";", machineExt.Where(o => !string.IsNullOrWhiteSpace(o.MACHINE_NAME)).Select(p => p.MACHINE_NAME).ToList());
                        rdo.EXECUTE_MACHINE_CODE = string.Join(";", machineExt.Where(o => !string.IsNullOrWhiteSpace(o.MACHINE_CODE)).Select(p => p.MACHINE_CODE).ToList());
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                ListRdo.Clear();
            }
        }

        private void CalcuatorAge(Mrs00058RDO rdo)
        {
            try
            {
                int? tuoi = RDOCommon.CalculateAge(rdo.TDL_PATIENT_DOB ?? 0);
                if (tuoi >= 0)
                {
                    if (rdo.TDL_PATIENT_GENDER_ID == IMSys.DbConfig.HIS_RS.HIS_GENDER.ID__MALE)
                    {
                        rdo.MALE_AGE = (tuoi >= 1) ? tuoi : 1;
                    }
                    else
                    {
                        rdo.FEMALE_AGE = (tuoi >= 1) ? tuoi : 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void IsBhyt( Mrs00058RDO rdo)
        {
            try
            {
                if (rdo.TDL_PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                {
                    rdo.IS_BHYT = "X";
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        protected override void SetTag(Dictionary<string, object> dicSingleTag, ProcessObjectTag objectTag, Store store)
        {
            dicSingleTag.Add("INTRUCTION_TIME_TO_STR", Inventec.Common.DateTime.Convert.TimeNumberToDateString(((Mrs00058Filter)this.reportFilter).TIME_FROM ?? 0) + Inventec.Common.DateTime.Convert.TimeNumberToDateString(((Mrs00058Filter)this.reportFilter).FINISH_TIME_FROM ?? 0));
            dicSingleTag.Add("INTRUCTION_TIME_FROM_STR", Inventec.Common.DateTime.Convert.TimeNumberToDateString(((Mrs00058Filter)this.reportFilter).TIME_TO ?? 0) + Inventec.Common.DateTime.Convert.TimeNumberToDateString(((Mrs00058Filter)this.reportFilter).FINISH_TIME_TO ?? 0));
            if (((Mrs00058Filter)this.reportFilter).EXECUTE_DEPARTMENT_ID != null)
            {
                var department = new HisDepartmentManager().GetById(((Mrs00058Filter)this.reportFilter).EXECUTE_DEPARTMENT_ID ?? 0);
                dicSingleTag.Add("EXECUTE_DEPARTMENT_NAME", department.DEPARTMENT_NAME);
            }

            if (((Mrs00058Filter)this.reportFilter).EXE_ROOM_ID != null)
            {
                var room = HisRoomCFG.HisRooms.FirstOrDefault(o => ((Mrs00058Filter)this.reportFilter).EXE_ROOM_ID == o.ID);
                dicSingleTag.Add("EXECUTE_ROOM_NAME", room.ROOM_NAME);
            }

            if (((Mrs00058Filter)this.reportFilter).REQUEST_DEPARTMENT_ID != null)
            {
                var department = new HisDepartmentManager().GetById(((Mrs00058Filter)this.reportFilter).REQUEST_DEPARTMENT_ID ?? 0);
                dicSingleTag.Add("REQUEST_DEPARTMENT_NAME", department.DEPARTMENT_NAME);
            }
            if (((Mrs00058Filter)this.reportFilter).EXECUTE_ROOM_IDs != null)
            {
                var room = HisRoomCFG.HisRooms.Where(o => ((Mrs00058Filter)this.reportFilter).EXECUTE_ROOM_IDs.Contains(o.ID)).ToList();
                dicSingleTag.Add("EXECUTE_ROOM_NAME", string.Join(" - ", room.Select(o => o.ROOM_NAME).ToList()));
            }

            if (castFilter.PATIENT_TYPE_IDs != null)
            {
                var patientType = HisPatientTypeCFG.PATIENT_TYPEs.Where(o => castFilter.PATIENT_TYPE_IDs.Contains(o.ID)).ToList();
                dicSingleTag.Add("PATIENT_TYPE_NAMEs", string.Join(";", patientType.Select(o => o.PATIENT_TYPE_NAME).ToList()));
            }
            if (castFilter.TDL_PATIENT_TYPE_IDs != null)
            {
                var patientType = HisPatientTypeCFG.PATIENT_TYPEs.Where(o => castFilter.TDL_PATIENT_TYPE_IDs.Contains(o.ID)).ToList();
                dicSingleTag.Add("TDL_PATIENT_TYPE_NAMEs", string.Join(";", patientType.Select(o => o.PATIENT_TYPE_NAME).ToList()));
            }

            objectTag.AddObjectData(store, "Report", ListRdo.OrderBy(o => o.FINISH_TIME).Take(1048500).ToList());

            objectTag.AddObjectData(store, "Report1048500", ListRdo.OrderBy(o => o.FINISH_TIME).Skip(1048500).ToList());
        }

    }
}
