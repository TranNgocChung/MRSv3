using System.Collections.Generic;

namespace MRS.Processor.Mrs01002
{
    public class Mrs01002Filter
    {
        public long TIME_FROM { get; set; }
        public long TIME_TO { get; set; }
        public List<long> DEPARTMENT_IDs { get; set; }
        public List<string> ICD_CODEs { get; set; }
    }
}
