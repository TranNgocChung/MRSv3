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
    
    public partial class HIS_REHA_SUM
    {
        public HIS_REHA_SUM()
        {
            this.HIS_SERVICE_REQ = new HashSet<HIS_SERVICE_REQ>();
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
        public long TREATMENT_ID { get; set; }
        public Nullable<long> ICD_ID__DELETE { get; set; }
        public string ICD_CODE { get; set; }
        public string ICD_NAME { get; set; }
        public string ICD_SUB_CODE { get; set; }
        public string ICD_TEXT { get; set; }
        public string ECG_BEFORE { get; set; }
        public string ECG_AFTER { get; set; }
        public string RESPIRATORY_BEFORE { get; set; }
        public string RESPIRATORY_AFTER { get; set; }
        public string SYMPTOM_BEFORE { get; set; }
        public string SYMPTOM_AFTER { get; set; }
        public string ADVISE { get; set; }
        public Nullable<long> NUM_ORDER { get; set; }
    
        public virtual HIS_TREATMENT HIS_TREATMENT { get; set; }
        public virtual ICollection<HIS_SERVICE_REQ> HIS_SERVICE_REQ { get; set; }
    }
}
