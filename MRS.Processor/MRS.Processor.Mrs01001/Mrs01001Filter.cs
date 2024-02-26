using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 

namespace MRS.Processor.Mrs01001
{
    public class Mrs01001Filter
    {
        public long EARLIER_TIME_FROM { get; set; }
        public long EARLIER_TIME_TO { get; set; }
        public long LATER_TIME_FROM { get; set; }
        public long LATER_TIME_TO { get; set; }
        public short? INPUT_DATA_ID_STT_TYPE { get; set; } //trạng thái bệnh nhân: 1: Đang điều trị; 2: Đã kết thúc; 3: Đã khóa viện phí; 4: Đã duyệt BHYT
        public List<string> ICD_CODEs { get; set; }
    }
}
