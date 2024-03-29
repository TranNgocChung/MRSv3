﻿using MOS.EFMODEL.DataModels; 
using MOS.MANAGER.HisTreatment; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 

namespace MRS.Processor.Mrs00386
{
    public class Mrs00386RDO:HIS_TREATMENT
    {
        public Mrs00386RDO(HIS_TREATMENT r)
        {
            System.Reflection.PropertyInfo[] pi = Inventec.Common.Repository.Properties.Get<MOS.EFMODEL.DataModels.HIS_TREATMENT>();
            foreach (var item in pi)
            {
                item.SetValue(this, (item.GetValue(r)));
            }
        }
        public Dictionary<string,decimal> DIC_SERVICE_TYPE_AMOUNT { get; set; }
        public Dictionary<string, int> DIC_BEGIN { get; set; }
        public Dictionary<string, int> DIC_IMP { get; set; }
        public Dictionary<string, int> DIC_EXP { get; set; }
        public Dictionary<string, int> DIC_END { get; set; }
        public Dictionary<string, int> DIC_EXP_END_TYPE { get; set; }

        public Mrs00386RDO()
        {

        }
        public long TOTAL { get; set; }
        public long SICK { get; set; }
        public long HEIN { get; set; }
        public long FEE { get; set; }
        public long FREE { get; set; }
        public long KSK {get; set; }	
        public long CHILD {get; set; }
        public long HAS_MEDI {get; set; }
        public long ROOM_1 { get; set; }
        public long ROOM_2 { get; set; }
        public long ROOM_3 { get; set; }
        public long ROOM_4 { get; set; }
        public long ROOM_5 { get; set; }
        public long ROOM_6 { get; set; }
        public long ROOM_7 { get; set; }
        public long ROOM_8 { get; set; }
        public long ROOM_9 { get; set; }
        public long ROOM_10 { get; set; }
        public long ROOM_11 { get; set; }
        public long ROOM_12 { get; set; }
        public long ROOM_13 { get; set; }
        public long ROOM_14 { get; set; }
        public long ROOM_15 { get; set; }
        public long ROOM_16 { get; set; }
        public long ROOM_17 { get; set; }
        public long ROOM_18 { get; set; }
        public long ROOM_19 { get; set; }
        public long ROOM_20 { get; set; }
        public long ROOM_21 { get; set; }
        public long ROOM_22 { get; set; }
        public long ROOM_23 { get; set; }
        public long ROOM_24 { get; set; }
        public long ROOM_25 { get; set; }
        public long ROOM_26 { get; set; }
        public long ROOM_27 { get; set; }
        public long ROOM_28 { get; set; }
        public long ROOM_29 { get; set; }
        public long ROOM_30 { get; set; }
        public long ROOM_31 { get; set; }
        public long ROOM_32 { get; set; }
        public long ROOM_33 { get; set; }
        public long ROOM_34 { get; set; }
        public long ROOM_35 { get; set; }
        public long ROOM_36 { get; set; }
        public long ROOM_37 { get; set; }
        public long ROOM_38 { get; set; }
        public long ROOM_39 { get; set; }
        public long ROOM_40 { get; set; }
        public long ROOM_41 { get; set; }
        public long ROOM_42 { get; set; }
        public long ROOM_43 { get; set; }
        public long ROOM_44 { get; set; }
        public long ROOM_45 { get; set; }
        public long ROOM_46 { get; set; }
        public long ROOM_47 { get; set; }
        public long ROOM_48 { get; set; }
        public long ROOM_49 { get; set; }
        public long ROOM_50 { get; set; }

