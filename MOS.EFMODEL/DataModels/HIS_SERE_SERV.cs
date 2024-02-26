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
    
    public partial class HIS_SERE_SERV
    {
        public HIS_SERE_SERV()
        {
            this.HIS_EXP_MEST_BLOOD = new HashSet<HIS_EXP_MEST_BLOOD>();
            this.HIS_EXP_MEST_MATERIAL = new HashSet<HIS_EXP_MEST_MATERIAL>();
            this.HIS_EXP_MEST_MEDICINE = new HashSet<HIS_EXP_MEST_MEDICINE>();
            this.HIS_SERE_SERV_BILL = new HashSet<HIS_SERE_SERV_BILL>();
            this.HIS_SERE_SERV_DEBT = new HashSet<HIS_SERE_SERV_DEBT>();
            this.HIS_SERE_SERV_DEPOSIT = new HashSet<HIS_SERE_SERV_DEPOSIT>();
            this.HIS_SERE_SERV_DEPOSIT1 = new HashSet<HIS_SERE_SERV_DEPOSIT>();
            this.HIS_SERE_SERV_FILE = new HashSet<HIS_SERE_SERV_FILE>();
            this.HIS_SERE_SERV1 = new HashSet<HIS_SERE_SERV>();
            this.HIS_SERE_SERV_MATY = new HashSet<HIS_SERE_SERV_MATY>();
            this.HIS_SERE_SERV_PTTT = new HashSet<HIS_SERE_SERV_PTTT>();
            this.HIS_SERE_SERV_REHA = new HashSet<HIS_SERE_SERV_REHA>();
            this.HIS_SERE_SERV_SUIN = new HashSet<HIS_SERE_SERV_SUIN>();
            this.HIS_SERE_SERV_TEIN = new HashSet<HIS_SERE_SERV_TEIN>();
            this.HIS_SERVICE_CHANGE_REQ = new HashSet<HIS_SERVICE_CHANGE_REQ>();
            this.HIS_SERVICE_CHANGE_REQ1 = new HashSet<HIS_SERVICE_CHANGE_REQ>();
            this.HIS_SESE_DEPO_REPAY = new HashSet<HIS_SESE_DEPO_REPAY>();
            this.HIS_SESE_TRANS_REQ = new HashSet<HIS_SESE_TRANS_REQ>();
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
        public string TDL_SERVICE_DESCRIPTION { get; set; }
        public Nullable<short> TDL_IS_OUT_OF_DRG { get; set; }
        public Nullable<long> SERVICE_CONDITION_ID { get; set; }
        public Nullable<short> IS_ACCEPTING_NO_EXECUTE { get; set; }
        public string TDL_REQUEST_USER_TITLE { get; set; }
        public string DISCOUNT_LOGINNAME { get; set; }
        public string DISCOUNT_USERNAME { get; set; }
        public string NO_EXECUTE_REASON { get; set; }
        public Nullable<decimal> ACTUAL_PRICE { get; set; }
        public Nullable<short> IS_TEMP_BED_PROCESSED { get; set; }
        public Nullable<short> IS_NOT_USE_BHYT { get; set; }
        public Nullable<long> TDL_CARER_CARD_BORROW_ID { get; set; }
        public Nullable<long> TDL_RATION_TIME_ID { get; set; }
        public Nullable<short> IS_CONFIRM_NO_EXCUTE { get; set; }
        public string CONFIRM_NO_EXCUTE_REASON { get; set; }
        public Nullable<short> TDL_IS_VACCINE { get; set; }
    
        public virtual HIS_BRANCH HIS_BRANCH { get; set; }
        public virtual HIS_DEPARTMENT HIS_DEPARTMENT { get; set; }
        public virtual HIS_DEPARTMENT HIS_DEPARTMENT1 { get; set; }
        public virtual HIS_EKIP HIS_EKIP { get; set; }
        public virtual HIS_EQUIPMENT_SET HIS_EQUIPMENT_SET { get; set; }
        public virtual HIS_EXECUTE_GROUP HIS_EXECUTE_GROUP { get; set; }
        public virtual ICollection<HIS_EXP_MEST_BLOOD> HIS_EXP_MEST_BLOOD { get; set; }
        public virtual ICollection<HIS_EXP_MEST_MATERIAL> HIS_EXP_MEST_MATERIAL { get; set; }
        public virtual HIS_EXP_MEST_MATERIAL HIS_EXP_MEST_MATERIAL1 { get; set; }
        public virtual ICollection<HIS_EXP_MEST_MEDICINE> HIS_EXP_MEST_MEDICINE { get; set; }
        public virtual HIS_EXP_MEST_MEDICINE HIS_EXP_MEST_MEDICINE1 { get; set; }
        public virtual HIS_HEIN_APPROVAL HIS_HEIN_APPROVAL { get; set; }
        public virtual HIS_OTHER_PAY_SOURCE HIS_OTHER_PAY_SOURCE { get; set; }
        public virtual HIS_PACKAGE HIS_PACKAGE { get; set; }
        public virtual HIS_PATIENT HIS_PATIENT { get; set; }
        public virtual HIS_PATIENT_TYPE HIS_PATIENT_TYPE { get; set; }
        public virtual HIS_PATIENT_TYPE HIS_PATIENT_TYPE1 { get; set; }
        public virtual HIS_RATION_TIME HIS_RATION_TIME { get; set; }
        public virtual HIS_ROOM HIS_ROOM { get; set; }
        public virtual HIS_ROOM HIS_ROOM1 { get; set; }
        public virtual ICollection<HIS_SERE_SERV_BILL> HIS_SERE_SERV_BILL { get; set; }
        public virtual ICollection<HIS_SERE_SERV_DEBT> HIS_SERE_SERV_DEBT { get; set; }
        public virtual ICollection<HIS_SERE_SERV_DEPOSIT> HIS_SERE_SERV_DEPOSIT { get; set; }
        public virtual ICollection<HIS_SERE_SERV_DEPOSIT> HIS_SERE_SERV_DEPOSIT1 { get; set; }
        public virtual ICollection<HIS_SERE_SERV_FILE> HIS_SERE_SERV_FILE { get; set; }
        public virtual HIS_SERVICE HIS_SERVICE { get; set; }
        public virtual HIS_SERVICE_REQ_TYPE HIS_SERVICE_REQ_TYPE { get; set; }
        public virtual HIS_SERVICE_CONDITION HIS_SERVICE_CONDITION { get; set; }
        public virtual HIS_TREATMENT HIS_TREATMENT { get; set; }
        public virtual HIS_SERVICE_REQ HIS_SERVICE_REQ { get; set; }
        public virtual HIS_SERVICE_UNIT HIS_SERVICE_UNIT { get; set; }
        public virtual ICollection<HIS_SERE_SERV> HIS_SERE_SERV1 { get; set; }
        public virtual HIS_SERE_SERV HIS_SERE_SERV2 { get; set; }
        public virtual HIS_SERVICE_TYPE HIS_SERVICE_TYPE { get; set; }
        public virtual ICollection<HIS_SERE_SERV_MATY> HIS_SERE_SERV_MATY { get; set; }
        public virtual ICollection<HIS_SERE_SERV_PTTT> HIS_SERE_SERV_PTTT { get; set; }
        public virtual ICollection<HIS_SERE_SERV_REHA> HIS_SERE_SERV_REHA { get; set; }
        public virtual ICollection<HIS_SERE_SERV_SUIN> HIS_SERE_SERV_SUIN { get; set; }
        public virtual ICollection<HIS_SERE_SERV_TEIN> HIS_SERE_SERV_TEIN { get; set; }
        public virtual ICollection<HIS_SERVICE_CHANGE_REQ> HIS_SERVICE_CHANGE_REQ { get; set; }
        public virtual ICollection<HIS_SERVICE_CHANGE_REQ> HIS_SERVICE_CHANGE_REQ1 { get; set; }
        public virtual ICollection<HIS_SESE_DEPO_REPAY> HIS_SESE_DEPO_REPAY { get; set; }
        public virtual ICollection<HIS_SESE_TRANS_REQ> HIS_SESE_TRANS_REQ { get; set; }
    }
}
