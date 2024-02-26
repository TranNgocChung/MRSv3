using MOS.MANAGER.HisPatientType;
using MOS.MANAGER.HisService;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisTransaction;
using MOS.MANAGER.HisServiceRetyCat;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisPatientTypeAlter;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MRS.MANAGER.Base;
using MRS.MANAGER.Config;
using MRS.MANAGER.Core.MrsReport;
using MOS.MANAGER.HisHeinApproval;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.MANAGER.HisSereServBill;

namespace MRS.Processor.Mrs00429
{
    class Mrs00429Processor : AbstractProcessor
    {
        Mrs00429Filter castFilter = null;
        List<Mrs00429RDO> listRdo = new List<Mrs00429RDO>();
        List<Mrs00429RDO> listGroupName = new List<Mrs00429RDO>();


        string thisReportTypeCode = "";
        public Mrs00429Processor(CommonParam param, string reportTypeCode)
            : base(param, reportTypeCode)
        {
            thisReportTypeCode = reportTypeCode;
        }

        public override Type FilterType()
        {
            return typeof(Mrs00429Filter);
        }

        List<V_HIS_PATIENT_TYPE_ALTER> listPatientTypeAlterHeins = new List<V_HIS_PATIENT_TYPE_ALTER>();
        List<V_HIS_PATIENT_TYPE_ALTER> listPatientTypeAlterFees = new List<V_HIS_PATIENT_TYPE_ALTER>();

        List<HIS_SERE_SERV> listSereServs = new List<HIS_SERE_SERV>();
        List<V_HIS_SERE_SERV_BILL> listSereServBills = new List<V_HIS_SERE_SERV_BILL>();
        List<V_HIS_SERVICE_RETY_CAT> listServiceRetyCats = new List<V_HIS_SERVICE_RETY_CAT>();
        List<HIS_TRANSACTION> listBills = new List<HIS_TRANSACTION>();
        List<HIS_HEIN_APPROVAL> listHeinApprovedBHYTs = new List<HIS_HEIN_APPROVAL>();

        public List<long> listServiceDI = null;
        public List<long> listServiceCT = null;


