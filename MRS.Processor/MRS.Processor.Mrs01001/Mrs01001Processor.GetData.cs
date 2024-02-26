using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisIcd;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MRS.Processor.Mrs01001
{
    public partial class Mrs01001Processor
    {
        private List<HIS_ICD> listIcd = new List<HIS_ICD>();
        private List<HIS_ICD> GetIcd()
        {
            List<HIS_ICD> result = null;
            try
            {
                HisIcdFilterQuery icdFilter = new HisIcdFilterQuery();
                icdFilter.IS_ACTIVE = 1;
                result = new HisIcdManager().Get(icdFilter);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        private string GetIcdNamesByCodes(string icdCodes)
        {
            string result = null;
            try
            {
                if (String.IsNullOrWhiteSpace(icdCodes) || !IsNotNullOrEmpty(listIcd))
                    return null;
                var listIcdCode = icdCodes.Split(';');
                if (IsNotNullOrEmpty(listIcdCode))
                {
                    List<string> icdNames = new List<string>();
                    foreach (var icdCode in listIcdCode)
                    {
                        if (String.IsNullOrWhiteSpace(icdCode)) continue;
                        var icd = listIcd.FirstOrDefault(o => o.ICD_CODE == icdCode);
                        if (icd != null) icdNames.Add(icd.ICD_NAME);
                    }
                    result = String.Join("; ", icdNames);
                }
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
