using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Processor.Mrs00826
{
    public class Mrs00826Filter
    {
        public long TIME_FROM { get; set; }
        public long TIME_TO { get; set; }
        public short? INPUT_DATA_ID__TIME_TYPE { get; set; }//1: thời gian vào viện, 2: ra viện 3: chỉ định 4: duyệt giám định bảo hiểm

        public List<long> TREATMENT_TYPE_IDs { get; set; }
    }
}
