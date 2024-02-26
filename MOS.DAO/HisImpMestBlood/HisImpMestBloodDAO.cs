using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisImpMestBlood
{
    public partial class HisImpMestBloodDAO : EntityBase
    {
        private HisImpMestBloodGet GetWorker
        {
            get
            {
                return (HisImpMestBloodGet)Worker.Get<HisImpMestBloodGet>();
            }
        }
        public List<HIS_IMP_MEST_BLOOD> Get(HisImpMestBloodSO search, CommonParam param)
        {
            List<HIS_IMP_MEST_BLOOD> result = new List<HIS_IMP_MEST_BLOOD>();
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

        public HIS_IMP_MEST_BLOOD GetById(long id, HisImpMestBloodSO search)
        {
            HIS_IMP_MEST_BLOOD result = null;
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
