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
    
    public partial class V_HIS_SESE_TRANS_REQ
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
        public long SERE_SERV_ID { get; set; }
        public decimal PRICE { get; set; }
        public long TRANS_REQ_ID { get; set; }
        public string TDL_SERVICE_NAME { get; set; }
        public decimal AMOUNT { get; set; }
        public string TDL_SERVICE_CODE { get; set; }
        public string TRANS_REQ_CODE { get; set; }
        public decimal TOTAL_REQUEST_AMOUNT { get; set; }
        public long TREATMENT_ID { get; set; }
    }
}
