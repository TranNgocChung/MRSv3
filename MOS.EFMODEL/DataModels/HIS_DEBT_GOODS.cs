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
    
    public partial class HIS_DEBT_GOODS
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
        public long DEBT_ID { get; set; }
        public string GOODS_UNIT_NAME { get; set; }
        public string GOODS_NAME { get; set; }
        public decimal AMOUNT { get; set; }
        public decimal PRICE { get; set; }
        public Nullable<decimal> DISCOUNT { get; set; }
        public string DESCRIPTION { get; set; }
        public Nullable<decimal> VAT_RATIO { get; set; }
        public string PACKAGE_NUMBER { get; set; }
        public Nullable<long> EXPIRED_DATE { get; set; }
        public string NATIONAL_NAME { get; set; }
        public string CONCENTRA { get; set; }
        public string MANUFACTURER_NAME { get; set; }
        public Nullable<long> MATERIAL_TYPE_ID { get; set; }
        public Nullable<long> MEDICINE_TYPE_ID { get; set; }
        public Nullable<long> NONE_MEDI_SERVICE_ID { get; set; }
        public Nullable<long> SERVICE_UNIT_ID { get; set; }
    
        public virtual HIS_TRANSACTION HIS_TRANSACTION { get; set; }
        public virtual HIS_SERVICE_UNIT HIS_SERVICE_UNIT { get; set; }
        public virtual HIS_MEDICINE_TYPE HIS_MEDICINE_TYPE { get; set; }
        public virtual HIS_MATERIAL_TYPE HIS_MATERIAL_TYPE { get; set; }
        public virtual HIS_NONE_MEDI_SERVICE HIS_NONE_MEDI_SERVICE { get; set; }
    }
}
