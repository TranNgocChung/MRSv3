using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisBedLog;
using MOS.MANAGER.HisDebate;
using MOS.MANAGER.HisDepartment;
using MOS.MANAGER.HisDepartmentTran;
using MOS.MANAGER.HisDhst;
using MOS.MANAGER.HisEkipUser;
using MOS.MANAGER.HisHeinApproval;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServPttt;
using MOS.MANAGER.HisSereServTein;
using MOS.MANAGER.HisTracking;
using MOS.MANAGER.HisTreatment;
using MRS.MANAGER.Base;
using MRS.MANAGER.Config;
using MRS.MANAGER.Core.MrsReport;
using MRS.Processor.Mrs00826.HoSoProcessor;
using MRS.Processor.Mrs00826.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.MANAGER.HisConfig;
using MOS.MANAGER.HisMediOrg;
using MOS.MANAGER.HisService;
using MOS.MANAGER.HisMaterialType;
using MOS.MANAGER.HisIcd;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisServiceReq;

namespace MRS.Processor.Mrs00826
{
    public class Mrs00826Processor : AbstractProcessor
    {
        public Mrs00826Filter filter;
        public List<Mrs00826RDO> ListRdo = new List<Mrs00826RDO>();
        public List<V_HIS_TREATMENT_1> listSelection = new List<V_HIS_TREATMENT_1>();
        public List<long> listSelectionSrId = new List<long>();
        public List<long> listSelectionHapId = new List<long>();

        public List<V_HIS_ROOM> ListRoom = new List<V_HIS_ROOM>();
        public List<HIS_DEPARTMENT> ListDepartment = new List<HIS_DEPARTMENT>();
        List<V_HIS_SERE_SERV_TEIN> hisSereServTeins = new List<V_HIS_SERE_SERV_TEIN>();
        List<V_HIS_SERE_SERV_PTTT> hisSereServPttts = new List<V_HIS_SERE_SERV_PTTT>();
        List<HIS_DHST> listDhst = new List<HIS_DHST>();
        List<HIS_TRACKING> hisTrackings = new List<HIS_TRACKING>();
        List<V_HIS_TREATMENT_3> hisTreatments = new List<V_HIS_TREATMENT_3>();
        List<V_HIS_SERE_SERV_2> ListSereServ = new List<V_HIS_SERE_SERV_2>();
        List<HIS_EKIP_USER> ListEkipUser = new List<HIS_EKIP_USER>();
        List<V_HIS_BED_LOG> ListBedlog = new List<V_HIS_BED_LOG>();
        List<V_HIS_HEIN_APPROVAL> listHeinApproval = new List<V_HIS_HEIN_APPROVAL>();
        List<V_HIS_PATIENT_TYPE_ALTER> listPatientTypeAlter = new List<V_HIS_PATIENT_TYPE_ALTER>();
        List<HIS_DEBATE> ListDebates = new List<HIS_DEBATE>();

        List<HIS_CONFIG> NewConfig = new List<HIS_CONFIG>();
        List<HIS_MEDI_ORG> listMediOrg = new List<HIS_MEDI_ORG>();
        List<V_HIS_SERVICE> listService = new List<V_HIS_SERVICE>();
        List<HIS_MATERIAL_TYPE> listMaterialType = new List<HIS_MATERIAL_TYPE>();
        List<HIS_ICD> listIcd = new List<HIS_ICD>();


        public Mrs00826Processor(CommonParam param, string reportTypeCode)
            : base(param, reportTypeCode)
        {
        }
       
        public override Type FilterType()
        {
            return typeof(Mrs00826Filter);
        }

        protected override bool GetData()
        {
            filter = (Mrs00826Filter)this.reportFilter;
            bool result = true;
            try
            {
                NewConfig = GetNewConfig();
                listMediOrg = GetMediOrg();
                listService = GetService();
                listMaterialType = GetMaterialType();
                listIcd = GetIcd();
                listSelection = GetTreatment();
                
                //lọc theo diện điều trị
                FilterByTreatmentType();

                var skip = 0;
                while (listSelection.Count - skip > 0)
                {
                    var limit = listSelection.Skip(skip).Take(ManagerConstant.MAX_REQUEST_LENGTH_PARAM).ToList();
                    skip += ManagerConstant.MAX_REQUEST_LENGTH_PARAM;
                     listHeinApproval = new List<V_HIS_HEIN_APPROVAL>();
                        ListSereServ = new List<V_HIS_SERE_SERV_2>();
                        ListEkipUser = new List<HIS_EKIP_USER>();
                        ListBedlog = new List<V_HIS_BED_LOG>();
                        hisTreatments = new List<V_HIS_TREATMENT_3>();
                        listDhst = new List<HIS_DHST>();
                        hisTrackings = new List<HIS_TRACKING>();
                        hisSereServTeins = new List<V_HIS_SERE_SERV_TEIN>();
                        hisSereServPttts = new List<V_HIS_SERE_SERV_PTTT>();
                        ListDebates = new List<HIS_DEBATE>();
                        listPatientTypeAlter = new List<V_HIS_PATIENT_TYPE_ALTER>();

                        CreateThreadGetData(limit);
                        ProcessExportXmlDetail(hisTreatments, listHeinApproval, ListSereServ, listDhst, hisSereServTeins, hisTrackings, hisSereServPttts, ListEkipUser, ListBedlog,  ListDebates, listPatientTypeAlter);
                }
               
	        }
	        catch (Exception ex)
	        {
                Inventec.Common.Logging.LogSystem.Error(ex);
		        result= false;
	        }
            return result;
            
        }