        public long E_ROOM_1 { get; set; } //E_ROOM: Các phòng kết thúc điều trị
        public long E_ROOM_2 { get; set; }
        public long E_ROOM_3 { get; set; }
        public long E_ROOM_4 { get; set; }
        public long E_ROOM_5 { get; set; }
        public long E_ROOM_6 { get; set; }
        public long E_ROOM_7 { get; set; }
        public long E_ROOM_8 { get; set; }
        public long E_ROOM_9 { get; set; }
        public long E_ROOM_10 { get; set; }
        public long E_ROOM_11 { get; set; }
        public long E_ROOM_12 { get; set; }
        public long E_ROOM_13 { get; set; }
        public long E_ROOM_14 { get; set; }
        public long E_ROOM_15 { get; set; }
        public long E_ROOM_16 { get; set; }
        public long E_ROOM_17 { get; set; }
        public long E_ROOM_18 { get; set; }
        public long E_ROOM_19 { get; set; }
        public long E_ROOM_20 { get; set; }
        public long E_ROOM_21 { get; set; }
        public long E_ROOM_22 { get; set; }
        public long E_ROOM_23 { get; set; }
        public long E_ROOM_24 { get; set; }
        public long E_ROOM_25 { get; set; }
        public long E_ROOM_26 { get; set; }
        public long E_ROOM_27 { get; set; }
        public long E_ROOM_28 { get; set; }
        public long E_ROOM_29 { get; set; }
        public long E_ROOM_30 { get; set; }
        public long E_ROOM_31 { get; set; }
        public long E_ROOM_32 { get; set; }
        public long E_ROOM_33 { get; set; }
        public long E_ROOM_34 { get; set; }
        public long E_ROOM_35 { get; set; }
        public long E_ROOM_36 { get; set; }
        public long E_ROOM_37 { get; set; }
        public long E_ROOM_38 { get; set; }
        public long E_ROOM_39 { get; set; }
        public long E_ROOM_40 { get; set; }
        public long E_ROOM_41 { get; set; }
        public long E_ROOM_42 { get; set; }
        public long E_ROOM_43 { get; set; }
        public long E_ROOM_44 { get; set; }
        public long E_ROOM_45 { get; set; }
        public long E_ROOM_46 { get; set; }
        public long E_ROOM_47 { get; set; }
        public long E_ROOM_48 { get; set; }
        public long E_ROOM_49 { get; set; }
        public long E_ROOM_50 { get; set; }

        public long F_ROOM_1 { get; set; }//F_ROOM: cac phong ket thuc kham tat ca cac cong kham co thuc hien
        public long F_ROOM_2 { get; set; }
        public long F_ROOM_3 { get; set; }
        public long F_ROOM_4 { get; set; }
        public long F_ROOM_5 { get; set; }
        public long F_ROOM_6 { get; set; }
        public long F_ROOM_7 { get; set; }
        public long F_ROOM_8 { get; set; }
        public long F_ROOM_9 { get; set; }
        public long F_ROOM_10 { get; set; }
        public long F_ROOM_11 { get; set; }
        public long F_ROOM_12 { get; set; }
        public long F_ROOM_13 { get; set; }
        public long F_ROOM_14 { get; set; }
        public long F_ROOM_15 { get; set; }
        public long F_ROOM_16 { get; set; }
        public long F_ROOM_17 { get; set; }
        public long F_ROOM_18 { get; set; }
        public long F_ROOM_19 { get; set; }
        public long F_ROOM_20 { get; set; }
        public long F_ROOM_21 { get; set; }
        public long F_ROOM_22 { get; set; }
        public long F_ROOM_23 { get; set; }
        public long F_ROOM_24 { get; set; }
        public long F_ROOM_25 { get; set; }
        public long F_ROOM_26 { get; set; }
        public long F_ROOM_27 { get; set; }
        public long F_ROOM_28 { get; set; }
        public long F_ROOM_29 { get; set; }
        public long F_ROOM_30 { get; set; }
        public long F_ROOM_31 { get; set; }
        public long F_ROOM_32 { get; set; }
        public long F_ROOM_33 { get; set; }
        public long F_ROOM_34 { get; set; }
        public long F_ROOM_35 { get; set; }
        public long F_ROOM_36 { get; set; }
        public long F_ROOM_37 { get; set; }
        public long F_ROOM_38 { get; set; }
        public long F_ROOM_39 { get; set; }
        public long F_ROOM_40 { get; set; }
        public long F_ROOM_41 { get; set; }
        public long F_ROOM_42 { get; set; }
        public long F_ROOM_43 { get; set; }
        public long F_ROOM_44 { get; set; }
        public long F_ROOM_45 { get; set; }
        public long F_ROOM_46 { get; set; }
        public long F_ROOM_47 { get; set; }
        public long F_ROOM_48 { get; set; }
        public long F_ROOM_49 { get; set; }
        public long F_ROOM_50 { get; set; }

