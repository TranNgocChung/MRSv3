using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 

namespace MRS.Processor.Mrs00222
{
    /// <summary>
    /// Báo cáo thống kê dịch vụ kỹ thuật thanh toán BHYT 21 theo đối tượng thẻ (Sử dụng thẻ cấu hình để cấu hình thẻ, vd: 
    /// TQ497,DN497... )(Cấu hình HeinCardNumber__HeinType__01); 
    /// </summary>
    public class Mrs00222Filter
    {
        public long BRANCH_ID { get;  set;  }

        public long TIME_FROM { get;  set;  }
        public long TIME_TO { get;  set;  }

        public Mrs00222Filter() { }
    }
}
