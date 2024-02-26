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
    
    public partial class HIS_MATERIAL
    {
        public HIS_MATERIAL()
        {
            this.HIS_EXP_MEST_MATERIAL = new HashSet<HIS_EXP_MEST_MATERIAL>();
            this.HIS_IMP_MEST_MATERIAL = new HashSet<HIS_IMP_MEST_MATERIAL>();
            this.HIS_MATERIAL_BEAN = new HashSet<HIS_MATERIAL_BEAN>();
            this.HIS_MATERIAL_MATERIAL = new HashSet<HIS_MATERIAL_MATERIAL>();
            this.HIS_MATERIAL_MATERIAL1 = new HashSet<HIS_MATERIAL_MATERIAL>();
            this.HIS_MATERIAL_PATY = new HashSet<HIS_MATERIAL_PATY>();
            this.HIS_MEDICINE_MATERIAL = new HashSet<HIS_MEDICINE_MATERIAL>();
            this.HIS_MEST_PERIOD_MATE = new HashSet<HIS_MEST_PERIOD_MATE>();
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
        public long MATERIAL_TYPE_ID { get; set; }
        public Nullable<long> SUPPLIER_ID { get; set; }
        public decimal AMOUNT { get; set; }
        public Nullable<long> IMP_SOURCE_ID { get; set; }
        public decimal IMP_PRICE { get; set; }
        public decimal IMP_VAT_RATIO { get; set; }
        public Nullable<decimal> INTERNAL_PRICE { get; set; }
        public Nullable<long> IMP_TIME { get; set; }
        public Nullable<long> BID_ID { get; set; }
        public string TDL_BID_NUMBER { get; set; }
        public string TDL_BID_NUM_ORDER { get; set; }
        public string TDL_BID_GROUP_CODE { get; set; }
        public string TDL_BID_PACKAGE_CODE { get; set; }
        public string TDL_BID_YEAR { get; set; }
        public string PACKAGE_NUMBER { get; set; }
        public Nullable<long> EXPIRED_DATE { get; set; }
        public Nullable<short> IS_PREGNANT { get; set; }
        public Nullable<short> IS_SALE_EQUAL_IMP_PRICE { get; set; }
        public long TDL_SERVICE_ID { get; set; }
        public Nullable<decimal> VIR_IMP_PRICE { get; set; }
        public Nullable<long> MAX_REUSE_COUNT { get; set; }
        public Nullable<long> DOCUMENT_PRICE { get; set; }
        public string CONCENTRA { get; set; }
        public Nullable<long> MANUFACTURER_ID { get; set; }
        public string NATIONAL_NAME { get; set; }
        public string TDL_IMP_MEST_CODE { get; set; }
        public string TDL_IMP_MEST_SUB_CODE { get; set; }
        public string MATERIAL_REGISTER_NUMBER { get; set; }
        public Nullable<decimal> IMP_UNIT_AMOUNT { get; set; }
        public Nullable<decimal> IMP_UNIT_PRICE { get; set; }
        public Nullable<long> TDL_IMP_UNIT_ID { get; set; }
        public Nullable<decimal> TDL_IMP_UNIT_CONVERT_RATIO { get; set; }
        public Nullable<long> MEDICAL_CONTRACT_ID { get; set; }
        public Nullable<decimal> CONTRACT_PRICE { get; set; }
        public Nullable<decimal> PROFIT_RATIO { get; set; }
        public string BID_MATERIAL_TYPE_CODE { get; set; }
        public string BID_MATERIAL_TYPE_NAME { get; set; }
        public Nullable<decimal> TAX_RATIO { get; set; }
        public string TDL_BID_EXTRA_CODE { get; set; }
        public string LOCKING_REASON { get; set; }
        public string MATERIAL_SIZE { get; set; }
    
        public virtual HIS_BID HIS_BID { get; set; }
        public virtual ICollection<HIS_EXP_MEST_MATERIAL> HIS_EXP_MEST_MATERIAL { get; set; }
        public virtual ICollection<HIS_IMP_MEST_MATERIAL> HIS_IMP_MEST_MATERIAL { get; set; }
        public virtual HIS_IMP_SOURCE HIS_IMP_SOURCE { get; set; }
        public virtual ICollection<HIS_MATERIAL_BEAN> HIS_MATERIAL_BEAN { get; set; }
        public virtual HIS_MATERIAL_TYPE HIS_MATERIAL_TYPE { get; set; }
        public virtual HIS_SUPPLIER HIS_SUPPLIER { get; set; }
        public virtual HIS_MEDICAL_CONTRACT HIS_MEDICAL_CONTRACT { get; set; }
        public virtual ICollection<HIS_MATERIAL_MATERIAL> HIS_MATERIAL_MATERIAL { get; set; }
        public virtual ICollection<HIS_MATERIAL_MATERIAL> HIS_MATERIAL_MATERIAL1 { get; set; }
        public virtual ICollection<HIS_MATERIAL_PATY> HIS_MATERIAL_PATY { get; set; }
        public virtual ICollection<HIS_MEDICINE_MATERIAL> HIS_MEDICINE_MATERIAL { get; set; }
        public virtual ICollection<HIS_MEST_PERIOD_MATE> HIS_MEST_PERIOD_MATE { get; set; }
    }
}