        private void FilterByTreatmentType()
        {
            if (filter.TREATMENT_TYPE_IDs != null)
            {
                listSelection = listSelection.Where(o => filter.TREATMENT_TYPE_IDs.Contains(o.TDL_TREATMENT_TYPE_ID ?? 0)).ToList();
            }
        }

        private List<V_HIS_TREATMENT_1> GetTreatment()
        {
            if (filter.INPUT_DATA_ID__TIME_TYPE == 1)//vào viện
            {
                HisTreatmentView1FilterQuery treaFilter = new HisTreatmentView1FilterQuery();
                treaFilter.IN_TIME_FROM = filter.TIME_FROM;
                treaFilter.IN_TIME_TO = filter.TIME_TO;
                return new HisTreatmentManager().GetView1(treaFilter);
            }
            else if(filter.INPUT_DATA_ID__TIME_TYPE == 2)//ra viện
            {
                HisTreatmentView1FilterQuery treaFilter = new HisTreatmentView1FilterQuery();
                treaFilter.OUT_TIME_FROM = filter.TIME_FROM;
                treaFilter.OUT_TIME_TO = filter.TIME_TO;
                return new HisTreatmentManager().GetView1(treaFilter);
            }
            else if (filter.INPUT_DATA_ID__TIME_TYPE == 3)//chỉ định
            {
                List<V_HIS_TREATMENT_1> result = new List<V_HIS_TREATMENT_1>();
                HisServiceReqFilterQuery serviceReqFilter = new HisServiceReqFilterQuery();
                serviceReqFilter.INTRUCTION_TIME_FROM = filter.TIME_FROM;
                serviceReqFilter.INTRUCTION_TIME_TO = filter.TIME_TO;
                var listSelectionSr = new HisServiceReqManager().Get(serviceReqFilter);
                listSelectionSrId = listSelectionSr.Select(o => o.ID).ToList();
                var treatmentIds = listSelectionSr.Select(o=>o.TREATMENT_ID).Distinct().ToList();
                var skip = 0;
                while (treatmentIds.Count - skip > 0)
                {
                    var limit = treatmentIds.Skip(skip).Take(ManagerConstant.MAX_REQUEST_LENGTH_PARAM).ToList();
                    skip = skip + ManagerConstant.MAX_REQUEST_LENGTH_PARAM;

                    HisTreatmentView1FilterQuery treaFilter = new HisTreatmentView1FilterQuery();
                    treaFilter.IDs = limit;
                    var rs = new HisTreatmentManager().GetView1(treaFilter);
                    if (rs != null && rs.Count > 0)
                    {
                        result.AddRange(rs);
                    }
                }
                return result;
            }
            else //giám định bảo hiểm
            {
                List<V_HIS_TREATMENT_1> result = new List<V_HIS_TREATMENT_1>();
                HisHeinApprovalFilterQuery heinApprovalFilter = new HisHeinApprovalFilterQuery();
                heinApprovalFilter.EXECUTE_TIME_FROM = filter.TIME_FROM;
                heinApprovalFilter.EXECUTE_TIME_TO = filter.TIME_TO;
                var listSelectionHap = new HisHeinApprovalManager().Get(heinApprovalFilter);
                listSelectionHapId = listSelectionHap.Select(o => o.ID).ToList();
                var treatmentIds = listSelectionHap.Select(o => o.TREATMENT_ID).Distinct().ToList();
                var skip = 0;
                while (treatmentIds.Count - skip > 0)
                {
                    var limit = treatmentIds.Skip(skip).Take(ManagerConstant.MAX_REQUEST_LENGTH_PARAM).ToList();
                    skip = skip + ManagerConstant.MAX_REQUEST_LENGTH_PARAM;

                    HisTreatmentView1FilterQuery treaFilter = new HisTreatmentView1FilterQuery();
                    treaFilter.IDs = limit;
                    var rs = new HisTreatmentManager().GetView1(treaFilter);
                    if (rs != null && rs.Count > 0)
                    {
                        result.AddRange(rs);
                    }
                }
                return result;
            }
        }


