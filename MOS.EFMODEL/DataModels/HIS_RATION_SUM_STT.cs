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
    
    public partial class HIS_RATION_SUM_STT
    {
        public HIS_RATION_SUM_STT()
        {
            this.HIS_RATION_SUM = new HashSet<HIS_RATION_SUM>();
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
        public string RATION_SUM_STT_CODE { get; set; }
        public string RATION_SUM_STT_NAME { get; set; }
    
        public virtual ICollection<HIS_RATION_SUM> HIS_RATION_SUM { get; set; }
    }
}
