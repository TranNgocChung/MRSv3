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
    
    public partial class V_HIS_TRACKING
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
        public long TREATMENT_ID { get; set; }
        public long TRACKING_TIME { get; set; }
        public Nullable<long> ICD_ID__DELETE { get; set; }
        public string ICD_CODE { get; set; }
        public string ICD_NAME { get; set; }
        public string ICD_SUB_CODE { get; set; }
        public string ICD_TEXT { get; set; }
        public string MEDICAL_INSTRUCTION { get; set; }
        public string CONTENT_BK { get; set; }
        public Nullable<long> DEPARTMENT_ID { get; set; }
        public string SUBCLINICAL_PROCESSES_BK { get; set; }
        public string CARE_INSTRUCTION { get; set; }
        public string GENERAL_EXPRESSION { get; set; }
        public string ORIENTATION_CAPACITY { get; set; }
        public string EMOTION { get; set; }
        public string PERCEPTION { get; set; }
        public string FORM_OF_THINKING { get; set; }
        public string CONTENT_OF_THINKING { get; set; }
        public string INSTINCTIVELY_BEHAVIOR { get; set; }
        public string AWARENESS_BEHAVIOR { get; set; }
        public string MEMORY { get; set; }
        public string INTELLECTUAL { get; set; }
        public string CONCENTRATION { get; set; }
        public string CARDIOVASCULAR { get; set; }
        public string RESPIRATORY { get; set; }
        public Nullable<long> ROOM_ID { get; set; }
        public string TRADITIONAL_ICD_CODE { get; set; }
        public string TRADITIONAL_ICD_NAME { get; set; }
        public string TRADITIONAL_ICD_SUB_CODE { get; set; }
        public string TRADITIONAL_ICD_TEXT { get; set; }
        public string EYE_TENSION_LEFT { get; set; }
        public string EYE_TENSION_RIGHT { get; set; }
        public string EYESIGHT_LEFT { get; set; }
        public string EYESIGHT_RIGHT { get; set; }
        public string EYESIGHT_GLASS_LEFT { get; set; }
        public string EYESIGHT_GLASS_RIGHT { get; set; }
        public Nullable<long> SHEET_ORDER { get; set; }
        public Nullable<long> EMR_DOCUMENT_STT_ID { get; set; }
        public string EMR_DOCUMENT_URL { get; set; }
        public string EMR_DOCUMENT_CODE { get; set; }
        public string CONTENT { get; set; }
        public string SUBCLINICAL_PROCESSES { get; set; }
        public string DISEASE_STAGE { get; set; }
        public string REHABILITATION_CONTENT { get; set; }
        public string TREATMENT_CODE { get; set; }
        public long PATIENT_ID { get; set; }
        public string TDL_PATIENT_CODE { get; set; }
        public string TDL_PATIENT_FIRST_NAME { get; set; }
        public string TDL_PATIENT_LAST_NAME { get; set; }
        public string TDL_PATIENT_NAME { get; set; }
        public long TDL_PATIENT_DOB { get; set; }
        public string TDL_PATIENT_ADDRESS { get; set; }
        public string TDL_PATIENT_GENDER_NAME { get; set; }
        public string ROOM_CODE { get; set; }
        public string ROOM_NAME { get; set; }
        public string DEPARTMENT_CODE { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public string EMR_DOCUMENT_STT_CODE { get; set; }
        public string EMR_DOCUMENT_STT_NAME { get; set; }
    }
}