        private List<HIS_CONFIG> GetNewConfig()
        {
            List<HIS_CONFIG> result = null;
            try
            {
                CommonParam paramGet = new CommonParam();
                HisConfigFilterQuery configFilter = new HisConfigFilterQuery();
                configFilter.IS_ACTIVE = 1;
                result = new HisConfigManager().Get(configFilter);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }


        private List<HIS_MEDI_ORG> GetMediOrg()
        {
            List<HIS_MEDI_ORG> result = null;
            try
            {
                CommonParam paramGet = new CommonParam();
                HisMediOrgFilterQuery mediOrgFilter = new HisMediOrgFilterQuery();
                mediOrgFilter.IS_ACTIVE = 1;
                result = new HisMediOrgManager().Get(mediOrgFilter);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }


        private List<V_HIS_SERVICE> GetService()
        {
            List<V_HIS_SERVICE> result = null;
            try
            {
                CommonParam paramGet = new CommonParam();
                HisServiceViewFilterQuery serviceFilter = new HisServiceViewFilterQuery();
                serviceFilter.IS_ACTIVE = 1;
                result = new HisServiceManager().GetView(serviceFilter);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }


        private List<HIS_MATERIAL_TYPE> GetMaterialType()
        {
            List<HIS_MATERIAL_TYPE> result = null;
            try
            {
                HisMaterialTypeFilterQuery materialTypeFilter = new HisMaterialTypeFilterQuery();
                materialTypeFilter.IS_ACTIVE = 1;
                result = new HisMaterialTypeManager().Get(materialTypeFilter);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }


        private List<HIS_ICD> GetIcd()
        {
            List<HIS_ICD> result = null;
            try
            {
                HisIcdFilterQuery icdFilter = new HisIcdFilterQuery();
                icdFilter.IS_ACTIVE = 1;
                result = new HisIcdManager().Get(icdFilter);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }
       void ProcessExportXmlDetail(List<V_HIS_TREATMENT_3> hisTreatments, List<V_HIS_HEIN_APPROVAL> hisHeinApprvals,
            List<V_HIS_SERE_SERV_2> ListSereServ, List<HIS_DHST> listDhst, List<V_HIS_SERE_SERV_TEIN> listSereServTein,
            List<HIS_TRACKING> hisTrackings, List<V_HIS_SERE_SERV_PTTT> hisSereServPttts, List<HIS_EKIP_USER> ListEkipUser,
            List<V_HIS_BED_LOG> ListBedlog, List<HIS_DEBATE> listDebate, List<V_HIS_PATIENT_TYPE_ALTER> listPatientTypeAlter)
        {
            Dictionary<string, List<string>> DicErrorMess = new Dictionary<string, List<string>>();
            try
            {
                Dictionary<long, List<V_HIS_HEIN_APPROVAL>> dicHeinApproval = new Dictionary<long, List<V_HIS_HEIN_APPROVAL>>();
                Dictionary<long, List<V_HIS_SERE_SERV_2>> dicSereServ = new Dictionary<long, List<V_HIS_SERE_SERV_2>>();
                Dictionary<long, List<V_HIS_SERE_SERV_TEIN>> dicSereServTein = new Dictionary<long, List<V_HIS_SERE_SERV_TEIN>>();
                Dictionary<long, HIS_DHST> dicDhst = new Dictionary<long, HIS_DHST>();
                Dictionary<long, List<HIS_TRACKING>> dicTracking = new Dictionary<long, List<HIS_TRACKING>>();
                Dictionary<long, List<V_HIS_SERE_SERV_PTTT>> dicSereServPttt = new Dictionary<long, List<V_HIS_SERE_SERV_PTTT>>();
                Dictionary<long, List<HIS_EKIP_USER>> dicEkipUser = new Dictionary<long, List<HIS_EKIP_USER>>();
                Dictionary<long, List<V_HIS_BED_LOG>> dicBedLog = new Dictionary<long, List<V_HIS_BED_LOG>>();
                Dictionary<long, List<HIS_DEBATE>> dicDebate = new Dictionary<long, List<HIS_DEBATE>>();
                Dictionary<long, List<HIS_DHST>> dicDhstList = new Dictionary<long, List<HIS_DHST>>();
                Dictionary<long, List<V_HIS_PATIENT_TYPE_ALTER>> dicPatientTypeAlter = new Dictionary<long, List<V_HIS_PATIENT_TYPE_ALTER>>();

                //kiểm tra loại thời gian đã chọn
                if (listSelectionHapId != null && listSelectionHapId.Count > 0)
                {
                    hisHeinApprvals = hisHeinApprvals.Where(o => listSelectionHapId.Contains(o.ID)).ToList();
                }

                if (listSelectionSrId != null && listSelectionSrId.Count > 0)
                {
                    ListSereServ = ListSereServ.Where(o => listSelectionSrId.Contains(o.SERVICE_REQ_ID ?? 0)).ToList();
                    listSereServTein = listSereServTein.Where(o => listSelectionSrId.Contains(o.TDL_SERVICE_REQ_ID ?? 0)).ToList();
                    ListBedlog = ListBedlog.Where(o => listSelectionSrId.Contains(o.SERVICE_REQ_ID ?? 0)).ToList();
                }

                if (hisHeinApprvals != null && hisHeinApprvals.Count > 0)
                {
                    foreach (var item in hisHeinApprvals)
                    {
                        if (!dicHeinApproval.ContainsKey(item.TREATMENT_ID))
                            dicHeinApproval[item.TREATMENT_ID] = new List<V_HIS_HEIN_APPROVAL>();
                        dicHeinApproval[item.TREATMENT_ID].Add(item);
                    }
                }

                if (listPatientTypeAlter != null && listPatientTypeAlter.Count > 0)
                {
                    foreach (var item in listPatientTypeAlter)
                    {
                        if (!dicPatientTypeAlter.ContainsKey(item.TREATMENT_ID))
                            dicPatientTypeAlter[item.TREATMENT_ID] = new List<V_HIS_PATIENT_TYPE_ALTER>();
                        dicPatientTypeAlter[item.TREATMENT_ID].Add(item);
                    }
                }

                if (ListSereServ != null && ListSereServ.Count > 0)
                {
                    foreach (var sereServ in ListSereServ)
                    {
                        if (sereServ.TDL_HEIN_SERVICE_TYPE_ID.HasValue && sereServ.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT && sereServ.AMOUNT > 0 && sereServ.PRICE > 0 && sereServ.IS_EXPEND != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE && sereServ.IS_NO_EXECUTE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE && sereServ.TDL_TREATMENT_ID.HasValue)
                        {
                            if (!dicSereServ.ContainsKey(sereServ.TDL_TREATMENT_ID.Value))
                                dicSereServ[sereServ.TDL_TREATMENT_ID.Value] = new List<V_HIS_SERE_SERV_2>();
                            dicSereServ[sereServ.TDL_TREATMENT_ID.Value].Add(sereServ);
                        }

                        if (sereServ.EKIP_ID.HasValue && ListEkipUser != null && ListEkipUser.Count > 0 && sereServ.TDL_TREATMENT_ID.HasValue)
                        {
                            var ekips = ListEkipUser.Where(o => o.EKIP_ID == sereServ.EKIP_ID).ToList();
                            if (ekips != null && ekips.Count > 0)
                            {
                                foreach (var item in ekips)
                                {
                                    if (!dicEkipUser.ContainsKey(sereServ.TDL_TREATMENT_ID.Value))
                                        dicEkipUser[sereServ.TDL_TREATMENT_ID.Value] = new List<HIS_EKIP_USER>();

                                    dicEkipUser[sereServ.TDL_TREATMENT_ID.Value].Add(item);
                                }
                            }
                        }
                    }
                }

                if (listSereServTein != null && listSereServTein.Count > 0)
                {
                    foreach (var ssTein in listSereServTein)
                    {
                        if (!ssTein.TDL_TREATMENT_ID.HasValue) continue;

                        if (!dicSereServTein.ContainsKey(ssTein.TDL_TREATMENT_ID.Value))
                            dicSereServTein[ssTein.TDL_TREATMENT_ID.Value] = new List<V_HIS_SERE_SERV_TEIN>();

                        dicSereServTein[ssTein.TDL_TREATMENT_ID.Value].Add(ssTein);
                    }
                }

                if (hisTrackings != null && hisTrackings.Count > 0)
                {
                    foreach (var tracking in hisTrackings)
                    {
                        if (!dicTracking.ContainsKey(tracking.TREATMENT_ID))
                            dicTracking[tracking.TREATMENT_ID] = new List<HIS_TRACKING>();

                        dicTracking[tracking.TREATMENT_ID].Add(tracking);
                    }
                }

                if (hisSereServPttts != null && hisSereServPttts.Count > 0)
                {
                    foreach (var ssPttt in hisSereServPttts)
                    {
                        if (!ssPttt.TDL_TREATMENT_ID.HasValue) continue;

                        if (!dicSereServPttt.ContainsKey(ssPttt.TDL_TREATMENT_ID.Value))
                            dicSereServPttt[ssPttt.TDL_TREATMENT_ID.Value] = new List<V_HIS_SERE_SERV_PTTT>();

                        dicSereServPttt[ssPttt.TDL_TREATMENT_ID.Value].Add(ssPttt);
                    }
                }

                if (listDhst != null && listDhst.Count > 0)
                {
                    //sap xep thoi gian tang dan de trong th co nhieu dhst se lay cai co thoi gian thuc hien lon nhat
                    //lay dhst cuoi cung co can nang
                    listDhst = listDhst.OrderBy(o => o.EXECUTE_TIME).ToList();
                    foreach (var item in listDhst)
                    {
                        if (dicDhst.ContainsKey(item.TREATMENT_ID))
                        {
                            if (item.WEIGHT.HasValue) dicDhst[item.TREATMENT_ID] = item;
                            else if (!dicDhst[item.TREATMENT_ID].WEIGHT.HasValue)
                                dicDhst[item.TREATMENT_ID] = item;
                        }
                        else
                            dicDhst[item.TREATMENT_ID] = item;

                        if (!dicDhstList.ContainsKey(item.TREATMENT_ID))
                            dicDhstList[item.TREATMENT_ID] = new List<HIS_DHST>();

                        dicDhstList[item.TREATMENT_ID].Add(item);
                    }
                }

                if (ListBedlog != null && ListBedlog.Count > 0)
                {
                    foreach (var bed in ListBedlog)
                    {
                        if (!dicBedLog.ContainsKey(bed.TREATMENT_ID))
                            dicBedLog[bed.TREATMENT_ID] = new List<V_HIS_BED_LOG>();

                        dicBedLog[bed.TREATMENT_ID].Add(bed);
                    }
                }

                if (listDebate != null && listDebate.Count > 0)
                {
                    foreach (var item in listDebate)
                    {
                        if (!dicDebate.ContainsKey(item.TREATMENT_ID))
                            dicDebate[item.TREATMENT_ID] = new List<HIS_DEBATE>();

                        dicDebate[item.TREATMENT_ID].Add(item);
                    }
                }

                foreach (var treatment in hisTreatments)
                {
                    InputADO ado = new InputADO();
                    ado.Treatment = treatment;
                    ado.HeinApprovals = new List<V_HIS_HEIN_APPROVAL>();
                    if (!dicHeinApproval.ContainsKey(treatment.ID))
                    {
                        
                        if (dicPatientTypeAlter.ContainsKey(treatment.ID))
                        {
                            var currentPatientTypeAlter = (dicPatientTypeAlter[treatment.ID]??new List<V_HIS_PATIENT_TYPE_ALTER>()).OrderBy(o=>o.LOG_TIME).ThenBy(p=>p.ID).LastOrDefault();
                            if (currentPatientTypeAlter != null)
                            {
                                V_HIS_HEIN_APPROVAL heinApp = new V_HIS_HEIN_APPROVAL();
                                Inventec.Common.Mapper.DataObjectMapper.Map<V_HIS_HEIN_APPROVAL>(heinApp, currentPatientTypeAlter);
                                ado.HeinApprovals.Add(heinApp);
                            }
                            else
                            {
                                ado.HeinApprovals = dicHeinApproval.ContainsKey(treatment.ID) ? dicHeinApproval[treatment.ID] : null;
                            }
                        }
                        else
                        {
                            ado.HeinApprovals = dicHeinApproval.ContainsKey(treatment.ID) ? dicHeinApproval[treatment.ID] : null;
                        }
                    }
                    else
                    {
                        ado.HeinApprovals = dicHeinApproval.ContainsKey(treatment.ID) ? dicHeinApproval[treatment.ID] : null;
                    }

                    if (!dicSereServ.ContainsKey(treatment.ID))
                    {
                        Inventec.Common.Logging.LogSystem.Info("treatment khong chua SereServ nao: " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => treatment), treatment));
                        var errorSereServ = "Hồ sơ không có dịch vụ";
                        if (!DicErrorMess.ContainsKey(errorSereServ))
                        {
                            DicErrorMess[errorSereServ] = new List<string>();
                        }

                        DicErrorMess[errorSereServ].Add(treatment.TREATMENT_CODE);
                        continue;
                    }

                    ado.ListSereServ = dicSereServ.ContainsKey(treatment.ID) ? dicSereServ[treatment.ID] : null;
                    ado.Branch = HisBranchCFG.HisBranchs.FirstOrDefault(o => o.ID == treatment.BRANCH_ID);
                    if (dicDhst.ContainsKey(treatment.ID))
                    {
                        ado.Dhst = dicDhst[treatment.ID];
                    }

                    if (dicSereServTein.ContainsKey(treatment.ID))
                    {
                        ado.SereServTeins = dicSereServTein[treatment.ID];
                    }

                    if (dicTracking.ContainsKey(treatment.ID))
                    {
                        ado.Trackings = dicTracking[treatment.ID];
                    }

                    if (dicSereServPttt.ContainsKey(treatment.ID))
                    {
                        ado.SereServPttts = dicSereServPttt[treatment.ID];
                    }

                    if (dicBedLog.ContainsKey(treatment.ID))
                    {
                        ado.BedLogs = dicBedLog[treatment.ID];
                    }

                    if (dicEkipUser.ContainsKey(treatment.ID))
                    {
                        ado.EkipUsers = dicEkipUser[treatment.ID].Distinct().ToList();
                    }

                    if (dicDebate.ContainsKey(treatment.ID))
                    {
                        ado.ListDebate = dicDebate[treatment.ID];
                    }

                    if (dicDhstList.ContainsKey(treatment.ID))
                    {
                        ado.ListDhsts = dicDhstList[treatment.ID];
                    }

                    ado.HeinApproval = ado.HeinApprovals != null ? ado.HeinApprovals.FirstOrDefault() : null;
                    ado.MaterialPackageOption = HisConfigKey.GetConfigData(NewConfig,HisConfigKey.MOS__BHYT__CALC_MATERIAL_PACKAGE_PRICE_OPTION);
                    ado.MaterialPriceOriginalOption = HisConfigKey.GetConfigData(NewConfig, HisConfigKey.XML__4210__MATERIAL_PRICE_OPTION);
                    ado.MaterialStentRatio = HisConfigKey.GetConfigData(NewConfig, HisConfigKey.XML__4210__MATERIAL_STENT_RATIO_OPTION);
                    ado.TenBenhOption =  HisConfigKey.GetConfigData(NewConfig,HisConfigKey.TEN_BENH_OPTION);
                    ado.HeinServiceTypeCodeNoTutorial =  HisConfigKey.GetConfigData(NewConfig,HisConfigKey.MaThuocOption);
                    ado.XMLNumbers =  HisConfigKey.GetConfigData(NewConfig,HisConfigKey.XmlNumbers);
                    ado.MaterialStent2Limit =  HisConfigKey.GetConfigData(NewConfig,HisConfigKey.Stent2LimitOption);
                    ado.IsTreatmentDayCount6556 =  HisConfigKey.GetConfigData(NewConfig,HisConfigKey.IS_TREATMENT_DAY_COUNT_6556);
                    ado.MaBacSiOption =  HisConfigKey.GetConfigData(NewConfig,HisConfigKey.MaBacSiOption);

                    ado.ConfigData = NewConfig;
                    ado.ListHeinMediOrg = this.listMediOrg;
                    ado.MaterialTypes = this.listMaterialType;
                    ado.TotalIcdData = this.listIcd;
                    ado.TotalSericeData = this.listService;
                    CreateNoiDungFile(ref ListRdo, ado);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
        protected override bool ProcessData()
        {
           bool result = true;
            try 
	        {
                //foreach (var item in ListTreatment)
                //{
                //    var Rooms = ListSereServ.Where(x=>x.TDL_TREATMENT_ID==item.ID).Select(x => x.EXECUTE_ROOM_NAME).Distinct().ToList();
                //    var Departmets = ListDepartmentTran.Where(x => x.TREATMENT_ID == item.ID).Select(x => x.DEPARTMENT_NAME).Distinct().ToList();
                //    Mrs00826RDO rdo = new Mrs00826RDO();
                    //rdo.PATIENT_CODE = item.TDL_PATIENT_CODE;
                    //rdo.PATIENT_NAME = item.TDL_PATIENT_NAME;
                    //rdo.PATIENT_DOB = item.TDL_PATIENT_DOB;
                    //rdo.PATIENT_DOB_STR =Inventec.Common.DateTime.Convert.TimeNumberToDateString(item.TDL_PATIENT_DOB);
                    ////rdo.PATIENT_AGE = Inventec.Common.DateTime.Calculation.AgeString(item.TDL_PATIENT_DOB);
                    //rdo.PATIENT_GENDER_NAME = item.TDL_PATIENT_GENDER_NAME;
                    //rdo.PATIENT_ADDRESS = item.TDL_PATIENT_ADDRESS;
                    //rdo.TREATMENT_CODE = item.TREATMENT_CODE;
                    //rdo.CCCD_NUMBER = item.TDL_PATIENT_CCCD_NUMBER;
                    //rdo.CMND_NUMBER = item.TDL_PATIENT_CMND_NUMBER;
                    //rdo.TDL_TREATMENT_TYPE_ID = item.TDL_TREATMENT_TYPE_ID ?? 0;
                    //if (Rooms!=null)
                    //{
                    //    rdo.ROOM_NAMEs = string.Join(",", Rooms.ToList());
                    //}
                    //if (Departmets!=null)
                    //{
                    //    rdo.DEPARTMENT_NAMEs = string.Join(",", Departmets.ToList());
                    //}
                //    ListRdo.Add(rdo);
                //}
                //result = true;
	        }
	        catch (Exception ex)
	        {
                Inventec.Common.Logging.LogSystem.Error(ex);
		        result= false;
	        }
            return result;
        }
        private void CreateNoiDungFile(ref List<Mrs00826RDO> listRdo,InputADO Data)
        {
            try
            {
                //object Xml1Data = null;
                //List<XML2DetailData> listDetailXml2 = new List<XML2DetailData>();
                //List<XML3DetailData> listDetailXml3 = new List<XML3DetailData>();
                //List<XML4DetailData> listDetailXml4 = new List<XML4DetailData>();
                //List<XML5DetailData> listDetailXml5 = new List<XML5DetailData>();

                //Xử lý xml2
                Xml2Processor xml2Processor = new Xml2Processor();
                List<Xml2ADO> xml2 = xml2Processor.GenerateXml2ADO(Data);
                //Xử lý xml 3
                Xml3Processor xml3Processor = new Xml3Processor();
                List<Xml3ADO> xml3 = xml3Processor.GenerateXml3ADO(Data);
                //xu ly xml 4
                Xml4Processor xml4Processor = new Xml4Processor();
                List<Xml4ADO> xml4 = xml4Processor.GenerateXml4ADO(Data);
                //xu ly xml 5
                Xml5Processor xml5Processor = new Xml5Processor();
                List<Xml5ADO> xml5 = xml5Processor.GenerateXml5ADO(Data);
                //Xu ly Xml 1
                Xml1Processor xml1Procssor = new Xml1Processor();
                Xml1ADO xml1 = xml1Procssor.GenerateXml1Data(Data, xml2, xml3);
                foreach (var item in xml2)
                {
                    Mrs00826RDO rdo = new Mrs00826RDO();
                    rdo.Xml1ADO = xml1;
                    rdo.Xml2ADO = item;
                    rdo.Xml3ADO = new Xml3ADO();
                    rdo.Xml4ADO = new Xml4ADO();
                    rdo.Xml5ADO = new Xml5ADO();
                    listRdo.Add(rdo);

                }
                foreach (var item in xml3)
                {
                    Mrs00826RDO rdo = new Mrs00826RDO();
                    rdo.Xml1ADO = xml1;
                    rdo.Xml2ADO = new Xml2ADO();
                    rdo.Xml3ADO = item;
                    rdo.Xml4ADO = new Xml4ADO();
                    rdo.Xml5ADO = new Xml5ADO();
                    listRdo.Add(rdo);
                }
                foreach (var item in xml4)
                {
                    Mrs00826RDO rdo = new Mrs00826RDO();
                    rdo.Xml1ADO = xml1;
                    rdo.Xml2ADO = new Xml2ADO();
                    rdo.Xml3ADO = new Xml3ADO();
                    rdo.Xml4ADO = item;
                    rdo.Xml5ADO = new Xml5ADO();
                    listRdo.Add(rdo);
                }
                foreach (var item in xml5)
                {
                    Mrs00826RDO rdo = new Mrs00826RDO();
                    rdo.Xml1ADO = xml1;
                    rdo.Xml2ADO = new Xml2ADO();
                    rdo.Xml3ADO = new Xml3ADO();
                    rdo.Xml4ADO = new Xml4ADO();
                    rdo.Xml5ADO = item;
                    listRdo.Add(rdo);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
        private void CreateThreadGetData(List<V_HIS_TREATMENT_1> listSelection)
        {
            System.Threading.Thread HeinApproval = new System.Threading.Thread(ThreadGetHeinApproval);
            System.Threading.Thread SereServ2 = new System.Threading.Thread(ThreadGetSereServ2);
            System.Threading.Thread Treatment3 = new System.Threading.Thread(ThreadGetTreatment3);
            System.Threading.Thread Dhst_Tracking = new System.Threading.Thread(ThreadGetDhst_Tracking);
            System.Threading.Thread SereServTein_PTTT = new System.Threading.Thread(ThreadGetSereServTein_PTTT);
            try
            {
                HeinApproval.Start(listSelection);
                SereServ2.Start(listSelection);
                Treatment3.Start(listSelection);
                Dhst_Tracking.Start(listSelection);
                SereServTein_PTTT.Start(listSelection);

                HeinApproval.Join();
                SereServ2.Join();
                Treatment3.Join();
                Dhst_Tracking.Join();
                SereServTein_PTTT.Join();
            }
            catch (Exception ex)
            {
                HeinApproval.Abort();
                SereServ2.Abort();
                Treatment3.Abort();
                Dhst_Tracking.Abort();
                SereServTein_PTTT.Abort();
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ThreadGetSereServTein_PTTT(object obj)
        {
            try
            {
                if (obj == null) return;
                List<V_HIS_TREATMENT_1> listSelection = (List<V_HIS_TREATMENT_1>)obj;

                var skip = 0;
                while (listSelection.Count - skip > 0)
                {
                    var limit = listSelection.Skip(skip).Take(ManagerConstant.MAX_REQUEST_LENGTH_PARAM).ToList();
                    skip = skip + ManagerConstant.MAX_REQUEST_LENGTH_PARAM;

                    CommonParam param = new CommonParam();

                    HisSereServTeinViewFilterQuery ssTeinFilter = new HisSereServTeinViewFilterQuery();
                    ssTeinFilter.TDL_TREATMENT_IDs = limit.Select(s => s.ID).ToList();
                    var resulTein = new HisSereServTeinManager().GetView( ssTeinFilter);
                    if (resulTein != null && resulTein.Count > 0)
                    {
                        hisSereServTeins.AddRange(resulTein);
                    }

                    HisSereServPtttViewFilterQuery ssPtttFilter = new HisSereServPtttViewFilterQuery();
                    ssPtttFilter.TDL_TREATMENT_IDs = limit.Select(s => s.ID).ToList();
                    var resultPttt = new HisSereServPtttManager().GetView(ssPtttFilter);
                    if (resultPttt != null && resultPttt.Count > 0)
                    {
                        hisSereServPttts.AddRange(resultPttt);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ThreadGetDhst_Tracking(object obj)
        {
            try
            {
                if (obj == null) return;
                List<V_HIS_TREATMENT_1> listSelection = (List<V_HIS_TREATMENT_1>)obj;

                var skip = 0;
                while (listSelection.Count - skip > 0)
                {
                    var limit = listSelection.Skip(skip).Take(ManagerConstant.MAX_REQUEST_LENGTH_PARAM).ToList();
                    skip = skip + ManagerConstant.MAX_REQUEST_LENGTH_PARAM;

                    CommonParam param = new CommonParam();

                    HisDhstFilterQuery dhstFilter = new HisDhstFilterQuery();
                    dhstFilter.TREATMENT_IDs = limit.Select(o => o.ID).ToList();
                    var resultDhst = new HisDhstManager().Get(dhstFilter);
                    if (resultDhst != null && resultDhst.Count > 0)
                    {
                        listDhst.AddRange(resultDhst);
                    }

                    HisTrackingFilterQuery trackingFilter = new HisTrackingFilterQuery();
                    trackingFilter.TREATMENT_IDs = limit.Select(o => o.ID).ToList();
                    var resultTracking = new HisTrackingManager().Get(trackingFilter);
                    if (resultTracking != null && resultTracking.Count > 0)
                    {
                        hisTrackings.AddRange(resultTracking);
                    }

                    HisDebateFilterQuery debateFilter = new HisDebateFilterQuery();
                    debateFilter.TREATMENT_IDs = limit.Select(o => o.ID).ToList();
                    var resultDebate = new HisDebateManager().Get( debateFilter);
                    if (resultDebate != null && resultDebate.Count > 0)
                    {
                        ListDebates.AddRange(resultDebate);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ThreadGetTreatment3(object obj)
        {
            try
            {
                if (obj == null) return;
                List<V_HIS_TREATMENT_1> listSelection = (List<V_HIS_TREATMENT_1>)obj;

                var skip = 0;
                while (listSelection.Count - skip > 0)
                {
                    var limit = listSelection.Skip(skip).Take(ManagerConstant.MAX_REQUEST_LENGTH_PARAM).ToList();
                    skip = skip + ManagerConstant.MAX_REQUEST_LENGTH_PARAM;

                    CommonParam param = new CommonParam();
                    HisTreatmentView3FilterQuery treatmentFilter = new HisTreatmentView3FilterQuery();
                    treatmentFilter.IDs = limit.Select(o => o.ID).ToList();
                    var resultTreatment = new HisTreatmentManager().GetView3( treatmentFilter);
                    if (resultTreatment != null && resultTreatment.Count > 0)
                    {
                        hisTreatments.AddRange(resultTreatment);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ThreadGetSereServ2(object obj)
        {
            try
            {
                if (obj == null) return;
                List<V_HIS_TREATMENT_1> listSelection = (List<V_HIS_TREATMENT_1>)obj;

                var skip = 0;
                while (listSelection.Count - skip > 0)
                {
                    var limit = listSelection.Skip(skip).Take(ManagerConstant.MAX_REQUEST_LENGTH_PARAM).ToList();
                    skip = skip + ManagerConstant.MAX_REQUEST_LENGTH_PARAM;

                    CommonParam param = new CommonParam();
                    HisSereServView2FilterQuery ssFilter = new HisSereServView2FilterQuery();
                    ssFilter.TREATMENT_IDs = limit.Select(o => o.ID).ToList();
                    var resultSS = new HisSereServManager().GetView2(ssFilter);
                    if (resultSS != null && resultSS.Count > 0)
                    {
                        ListSereServ.AddRange(resultSS);

                        var ekipIds = resultSS.Select(o => o.EKIP_ID ?? 0).Distinct().ToList();
                        if (ekipIds != null && ekipIds.Count > 1)//null sẽ có 1 id bằng 0
                        {
                            HisEkipUserFilterQuery ekipFilter = new HisEkipUserFilterQuery();
                            ekipFilter.EKIP_IDs = ekipIds;
                            var resultEkip = new HisEkipUserManager().Get(ekipFilter);
                            if (resultEkip != null && resultEkip.Count > 0)
                            {
                                ListEkipUser.AddRange(resultEkip);
                            }
                        }
                    }

                    HisBedLogViewFilterQuery bedFilter = new HisBedLogViewFilterQuery();
                    bedFilter.TREATMENT_IDs = limit.Select(o => o.ID).ToList();
                    var resultBed = new HisBedLogManager().GetView(bedFilter);
                    if (resultBed != null && resultBed.Count > 0)
                    {
                        ListBedlog.AddRange(resultBed);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ThreadGetHeinApproval(object obj)
        {
            try
            {
                if (obj == null) return;
                List<V_HIS_TREATMENT_1> listSelection = (List<V_HIS_TREATMENT_1>)obj;

                var skip = 0;
                while (listSelection.Count - skip > 0)
                {
                    var limit = listSelection.Skip(skip).Take(ManagerConstant.MAX_REQUEST_LENGTH_PARAM).ToList();
                    skip = skip + ManagerConstant.MAX_REQUEST_LENGTH_PARAM;

                    HisHeinApprovalViewFilterQuery approvalFilter = new HisHeinApprovalViewFilterQuery();
                    approvalFilter.TREATMENT_IDs = limit.Select(s => s.ID).ToList();
                    var resultHeinApproval = new HisHeinApprovalManager().GetView(approvalFilter);
                    if (resultHeinApproval != null && resultHeinApproval.Count > 0)
                    {
                        listHeinApproval.AddRange(resultHeinApproval);
                    }
                    var treatmentIdNotApproval = limit.Where(o => resultHeinApproval == null || !resultHeinApproval.Exists(p => p.TREATMENT_ID == o.ID)).ToList();
                    HisPatientTypeAlterViewFilterQuery patientTypeAlterFilter = new HisPatientTypeAlterViewFilterQuery();
                    patientTypeAlterFilter.TREATMENT_IDs = treatmentIdNotApproval.Select(s => s.ID).ToList();
                    var resultPatientTypeAlter = new HisPatientTypeAlterManager().GetView(patientTypeAlterFilter);
                    if (resultPatientTypeAlter != null && resultPatientTypeAlter.Count > 0)
                    {
                        listPatientTypeAlter.AddRange(resultPatientTypeAlter);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
        protected override void SetTag(Dictionary<string, object> dicSingleTag, Inventec.Common.FlexCellExport.ProcessObjectTag objectTag, Inventec.Common.FlexCellExport.Store store)
        {
            dicSingleTag.Add("TIME_FROM", filter.TIME_FROM);
            dicSingleTag.Add("TIME_TO", filter.TIME_TO);
            objectTag.AddObjectData(store, "Report", ListRdo);
        }
    }
}
