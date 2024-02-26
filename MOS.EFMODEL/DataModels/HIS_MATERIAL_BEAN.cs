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
    
    public partial class HIS_MATERIAL_BEAN
    {
        public HIS_MATERIAL_BEAN()
        {
            this.HIS_MATERIAL_BEAN1 = new HashSet<HIS_MATERIAL_BEAN>();
        }
    
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
        public long MATERIAL_ID { get; set; }
        public decimal AMOUNT { get; set; }
        public Nullable<long> MEDI_STOCK_ID { get; set; }
        public Nullable<long> SOURCE_ID { get; set; }
        public Nullable<decimal> BK_DECREASE_AMOUNT { get; set; }
        public Nullable<long> BK_MEDI_STOCK_ID { get; set; }
        public Nullable<long> EXP_MEST_MATERIAL_ID { get; set; }
        public Nullable<decimal> DETACH_AMOUNT { get; set; }
        public string DETACH_KEY { get; set; }
        public Nullable<short> IS_TH { get; set; }
        public Nullable<short> IS_CK { get; set; }
        public Nullable<short> IS_USE { get; set; }
        public string SESSION_KEY { get; set; }
        public Nullable<long> SESSION_TIME { get; set; }
        public long TDL_MATERIAL_TYPE_ID { get; set; }
        public Nullable<short> TDL_MATERIAL_IS_ACTIVE { get; set; }
        public decimal TDL_MATERIAL_IMP_PRICE { get; set; }
        public decimal TDL_MATERIAL_IMP_VAT_RATIO { get; set; }
        public Nullable<long> TDL_MATERIAL_IMP_TIME { get; set; }
        public Nullable<long> TDL_MATERIAL_EXPIRED_DATE { get; set; }
        public Nullable<short> TDL_IS_SALE_EQUAL_IMP_PRICE { get; set; }
        public long TDL_SERVICE_ID { get; set; }
        public string SERIAL_NUMBER { get; set; }
        public Nullable<long> REMAIN_REUSE_COUNT { get; set; }
        public Nullable<long> TDL_MATERIAL_MAX_REUSE_COUNT { get; set; }
        public string TDL_PACKAGE_NUMBER { get; set; }
        public string LOCKING_REASON { get; set; }
    
        public virtual HIS_EXP_MEST_MATERIAL HIS_EXP_MEST_MATERIAL { get; set; }
        public virtual HIS_MATERIAL HIS_MATERIAL { get; set; }
        public virtual HIS_MEDI_STOCK HIS_MEDI_STOCK { get; set; }
        public virtual ICollection<HIS_MATERIAL_BEAN> HIS_MATERIAL_BEAN1 { get; set; }
        public virtual HIS_MATERIAL_BEAN HIS_MATERIAL_BEAN2 { get; set; }
    }
}
