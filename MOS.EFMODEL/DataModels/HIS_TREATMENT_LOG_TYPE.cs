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
    
    public partial class HIS_TREATMENT_LOG_TYPE
    {
        public HIS_TREATMENT_LOG_TYPE()
        {
            this.HIS_TREATMENT_LOGGING = new HashSet<HIS_TREATMENT_LOGGING>();
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
        public string TREATMENT_LOG_TYPE_CODE { get; set; }
        public string TREATMENT_LOG_TYPE_NAME { get; set; }
    
        public virtual ICollection<HIS_TREATMENT_LOGGING> HIS_TREATMENT_LOGGING { get; set; }
    }
}
