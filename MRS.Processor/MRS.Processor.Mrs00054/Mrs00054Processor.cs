using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisTransaction;
using MRS.MANAGER.Core.MrsReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Processor.Mrs00054
{
    public class Mrs00054Processor : AbstractProcessor
    {
        Mrs00054Filter castFilter = null;
        List<Mrs00054RDO> ListRdo = new List<Mrs00054RDO>();
        List<V_HIS_TRANSACTION> ListCurrentBill = new List<V_HIS_TRANSACTION>();
        string Cashier_UserName = "";

        public Mrs00054Processor(CommonParam param, string reportTypeCode)
            : base(param, reportTypeCode)
        { }

        public override Type FilterType()
        {
            return typeof(Mrs00054Filter);
        }

        protected override bool GetData()
        {
            bool result = false;
            try
            {
                castFilter = ((Mrs00054Filter)this.reportFilter);
                if (castFilter.CASHIER_LOGINNAME != null)
                {
                    LoadDataToRam();
                    ProcessListCurrentBill();
                    result = true;
                }
                else
                {
                    Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => castFilter), castFilter));
                    throw new DataMisalignedException("filter truyen vao thieu CASHIER_LOGINNAME.");
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
                if (IsNotNullOrEmpty(ListCurrentBill))
                {
                    ListRdo = (from r in ListCurrentBill select new Mrs00054RDO(r)).ToList();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private void ProcessListCurrentBill()
        {
            try
            {
                if (ListCurrentBill != null && ListCurrentBill.Count > 0)
                {
                    ListCurrentBill = ListCurrentBill.Where(o => o.CASHIER_LOGINNAME == castFilter.CASHIER_LOGINNAME && o.IS_CANCEL != 1 && /* o.IS_TRANSFER_ACCOUNTING != 1 &&*/ o.AMOUNT > 200000).ToList().OrderBy(o => o.TRANSACTION_CODE).ToList();
                    if (ListCurrentBill.Count > 0)
                    {
                        Cashier_UserName = castFilter.CASHIER_LOGINNAME + " - " + ListCurrentBill[0].CASHIER_USERNAME;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                ListCurrentBill.Clear();
            }
        }

        private void LoadDataToRam()
        {
            try
            {
                HisTransactionViewFilterQuery filter = new HisTransactionViewFilterQuery();
                filter.TRANSACTION_TIME_FROM = castFilter.TIME_FROM;
                filter.TRANSACTION_TIME_TO = castFilter.TIME_TO;
                filter.TRANSACTION_TYPE_IDs = new List<long> { IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT };
                ListCurrentBill = new HisTransactionManager().GetView(filter);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        protected override void SetTag(Dictionary<string, object> dicSingleTag, Inventec.Common.FlexCellExport.ProcessObjectTag objectTag, Inventec.Common.FlexCellExport.Store store)
        {
            try
            {
                if (castFilter.TIME_FROM > 0)
                {
                    dicSingleTag.Add("CREATE_TIME_TO_STR", Inventec.Common.DateTime.Convert.TimeNumberToTimeString(castFilter.TIME_FROM));
                }
                if (castFilter.TIME_TO > 0)
                {
                    dicSingleTag.Add("CREATE_TIME_FROM_STR", Inventec.Common.DateTime.Convert.TimeNumberToTimeString(castFilter.TIME_TO));
                }

                dicSingleTag.Add("CASHIER_USERNAME", Cashier_UserName);

                ListRdo = ListRdo.OrderBy(o => o.CREATE_TIME).ThenBy(t => t.TRANSACTION_CODE).ToList();
                objectTag.AddObjectData(store, "Report", ListRdo);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
