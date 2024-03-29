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
    
    public partial class V_HIS_SERE_SERV_15
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
        public long SERVICE_ID { get; set; }
        public Nullable<long> SERVICE_REQ_ID { get; set; }
        public long PATIENT_TYPE_ID { get; set; }
        public Nullable<long> PRIMARY_PATIENT_TYPE_ID { get; set; }
        public Nullable<decimal> PRIMARY_PRICE { get; set; }
        public Nullable<decimal> LIMIT_PRICE { get; set; }
        public Nullable<long> PARENT_ID { get; set; }
        public Nullable<long> HEIN_APPROVAL_ID { get; set; }
        public string JSON_PATIENT_TYPE_ALTER { get; set; }
        public decimal AMOUNT { get; set; }
        public decimal PRICE { get; set; }
        public decimal ORIGINAL_PRICE { get; set; }
        public Nullable<decimal> HEIN_PRICE { get; set; }
        public Nullable<decimal> HEIN_RATIO { get; set; }
        public Nullable<decimal> HEIN_LIMIT_PRICE { get; set; }
        public Nullable<decimal> HEIN_LIMIT_RATIO { get; set; }
        public Nullable<decimal> HEIN_NORMAL_PRICE { get; set; }
        public Nullable<decimal> ADD_PRICE { get; set; }
        public Nullable<decimal> OVERTIME_PRICE { get; set; }
        public Nullable<decimal> DISCOUNT { get; set; }
        public decimal VAT_RATIO { get; set; }
        public Nullable<long> SHARE_COUNT { get; set; }
        public Nullable<long> STENT_ORDER { get; set; }
        public Nullable<short> IS_EXPEND { get; set; }
        public Nullable<short> IS_NO_PAY { get; set; }
        public Nullable<short> IS_NO_EXECUTE { get; set; }
        public Nullable<short> IS_OUT_PARENT_FEE { get; set; }
        public Nullable<short> IS_NO_HEIN_DIFFERENCE { get; set; }
        public Nullable<short> IS_SPECIMEN { get; set; }
        public Nullable<short> IS_ADDITION { get; set; }
        public Nullable<short> IS_SENT_EXT { get; set; }
        public Nullable<long> EXECUTE_TIME { get; set; }
        public string HEIN_CARD_NUMBER { get; set; }
        public Nullable<long> MEDICINE_ID { get; set; }
        public Nullable<long> MATERIAL_ID { get; set; }
        public Nullable<long> EXP_MEST_MEDICINE_ID { get; set; }
        public Nullable<long> EXP_MEST_MATERIAL_ID { get; set; }
        public Nullable<long> BLOOD_ID { get; set; }
        public Nullable<long> EKIP_ID { get; set; }
        public Nullable<long> PACKAGE_ID { get; set; }
        public Nullable<long> EQUIPMENT_SET_ID { get; set; }
        public Nullable<long> EQUIPMENT_SET_ORDER { get; set; }
        public long TDL_INTRUCTION_TIME { get; set; }
        public long TDL_INTRUCTION_DATE { get; set; }
        public Nullable<long> TDL_PATIENT_ID { get; set; }
        public Nullable<long> TDL_TREATMENT_ID { get; set; }
        public string TDL_TREATMENT_CODE { get; set; }
        public string TDL_SERVICE_CODE { get; set; }
        public string TDL_SERVICE_NAME { get; set; }
        public string TDL_HEIN_SERVICE_BHYT_CODE { get; set; }
        public string TDL_HEIN_SERVICE_BHYT_NAME { get; set; }
        public string TDL_HEIN_ORDER { get; set; }
        public long TDL_SERVICE_TYPE_ID { get; set; }
        public long TDL_SERVICE_UNIT_ID { get; set; }
        public Nullable<long> TDL_HEIN_SERVICE_TYPE_ID { get; set; }
        public string TDL_ACTIVE_INGR_BHYT_CODE { get; set; }
        public string TDL_ACTIVE_INGR_BHYT_NAME { get; set; }
        public string TDL_MEDICINE_CONCENTRA { get; set; }
        public string TDL_MEDICINE_BID_NUM_ORDER { get; set; }
        public string TDL_MEDICINE_REGISTER_NUMBER { get; set; }
        public string TDL_MEDICINE_PACKAGE_NUMBER { get; set; }
        public string TDL_SERVICE_REQ_CODE { get; set; }
        public long TDL_REQUEST_ROOM_ID { get; set; }
        public long TDL_REQUEST_DEPARTMENT_ID { get; set; }
        public string TDL_REQUEST_LOGINNAME { get; set; }
        public string TDL_REQUEST_USERNAME { get; set; }
        public long TDL_EXECUTE_ROOM_ID { get; set; }
        public long TDL_EXECUTE_DEPARTMENT_ID { get; set; }
        public long TDL_EXECUTE_BRANCH_ID { get; set; }
        public Nullable<long> TDL_EXECUTE_GROUP_ID { get; set; }
        public string TDL_SPECIALITY_CODE { get; set; }
        public long TDL_SERVICE_REQ_TYPE_ID { get; set; }
        public string TDL_HST_BHYT_CODE { get; set; }
        public string TDL_PACS_TYPE_CODE { get; set; }
        public Nullable<short> TDL_IS_MAIN_EXAM { get; set; }
        public Nullable<short> TDL_BILL_OPTION { get; set; }
        public string TDL_MATERIAL_GROUP_BHYT { get; set; }
        public Nullable<short> TDL_IS_SPECIFIC_HEIN_PRICE { get; set; }
        public Nullable<long> EXPEND_TYPE_ID { get; set; }
        public Nullable<long> INVOICE_ID { get; set; }
        public Nullable<short> USE_ORIGINAL_UNIT_FOR_PRES { get; set; }
        public Nullable<decimal> AMOUNT_TEMP { get; set; }
        public Nullable<short> IS_FUND_ACCEPTED { get; set; }
        public Nullable<short> IS_OTHER_SOURCE_PAID { get; set; }
        public Nullable<short> IS_NOT_PRES { get; set; }
        public Nullable<short> IS_USER_PACKAGE_PRICE { get; set; }
        public Nullable<decimal> PACKAGE_PRICE { get; set; }
        public Nullable<decimal> USER_PRICE { get; set; }
        public Nullable<decimal> PATIENT_PRICE_BHYT { get; set; }
        public Nullable<decimal> OTHER_SOURCE_PRICE { get; set; }
        public Nullable<decimal> VIR_PRICE { get; set; }
        public Nullable<decimal> VIR_PRICE_NO_ADD_PRICE { get; set; }
        public Nullable<decimal> VIR_PRICE_NO_EXPEND { get; set; }
        public Nullable<decimal> VIR_HEIN_PRICE { get; set; }
        public Nullable<decimal> VIR_PATIENT_PRICE { get; set; }
        public Nullable<decimal> VIR_PATIENT_PRICE_BHYT { get; set; }
        public Nullable<decimal> VIR_TOTAL_PRICE { get; set; }
        public Nullable<decimal> VIR_TOTAL_PRICE_NO_ADD_PRICE { get; set; }
        public Nullable<decimal> VIR_TOTAL_PRICE_NO_EXPEND { get; set; }
        public Nullable<decimal> VIR_TOTAL_HEIN_PRICE { get; set; }
        public Nullable<decimal> VIR_TOTAL_PATIENT_PRICE { get; set; }
        public Nullable<decimal> VIR_TOTAL_PATIENT_PRICE_BHYT { get; set; }
        public Nullable<decimal> VIR_TOTAL_PATIENT_PRICE_NO_DC { get; set; }
        public Nullable<decimal> VIR_TOTAL_PATIENT_PRICE_TEMP { get; set; }
        public Nullable<long> OTHER_PAY_SOURCE_ID { get; set; }
        public Nullable<long> TDL_SERVICE_TAX_RATE_TYPE { get; set; }
        public Nullable<decimal> CONFIG_HEIN_LIMIT_PRICE { get; set; }
        public long INTRUCTION_TIME { get; set; }
        public Nullable<long> FINISH_TIME { get; set; }
        public long SERVICE_REQ_STT_ID { get; set; }
        public string TREATMENT_INSTRUCTION { get; set; }
        public string NOTE { get; set; }
        public string TDL_HEIN_CARD_NUMBER { get; set; }
        public string TDL_PATIENT_CODE { get; set; }
        public string TDL_PATIENT_NAME { get; set; }
        public string TDL_PATIENT_FIRST_NAME { get; set; }
        public string TDL_PATIENT_LAST_NAME { get; set; }
        public long TDL_PATIENT_DOB { get; set; }
        public Nullable<short> TDL_PATIENT_IS_HAS_NOT_DAY_DOB { get; set; }
        public string TDL_PATIENT_ADDRESS { get; set; }
        public Nullable<long> TDL_PATIENT_GENDER_ID { get; set; }
        public string TDL_PATIENT_GENDER_NAME { get; set; }
        public string TDL_PATIENT_CAREER_NAME { get; set; }
        public string TDL_PATIENT_WORK_PLACE { get; set; }
        public string TDL_PATIENT_WORK_PLACE_NAME { get; set; }
        public string TDL_PATIENT_DISTRICT_CODE { get; set; }
        public string TDL_PATIENT_PROVINCE_CODE { get; set; }
        public string TDL_PATIENT_MILITARY_RANK_NAME { get; set; }
        public string TDL_PATIENT_NATIONAL_NAME { get; set; }
        public string TDL_HEIN_MEDI_ORG_CODE { get; set; }
        public string TDL_HEIN_MEDI_ORG_NAME { get; set; }
        public string TDL_PATIENT_AVATAR_URL { get; set; }
        public Nullable<long> PLAN_TIME_FROM { get; set; }
        public Nullable<long> PLAN_TIME_TO { get; set; }
        public Nullable<long> RATION_TIME_ID { get; set; }
        public Nullable<long> RATION_SUM_ID { get; set; }
        public long EXECUTE_ROOM_ID { get; set; }
        public long SERVICE_REQ_TYPE_ID { get; set; }
        public Nullable<short> IS_EMERGENCY { get; set; }
        public Nullable<long> PTTT_APPROVAL_STT_ID { get; set; }
        public string EXECUTE_DEPARTMENT_CODE { get; set; }
        public string EXECUTE_DEPARTMENT_NAME { get; set; }
        public long EXECUTE_BRANCH_ID { get; set; }
        public string REQUEST_DEPARTMENT_CODE { get; set; }
        public string REQUEST_DEPARTMENT_NAME { get; set; }
        public string INSTRUCTION_NOTE { get; set; }
    }
}
