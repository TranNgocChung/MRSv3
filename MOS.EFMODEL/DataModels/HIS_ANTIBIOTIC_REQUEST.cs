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
    
    public partial class HIS_ANTIBIOTIC_REQUEST
    {
        public HIS_ANTIBIOTIC_REQUEST()
        {
            this.HIS_ANTIBIOTIC_MICROBI = new HashSet<HIS_ANTIBIOTIC_MICROBI>();
            this.HIS_ANTIBIOTIC_NEW_REG = new HashSet<HIS_ANTIBIOTIC_NEW_REG>();
            this.HIS_ANTIBIOTIC_OLD_REG = new HashSet<HIS_ANTIBIOTIC_OLD_REG>();
            this.HIS_EXP_MEST = new HashSet<HIS_EXP_MEST>();
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
        public string ANTIBIOTIC_REQUEST_CODE { get; set; }
        public long TREATMENT_ID { get; set; }
        public long DHST_ID { get; set; }
        public long REQUEST_DEPARTMENT_ID { get; set; }
        public long REQUEST_TIME { get; set; }
        public string REQUEST_LOGINNAME { get; set; }
        public string REQUEST_USERNAME { get; set; }
        public Nullable<long> APPROVE_TIME { get; set; }
        public long ANTIBIOTIC_REQUEST_STT { get; set; }
        public string APPROVAL_LOGINNAME { get; set; }
        public string APPROVAL_USERNAME { get; set; }
        public string ALLERGY { get; set; }
        public Nullable<short> IS_INFECTION_SHOCK { get; set; }
        public Nullable<short> IS_COMMUNITY_PNEUMONIA { get; set; }
        public Nullable<short> IS_HOSPITAL_PNEUMONIA { get; set; }
        public Nullable<short> IS_VENTILATOR_PNEUMONIA { get; set; }
        public Nullable<short> IS_MENINGITIS { get; set; }
        public Nullable<short> IS_URINARY_INFECTION { get; set; }
        public Nullable<short> IS_ABDOMINAL_INFECTION { get; set; }
        public Nullable<short> IS_SEPSIS { get; set; }
        public Nullable<short> IS_SKIN_INFECTION { get; set; }
        public Nullable<short> IS_OTHER_INFECTION { get; set; }
        public string INFECTION_ENTRY { get; set; }
        public string ICD_SUB_CODE { get; set; }
        public string ICD_TEXT { get; set; }
        public string CLINICAL_CONDITION { get; set; }
        public string WHITE_BLOOD_CELL { get; set; }
        public string CRP { get; set; }
        public string PCT { get; set; }
        public string SUBCLINICAL_RESULT { get; set; }
        public Nullable<decimal> CRCL { get; set; }
        public string NO_MICROBIOLOGY_REASON { get; set; }
        public Nullable<short> IS_LESS_RESPONSIVE_REGIMEN { get; set; }
        public Nullable<short> IS_DRUG_RESISTANCE { get; set; }
        public string ADR_ANTIBIOTIC_NAME { get; set; }
        public string OTHER_REASON { get; set; }
        public string OTHER_OPINION { get; set; }
        public Nullable<short> IS_CONTINUOUS_DIALYSIS { get; set; }
        public Nullable<short> IS_HD_DIALYSIS { get; set; }
    
        public virtual ICollection<HIS_ANTIBIOTIC_MICROBI> HIS_ANTIBIOTIC_MICROBI { get; set; }
        public virtual ICollection<HIS_ANTIBIOTIC_NEW_REG> HIS_ANTIBIOTIC_NEW_REG { get; set; }
        public virtual ICollection<HIS_ANTIBIOTIC_OLD_REG> HIS_ANTIBIOTIC_OLD_REG { get; set; }
        public virtual HIS_TREATMENT HIS_TREATMENT { get; set; }
        public virtual HIS_DEPARTMENT HIS_DEPARTMENT { get; set; }
        public virtual HIS_DHST HIS_DHST { get; set; }
        public virtual ICollection<HIS_EXP_MEST> HIS_EXP_MEST { get; set; }
    }
}