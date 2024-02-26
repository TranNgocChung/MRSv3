using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
using MOS.EFMODEL.DataModels; 

namespace MRS.Processor.Mrs01001
{
    public class Mrs01001RDO
    {
        public string HEIN_MEDI_ORG_CODE { get; set; }
        public long TDL_TREATMENT_TYPE_ID { get; set; }
        public long ICD_GROUP_ID { get; set; }
        public string ICD_GROUP_CODE { get; set; }
        public string ICD_GROUP_NAME { get; set; }
        public string ICD_CODE { get; set; }
        public string ICD_NAME { get; set; }
        public string ATTACH_ICD_CODES { get; set; }
        public string ATTACH_ICD_NAMES { get; set; }

        public int EARLIER_TOTAL_COUNT_TREATMENT { get; set; }//tổng số ca khám chữa bệnh
        public int EARLIER_TOTAL_COUNT_TREATMENT_ABNORMAL { get; set; }//tổng số ca khám chữa bệnh bất thường (Đến khám và điều trị sinh 2 hồ sơ điều trị trong ngày nhưng lần sau nhập viện, thì tính lần 1 khám là 1 lượt bất thường)
        public decimal EARLIER_TOTAL_HEIN_PRICE { get; set; }//Tiền BHTT

        public int LATER_TOTAL_COUNT_TREATMENT { get; set; }//tổng số ca khám chữa bệnh
        public int LATER_TOTAL_COUNT_TREATMENT_ABNORMAL { get; set; }//tổng số ca khám chữa bệnh bất thường (Đến khám và điều trị sinh 2 hồ sơ điều trị trong ngày nhưng lần sau nhập viện, thì tính lần 1 khám là 1 lượt bất thường)
        public decimal LATER_TOTAL_HEIN_PRICE { get; set; }//Tiền BHTT
    }

}
