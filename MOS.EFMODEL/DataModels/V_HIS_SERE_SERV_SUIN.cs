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
    
    public partial class V_HIS_SERE_SERV_SUIN
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
        public long SUIM_INDEX_ID { get; set; }
        public string VALUE { get; set; }
        public string DESCRIPTION { get; set; }
        public long TDL_TREATMENT_ID { get; set; }
        public string SUIM_INDEX_CODE { get; set; }
        public string SUIM_INDEX_NAME { get; set; }
        public string SUIM_INDEX_UNIT_CODE { get; set; }
        public string SUIM_INDEX_UNIT_NAME { get; set; }
    }
}
