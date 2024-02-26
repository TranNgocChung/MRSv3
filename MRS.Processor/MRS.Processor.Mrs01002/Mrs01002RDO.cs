using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
using MOS.EFMODEL.DataModels; 

namespace MRS.Processor.Mrs01002
{
    public class Mrs01002RDO
    {
        public long TDL_TREATMENT_TYPE_ID { get; set; }
        public long ICD_GROUP_ID { get; set; }
        public string ICD_CODE { get; set; }
        public string ICD_NAME { get; set; }
        public string ICD_CODE_SUB { get; set; }
        public string ICD_GROUP_CODE { get; set; }
        public string ICD_GROUP_NAME { get; set; }

        public decimal? TOTAL_TONG { get; set; }
        public decimal? TOTAL_VAO_VIEN { get; set; }
        public decimal? TOTAL_TU_THANG_TRUOC { get; set; }
        public decimal? TOTAL_GIOITINH_NU { get; set; }
        public decimal? TOTAL_GIOITINH_NAM { get; set; }
        public decimal? TOTAL_RA_VIEN { get; set; }
        public decimal? TOTAL_CHUYEN_VIEN { get; set; }
        public decimal? TOTAL_CHUYEN_KHOA { get; set; }
        public decimal? TOTAL_TU_VONG { get; set; }
        public decimal? TOTAL_TREATMENT_DAY { get; set; }

        public Dictionary<string, int> DIC_DEATH_24H_AMOUNT { get; set; }
        public Dictionary<string, int> DIC_DEATH_FEMALE_24H_AMOUNT { get; set; }

        public decimal? TOTAL_LESS_THAN_15_AGE { get; set; }
        public decimal? TOTAL_DEATH_LESS_THAN_15_AGE { get; set; }
        public decimal? TOTAL_TREATMENT_DAY_LESS_THAN_15_AGE { get; set; }

        public decimal? TOTAL_LESS_THAN_6_AGE { get; set; }
        public decimal? TOTAL_DEATH_LESS_THAN_6_AGE { get; set; }
        public decimal? TOTAL_TREATMENT_DAY_LESS_THAN_6_AGE { get; set; }
    }

    public class Mrs01002RDORDO_PARENT
    {
        public HIS_ICD_GROUP ICD_GROUP { get; set; }
    }
}
