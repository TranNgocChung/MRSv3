using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.StagingObject
{
    public class HisEquipmentSetSO : StagingObjectBase
    {
        public HisEquipmentSetSO()
        {
            
        }

        public List<System.Linq.Expressions.Expression<Func<HIS_EQUIPMENT_SET, bool>>> listHisEquipmentSetExpression = new List<System.Linq.Expressions.Expression<Func<HIS_EQUIPMENT_SET, bool>>>();
    }
}
