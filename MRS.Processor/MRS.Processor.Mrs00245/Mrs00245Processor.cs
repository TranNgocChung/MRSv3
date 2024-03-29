﻿using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisBranch;
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

using MOS.MANAGER.HisInvoice;
using MOS.MANAGER.HisInvoiceDetail;
using MOS.MANAGER.HisHeinApproval;
using MRS.MANAGER.Config;

namespace MRS.Processor.Mrs00245
{
    public class Mrs00245Processor : AbstractProcessor
    {
        Mrs00245Filter castFilter = null;

        List<Mrs00245RDO> ListRdoA = new List<Mrs00245RDO>();
        List<Mrs00245RDO> ListRdoB = new List<Mrs00245RDO>();
        List<Mrs00245RDO> ListRdoC = new List<Mrs00245RDO>();

        Dictionary<short, Mrs00245RDO> dicRdoA = new Dictionary<short, Mrs00245RDO>();
        Dictionary<short, Mrs00245RDO> dicRdoB = new Dictionary<short, Mrs00245RDO>();
        Dictionary<short, Mrs00245RDO> dicRdoC = new Dictionary<short, Mrs00245RDO>();

        List<Mrs00245RDO> ListTotalRdoA = new List<Mrs00245RDO>();
        List<Mrs00245RDO> ListTotalRdoB = new List<Mrs00245RDO>();
        List<Mrs00245RDO> ListTotalRdoC = new List<Mrs00245RDO>();

        List<Mrs00245RDO> ListTotalAllRdoA = new List<Mrs00245RDO>();
        List<Mrs00245RDO> ListTotalAllRdoB = new List<Mrs00245RDO>();
        List<Mrs00245RDO> ListTotalAllRdoC = new List<Mrs00245RDO>();
        const short DungTuyen = 1;
        const short TraiTuyen = 0;
        decimal TotalAmount = 0;

        List<V_HIS_HEIN_APPROVAL> ListHeinApproval = new List<V_HIS_HEIN_APPROVAL>();

        HIS_BRANCH _Branch = null;

        public Mrs00245Processor(CommonParam param, string reportTypeCode)
            : base(param, reportTypeCode)
        {
        }

        public override Type FilterType()
        {
            return typeof(Mrs00245Filter);
        }

