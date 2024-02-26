using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisIcd;
using MOS.MANAGER.HisIcdGroup;
using MRS.MANAGER.Config;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MRS.Processor.Mrs01002
{
    public partial class Mrs01002Processor
    {
        private List<HIS_ICD> listIcds = new List<HIS_ICD>();
        private List<HIS_ICD_GROUP> listIcdGroups = new List<HIS_ICD_GROUP>();
        private List<V_HIS_TREATMENT> listOuts = new List<V_HIS_TREATMENT>();
        private List<V_HIS_TREATMENT> listDeaths = new List<V_HIS_TREATMENT>();
        private List<V_HIS_TREATMENT> listDepaTrans = new List<V_HIS_TREATMENT>();

        private List<HIS_ICD> GetIcd()
        {
            HisIcdFilterQuery icdFilter = new HisIcdFilterQuery();
            icdFilter.IS_ACTIVE = 1;
            return new HisIcdManager().Get(icdFilter);
        }
        private List<HIS_ICD_GROUP> GetIcdGroup()
        {
            HisIcdGroupFilterQuery icdGroupFilter = new HisIcdGroupFilterQuery();
            icdGroupFilter.IS_ACTIVE = 1;
            return new HisIcdGroupManager().Get(icdGroupFilter);
        }

        private string GetIcdNamesByCodes(string icdCodes)
        {
            string result = null;
            try
            {
                if (String.IsNullOrWhiteSpace(icdCodes) || !IsNotNullOrEmpty(listIcds))
                    return null;
                var listIcdCode = icdCodes.Split(';');
                if (IsNotNullOrEmpty(listIcdCode))
                {
                    List<string> icdNames = new List<string>();
                    foreach (var icdCode in listIcdCode)
                    {
                        if (String.IsNullOrWhiteSpace(icdCode)) continue;
                        var icd = listIcds.FirstOrDefault(o => o.ICD_CODE == icdCode);
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

        private void GetTreatments()
        {
            //Ra viện,Chuyển viện,Tử vong
            this.listOuts = new ManagerSql().GetHoSoRavien(filter) ?? new List<V_HIS_TREATMENT>();
            this.listDeaths = this.listOuts.Where(o => o.TREATMENT_RESULT_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_RESULT.ID__CHET || o.TREATMENT_END_TYPE_ID == HisTreatmentEndTypeCFG.TREATMENT_END_TYPE_ID__DEATH).ToList();
            //Chuyển khoa
            this.listDepaTrans = new ManagerSql().GetHoSoChuyenKhoa(filter) ?? new List<V_HIS_TREATMENT>();
        }


    }
}