        public long T_ROOM_1 { get; set; }//T_ROOM: cac phong co xu li kham nhap vien hoac ket thuc dieu tri
        public long T_ROOM_2 { get; set; }
        public long T_ROOM_3 { get; set; }
        public long T_ROOM_4 { get; set; }
        public long T_ROOM_5 { get; set; }
        public long T_ROOM_6 { get; set; }
        public long T_ROOM_7 { get; set; }
        public long T_ROOM_8 { get; set; }
        public long T_ROOM_9 { get; set; }
        public long T_ROOM_10 { get; set; }
        public long T_ROOM_11 { get; set; }
        public long T_ROOM_12 { get; set; }
        public long T_ROOM_13 { get; set; }
        public long T_ROOM_14 { get; set; }
        public long T_ROOM_15 { get; set; }
        public long T_ROOM_16 { get; set; }
        public long T_ROOM_17 { get; set; }
        public long T_ROOM_18 { get; set; }
        public long T_ROOM_19 { get; set; }
        public long T_ROOM_20 { get; set; }
        public long T_ROOM_21 { get; set; }
        public long T_ROOM_22 { get; set; }
        public long T_ROOM_23 { get; set; }
        public long T_ROOM_24 { get; set; }
        public long T_ROOM_25 { get; set; }
        public long T_ROOM_26 { get; set; }
        public long T_ROOM_27 { get; set; }
        public long T_ROOM_28 { get; set; }
        public long T_ROOM_29 { get; set; }
        public long T_ROOM_30 { get; set; }
        public long T_ROOM_31 { get; set; }
        public long T_ROOM_32 { get; set; }
        public long T_ROOM_33 { get; set; }
        public long T_ROOM_34 { get; set; }
        public long T_ROOM_35 { get; set; }
        public long T_ROOM_36 { get; set; }
        public long T_ROOM_37 { get; set; }
        public long T_ROOM_38 { get; set; }
        public long T_ROOM_39 { get; set; }
        public long T_ROOM_40 { get; set; }
        public long T_ROOM_41 { get; set; }
        public long T_ROOM_42 { get; set; }
        public long T_ROOM_43 { get; set; }
        public long T_ROOM_44 { get; set; }
        public long T_ROOM_45 { get; set; }
        public long T_ROOM_46 { get; set; }
        public long T_ROOM_47 { get; set; }
        public long T_ROOM_48 { get; set; }
        public long T_ROOM_49 { get; set; }
        public long T_ROOM_50 { get; set; }

        public long C_ROOM_1 { get; set; }//C_ROOM: số lượng bệnh nhân mới vòa viện theo phòng
        public long C_ROOM_2 { get; set; }
        public long C_ROOM_3 { get; set; }
        public long C_ROOM_4 { get; set; }
        public long C_ROOM_5 { get; set; }
        public long C_ROOM_6 { get; set; }
        public long C_ROOM_7 { get; set; }
        public long C_ROOM_8 { get; set; }
        public long C_ROOM_9 { get; set; }
        public long C_ROOM_10 { get; set; }
        public long C_ROOM_11 { get; set; }
        public long C_ROOM_12 { get; set; }
        public long C_ROOM_13 { get; set; }
        public long C_ROOM_14 { get; set; }
        public long C_ROOM_15 { get; set; }
        public long C_ROOM_16 { get; set; }
        public long C_ROOM_17 { get; set; }
        public long C_ROOM_18 { get; set; }
        public long C_ROOM_19 { get; set; }
        public long C_ROOM_20 { get; set; }
        public long C_ROOM_21 { get; set; }
        public long C_ROOM_22 { get; set; }
        public long C_ROOM_23 { get; set; }
        public long C_ROOM_24 { get; set; }
        public long C_ROOM_25 { get; set; }
        public long C_ROOM_26 { get; set; }
        public long C_ROOM_27 { get; set; }
        public long C_ROOM_28 { get; set; }
        public long C_ROOM_29 { get; set; }
        public long C_ROOM_30 { get; set; }
        public long C_ROOM_31 { get; set; }
        public long C_ROOM_32 { get; set; }
        public long C_ROOM_33 { get; set; }
        public long C_ROOM_34 { get; set; }
        public long C_ROOM_35 { get; set; }
        public long C_ROOM_36 { get; set; }
        public long C_ROOM_37 { get; set; }
        public long C_ROOM_38 { get; set; }
        public long C_ROOM_39 { get; set; }
        public long C_ROOM_40 { get; set; }
        public long C_ROOM_41 { get; set; }
        public long C_ROOM_42 { get; set; }
        public long C_ROOM_43 { get; set; }
        public long C_ROOM_44 { get; set; }
        public long C_ROOM_45 { get; set; }
        public long C_ROOM_46 { get; set; }
        public long C_ROOM_47 { get; set; }
        public long C_ROOM_48 { get; set; }
        public long C_ROOM_49 { get; set; }
        public long C_ROOM_50 { get; set; }