        protected override void SetTag(Dictionary<string, object> dicSingleTag, Inventec.Common.FlexCellExport.ProcessObjectTag objectTag, Inventec.Common.FlexCellExport.Store store)
        {
            try
            {
                if (castFilter.TIME_FROM > 0)
                {
                    dicSingleTag.Add("DATE_FROM_STR", Inventec.Common.DateTime.Convert.TimeNumberToTimeString(castFilter.TIME_FROM));
                }
                if (castFilter.TIME_TO > 0)
                {
                    dicSingleTag.Add("DATE_TO_STR", Inventec.Common.DateTime.Convert.TimeNumberToTimeString(castFilter.TIME_TO));
                }

                bool exportSuccess = true;
                objectTag.AddObjectData(store, "Report", listRdo);
                objectTag.AddObjectData(store, "GroupName", listGroupName);
                exportSuccess = exportSuccess && objectTag.AddRelationship(store, "GroupName", "Report", "GROUP_NAME", "GROUP_NAME");

                exportSuccess = exportSuccess && store.SetCommonFunctions();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }


        protected override bool GetData()
        {
            bool result = true;
            try
            {
                this.castFilter = (Mrs00429Filter)this.reportFilter;
                var skip = 0;
                HisServiceRetyCatViewFilterQuery serviceRetyCatFilter = new HisServiceRetyCatViewFilterQuery();
                serviceRetyCatFilter.REPORT_TYPE_CODE__EXACT = "MRS00429";
                var listServices = new MOS.MANAGER.HisServiceRetyCat.HisServiceRetyCatManager(param).GetView(serviceRetyCatFilter);
                var listServiceIds = listServices.Select(s => s.SERVICE_ID).ToList();

                listServiceDI = listServices.Where(w => w.CATEGORY_CODE == "429DI").Select(s => s.SERVICE_ID).ToList();
                listServiceCT = listServices.Where(w => w.CATEGORY_CODE == "429CT").Select(s => s.SERVICE_ID).ToList();
                // BN BHYT + duyet khoa giam dinh
                HisHeinApprovalFilterQuery heinApprovalFilters = new HisHeinApprovalFilterQuery();
                heinApprovalFilters.EXECUTE_TIME_FROM = this.castFilter.TIME_FROM;
                heinApprovalFilters.EXECUTE_TIME_TO = this.castFilter.TIME_TO;
                listHeinApprovedBHYTs = new HisHeinApprovalManager(param).Get(heinApprovalFilters);
                //lay sereserv
                var approvalIds = listHeinApprovedBHYTs.Select(s => s.ID).ToList();
                skip = 0;
                while (approvalIds.Count - skip > 0)
                {
                    var listIds = approvalIds.Skip(skip).Take(ManagerConstant.MAX_REQUEST_LENGTH_PARAM).ToList();
                    skip += ManagerConstant.MAX_REQUEST_LENGTH_PARAM;
                    HisSereServFilterQuery filter = new HisSereServFilterQuery();
                    filter.HEIN_APPROVAL_IDs = listIds;
                    filter.SERVICE_IDs = listServiceIds;
                    filter.IS_EXPEND = false;
                    filter.HAS_EXECUTE = true;
                    var sereServs = new MOS.MANAGER.HisSereServ.HisSereServManager(param).Get(filter);
                    listSereServs.AddRange(sereServs);
                }
                //listTreatments.AddRange(listTreatmentHeins); 

                //BN vien phi co giao dich
                HisTransactionFilterQuery transactionFilter = new HisTransactionFilterQuery();
                transactionFilter.TRANSACTION_TIME_FROM = this.castFilter.TIME_FROM;
                transactionFilter.TRANSACTION_TIME_TO = this.castFilter.TIME_TO;
                transactionFilter.IS_CANCEL = false;
                transactionFilter.TRANSACTION_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT;
                transactionFilter.HAS_SALL_TYPE = false;
                listBills = new MOS.MANAGER.HisTransaction.HisTransactionManager(param).Get(transactionFilter);

                //lay sereservbill
                var billIds = listBills.Select(s => s.ID).ToList();
                var listSereServBills = new List<HIS_SERE_SERV_BILL>();
                skip = 0;
                while (billIds.Count - skip > 0)
                {
                    var listIds = billIds.Skip(skip).Take(ManagerConstant.MAX_REQUEST_LENGTH_PARAM).ToList();
                    skip += ManagerConstant.MAX_REQUEST_LENGTH_PARAM;
                    HisSereServBillFilterQuery filter = new HisSereServBillFilterQuery();
                    filter.BILL_IDs = listIds;
                    var sereServBills = new MOS.MANAGER.HisSereServBill.HisSereServBillManager(param).Get(filter);
                    listSereServBills.AddRange(sereServBills);
                }

                //lay sereserv
                var sereServIds = listSereServBills.Select(s => s.SERE_SERV_ID).Distinct().ToList();
                skip = 0;
                while (sereServIds.Count - skip > 0)
                {
                    var listIds = sereServIds.Skip(skip).Take(ManagerConstant.MAX_REQUEST_LENGTH_PARAM).ToList();
                    skip += ManagerConstant.MAX_REQUEST_LENGTH_PARAM;
                    HisSereServFilterQuery filter = new HisSereServFilterQuery();
                    filter.IDs = listIds;
                    filter.SERVICE_IDs = listServiceIds;
                    filter.IS_EXPEND = false;
                    filter.HAS_EXECUTE = true;
                    var sereServs = new MOS.MANAGER.HisSereServ.HisSereServManager(param).Get(filter);
                    if (sereServs != null)
                    {
                        sereServs = sereServs.Where(o => o.PATIENT_TYPE_ID != HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT).ToList();
                        listSereServs.AddRange(sereServs);
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

        protected override bool ProcessData()
        {
            bool result = true;
            try
            {
                CommonParam paramGet = new CommonParam();
                if (IsNotNullOrEmpty(listSereServs))
                {
                    foreach (var sereServs in listSereServs)
                    {
                        Mrs00429RDO rdo = new Mrs00429RDO();
                        if (sereServs.PATIENT_TYPE_ID == MANAGER.Config.HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                        {
                            rdo.GROUP_NAME = "Bảo hiểm y tế";
                            rdo.PRICE = sereServs.PRICE;

                            if (listServiceDI.Contains(sereServs.SERVICE_ID))
                            {
                                rdo.AMOUNT_DI = sereServs.AMOUNT;
                                rdo.TOTAL_PRICE_DI = rdo.PRICE * sereServs.AMOUNT;
                            }
                            if (listServiceCT.Contains(sereServs.SERVICE_ID))
                            {
                                rdo.AMOUNT_CT = sereServs.AMOUNT;
                                rdo.TOTAL_PRICE_CT = rdo.PRICE * sereServs.AMOUNT;
                            }
                            listRdo.Add(rdo);
                        }

                        if (sereServs.PATIENT_TYPE_ID == MANAGER.Config.HisPatientTypeCFG.PATIENT_TYPE_ID__FEE)
                        {
                            rdo.GROUP_NAME = "Viện phí";
                            rdo.PRICE = sereServs.PRICE;

                            if (listServiceDI.Contains(sereServs.SERVICE_ID))
                            {
                                rdo.AMOUNT_DI = sereServs.AMOUNT;
                                rdo.TOTAL_PRICE_DI = rdo.PRICE * sereServs.AMOUNT;
                            }
                            if (listServiceCT.Contains(sereServs.SERVICE_ID))
                            {
                                rdo.AMOUNT_CT = sereServs.AMOUNT;
                                rdo.TOTAL_PRICE_CT = rdo.PRICE * sereServs.AMOUNT;
                            }
                            listRdo.Add(rdo);
                        }
                    }
                }

                listRdo = listRdo.GroupBy(g => new { g.PRICE, g.GROUP_NAME }).Select(s => new Mrs00429RDO
                {
                    GROUP_NAME = s.First().GROUP_NAME,
                    PRICE = s.First().PRICE,
                    AMOUNT_DI = s.Sum(a => a.AMOUNT_DI),
                    AMOUNT_CT = s.Sum(a => a.AMOUNT_CT),
                    TOTAL_PRICE_DI = s.Sum(a => a.TOTAL_PRICE_DI),
                    TOTAL_PRICE_CT = s.Sum(a => a.TOTAL_PRICE_CT)
                }).ToList();

                listGroupName = listRdo.GroupBy(g => new { g.GROUP_NAME }).Select(s => new Mrs00429RDO
                {
                    GROUP_NAME = s.First().GROUP_NAME,
                    PRICE = s.First().PRICE,
                    AMOUNT_DI = s.Sum(a => a.AMOUNT_DI),
                    AMOUNT_CT = s.Sum(a => a.AMOUNT_CT),
                    TOTAL_PRICE_DI = s.Sum(a => a.TOTAL_PRICE_DI),
                    TOTAL_PRICE_CT = s.Sum(a => a.TOTAL_PRICE_CT)
                }).ToList();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }


    }
}
