using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 

namespace MRS.Processor.Mrs00257
{
    public class Mrs00257Filter
    {
        public long TIME_FROM { get;  set;  }
        public long TIME_TO { get;  set;  }
        public List<long> BID_IDs { get;  set;  }
        public long MEDI_STOCK_ID { get;  set;  }
        
        
    }
}