        public long KH_ROOM_1 { get; set; } //KH_ROOM: Các phòng kết thúc công khám không tích là hao phí
        public long KH_ROOM_2 { get; set; }
        public long KH_ROOM_3 { get; set; }
        public long KH_ROOM_4 { get; set; }
        public long KH_ROOM_5 { get; set; }
        public long KH_ROOM_6 { get; set; }
        public long KH_ROOM_7 { get; set; }
        public long KH_ROOM_8 { get; set; }
        public long KH_ROOM_9 { get; set; }
        public long KH_ROOM_10 { get; set; }
        public long KH_ROOM_11 { get; set; }
        public long KH_ROOM_12 { get; set; }
        public long KH_ROOM_13 { get; set; }
        public long KH_ROOM_14 { get; set; }
        public long KH_ROOM_15 { get; set; }
        public long KH_ROOM_16 { get; set; }
        public long KH_ROOM_17 { get; set; }
        public long KH_ROOM_18 { get; set; }
        public long KH_ROOM_19 { get; set; }
        public long KH_ROOM_20 { get; set; }
        public long KH_ROOM_21 { get; set; }
        public long KH_ROOM_22 { get; set; }
        public long KH_ROOM_23 { get; set; }
        public long KH_ROOM_24 { get; set; }
        public long KH_ROOM_25 { get; set; }
        public long KH_ROOM_26 { get; set; }
        public long KH_ROOM_27 { get; set; }
        public long KH_ROOM_28 { get; set; }
        public long KH_ROOM_29 { get; set; }
        public long KH_ROOM_30 { get; set; }
        public long KH_ROOM_31 { get; set; }
        public long KH_ROOM_32 { get; set; }
        public long KH_ROOM_33 { get; set; }
        public long KH_ROOM_34 { get; set; }
        public long KH_ROOM_35 { get; set; }
        public long KH_ROOM_36 { get; set; }
        public long KH_ROOM_37 { get; set; }
        public long KH_ROOM_38 { get; set; }
        public long KH_ROOM_39 { get; set; }
        public long KH_ROOM_40 { get; set; }
        public long KH_ROOM_41 { get; set; }
        public long KH_ROOM_42 { get; set; }
        public long KH_ROOM_43 { get; set; }
        public long KH_ROOM_44 { get; set; }
        public long KH_ROOM_45 { get; set; }
        public long KH_ROOM_46 { get; set; }
        public long KH_ROOM_47 { get; set; }
        public long KH_ROOM_48 { get; set; }
        public long KH_ROOM_49 { get; set; }
        public long KH_ROOM_50 { get; set; }

