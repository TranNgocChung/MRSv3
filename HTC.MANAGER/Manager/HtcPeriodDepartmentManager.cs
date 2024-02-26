using HTC.MANAGER.Core.PeriodBO;
using Inventec.Core;
using System;

namespace HTC.MANAGER.Manager
{
    public partial class HtcPeriodDepartmentManager : Inventec.Backend.MANAGER.ManagerBase
    {
        public HtcPeriodDepartmentManager(CommonParam param)
            : base(param)
        {

        }

        public T Get<T>(object data)
        {
            T result = default(T);
            try
            {
                if (!IsNotNull(data)) throw new ArgumentNullException("data");
                BusinessObject bo = new BusinessObject();
                bo.CopyCommonParamInfoGet(param);
                result = bo.Get<T>(data);
                CopyCommonParamInfo(bo);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = default(T);
            }
            return result;
        }
    }
}