        string NumDigits = NumDigitsOptionCFG.NUM_DIGITS_OPTION_VALUE;
		protected override bool GetData()
		{
            bool result = true;
            CommonParam paramGet = new CommonParam();
            try
            {
                castFilter = ((Mrs00245Filter)this.reportFilter);
                //this._Branch = MRS.MANAGER.Config.HisBranchCFG.HisBranchs.FirstOrDefault(o => o.ID == this.castFilter.BRANCH_ID);
                //if (this._Branch == null)
                //    throw new NullReferenceException("Nguoi dung truyen len branchId khong chin xac");
                Inventec.Common.Logging.LogSystem.Debug("Bat dau lay du lieu V_HIS_HEIN_APPROVAL, Mrs00245, filter: " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => castFilter), castFilter));
                HisHeinApprovalViewFilterQuery approvalFilter = new HisHeinApprovalViewFilterQuery();
                approvalFilter.EXECUTE_TIME_FROM = castFilter.TIME_FROM;
                approvalFilter.EXECUTE_TIME_TO = castFilter.TIME_TO;
                approvalFilter.BRANCH_ID = castFilter.BRANCH_ID;
                approvalFilter.BRANCH_IDs = castFilter.BRANCH_IDs;
                approvalFilter.ORDER_DIRECTION = "ASC";
                approvalFilter.ORDER_FIELD = "EXECUTE_TIME";
                ListHeinApproval = new HisHeinApprovalManager(paramGet).GetView(approvalFilter);
                if (paramGet.HasException)
                {
                    throw new DataMisalignedException("Co loi xay ra trong qua trinh lay du lieu V_HIS_HEIN_APPROVAL, Mrs00245");
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        protected override bool ProcessData()
        {
            bool result = true;
            try
            {
                if (IsNotNullOrEmpty(ListHeinApproval))
                {
                    var treatmentIds = ListHeinApproval.Select(o => o.TREATMENT_ID).Distinct().ToList();
                    CommonParam paramGet = new CommonParam();
                    int start = 0;
                    int count = treatmentIds.Count;
                    while (count > 0)
                    {
                        int limit = (count <= ManagerConstant.MAX_REQUEST_LENGTH_PARAM) ? count : ManagerConstant.MAX_REQUEST_LENGTH_PARAM;
                        var tmIds = treatmentIds.Skip(start).Take(limit).ToList();
                        var hisHeinApprovals = ListHeinApproval.Where(o => tmIds.Contains(o.TREATMENT_ID)).ToList();

                        HisSereServView3FilterQuery ssFilter = new HisSereServView3FilterQuery();
                        ssFilter.HEIN_APPROVAL_IDs = hisHeinApprovals.Select(s => s.ID).ToList();
                        List<V_HIS_SERE_SERV_3> ListSereServ = new MOS.MANAGER.HisSereServ.HisSereServManager(paramGet).GetView3(ssFilter);
                        if (ListSereServ != null)
                        {
                            ListSereServ = ListSereServ.Where(o => o.VIR_TOTAL_HEIN_PRICE > 0).ToList();
                        }

                        HisTreatmentViewFilterQuery treatmentFilter = new HisTreatmentViewFilterQuery();
                        treatmentFilter.IDs = hisHeinApprovals.Select(s => s.TREATMENT_ID).ToList().Distinct().ToList();
                        List<V_HIS_TREATMENT> ListTreatment = new MOS.MANAGER.HisTreatment.HisTreatmentManager(paramGet).GetView(treatmentFilter);
                        if ((castFilter.DEPARTMENT_ID ?? 0) != 0)
                        {
                            ListTreatment = ListTreatment.Where(o => o.END_DEPARTMENT_ID == castFilter.DEPARTMENT_ID).ToList();
                        }
                        ListSereServ = ListSereServ.Where(o => ListTreatment.Exists(p => p.ID == o.TDL_TREATMENT_ID)).ToList();

                        if (paramGet.HasException)
                        {
                            throw new DataMisalignedException("Co exception xay ra tai DAOGET trong qua trinh tong hop du lieu Mrs00245.");
                        }
                        GeneralDataByListHeinApproval(hisHeinApprovals, ListSereServ, ListTreatment);
                        start += ManagerConstant.MAX_REQUEST_LENGTH_PARAM;
                        count -= ManagerConstant.MAX_REQUEST_LENGTH_PARAM;
                    }
                    ListRdoA = dicRdoA.Select(s => s.Value).OrderBy(o => o.DUNG_TRAI).ToList();
                    ListRdoB = dicRdoB.Select(s => s.Value).OrderBy(o => o.DUNG_TRAI).ToList();
                    ListRdoC = dicRdoC.Select(s => s.Value).OrderBy(o => o.DUNG_TRAI).ToList();

                    ListRdoA = CheckList(ListRdoA);
                    ListRdoB = CheckList(ListRdoB);
                    ListRdoC = CheckList(ListRdoC);
                }
            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        private void GeneralDataByListHeinApproval(List<V_HIS_HEIN_APPROVAL> hisHeinApprovals, List<V_HIS_SERE_SERV_3> ListSereServ, List<V_HIS_TREATMENT> ListTreatment)
        {
            try
            {
                if (IsNotNullOrEmpty(hisHeinApprovals))
                {
                    Dictionary<long, V_HIS_TREATMENT> dicTreatment = new Dictionary<long, V_HIS_TREATMENT>();
                    Dictionary<long, List<V_HIS_SERE_SERV_3>> dicSereServ = new Dictionary<long, List<V_HIS_SERE_SERV_3>>();

                    if (IsNotNullOrEmpty(ListTreatment))
                    {
                        foreach (var treatment in ListTreatment)
                        {
                            dicTreatment[treatment.ID] = treatment;
                        }
                    }

                    if (IsNotNullOrEmpty(ListSereServ))
                    {
                        foreach (var sere in ListSereServ)
                        {
                            if (sere.IS_EXPEND != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE && sere.AMOUNT > 0 && sere.HEIN_APPROVAL_ID.HasValue && sere.IS_NO_EXECUTE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE && sere.PRICE != 0)
                            {
                                if (castFilter.IS_MERGE_TREATMENT == true)
                                {
                                    if (!dicSereServ.ContainsKey(sere.TDL_TREATMENT_ID ?? 0))
                                        dicSereServ[sere.TDL_TREATMENT_ID ?? 0] = new List<V_HIS_SERE_SERV_3>();
                                    dicSereServ[sere.TDL_TREATMENT_ID ?? 0].Add(sere);
                                }
                                else
                                {
                                    if (!dicSereServ.ContainsKey(sere.HEIN_APPROVAL_ID.Value))
                                        dicSereServ[sere.HEIN_APPROVAL_ID.Value] = new List<V_HIS_SERE_SERV_3>();
                                    dicSereServ[sere.HEIN_APPROVAL_ID.Value].Add(sere);
                                }
                            }
                        }
                    }
                    if (castFilter.IS_MERGE_TREATMENT == true)
                    {
                        hisHeinApprovals = hisHeinApprovals.GroupBy(o => o.TREATMENT_ID).Select(p => p.First()).ToList();
                    }
                    if (castFilter.INPUT_DATA_ID_ROUTE_TYPE!=null)
                    {
                        hisHeinApprovals = hisHeinApprovals.Where(o => o.RIGHT_ROUTE_CODE == MOS.LibraryHein.Bhyt.HeinRightRoute.HeinRightRouteCode.TRUE && castFilter.INPUT_DATA_ID_ROUTE_TYPE == 1 || o.RIGHT_ROUTE_CODE == MOS.LibraryHein.Bhyt.HeinRightRoute.HeinRightRouteCode.FALSE && castFilter.INPUT_DATA_ID_ROUTE_TYPE == 2).ToList();
                    }
                    foreach (var heinApproval in hisHeinApprovals)
                    {
                        this._Branch = HisBranchCFG.HisBranchs.FirstOrDefault(o => o.ID == heinApproval.BRANCH_ID);
                        if (_Branch == null) continue;
                        var treatment = ListTreatment.FirstOrDefault(p => p.ID == heinApproval.TREATMENT_ID) ?? new V_HIS_TREATMENT();
                        if (heinApproval.HEIN_TREATMENT_TYPE_CODE == MOS.LibraryHein.Bhyt.HeinTreatmentType.HeinTreatmentTypeCode.EXAM && CheckHeinCardNumberType(heinApproval.HEIN_CARD_NUMBER))
                        {
                            Mrs00245RDO rdo = null;
                            short isDungTrai;
                            if (heinApproval.RIGHT_ROUTE_CODE == MOS.LibraryHein.Bhyt.HeinRightRoute.HeinRightRouteCode.TRUE)
                            {
                                isDungTrai = DungTuyen;
                            }
                            else
                            {
                                isDungTrai = TraiTuyen;
                            }

                            if (this._Branch.ACCEPT_HEIN_MEDI_ORG_CODE.Contains(heinApproval.HEIN_MEDI_ORG_CODE) && checkBhytProvinceCode(heinApproval.HEIN_CARD_NUMBER))
                            {
                                
                                if (!dicRdoA.ContainsKey(isDungTrai))
                                {
                                    
                                    rdo = new Mrs00245RDO(heinApproval);
                                    dicRdoA[isDungTrai] = rdo;
                                }
                                else
                                {
                                    rdo = dicRdoA[isDungTrai];
                                    
                                }
                            }
                            else if (checkBhytProvinceCode(heinApproval.HEIN_CARD_NUMBER))
                            {
                                
                                if (!dicRdoB.ContainsKey(isDungTrai))
                                {
                                    
                                    rdo = new Mrs00245RDO(heinApproval);
                                    dicRdoB[isDungTrai] = rdo;
                                }
                                else
                                {
                                    rdo = dicRdoB[isDungTrai];
                                    
                                }
                            }
                            else
                            {
                                
                                if (!dicRdoC.ContainsKey(isDungTrai))
                                {
                                    
                                    rdo = new Mrs00245RDO(heinApproval);
                                    dicRdoC[isDungTrai] = rdo;
                                }
                                else
                                {
                                    rdo = dicRdoC[isDungTrai];
                                    
                                }
                            }

                            //ProcessTreatmentTypeAndPaitentType(rdo, treatment);
                            rdo.HEIN_TREATMENT_TYPE_CODE = heinApproval.HEIN_TREATMENT_TYPE_CODE; //== MOS.LibraryHein.Bhyt.HeinTreatmentType.HeinTreatmentTypeCode.EXAM && 

                            if (castFilter.IS_MERGE_TREATMENT == true)
                            {
                                if (IsNotNull(rdo) && dicSereServ.ContainsKey(heinApproval.TREATMENT_ID) && dicTreatment.ContainsKey(heinApproval.TREATMENT_ID))
                                {
                                    if (dicSereServ.ContainsKey(heinApproval.TREATMENT_ID))
                                    {
                                        ProcessTotalPrice(rdo, dicSereServ[heinApproval.TREATMENT_ID], heinApproval, dicTreatment[heinApproval.TREATMENT_ID]);
                                    }
                                }
                            }
                            else
                            {
                                if (IsNotNull(rdo) && dicSereServ.ContainsKey(heinApproval.ID) && dicTreatment.ContainsKey(heinApproval.TREATMENT_ID))
                                {
                                    if (dicSereServ.ContainsKey(heinApproval.ID))
                                    {
                                        ProcessTotalPrice(rdo, dicSereServ[heinApproval.ID], heinApproval, dicTreatment[heinApproval.TREATMENT_ID]);
                                    }
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ProcessTreatmentTypeAndPaitentType(Mrs00245RDO rdo, V_HIS_TREATMENT treatment)
        {
            if (treatment != null)
            {
                if (treatment.TDL_TREATMENT_TYPE_ID != null)
                {
                    LogSystem.Info("treatment_type:" + (treatment.TDL_TREATMENT_TYPE_ID ?? 0).ToString());
                    if (treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU || treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM)
                    {
                        rdo.TREATMENT_TYPE_CODE = "KH_NGOAITRU";
                        rdo.TREATMENT_TYPE_NAME = "KHÁM CHỮA BỆNH, NGOẠI TRÚ";
                    }
                    if (treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU)
                    {
                        rdo.TREATMENT_TYPE_CODE = "DTNOITRU";
                        rdo.TREATMENT_TYPE_NAME = "ĐIỀU TRỊ NỘI TRÚ";
                    }
                    if (treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU &&
                        treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU &&
                        treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM)
                    {
                        rdo.TREATMENT_TYPE_CODE = "DT_KHAC";
                        rdo.TREATMENT_TYPE_NAME = "DIỆN ĐIỀU TRỊ KHÁC";
                    }
                }
                else
                {
                    rdo.TREATMENT_TYPE_CODE = "NO_TREATMENT_TYPE";
                    rdo.TREATMENT_TYPE_NAME = "KHÔNG CÓ DIỆN ĐIỀU TRỊ";
                }

                if (!string.IsNullOrEmpty(treatment.TDL_HEIN_CARD_NUMBER))
                {
                    if (treatment.TDL_HEIN_CARD_NUMBER.Substring(0, 2) == "CA" || treatment.TDL_HEIN_CARD_NUMBER.Substring(0, 2) == "AN" || treatment.TDL_HEIN_CARD_NUMBER.Substring(0, 2) == "CY")
                    {
                        rdo.BHYT_TYPE = "DT70";
                        rdo.BHYT_TYPE_NAME = "ĐỐI TƯỢNG THEO NGHỊ ĐỊNH 70";
                    }
                    else
                    {
                        rdo.BHYT_TYPE = "DT146";
                        rdo.BHYT_TYPE_NAME = "ĐỐI TƯỢNG THEO NGHỊ ĐỊNH 146";
                    }
                }
                rdo.TREATMENT_DAY_COUNT = treatment.TREATMENT_DAY_COUNT ?? 0;
            }
        }

        private void ProcessTotalPrice(Mrs00245RDO rdo, List<V_HIS_SERE_SERV_3> hisSereServs, V_HIS_HEIN_APPROVAL heinApproval, V_HIS_TREATMENT treatment)
        {
            try
            {
                if (IsNotNullOrEmpty(hisSereServs))
                {
                    decimal totalHeinPrice = 0;
                    foreach (var sereServ in hisSereServs)
                    {
                        if (!sereServ.VIR_TOTAL_HEIN_PRICE.HasValue || sereServ.VIR_TOTAL_HEIN_PRICE.Value <= 0)
                            continue;
                        var TotalPriceTreatment = Mrs.Bhyt.PayRateAndTotalPrice.Caculator.TotalPrice(NumDigits,sereServ, HisBranchCFG.HisBranchs.FirstOrDefault(o => o.ID == heinApproval.BRANCH_ID) ?? new HIS_BRANCH());
                        if (sereServ.TDL_HEIN_SERVICE_TYPE_ID != null)
                        {
                            if (sereServ.TDL_HEIN_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__XN)
                            {
                                rdo.TEST_PRICE += TotalPriceTreatment;
                            }
                            else if (sereServ.TDL_HEIN_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__CDHA || sereServ.TDL_HEIN_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__TDCN)
                            {
                                rdo.DIIM_PRICE += TotalPriceTreatment;
                            }
                            else if (sereServ.TDL_HEIN_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__TH_TDM || sereServ.TDL_HEIN_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__TH_NDM)
                            {
                                rdo.MEDICINE_PRICE += TotalPriceTreatment;
                            }
                            else if (sereServ.TDL_HEIN_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__MAU)
                            {
                                rdo.BLOOD_PRICE += TotalPriceTreatment;
                            }
                            else if (sereServ.TDL_HEIN_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__PTTT)
                            {
                                rdo.SURGMISU_PRICE += TotalPriceTreatment;
                            }
                            else if (sereServ.TDL_HEIN_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__VT_TDM || sereServ.TDL_HEIN_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__VT_NDM)
                            {
                                rdo.MATERIAL_PRICE += TotalPriceTreatment;
                            }
                            else if (sereServ.TDL_HEIN_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__VT_TL || sereServ.TDL_HEIN_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__VT_TT)
                            {
                                rdo.MATERIAL_PRICE_RATIO += TotalPriceTreatment;
                            }
                            else if (sereServ.TDL_HEIN_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__TH_TL || sereServ.TDL_HEIN_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__TH_UT)
                            {
                                rdo.MEDICINE_PRICE_RATIO += TotalPriceTreatment;
                            }
                            else if (sereServ.TDL_HEIN_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__KH)
                            {
                                rdo.EXAM_PRICE += TotalPriceTreatment;
                            }
                            else if (sereServ.TDL_HEIN_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__GI_NT || sereServ.TDL_HEIN_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__GI_NGT || sereServ.TDL_HEIN_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__GI_BN || sereServ.TDL_HEIN_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__GI_L)
                            {
                                rdo.BED_PRICE += TotalPriceTreatment;
                            }
                            else if (sereServ.TDL_HEIN_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__DVKTC)
                            {
                                rdo.SERVICE_PRICE_RATIO += TotalPriceTreatment;
                            }
                            else if (sereServ.TDL_HEIN_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__VC)
                            {
                                rdo.TRAN_PRICE += TotalPriceTreatment;
                            }
                            else if (sereServ.TDL_HEIN_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__TT)
                            {
                                rdo.TT_PRICE += TotalPriceTreatment;
                            }
                            else
                            {
                                continue;
                            }
                            rdo.TOTAL_PRICE += TotalPriceTreatment;
                            rdo.TOTAL_PATIENT_PRICE += TotalPriceTreatment - (sereServ.VIR_TOTAL_HEIN_PRICE ?? 0);
                            totalHeinPrice += sereServ.VIR_TOTAL_HEIN_PRICE ?? 0;
                            rdo.TOTAL_OTHER_SOURCE_PRICE += (sereServ.OTHER_SOURCE_PRICE ?? 0) * sereServ.AMOUNT;
                            rdo.TOTAL_PATIENT_PRICE_BHYT += sereServ.VIR_PATIENT_PRICE_BHYT ?? 0;
                            rdo.TOTAL_PATIENT_PRICE_SELF += TotalPriceTreatment - totalHeinPrice - (sereServ.VIR_PATIENT_PRICE_BHYT ?? 0) - ((sereServ.OTHER_SOURCE_PRICE ?? 0) * sereServ.AMOUNT);
                        }
                    }
                    rdo.TOTAL_COUNT++;
                    if (checkBhytNsd(heinApproval, treatment))
                    {
                        rdo.TOTAL_HEIN_PRICE_NDS += totalHeinPrice;
                    }
                    else
                    {
                        rdo.TOTAL_HEIN_PRICE += totalHeinPrice;
                    }
                    TotalAmount += totalHeinPrice;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private List<Mrs00245RDO> CheckList(List<Mrs00245RDO> listRdo)
        {
            List<Mrs00245RDO> result = new List<Mrs00245RDO>();
            try
            {
                if (IsNotNullOrEmpty(listRdo))
                {
                    foreach (var rdo in listRdo)
                    {
                        //khong co gia thi bo qua
                        if (!CheckPrice(rdo)) continue;

                        result.Add(rdo);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = new List<Mrs00245RDO>();
            }
            return result;
        }

        private bool CheckPrice(Mrs00245RDO rdo)
        {
            bool result = false;
            try
            {
                result = rdo.BED_PRICE > 0 || rdo.BLOOD_PRICE > 0 || rdo.DIIM_PRICE > 0 || rdo.EXAM_PRICE > 0 ||
                    rdo.MATERIAL_PRICE > 0 || rdo.MEDICINE_PRICE > 0 || rdo.SURGMISU_PRICE > 0 || rdo.TEST_PRICE > 0 ||
                    rdo.TOTAL_HEIN_PRICE > 0 || rdo.TOTAL_HEIN_PRICE_NDS > 0 || rdo.TOTAL_PATIENT_PRICE > 0 || rdo.TOTAL_PRICE > 0 || rdo.TRAN_PRICE > 0 || rdo.TT_PRICE > 0;
            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        private bool CheckHeinCardNumberType(string HeinCardNumber)
        {
            bool result = false;
            try
            {
                if (!String.IsNullOrEmpty(HeinCardNumber))
                {
                    result = true;
                    if (IsNotNullOrEmpty(MANAGER.Config.HeinCardNumberTypeCFG.HeinCardNumber__HeinType__All))
                    {
                        foreach (var type in MANAGER.Config.HeinCardNumberTypeCFG.HeinCardNumber__HeinType__All)
                        {
                            if (HeinCardNumber.StartsWith(type))
                            {
                                result = false;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private bool checkBhytProvinceCode(string HeinNumber)
        {
            bool result = false;
            try
            {
                if (!string.IsNullOrEmpty(HeinNumber) && HeinNumber.Length == 15)
                {
                    string provinceCode = HeinNumber.Substring(3, 2);
                    if (this._Branch.HEIN_PROVINCE_CODE.Equals(provinceCode))
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }


        private bool checkBhytNsd(V_HIS_HEIN_APPROVAL heinApproval, V_HIS_TREATMENT treatment)
        {
            bool result = false;
            try
            {
                if (MRS.MANAGER.Config.ReportBhytNdsIcdCodeCFG.ReportBhytNdsIcdCode__Other.Contains(treatment.ICD_CODE))
                {
                    result = true;
                }
                else if (!String.IsNullOrEmpty(treatment.ICD_CODE))
                {
                    if (heinApproval.HEIN_CARD_NUMBER.Substring(0, 2).Equals("TE") && MRS.MANAGER.Config.ReportBhytNdsIcdCodeCFG.ReportBhytNdsIcdCode__Te.Contains(treatment.ICD_CODE.Substring(0, 3)))
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        private void ProcessSumTotal()
        {
            try
            {
                Mrs00245RDO rdoA = new Mrs00245RDO();
                foreach (var item in ListRdoA)
                {
                    rdoA.BED_PRICE += item.BED_PRICE;
                    rdoA.BLOOD_PRICE += item.BLOOD_PRICE;
                    rdoA.DIIM_PRICE += item.DIIM_PRICE;
                    rdoA.EXAM_PRICE += item.EXAM_PRICE;
                    rdoA.MATERIAL_PRICE += item.MATERIAL_PRICE;
                    rdoA.MATERIAL_PRICE_RATIO += item.MATERIAL_PRICE_RATIO;
                    rdoA.MEDICINE_PRICE += item.MEDICINE_PRICE;
                    rdoA.MEDICINE_PRICE_RATIO += item.MEDICINE_PRICE_RATIO;
                    rdoA.SERVICE_PRICE_RATIO += item.SERVICE_PRICE_RATIO;
                    rdoA.SURGMISU_PRICE += item.SURGMISU_PRICE;
                    rdoA.TEST_PRICE += item.TEST_PRICE;
                    rdoA.TOTAL_COUNT += item.TOTAL_COUNT;
                    rdoA.TOTAL_HEIN_PRICE += item.TOTAL_HEIN_PRICE;
                    rdoA.TOTAL_HEIN_PRICE_NDS += item.TOTAL_HEIN_PRICE_NDS;
                    rdoA.TOTAL_PATIENT_PRICE += item.TOTAL_PATIENT_PRICE;
                    rdoA.TOTAL_PRICE += item.TOTAL_PRICE;
                    rdoA.TRAN_PRICE += item.TRAN_PRICE;
                    rdoA.TT_PRICE += item.TT_PRICE;
                    rdoA.TOTAL_OTHER_SOURCE_PRICE += item.TOTAL_OTHER_SOURCE_PRICE;
                }
                ListTotalRdoA.Add(rdoA);

                Mrs00245RDO rdoB = new Mrs00245RDO();
                foreach (var item in ListRdoB)
                {
                    rdoB.BED_PRICE += item.BED_PRICE;
                    rdoB.BLOOD_PRICE += item.BLOOD_PRICE;
                    rdoB.DIIM_PRICE += item.DIIM_PRICE;
                    rdoB.EXAM_PRICE += item.EXAM_PRICE;
                    rdoB.MATERIAL_PRICE += item.MATERIAL_PRICE;
                    rdoB.MATERIAL_PRICE_RATIO += item.MATERIAL_PRICE_RATIO;
                    rdoB.MEDICINE_PRICE += item.MEDICINE_PRICE;
                    rdoB.MEDICINE_PRICE_RATIO += item.MEDICINE_PRICE_RATIO;
                    rdoB.SERVICE_PRICE_RATIO += item.SERVICE_PRICE_RATIO;
                    rdoB.SURGMISU_PRICE += item.SURGMISU_PRICE;
                    rdoB.TEST_PRICE += item.TEST_PRICE;
                    rdoB.TOTAL_COUNT += item.TOTAL_COUNT;
                    rdoB.TOTAL_HEIN_PRICE += item.TOTAL_HEIN_PRICE;
                    rdoB.TOTAL_HEIN_PRICE_NDS += item.TOTAL_HEIN_PRICE_NDS;
                    rdoB.TOTAL_PATIENT_PRICE += item.TOTAL_PATIENT_PRICE;
                    rdoB.TOTAL_PRICE += item.TOTAL_PRICE;
                    rdoB.TRAN_PRICE += item.TRAN_PRICE;
                    rdoB.TT_PRICE += item.TT_PRICE;
                    rdoB.TOTAL_OTHER_SOURCE_PRICE += item.TOTAL_OTHER_SOURCE_PRICE;
                }
                ListTotalRdoB.Add(rdoB);

                Mrs00245RDO rdoC = new Mrs00245RDO();
                foreach (var item in ListRdoC)
                {
                    rdoC.BED_PRICE += item.BED_PRICE;
                    rdoC.BLOOD_PRICE += item.BLOOD_PRICE;
                    rdoC.DIIM_PRICE += item.DIIM_PRICE;
                    rdoC.EXAM_PRICE += item.EXAM_PRICE;
                    rdoC.MATERIAL_PRICE += item.MATERIAL_PRICE;
                    rdoC.MATERIAL_PRICE_RATIO += item.MATERIAL_PRICE_RATIO;
                    rdoC.MEDICINE_PRICE += item.MEDICINE_PRICE;
                    rdoC.MEDICINE_PRICE_RATIO += item.MEDICINE_PRICE_RATIO;
                    rdoC.SERVICE_PRICE_RATIO += item.SERVICE_PRICE_RATIO;
                    rdoC.SURGMISU_PRICE += item.SURGMISU_PRICE;
                    rdoC.TEST_PRICE += item.TEST_PRICE;
                    rdoC.TOTAL_COUNT += item.TOTAL_COUNT;
                    rdoC.TOTAL_HEIN_PRICE += item.TOTAL_HEIN_PRICE;
                    rdoC.TOTAL_HEIN_PRICE_NDS += item.TOTAL_HEIN_PRICE_NDS;
                    rdoC.TOTAL_PATIENT_PRICE += item.TOTAL_PATIENT_PRICE;
                    rdoC.TOTAL_PRICE += item.TOTAL_PRICE;
                    rdoC.TRAN_PRICE += item.TRAN_PRICE;
                    rdoC.TT_PRICE += item.TT_PRICE;
                    rdoC.TOTAL_OTHER_SOURCE_PRICE += item.TOTAL_OTHER_SOURCE_PRICE;
                }
                ListTotalRdoC.Add(rdoC);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ProcessSumTotalGroupByTreatmentType()
        {
            try
            {
                if (ListRdoA != null)
                {
                    Mrs00245RDO rdo = new Mrs00245RDO();
                    var listRdo = ListRdoA;
                    if (listRdo != null)
                    {
                        foreach (var item in listRdo)
                        {

                            rdo.TREATMENT_TYPE_CODE = item.TREATMENT_TYPE_CODE;
                            rdo.TREATMENT_TYPE_NAME = item.TREATMENT_TYPE_NAME;
                            rdo.BHYT_TYPE_NAME = item.BHYT_TYPE_NAME;
                            rdo.TYPE = "A";
                            
                            rdo.BHYT_TYPE = item.BHYT_TYPE;

                            rdo.BED_PRICE += item.BED_PRICE;
                            rdo.BLOOD_PRICE += item.BLOOD_PRICE;
                            rdo.DIIM_PRICE += item.DIIM_PRICE;
                            rdo.EXAM_PRICE += item.EXAM_PRICE;
                            rdo.MATERIAL_PRICE += item.MATERIAL_PRICE;
                            rdo.MATERIAL_PRICE_RATIO = item.MATERIAL_PRICE_RATIO;
                            rdo.MEDICINE_PRICE += item.MEDICINE_PRICE;
                            rdo.MEDICINE_PRICE_RATIO = item.MEDICINE_PRICE_RATIO;
                            rdo.SERVICE_PRICE_RATIO = item.SERVICE_PRICE_RATIO;
                            rdo.SURGMISU_PRICE += item.SURGMISU_PRICE;
                            rdo.TEST_PRICE += item.TEST_PRICE;
                            rdo.TOTAL_COUNT += item.TOTAL_COUNT;
                            rdo.TOTAL_HEIN_PRICE += item.TOTAL_HEIN_PRICE;
                            rdo.TOTAL_HEIN_PRICE_NDS += item.TOTAL_HEIN_PRICE_NDS;
                            rdo.TOTAL_PATIENT_PRICE += item.TOTAL_PATIENT_PRICE;
                            rdo.TOTAL_PRICE += item.TOTAL_PRICE;
                            rdo.TRAN_PRICE += item.TRAN_PRICE;
                            rdo.TT_PRICE += item.TT_PRICE;
                            rdo.TOTAL_OTHER_SOURCE_PRICE += item.TOTAL_OTHER_SOURCE_PRICE;
                        }
                    }
                    ListTotalAllRdoA.Add(rdo);
                }

                if (ListRdoB != null)
                {
                    Mrs00245RDO rdo = new Mrs00245RDO();
                    var listRdo = ListRdoB;
                    if (listRdo != null)
                    {
                        foreach (var item in listRdo)
                        {
                            rdo.TREATMENT_TYPE_CODE = item.TREATMENT_TYPE_CODE;
                            rdo.TREATMENT_TYPE_NAME = item.TREATMENT_TYPE_NAME;
                            rdo.BHYT_TYPE_NAME = item.BHYT_TYPE_NAME;
                            rdo.TYPE = "B";
                            rdo.BHYT_TYPE = item.BHYT_TYPE;
                            rdo.BED_PRICE += item.BED_PRICE;
                            rdo.BLOOD_PRICE += item.BLOOD_PRICE;
                            rdo.DIIM_PRICE += item.DIIM_PRICE;
                            rdo.EXAM_PRICE += item.EXAM_PRICE;
                            rdo.MATERIAL_PRICE += item.MATERIAL_PRICE;
                            rdo.MATERIAL_PRICE_RATIO = item.MATERIAL_PRICE_RATIO;
                            rdo.MEDICINE_PRICE += item.MEDICINE_PRICE;
                            rdo.MEDICINE_PRICE_RATIO = item.MEDICINE_PRICE_RATIO;
                            rdo.SERVICE_PRICE_RATIO = item.SERVICE_PRICE_RATIO;
                            rdo.SURGMISU_PRICE += item.SURGMISU_PRICE;
                            rdo.TEST_PRICE += item.TEST_PRICE;
                            rdo.TOTAL_COUNT += item.TOTAL_COUNT;
                            rdo.TOTAL_HEIN_PRICE += item.TOTAL_HEIN_PRICE;
                            rdo.TOTAL_HEIN_PRICE_NDS += item.TOTAL_HEIN_PRICE_NDS;
                            rdo.TOTAL_PATIENT_PRICE += item.TOTAL_PATIENT_PRICE;
                            rdo.TOTAL_PRICE += item.TOTAL_PRICE;
                            rdo.TRAN_PRICE += item.TRAN_PRICE;
                            rdo.TT_PRICE += item.TT_PRICE;
                            rdo.TOTAL_OTHER_SOURCE_PRICE += item.TOTAL_OTHER_SOURCE_PRICE;
                        }
                    }
                    ListTotalAllRdoB.Add(rdo);
                }

                if (ListRdoC != null)
                {
                    Mrs00245RDO rdo = new Mrs00245RDO();
                    var listRdo = ListRdoC;
                    if (listRdo != null)
                    {
                        foreach (var item in listRdo)
                        {

                            rdo.TREATMENT_TYPE_CODE = item.TREATMENT_TYPE_CODE;
                            rdo.TREATMENT_TYPE_NAME = item.TREATMENT_TYPE_NAME;
                            rdo.BHYT_TYPE_NAME = item.BHYT_TYPE_NAME;
                            rdo.TYPE = "C";
                            
                            rdo.BHYT_TYPE = item.BHYT_TYPE;

                            rdo.BED_PRICE += item.BED_PRICE;
                            rdo.BLOOD_PRICE += item.BLOOD_PRICE;
                            rdo.DIIM_PRICE += item.DIIM_PRICE;
                            rdo.EXAM_PRICE += item.EXAM_PRICE;
                            rdo.MATERIAL_PRICE += item.MATERIAL_PRICE;
                            rdo.MATERIAL_PRICE_RATIO = item.MATERIAL_PRICE_RATIO;
                            rdo.MEDICINE_PRICE += item.MEDICINE_PRICE;
                            rdo.MEDICINE_PRICE_RATIO = item.MEDICINE_PRICE_RATIO;
                            rdo.SERVICE_PRICE_RATIO = item.SERVICE_PRICE_RATIO;
                            rdo.SURGMISU_PRICE += item.SURGMISU_PRICE;
                            rdo.TEST_PRICE += item.TEST_PRICE;
                            rdo.TOTAL_COUNT += item.TOTAL_COUNT;
                            rdo.TOTAL_HEIN_PRICE += item.TOTAL_HEIN_PRICE;
                            rdo.TOTAL_HEIN_PRICE_NDS += item.TOTAL_HEIN_PRICE_NDS;
                            rdo.TOTAL_PATIENT_PRICE += item.TOTAL_PATIENT_PRICE;
                            rdo.TOTAL_PRICE += item.TOTAL_PRICE;
                            rdo.TRAN_PRICE += item.TRAN_PRICE;
                            rdo.TT_PRICE += item.TT_PRICE;
                            rdo.TOTAL_OTHER_SOURCE_PRICE += item.TOTAL_OTHER_SOURCE_PRICE;
                        }
                    }
                    ListTotalAllRdoC.Add(rdo);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        protected override void SetTag(Dictionary<string, object> dicSingleTag, ProcessObjectTag objectTag, Store store)
        {
            dicSingleTag.Add("AMOUNT_TEXT", Inventec.Common.String.Convert.CurrencyToVneseString(Math.Round(TotalAmount).ToString()));
            dicSingleTag.Add("EXECUTE_DATE_FROM_STR", Inventec.Common.DateTime.Convert.TimeNumberToDateString(castFilter.TIME_FROM));
            dicSingleTag.Add("EXECUTE_DATE_TO_STR", Inventec.Common.DateTime.Convert.TimeNumberToDateString(castFilter.TIME_TO));
            var department = HisDepartmentCFG.DEPARTMENTs.FirstOrDefault(o => o.ID == castFilter.DEPARTMENT_ID) ?? new HIS_DEPARTMENT();
            dicSingleTag.Add("DEPARTMENT_NAME", department.DEPARTMENT_NAME);
            ProcessSumTotal();
            objectTag.AddObjectData(store, "PatientTypeAs", ListRdoA);
            objectTag.AddObjectData(store, "SumTotalAs", ListTotalRdoA);
            objectTag.AddObjectData(store, "PatientTypeBs", ListRdoB);
            objectTag.AddObjectData(store, "SumTotalBs", ListTotalRdoB);
            objectTag.AddObjectData(store, "PatientTypeCs", ListRdoC);
            objectTag.AddObjectData(store, "SumTotalCs", ListTotalRdoC);

            
            ProcessSumTotalGroupByTreatmentType();
            List<Mrs00245RDO> ListAll = new List<Mrs00245RDO>();
            ListAll.AddRange(ListRdoA);
            ListAll.AddRange(ListRdoB);
            ListAll.AddRange(ListRdoC);
            objectTag.AddObjectData(store, "ListAll", ListAll.OrderByDescending(p => p.TREATMENT_TYPE_CODE).ToList());
            
        }
    }
}
