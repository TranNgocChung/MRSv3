using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
using MOS.EFMODEL.DataModels; 
using System.Reflection; 

namespace MRS.Processor.Mrs00191
{
    public class Mrs00191RDO : V_HIS_TREATMENT
    {
        public string AGE_MALE { get; set; }
        public string AGE_FEMALE { get; set; }

        public Mrs00191RDO(V_HIS_TREATMENT r)
        {
            PropertyInfo[] p = typeof(V_HIS_TREATMENT).GetProperties();
            foreach (var item in p)
            {
                item.SetValue(this, item.GetValue(r));
            }
        }
        public Mrs00191RDO()
        {
        }
    }
}
