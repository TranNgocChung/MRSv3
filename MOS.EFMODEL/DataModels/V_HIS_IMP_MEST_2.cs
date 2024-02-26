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
    
    public partial class V_HIS_IMP_MEST_2
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
        public string IMP_MEST_CODE { get; set; }
        public long IMP_MEST_TYPE_ID { get; set; }
        public long IMP_MEST_STT_ID { get; set; }
        public long MEDI_STOCK_ID { get; set; }
        public Nullable<long> MEDI_STOCK_PERIOD_ID { get; set; }
        public string IMP_LOGINNAME { get; set; }
        public string IMP_USERNAME { get; set; }
        public Nullable<long> IMP_TIME { get; set; }
        public Nullable<long> IMP_DATE { get; set; }
        public string APPROVAL_LOGINNAME { get; set; }
        public string APPROVAL_USERNAME { get; set; }
        public Nullable<long> APPROVAL_TIME { get; set; }
        public string REQ_LOGINNAME { get; set; }
        public string REQ_USERNAME { get; set; }
        public Nullable<long> REQ_ROOM_ID { get; set; }
        public Nullable<long> REQ_DEPARTMENT_ID { get; set; }
        public long CREATE_DATE { get; set; }
        public string DESCRIPTION { get; set; }
        public Nullable<long> CHMS_EXP_MEST_ID { get; set; }
        public string TDL_CHMS_EXP_MEST_CODE { get; set; }
        public Nullable<long> AGGR_IMP_MEST_ID { get; set; }
        public string TDL_AGGR_IMP_MEST_CODE { get; set; }
        public Nullable<long> MOBA_EXP_MEST_ID { get; set; }
        public string TDL_MOBA_EXP_MEST_CODE { get; set; }
        public Nullable<long> TDL_TREATMENT_ID { get; set; }
        public Nullable<long> DISPENSE_ID { get; set; }
        public string TDL_DISPENSE_CODE { get; set; }
        public Nullable<long> SUPPLIER_ID { get; set; }
        public string DOCUMENT_NUMBER { get; set; }
        public Nullable<long> DOCUMENT_DATE { get; set; }
        public Nullable<decimal> DOCUMENT_PRICE { get; set; }
        public Nullable<decimal> DISCOUNT { get; set; }
        public Nullable<decimal> DISCOUNT_RATIO { get; set; }
        public string DELIVERER { get; set; }
        public string TDL_TREATMENT_CODE { get; set; }
        public Nullable<long> TDL_PATIENT_ID { get; set; }
        public string TDL_PATIENT_CODE { get; set; }
        public string TDL_PATIENT_NAME { get; set; }
        public Nullable<long> TDL_PATIENT_DOB { get; set; }
        public string TDL_PATIENT_ADDRESS { get; set; }
        public Nullable<long> TDL_PATIENT_GENDER_ID { get; set; }
        public string TDL_PATIENT_GENDER_NAME { get; set; }
        public string TDL_PATIENT_FIRST_NAME { get; set; }
        public string TDL_PATIENT_LAST_NAME { get; set; }
        public Nullable<short> TDL_PATIENT_IS_HAS_NOT_DAY_DOB { get; set; }
        public string NATIONAL_IMP_MEST_CODE { get; set; }
        public Nullable<long> IMP_MEST_PROPOSE_ID { get; set; }
        public string CREDIT_ACCOUNT { get; set; }
        public string DEBIT_ACCOUNT { get; set; }
        public string INVOICE_SYMBOL { get; set; }
        public Nullable<long> CHMS_TYPE_ID { get; set; }
        public Nullable<long> CHMS_MEDI_STOCK_ID { get; set; }
        public Nullable<long> SOURCE_MEST_PERIOD_ID { get; set; }
        public string IMP_MEST_SUB_CODE { get; set; }
        public string TDL_BID_NAMES { get; set; }
        public string TDL_BID_GROUP_CODES { get; set; }
        public string TDL_BID_NUMBERS { get; set; }
        public string RECEIVER_LOGINNAME { get; set; }
        public string RECEIVER_USERNAME { get; set; }
        public string IMP_MEST_SUB_CODE_2 { get; set; }
        public Nullable<long> SPECIAL_MEDICINE_NUM_ORDER { get; set; }
        public Nullable<long> SPECIAL_MEDICINE_TYPE { get; set; }
        public Nullable<decimal> VIR_CREATE_YEAR { get; set; }
        public string VIR_SPECIAL_MEDICINE_NUM_ORDER { get; set; }
        public Nullable<long> MEDICAL_CONTRACT_ID { get; set; }
        public Nullable<long> APPROVAL_IMP_MEST_ID { get; set; }
        public Nullable<long> TRACKING_ID { get; set; }
        public Nullable<short> HAS_IDENTITY_MATERIAL { get; set; }
        public string IMP_MEST_TYPE_CODE { get; set; }
        public string IMP_MEST_TYPE_NAME { get; set; }
        public string IMP_MEST_STT_CODE { get; set; }
        public string IMP_MEST_STT_NAME { get; set; }
        public string MEDI_STOCK_CODE { get; set; }
        public string MEDI_STOCK_NAME { get; set; }
        public Nullable<short> IS_CABINET { get; set; }
        public Nullable<short> IS_ODD { get; set; }
        public Nullable<short> IS_MOBA_CHANGE_AMOUNT { get; set; }
        public string REQ_DEPARTMENT_CODE { get; set; }
        public string REQ_DEPARTMENT_NAME { get; set; }
        public string TDL_SERVICE_REQ_CODE { get; set; }
        public Nullable<long> TDL_INTRUCTION_TIME { get; set; }
        public Nullable<long> TDL_INTRUCTION_DATE { get; set; }
        public string TDL_AGGR_EXP_MEST_CODE { get; set; }
    }
}