        public long PARENT_1 { get; set; }
        public long PARENT_2 { get;  set;  }
        public long PARENT_3 { get;  set;  }
        public long PARENT_4 { get;  set;  }
        public long PARENT_5 { get;  set;  }
        public long PARENT_6 { get;  set;  }
        public long PARENT_7 { get;  set;  }
        public long PARENT_8 { get;  set;  }
        public long PARENT_9 { get;  set;  }
        public long PARENT_10 { get;  set;  }
        public long PARENT_11 { get;  set;  }
        public long PARENT_12 { get;  set;  }
        public long PARENT_13 { get;  set;  }
        public long PARENT_14 { get;  set;  }
        public long PARENT_15 { get;  set;  }
        public long PARENT_16 { get;  set;  }
        public long PARENT_17 { get;  set;  }
        public long PARENT_18 { get;  set;  }
        public long PARENT_19 { get;  set;  }
        public long PARENT_20 { get;  set;  }
        public long PARENT_21 { get;  set;  }
        public long PARENT_22 { get;  set;  }
        public long PARENT_23 { get;  set;  }
        public long PARENT_24 { get;  set;  }
        public long PARENT_25 { get;  set;  }
        public long PARENT_26 { get;  set;  }
        public long PARENT_27 { get;  set;  }
        public long PARENT_28 { get;  set;  }
        public long PARENT_29 { get;  set;  }
        public long PARENT_30 { get;  set;  }
        public long PARENT_31 { get;  set;  }
        public long PARENT_32 { get;  set;  }
        public long PARENT_33 { get;  set;  }
        public long PARENT_34 { get;  set;  }
        public long PARENT_35 { get;  set;  }
        public long PARENT_36 { get;  set;  }
        public long PARENT_37 { get;  set;  }
        public long PARENT_38 { get;  set;  }
        public long PARENT_39 { get;  set;  }
        public long PARENT_40 { get;  set;  }
        public long PARENT_41 { get;  set;  }
        public long PARENT_42 { get;  set;  }
        public long PARENT_43 { get;  set;  }
        public long PARENT_44 { get;  set;  }
        public long PARENT_45 { get;  set;  }
        public long PARENT_46 { get;  set;  }
        public long PARENT_47 { get;  set;  }
        public long PARENT_48 { get;  set;  }
        public long PARENT_49 { get;  set;  }
        public long PARENT_50 { get;  set;  }

        public long E_PARENT_1 { get; set; }
        public long E_PARENT_2 { get; set; }
        public long E_PARENT_3 { get; set; }
        public long E_PARENT_4 { get; set; }
        public long E_PARENT_5 { get; set; }
        public long E_PARENT_6 { get; set; }
        public long E_PARENT_7 { get; set; }
        public long E_PARENT_8 { get; set; }
        public long E_PARENT_9 { get; set; }
        public long E_PARENT_10 { get; set; }
        public long E_PARENT_11 { get; set; }
        public long E_PARENT_12 { get; set; }
        public long E_PARENT_13 { get; set; }
        public long E_PARENT_14 { get; set; }
        public long E_PARENT_15 { get; set; }
        public long E_PARENT_16 { get; set; }
        public long E_PARENT_17 { get; set; }
        public long E_PARENT_18 { get; set; }
        public long E_PARENT_19 { get; set; }
        public long E_PARENT_20 { get; set; }
        public long E_PARENT_21 { get; set; }
        public long E_PARENT_22 { get; set; }
        public long E_PARENT_23 { get; set; }
        public long E_PARENT_24 { get; set; }
        public long E_PARENT_25 { get; set; }
        public long E_PARENT_26 { get; set; }
        public long E_PARENT_27 { get; set; }
        public long E_PARENT_28 { get; set; }
        public long E_PARENT_29 { get; set; }
        public long E_PARENT_30 { get; set; }
        public long E_PARENT_31 { get; set; }
        public long E_PARENT_32 { get; set; }
        public long E_PARENT_33 { get; set; }
        public long E_PARENT_34 { get; set; }
        public long E_PARENT_35 { get; set; }
        public long E_PARENT_36 { get; set; }
        public long E_PARENT_37 { get; set; }
        public long E_PARENT_38 { get; set; }
        public long E_PARENT_39 { get; set; }
        public long E_PARENT_40 { get; set; }
        public long E_PARENT_41 { get; set; }
        public long E_PARENT_42 { get; set; }
        public long E_PARENT_43 { get; set; }
        public long E_PARENT_44 { get; set; }
        public long E_PARENT_45 { get; set; }
        public long E_PARENT_46 { get; set; }
        public long E_PARENT_47 { get; set; }
        public long E_PARENT_48 { get; set; }
        public long E_PARENT_49 { get; set; }
        public long E_PARENT_50 { get; set; }

