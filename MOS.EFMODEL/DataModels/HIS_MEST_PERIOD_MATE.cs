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
    
    public partial class HIS_MEST_PERIOD_MATE
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
        public long MEDI_STOCK_PERIOD_ID { get; set; }
        public long MATERIAL_ID { get; set; }
        public decimal AMOUNT { get; set; }
        public Nullable<decimal> BEGIN_AMOUNT { get; set; }
        public Nullable<decimal> IN_AMOUNT { get; set; }
        public Nullable<decimal> OUT_AMOUNT { get; set; }
        public Nullable<decimal> INVENTORY_AMOUNT { get; set; }
        public Nullable<decimal> VIR_END_AMOUNT { get; set; }
        public Nullable<long> MEDI_STOCK_ID { get; set; }
        public Nullable<long> TDL_MATERIAL_TYPE_ID { get; set; }
    
        public virtual HIS_MATERIAL HIS_MATERIAL { get; set; }
        public virtual HIS_MEDI_STOCK HIS_MEDI_STOCK { get; set; }
        public virtual HIS_MEDI_STOCK_PERIOD HIS_MEDI_STOCK_PERIOD { get; set; }
    }
}
