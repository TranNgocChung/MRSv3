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
    
    public partial class HIS_KSK
    {
        public HIS_KSK()
        {
            this.HIS_KSK_SERVICE = new HashSet<HIS_KSK_SERVICE>();
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
        public string KSK_CODE { get; set; }
        public string KSK_NAME { get; set; }
        public Nullable<long> KSK_CONTRACT_ID { get; set; }
    
        public virtual HIS_KSK_CONTRACT HIS_KSK_CONTRACT { get; set; }
        public virtual ICollection<HIS_KSK_SERVICE> HIS_KSK_SERVICE { get; set; }
    }
}
