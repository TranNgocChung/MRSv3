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
    
    public partial class HIS_EYE_SURGRY_DESC
    {
        public HIS_EYE_SURGRY_DESC()
        {
            this.HIS_SERE_SERV_PTTT = new HashSet<HIS_SERE_SERV_PTTT>();
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
        public long LOAI_PT_MAT { get; set; }
        public Nullable<long> CHAN_DOAN { get; set; }
        public string NGUYEN_NHAN { get; set; }
        public Nullable<long> GIAI_DOAN_BENH { get; set; }
        public Nullable<long> MAT_PHAU_THUAT { get; set; }
        public Nullable<long> PP_PHAU_THUAT { get; set; }
        public Nullable<short> PP_PT_CAT_MONG_MAT { get; set; }
        public Nullable<long> PP_VO_CAM { get; set; }
        public string THUOC_TE { get; set; }
        public Nullable<long> CO_DINH_NHAN_CAU { get; set; }
        public string TAO_VAT_KM_KINH_TUYEN { get; set; }
        public Nullable<long> TAO_VAT_KM_VI_TRI { get; set; }
        public Nullable<long> TINH_TRANG_BAO_TENON { get; set; }
        public Nullable<long> TINH_TRANG_T3 { get; set; }
        public Nullable<short> UC_CHE_TAO_XO { get; set; }
        public Nullable<long> UC_CHE_TAO_XO_TT_BS { get; set; }
        public string UC_CHE_TAO_XO_TT_BS_KHAC { get; set; }
        public Nullable<long> UC_CHE_TAO_XO_VITRI { get; set; }
        public string UC_CHE_TAO_XO_THOIGIAN { get; set; }
        public Nullable<short> LANG_BOT_BAO_TENON { get; set; }
        public string VAT_CM_HINHDANG { get; set; }
        public Nullable<decimal> VAT_CM_KICHTHUOC { get; set; }
        public Nullable<short> CHOC_TP { get; set; }
        public Nullable<long> CAT_MAU_BE { get; set; }
        public Nullable<short> CAT_MONG_MAT { get; set; }
        public Nullable<decimal> KHAU_NAP_CM_SO_MUI { get; set; }
        public Nullable<short> KHAU_NAP_CM_CHIRUT { get; set; }
        public string KHAU_NAP_CM_LOAICHI { get; set; }
        public Nullable<long> TAI_TAO_TP { get; set; }
        public Nullable<long> KHAU_KM { get; set; }
        public Nullable<decimal> KHAU_KM_SOMUI { get; set; }
        public Nullable<long> KHAU_KM_LOAICHI { get; set; }
        public string KHAU_KM_LOAICHI_BS { get; set; }
        public Nullable<long> MO_KM_RIA { get; set; }
        public string MO_KM_RIA_KINH_TUYEN { get; set; }
        public Nullable<long> MO_VAO_TP { get; set; }
        public string MO_VAO_TP_KINH_TUYEN { get; set; }
        public Nullable<decimal> MO_VAO_TP_KICH_THUOC { get; set; }
        public Nullable<short> MO_VAO_TP_RACH_PHU { get; set; }
        public Nullable<short> NHUOM_BAO { get; set; }
        public Nullable<long> LOAI_MO_BAO { get; set; }
        public Nullable<short> TACH_NHAN { get; set; }
        public Nullable<long> XOAY_NHAN { get; set; }
        public Nullable<short> DAY_NHAN { get; set; }
        public Nullable<long> CACH_DAY_NHAN { get; set; }
        public Nullable<decimal> TAN_NHAN_NANG_LUONG { get; set; }
        public Nullable<decimal> TAN_NHAN_LUC_HUT { get; set; }
        public string TAN_NHAN_TOC_DO_DC { get; set; }
        public Nullable<long> HUT_CHAT_T3 { get; set; }
        public Nullable<long> DAT_IOL_LOAI { get; set; }
        public Nullable<long> DAT_IOL_CACH_THUC { get; set; }
        public Nullable<long> RACH_BAO_SAU { get; set; }
        public string RACH_BAO_SAU_VI_TRI { get; set; }
        public Nullable<decimal> RACH_BAO_SAU_KICH_THUOC { get; set; }
        public Nullable<short> CAT_BAO_SAU { get; set; }
        public Nullable<long> CAT_BAO_SAU_CACH_THUC { get; set; }
        public Nullable<short> CAT_MONG_MAT_NGOAI_VI { get; set; }
        public string CAT_MONG_MAT_NGOAI_VI_VITRI { get; set; }
        public Nullable<long> PHUC_HOI_VET_MO { get; set; }
        public Nullable<short> DAT_VANH_MI { get; set; }
        public Nullable<short> TIEM_LIDOCANIE_THAN_MONG { get; set; }
        public Nullable<short> CAT_DAU_MONG { get; set; }
        public Nullable<short> PHAN_TICH_THAN_MONG { get; set; }
        public Nullable<short> DOT_CAM_MAU { get; set; }
        public Nullable<short> GOT_GIAC_MAC_DAU_MONG { get; set; }
        public Nullable<long> LAY_MANH_KM_SAT_RIA { get; set; }
        public string LAY_MANH_KM_SAT_RIA_KT { get; set; }
        public Nullable<short> LAY_MANH_MANG_OI { get; set; }
        public string LAY_MANH_MANG_OI_KT { get; set; }
        public Nullable<long> KHAU_MANH_GHEP_CHI { get; set; }
        public Nullable<decimal> KHAU_MANH_GHEP_CHI_SO_MUI { get; set; }
        public Nullable<short> KHAU_KM_CHE_PHAN_CAT_KM { get; set; }
        public Nullable<decimal> KHAU_KM_CHE_PHAN_CAT_KM_SO_MUI { get; set; }
        public Nullable<long> BIEN_CHUNG { get; set; }
        public string XU_TRI_BIEN_CHUNG { get; set; }
        public Nullable<short> TIEM_MAT { get; set; }
        public Nullable<long> TIEM_MAT_TT_BO_SUNG { get; set; }
        public string TIEM_MAT_THUOC { get; set; }
        public Nullable<long> TRA_MAT { get; set; }
        public string TRA_MAT_THUOC { get; set; }
        public Nullable<short> TRA_MAT_BANG_EP { get; set; }
        public string TRA_MAT_BANG_TT { get; set; }
        public string DIEN_BIEN_KHAC { get; set; }
        public Nullable<short> CHAN_DOAN_SUP_MI_MT { get; set; }
        public Nullable<long> CHAN_DOAN_SUP_MI_MT_DO { get; set; }
        public Nullable<short> CHAN_DOAN_SUP_MI_MP { get; set; }
        public Nullable<long> CHAN_DOAN_SUP_MI_MP_DO { get; set; }
        public Nullable<short> PP_PT_CO_NANG_MI { get; set; }
        public Nullable<long> PP_PT_TREO_MI_CO_TRAN { get; set; }
        public Nullable<long> VI_TRI_DUONG_RACH { get; set; }
        public string THUOC_TE_TAI_CHO { get; set; }
        public Nullable<short> PHAU_TICH_DA_DU { get; set; }
        public Nullable<short> CAT_CO_VONG_MI { get; set; }
        public Nullable<short> TACH_CO_NANG_MI_KHOI_KM { get; set; }
        public Nullable<long> CAT_NGAN_CO_NANG_MI { get; set; }
        public Nullable<short> GAP_CO_NANG_MI { get; set; }
        public Nullable<long> GAP_CO_NANG_MI_KHOANG { get; set; }
        public Nullable<short> TREO_MI_CO_TRAN { get; set; }
        public Nullable<long> TREO_MI_CO_TRAN_LOAI { get; set; }
        public Nullable<short> KHAU_CO_DINH_CO_NANG_MI { get; set; }
        public Nullable<short> LUON_CHI_HINH_NGU_GIAC { get; set; }
        public string KHAU_DA_MI_TAO_MI_CHI { get; set; }
        public Nullable<long> CHAN_DOAN_DUT_LE_QUAN { get; set; }
        public Nullable<long> DUT_LE_QUAN_GIO_THU { get; set; }
        public Nullable<long> PP_PHAU_THUAT_LE_QUAN { get; set; }
        public Nullable<long> PHAU_THUAT_LAN_THU { get; set; }
        public Nullable<short> PP_VO_CAM_DUOI_HOC { get; set; }
        public Nullable<short> PP_VO_CAM_MUI { get; set; }
        public Nullable<short> PP_VO_CAM_VUNG_TUI_LE { get; set; }
        public Nullable<short> PP_VO_CAM_THE0_VET_RACH_MI { get; set; }
        public Nullable<short> LAY_DI_VAT { get; set; }
        public Nullable<short> LE_QUAN_LANH_DUT { get; set; }
        public Nullable<short> TIM_DAU_DUT_NGOAI { get; set; }
        public Nullable<short> TIM_DAU_DUT_TRONG { get; set; }
        public Nullable<long> DAU_DUT_TRONG_VITRI { get; set; }
        public Nullable<long> DAT_LE_QUAN { get; set; }
        public string CHI_NOI_2_DAU_LE_QUAN { get; set; }
        public Nullable<short> TAI_TAO_MI_KET_MAC { get; set; }
        public Nullable<short> TAI_TAO_MI_MO_DUOI_DA { get; set; }
        public Nullable<short> TAI_TAO_MI_DA { get; set; }
        public Nullable<long> CO_DINH_ONG_SILICON { get; set; }
        public string NYLON_CO_DINH_ONG_SILICON { get; set; }
        public Nullable<long> CHAN_DOAN_NGHI_NGO_GLOCOM { get; set; }
        public Nullable<long> CHAN_DOAN_GLOCOM { get; set; }
        public Nullable<long> LASER_YAG_NANG_LUONG { get; set; }
        public string LASER_YAG_NANG_LUONG_KHAC { get; set; }
        public Nullable<long> LASER_YAG_DIEM_NO { get; set; }
        public Nullable<decimal> VI_TRI_CAT_MONG_CHU_BIEN { get; set; }
        public Nullable<long> CD_DUC_BAO_SAU_SAU_MO_TTT { get; set; }
        public Nullable<long> HINH_DANG_MO_BAO_SAU { get; set; }
    
        public virtual ICollection<HIS_SERE_SERV_PTTT> HIS_SERE_SERV_PTTT { get; set; }
    }
}