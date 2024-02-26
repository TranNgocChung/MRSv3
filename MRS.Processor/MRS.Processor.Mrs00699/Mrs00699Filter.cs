using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Processor.Mrs00699
{
    public class Mrs00699Filter
    {
        public long? TIME_FROM { get; set; }
        public long? TIME_TO { get; set; }
        public List<long> TREATMENT_TYPE_IDs { get; set; }
        public bool? IS_VIENPHI { set; get; }
        public bool? IS_DICHVU { get; set; }
        public bool? IS_BHXH { get; set; }

        public List<long> PATIENT_TYPE_IDs { get; set; }
    }
}