        public long CLN_HEIN { get; set; }
        public long CLN_FEE { get; set; }
        public long CLN_FREE { get; set; }	
        public long TRAN_OUT {get; set; }
        public long LEFT_LINE {get; set; }
        public long HEN_KHAM { get; set; }

        public long CLN_BHYT { get; set; }

        public long CLN_VP { get; set; }

        public long CLN_MP { get; set; }

        public long TRANSFER_OUT { get; set; }

        public string IN_DATE_STR { get; set; }

        public int KSKs { get; set; }

        public int COUNT_BHYT { get; set; }

        public int COUNT_KSK { get; set; }

        public int COUNT_VP { get; set; }

        public decimal KSK_TOTAL_PRICE { get; set; }

        public decimal VP_TOTAL_PRICE { get; set; }

        public decimal BHYT_TOTAL_PRICE { get; set; }

        public int MALE { get; set; }

        public int FEMALE { get; set; }

        //tong theo ho so dieu tri

        public long T_TOTAL { get; set; }
        public long T_SICK { get; set; }
        public long T_HEIN { get; set; }
        public long T_FEE { get; set; }
        public long T_FREE { get; set; }
        public long T_KSK { get; set; }
        public long T_CHILD { get; set; }
        public long T_HAS_MEDI { get; set; }
        public long T_CLN_HEIN { get; set; }
        public long T_CLN_FEE { get; set; }
        public long T_CLN_FREE { get; set; }
        public long T_TRAN_OUT { get; set; }
        public long T_LEFT_LINE { get; set; }
        public long T_HEN_KHAM { get; set; }

        public int T_KSKs { get; set; }


        public long E_TOTAL { get; set; }

        public long E_SICK { get; set; }

        public long E_HEIN { get; set; }

        public long E_FEE { get; set; }

        public long E_FREE { get; set; }

        public long E_KSKs { get; set; }

        public long E_KSK { get; set; }

        public long E_CHILD { get; set; }

        public long E_HAS_MEDI { get; set; }

        public long E_CLN_HEIN { get; set; }

        public long E_CLN_FEE { get; set; }

        public long E_CLN_FREE { get; set; }

        public long E_TRAN_OUT { get; set; }

        public long E_LEFT_LINE { get; set; }

        public long KH_TOTAL { get; set; }

        public long KH_SICK { get; set; }

        public long KH_HEIN { get; set; }

        public long KH_FEE { get; set; }

        public long KH_FREE { get; set; }

        public long KH_KSKs { get; set; }

        public long KH_KSK { get; set; }

        public long KH_CHILD { get; set; }

        public long KH_HAS_MEDI { get; set; }

        public long KH_CLN_HEIN { get; set; }

        public long KH_CLN_FEE { get; set; }

        public long KH_CLN_FREE { get; set; }

        public long KH_TRAN_OUT { get; set; }

        public long KH_LEFT_LINE { get; set; }

