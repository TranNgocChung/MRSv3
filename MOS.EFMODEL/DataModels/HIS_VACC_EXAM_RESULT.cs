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
    
    public partial class HIS_VACC_EXAM_RESULT
    {
        public HIS_VACC_EXAM_RESULT()
        {
            this.HIS_VAEX_VAER = new HashSet<HIS_VAEX_VAER>();
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
        public string VACC_EXAM_RESULT_CODE { get; set; }
        public string VACC_EXAM_RESULT_NAME { get; set; }
        public Nullable<short> IS_BABY { get; set; }
    
        public virtual ICollection<HIS_VAEX_VAER> HIS_VAEX_VAER { get; set; }
    }
}