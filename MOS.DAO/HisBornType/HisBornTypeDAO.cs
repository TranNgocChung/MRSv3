using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBornType
{
    public partial class HisBornTypeDAO : EntityBase
    {
        private HisBornTypeGet GetWorker
        {
            get
            {
                return (HisBornTypeGet)Worker.Get<HisBornTypeGet>();
            }
        }
        public List<HIS_BORN_TYPE> Get(HisBornTypeSO search, CommonParam param)
        {
            List<HIS_BORN_TYPE> result = new List<HIS_BORN_TYPE>();
            try
            {
                result = GetWorker.Get(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }
            return result;
        }

        public HIS_BORN_TYPE GetById(long id, HisBornTypeSO search)
        {
            HIS_BORN_TYPE result = null;
            try
            {
                result = GetWorker.GetById(id, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }
    }
}