        public long KH_HEN_KHAM { get; set; }
    }
    class Title
    {
        public string ROOM_NAME_1 { get; set; }
        public string ROOM_NAME_2 { get; set; }
        public string ROOM_NAME_3 { get; set; }
        public string ROOM_NAME_4 { get; set; }
        public string ROOM_NAME_5 { get; set; }
        public string ROOM_NAME_6 { get; set; }
        public string ROOM_NAME_7 { get; set; }
        public string ROOM_NAME_8 { get; set; }
        public string ROOM_NAME_9 { get; set; }
        public string ROOM_NAME_10 { get; set; }
        public string ROOM_NAME_11 { get; set; }
        public string ROOM_NAME_12 { get; set; }
        public string ROOM_NAME_13 { get; set; }
        public string ROOM_NAME_14 { get; set; }
        public string ROOM_NAME_15 { get; set; }
        public string ROOM_NAME_16 { get; set; }
        public string ROOM_NAME_17 { get; set; }
        public string ROOM_NAME_18 { get; set; }
        public string ROOM_NAME_19 { get; set; }
        public string ROOM_NAME_20 { get; set; }
        public string ROOM_NAME_21 { get; set; }
        public string ROOM_NAME_22 { get; set; }
        public string ROOM_NAME_23 { get; set; }
        public string ROOM_NAME_24 { get; set; }
        public string ROOM_NAME_25 { get; set; }
        public string ROOM_NAME_26 { get; set; }
        public string ROOM_NAME_27 { get; set; }
        public string ROOM_NAME_28 { get; set; }
        public string ROOM_NAME_29 { get; set; }
        public string ROOM_NAME_30 { get; set; }
        public string ROOM_NAME_31 { get; set; }
        public string ROOM_NAME_32 { get; set; }
        public string ROOM_NAME_33 { get; set; }
        public string ROOM_NAME_34 { get; set; }
        public string ROOM_NAME_35 { get; set; }
        public string ROOM_NAME_36 { get; set; }
        public string ROOM_NAME_37 { get; set; }
        public string ROOM_NAME_38 { get; set; }
        public string ROOM_NAME_39 { get; set; }
        public string ROOM_NAME_40 { get; set; }
        public string ROOM_NAME_41 { get; set; }
        public string ROOM_NAME_42 { get; set; }
        public string ROOM_NAME_43 { get; set; }
        public string ROOM_NAME_44 { get; set; }
        public string ROOM_NAME_45 { get; set; }
        public string ROOM_NAME_46 { get; set; }
        public string ROOM_NAME_47 { get; set; }
        public string ROOM_NAME_48 { get; set; }
        public string ROOM_NAME_49 { get; set; }
        public string ROOM_NAME_50 { get; set; }

        public string PARENT_NAME_1 { get; set; }
        public string PARENT_NAME_2 { get; set; }
        public string PARENT_NAME_3 { get; set; }
        public string PARENT_NAME_4 { get; set; }
        public string PARENT_NAME_5 { get; set; }
        public string PARENT_NAME_6 { get; set; }
        public string PARENT_NAME_7 { get; set; }
        public string PARENT_NAME_8 { get; set; }
        public string PARENT_NAME_9 { get; set; }
        public string PARENT_NAME_10 { get; set; }
        public string PARENT_NAME_11 { get; set; }
        public string PARENT_NAME_12 { get; set; }
        public string PARENT_NAME_13 { get; set; }
        public string PARENT_NAME_14 { get; set; }
        public string PARENT_NAME_15 { get; set; }
        public string PARENT_NAME_16 { get; set; }
        public string PARENT_NAME_17 { get; set; }
        public string PARENT_NAME_18 { get; set; }
        public string PARENT_NAME_19 { get; set; }
        public string PARENT_NAME_20 { get; set; }
        public string PARENT_NAME_21 { get; set; }
        public string PARENT_NAME_22 { get; set; }
        public string PARENT_NAME_23 { get; set; }
        public string PARENT_NAME_24 { get; set; }
        public string PARENT_NAME_25 { get; set; }
        public string PARENT_NAME_26 { get; set; }
        public string PARENT_NAME_27 { get; set; }
        public string PARENT_NAME_28 { get; set; }
        public string PARENT_NAME_29 { get; set; }
        public string PARENT_NAME_30 { get; set; }
        public string PARENT_NAME_31 { get; set; }
        public string PARENT_NAME_32 { get; set; }
        public string PARENT_NAME_33 { get; set; }
        public string PARENT_NAME_34 { get; set; }
        public string PARENT_NAME_35 { get; set; }
        public string PARENT_NAME_36 { get; set; }
        public string PARENT_NAME_37 { get; set; }
        public string PARENT_NAME_38 { get; set; }
        public string PARENT_NAME_39 { get; set; }
        public string PARENT_NAME_40 { get; set; }
        public string PARENT_NAME_41 { get; set; }
        public string PARENT_NAME_42 { get; set; }
        public string PARENT_NAME_43 { get; set; }
        public string PARENT_NAME_44 { get; set; }
        public string PARENT_NAME_45 { get; set; }
        public string PARENT_NAME_46 { get; set; }
        public string PARENT_NAME_47 { get; set; }
        public string PARENT_NAME_48 { get; set; }
        public string PARENT_NAME_49 { get; set; }
        public string PARENT_NAME_50 { get; set; }
    }
}
