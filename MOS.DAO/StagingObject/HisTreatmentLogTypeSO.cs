using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisTreatmentLogTypeSO : StagingObjectBase
    {
        public HisTreatmentLogTypeSO()
        {
            //listHisTreatmentLogTypeExpression.Add(o => !o.IS_DELETE.HasValue || o.IS_DELETE.Value != (short)1);
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_TREATMENT_LOG_TYPE, bool>>> listHisTreatmentLogTypeExpression = new List<System.Linq.Expressions.Expression<Func<HIS_TREATMENT_LOG_TYPE, bool>>>();
    }
}
