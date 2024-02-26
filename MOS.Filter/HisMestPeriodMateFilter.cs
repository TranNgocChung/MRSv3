
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisMestPeriodMateFilter : FilterBase
    {
        public long? MEDI_STOCK_PERIOD_ID { get; set; }
        public long? MATERIAL_ID { get; set; }

        public List<long> MEDI_STOCK_PERIOD_IDs { get; set; }
        public List<long> MATERIAL_IDs { get; set; }

        public HisMestPeriodMateFilter()
            : base()
        {
        }
    }
}