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
    
    public partial class L_HIS_TREATMENT_BED_ROOM
    {
        public long ID { get; set; }
        public long TREATMENT_ID { get; set; }
        public Nullable<long> CO_TREATMENT_ID { get; set; }
        public long ADD_TIME { get; set; }
        public long BED_ROOM_ID { get; set; }
        public Nullable<long> REMOVE_TIME { get; set; }
        public Nullable<long> TREATMENT_ROOM_ID { get; set; }
        public Nullable<long> TDL_OBSERVED_TIME_FROM { get; set; }
        public Nullable<long> TDL_OBSERVED_TIME_TO { get; set; }
        public long PATIENT_ID { get; set; }
        public string TREATMENT_CODE { get; set; }
        public string TDL_PATIENT_FIRST_NAME { get; set; }
        public string TDL_PATIENT_LAST_NAME { get; set; }
        public string TDL_PATIENT_NAME { get; set; }
        public long TDL_PATIENT_DOB { get; set; }
        public string TDL_PATIENT_GENDER_NAME { get; set; }
        public string TDL_PATIENT_CODE { get; set; }
        public string TDL_PATIENT_ADDRESS { get; set; }
        public string TDL_HEIN_CARD_NUMBER { get; set; }
        public string TDL_HEIN_MEDI_ORG_CODE { get; set; }
        public string ICD_CODE { get; set; }
        public string ICD_NAME { get; set; }
        public string ICD_TEXT { get; set; }
        public string ICD_SUB_CODE { get; set; }
        public long TDL_PATIENT_GENDER_ID { get; set; }
        public string TDL_HEIN_MEDI_ORG_NAME { get; set; }
        public Nullable<short> IS_PAUSE { get; set; }
        public Nullable<short> IS_APPROVE_FINISH { get; set; }
        public string APPROVE_FINISH_NOTE { get; set; }
        public Nullable<long> TDL_PATIENT_CLASSIFY_ID { get; set; }
        public Nullable<long> TDL_TREATMENT_TYPE_ID { get; set; }
        public Nullable<long> EMR_COVER_TYPE_ID { get; set; }
        public Nullable<long> CLINICAL_IN_TIME { get; set; }
        public string CO_TREAT_DEPARTMENT_IDS { get; set; }
        public Nullable<long> OUT_TIME { get; set; }
        public string TDL_PATIENT_AVATAR_URL { get; set; }
        public Nullable<long> LAST_DEPARTMENT_ID { get; set; }
        public string TDL_PATIENT_UNSIGNED_NAME { get; set; }
        public Nullable<long> TREATMENT_END_TYPE_ID { get; set; }
        public string TREATMENT_METHOD { get; set; }
        public string TDL_PATIENT_PHONE { get; set; }
        public Nullable<long> TDL_HEIN_CARD_FROM_TIME { get; set; }
        public Nullable<long> TDL_HEIN_CARD_TO_TIME { get; set; }
        public string PATIENT_TYPE_NAME { get; set; }
        public string BED_NAME { get; set; }
        public string BED_CODE { get; set; }
        public string PATIENT_TYPE_CODE { get; set; }
        public string BED_ROOM_NAME { get; set; }
        public string PATIENT_CLASSIFY_NAME { get; set; }
        public string DISPLAY_COLOR { get; set; }
        public string TREATMENT_ROOM_CODE { get; set; }
        public string TREATMENT_ROOM_NAME { get; set; }
        public string LAST_DEPARTMENT_CODE { get; set; }
        public string LAST_DEPARTMENT_NAME { get; set; }
        public string NOTE { get; set; }
    }
}
