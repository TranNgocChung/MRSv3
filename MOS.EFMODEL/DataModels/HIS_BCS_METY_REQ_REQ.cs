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
    
    public partial class HIS_BCS_METY_REQ_REQ
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
        public long EXP_MEST_METY_REQ_ID { get; set; }
        public long PRE_EXP_MEST_METY_REQ_ID { get; set; }
        public decimal AMOUNT { get; set; }
        public long TDL_XBTT_EXP_MEST_ID { get; set; }
        public string TDL_XBTT_EXP_MEST_CODE { get; set; }
    
        public virtual HIS_EXP_MEST_METY_REQ HIS_EXP_MEST_METY_REQ { get; set; }
        public virtual HIS_EXP_MEST_METY_REQ HIS_EXP_MEST_METY_REQ1 { get; set; }
    }
}