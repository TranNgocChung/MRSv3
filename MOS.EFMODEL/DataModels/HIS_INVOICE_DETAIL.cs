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
    
    public partial class HIS_INVOICE_DETAIL
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
        public long INVOICE_ID { get; set; }
        public string GOODS_UNIT { get; set; }
        public string GOODS_NAME { get; set; }
        public decimal PRICE { get; set; }
        public decimal AMOUNT { get; set; }
        public Nullable<decimal> DISCOUNT { get; set; }
        public string DESCRIPTION { get; set; }
        public Nullable<decimal> VIR_TOTAL_PRICE { get; set; }
    
        public virtual HIS_INVOICE HIS_INVOICE { get; set; }
    }
}
