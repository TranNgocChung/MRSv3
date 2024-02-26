//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MOS.EFMODEL.DataModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class HIS_SESE_DEPO_REPAY
    {
        public long ID { get; set; }
        public Nullable<long> CREATE_TIME { get; set; }
        public Nullable<long> MODIFY_TIME { get; set; }
        public string CREATOR { get; set; }
        public string MODIFIER { get; set; }
        public string APP_CREATOR { get; set; }
        public string APP_MODIFIER { get; set; }
        public Nullable<short> IS_ACTIVE { get; set; }
        public Nullable<short> IS_DELETE { get; set; }
        public string GROUP_CODE { get; set; }
        public long SERE_SERV_DEPOSIT_ID { get; set; }
        public long REPAY_ID { get; set; }
        public decimal AMOUNT { get; set; }
        public Nullable<short> IS_CANCEL { get; set; }
        public long TDL_TREATMENT_ID { get; set; }
        public Nullable<long> TDL_SERVICE_REQ_ID { get; set; }
        public long TDL_SERVICE_ID { get; set; }
        public string TDL_SERVICE_CODE { get; set; }
        public string TDL_SERVICE_NAME { get; set; }
        public long TDL_SERVICE_TYPE_ID { get; set; }
        public long TDL_SERVICE_UNIT_ID { get; set; }
        public long TDL_PATIENT_TYPE_ID { get; set; }
        public Nullable<long> TDL_HEIN_SERVICE_TYPE_ID { get; set; }
        public long TDL_REQUEST_DEPARTMENT_ID { get; set; }
        public long TDL_EXECUTE_DEPARTMENT_ID { get; set; }
        public Nullable<long> TDL_SERE_SERV_PARENT_ID { get; set; }
        public Nullable<short> TDL_IS_OUT_PARENT_FEE { get; set; }
        public decimal TDL_AMOUNT { get; set; }
        public Nullable<short> TDL_IS_EXPEND { get; set; }
        public Nullable<decimal> TDL_HEIN_PRICE { get; set; }
        public Nullable<decimal> TDL_HEIN_LIMIT_PRICE { get; set; }
        public decimal TDL_VIR_PRICE { get; set; }
        public decimal TDL_VIR_PRICE_NO_ADD_PRICE { get; set; }
        public decimal TDL_VIR_HEIN_PRICE { get; set; }
        public decimal TDL_VIR_TOTAL_PRICE { get; set; }
        public decimal TDL_VIR_TOTAL_HEIN_PRICE { get; set; }
        public decimal TDL_VIR_TOTAL_PATIENT_PRICE { get; set; }
    
        public virtual HIS_SERE_SERV HIS_SERE_SERV { get; set; }
        public virtual HIS_SERE_SERV_DEPOSIT HIS_SERE_SERV_DEPOSIT { get; set; }
        public virtual HIS_TRANSACTION HIS_TRANSACTION { get; set; }
    }
}
