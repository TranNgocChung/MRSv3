using TYT.DAO.StagingObject;
using TYT.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace TYT.DAO.TytMalaria
{
    public partial class TytMalariaDAO : EntityBase
    {
        public List<V_TYT_MALARIA> GetView(TytMalariaSO search, CommonParam param)
        {
            List<V_TYT_MALARIA> result = new List<V_TYT_MALARIA>();

            try
            {
                result = GetWorker.GetView(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }

            return result;
        }

        public TYT_MALARIA GetByCode(string code, TytMalariaSO search)
        {
            TYT_MALARIA result = null;

            try
            {
                result = GetWorker.GetByCode(code, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }
        
        public V_TYT_MALARIA GetViewById(long id, TytMalariaSO search)
        {
            V_TYT_MALARIA result = null;

            try
            {
                result = GetWorker.GetViewById(id, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }

        public V_TYT_MALARIA GetViewByCode(string code, TytMalariaSO search)
        {
            V_TYT_MALARIA result = null;

            try
            {
                result = GetWorker.GetViewByCode(code, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        public Dictionary<string, TYT_MALARIA> GetDicByCode(TytMalariaSO search, CommonParam param)
        {
            Dictionary<string, TYT_MALARIA> result = new Dictionary<string, TYT_MALARIA>();
            try
            {
                result = GetWorker.GetDicByCode(search, param);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }

            return result;
        }

        public bool ExistsCode(string code, long? id)
        {
            try
            {
                return CheckWorker.ExistsCode(code, id);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                throw;
            }
        }
    }
}
