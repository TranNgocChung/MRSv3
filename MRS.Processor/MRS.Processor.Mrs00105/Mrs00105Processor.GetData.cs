using MOS.MANAGER.HisService;
using MOS.MANAGER.HisMedicine;
using MOS.MANAGER.HisMaterial;
using FlexCel.Report;
using Inventec.Common.FlexCellExport;
using Inventec.Common.Logging;
using Inventec.Common.Repository;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MRS.MANAGER.Base;
using MRS.MANAGER.Config;
using MRS.MANAGER.Core.MrsReport;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisImpMest;
using MOS.MANAGER.HisImpMestMaterial;
using MOS.MANAGER.HisImpMestMedicine;
using MOS.MANAGER.HisMaterialType;
using MOS.MANAGER.HisMedicineType;
using MOS.MANAGER.HisMediStock;
using MOS.MANAGER.HisMediStockPeriod;
using MOS.MANAGER.HisMestPeriodMate;
using MOS.MANAGER.HisMestPeriodMedi;
using MOS.MANAGER.HisReportTypeCat;
using MOS.MANAGER.HisServiceRetyCat;
using MRS.Proccessor.Mrs00105;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using MRS.MANAGER.Core.MrsReport.RDO.RDOImpExpMestType;
using MOS.MANAGER.HisBidMedicineType;
using MOS.MANAGER.HisBidMaterialType;
using MOS.MANAGER.HisBid;
using MOS.MANAGER.HisAnticipateMety;
using MOS.MANAGER.HisAnticipateMaty;
using MOS.MANAGER.HisSupplier;
using MOS.MANAGER.HisExpMestReason;
using MOS.MANAGER.HisImpSource;
using MOS.MANAGER.HisMedicinePaty;
using MOS.MANAGER.HisMaterialPaty;
using MOS.MANAGER.HisBloodType;
using MOS.MANAGER.HisBlood;
using MOS.MANAGER.HisBidBloodType;
using MOS.MANAGER.HisDepartment;
using MOS.MANAGER.HisServiceMety;
using MOS.MANAGER.HisServiceMaty;
using MOS.MANAGER.HisExpMestBlood;
using MOS.MANAGER.HisImpMestBlood;
using MOS.DAO.Sql;

namespace MRS.Processor.Mrs00105
{
    public partial class Mrs00105Processor
    {
        protected override bool GetData()
        {
            var result = true;
            castFilter = (Mrs00105Filter)this.reportFilter;
            MapDataFilterToFilter();
            if (castFilter.IS_MEDICINE != true && castFilter.IS_MATERIAL != true && castFilter.IS_CHEMICAL_SUBSTANCE != true && castFilter.IS_BLOOD != true)
            {
                castFilter.IS_MEDICINE = true;
                castFilter.IS_MATERIAL = true;
                castFilter.IS_CHEMICAL_SUBSTANCE = true;
                castFilter.IS_BLOOD = true;
            }
            try
            {
                ListMediStock = HisMediStockCFG.HisMediStocks;

                ///Danh sách kho
                GetMediStock();

                //Neu ket qua loc khong co kho phu hop thi bao loi ve may tram
                if (ListMediStock.Count == 0)
                {
                    return true;
                }

                //kiem tra loc theo thang
                checkFilterMonth();

                //kiem tra loc thoi gian tao thau
                checkFilterBidCreateTime();

                //Tao loai nhap xuat
                makeRdo();

                //Loai thuoc, vat tu, mau
                GetMedicineTypeMaterialTypeBloodType();

                //lay thong tin cac kho cha da tung nhap thuoc vat tu cho tu truc
                GetChmsMediStockForImpMest();

                //lay thong tin kho cha
                GetMediStockMetyMaty();

                //Danh sách kho được chuyển
                GetImpMediStockOnTime();

                //Danh sách kho chuyển
                GetChmsMediStockOnTime();

                //Du lieu ton thuoc, vat tu, mau
                GetMediMateBloodPeriod();

                //Xuat thuoc, vat tu, mau
                GetExpMestMediMateBlood();

                //Nhap thuoc, vat tu, mau
                GetImpMestMediMateBlood();

                //Lay thong tin medicine, material, blood cua cac thuoc vat tu mau
                GetMediMateBlood();

                //Lay thong tin goi thau
                GetBid();

                //Lay cac thuoc vat tu dau vao kiem ke
                GetInputEndAmount();

                ///Danh sách khoa phòng yêu cầu xuất
                GetExpReqDepartment();

                ///Danh sách phiếu xuất trong kỳ
                GetExpMest();

                ///Danh sách đang dự trù
                GetAnticipate();

                ///Danh sách nhà cung cấp
                GetSupplier();

                ///Danh sách lí do xuất khác
                GetExpMestReason();

                ///Danh sách nguồn nhập
                GetImpSource();

                ///Danh sách chinh sach gia
                GetMediMatePaty();

                //Danh sách cấu hình giá theo giá nhập HIS_SALE_PROFIT_CFG
                GetSaleProfitCfg();

                ///Danh sách so luong thuoc vat tu trong thau
                GetBidMetyBidMatyAmount();

                //dịch vụ định mức
                GetServiceMetyMaty();

                //nhóm máu
                GetBloodAbo();

                //thông tin chứng từ
                GetDocumentInfo();

                //thông tin gộp chi tiết thầu
                GetGroupBidDetailInfo();

                //thông tin nhập từ thầu
                GetBidImpInfo();

                //thông tin lệch tiền hóa đơn
                GetDiffDocumentPrice();

                //thông tin tái sử dụng
                GetIsReusabling();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private void GetGroupBidDetailInfo()
        {
            //khi có điều kiện lọc từ template thì đổi sang key từ template
            if (this.dicDataFilter.ContainsKey("KEY_GROUP_BID_DETAIL") && this.dicDataFilter["KEY_GROUP_BID_DETAIL"] != null && !string.IsNullOrWhiteSpace(this.dicDataFilter["KEY_GROUP_BID_DETAIL"].ToString()))
            {
                this.KeyGroupBidDetail = this.dicDataFilter["KEY_GROUP_BID_DETAIL"].ToString();
            }
        }

        private void GetIsReusabling()
        {
            string sql = @"select
MATERIAL_ID,
(case when min(imp_time)>{0} then 2 else 1 end) IS_REUSABLING
from v_his_imp_mest_material 
where 1=1
and imp_mest_stt_id=5
and imp_mest_type_id in (17)
and imp_time <{1}
and material_type_id in (select id from his_material_type where is_reusable=1)
group by
material_id";
            CommonParam paramGet = new CommonParam();
            listIsReusabling = new SqlDAO().GetSql<MATERIAL_REUSABLING>(paramGet, string.Format(sql, castFilter.TIME_FROM, castFilter.TIME_TO));
        }

        private void GetImpMediStockOnTime()
        {
            castFilter.MEDI_STOCK_CODE__IMPs = string.Join(",", (new ManagerSql().GetMediStockCodeImps(ListMediStock.Select(o => o.ID).ToList(), castFilter) ?? new List<MEDI_STOCK_CODE_IMPs>()).Select(o => o.MEDI_STOCK_CODE).ToList());
        }

        private void GetChmsMediStockOnTime()
        {
            castFilter.MEDI_STOCK_CODE__CHMSs = string.Join(",", (new ManagerSql().GetMediStockCodeChmss(ListMediStock.Select(o => o.ID).ToList(), castFilter) ?? new List<MEDI_STOCK_CODE_IMPs>()).Select(o => o.MEDI_STOCK_CODE).ToList());
        }

        private void GetBidImpInfo()
        {
            if (castFilter.IS_ADD_BID_IMP_INFO == true)
            {
                var listBidImpMedi = new ManagerSql().GetBidImpMediInfo(ListMediStock.Select(o => o.ID).ToList(), castFilter, this.KeyGroupBidDetail) ?? new List<BID_IMP>();
                dicBidImpMedi = listBidImpMedi.GroupBy(o => o.KEY_BID_IMP).ToDictionary(p => p.Key, q => new BID_IMP() { KEY_BID_IMP = q.First().KEY_BID_IMP, BID_AMOUNT = q.First().BID_AMOUNT, BID_ADJUST_AMOUNT = q.First().BID_ADJUST_AMOUNT, BID_IMP_AMOUNT = q.Sum(s => s.BID_IMP_AMOUNT) });
                var listBidImpMate = new ManagerSql().GetBidImpMateInfo(ListMediStock.Select(o => o.ID).ToList(), castFilter, this.KeyGroupBidDetail) ?? new List<BID_IMP>();
                dicBidImpMate = listBidImpMate.GroupBy(o => o.KEY_BID_IMP).ToDictionary(p => p.Key, q => new BID_IMP() { KEY_BID_IMP = q.First().KEY_BID_IMP, BID_AMOUNT = q.First().BID_AMOUNT, BID_ADJUST_AMOUNT = q.First().BID_ADJUST_AMOUNT, BID_IMP_AMOUNT = q.Sum(s => s.BID_IMP_AMOUNT) });
                var listBidImpBlood = new ManagerSql().GetBidImpBloodInfo(ListMediStock.Select(o => o.ID).ToList(), castFilter, this.KeyGroupBidDetail) ?? new List<BID_IMP>();
                dicBidImpBlood = listBidImpBlood.GroupBy(o => o.KEY_BID_IMP).ToDictionary(p => p.Key, q => new BID_IMP() { KEY_BID_IMP = q.First().KEY_BID_IMP, BID_AMOUNT = q.First().BID_AMOUNT, BID_ADJUST_AMOUNT = q.First().BID_ADJUST_AMOUNT, BID_IMP_AMOUNT = q.Sum(s => s.BID_IMP_AMOUNT) });
            }
        }

        private void GetDocumentInfo()
        {
            var documentInfo = new ManagerSql().GetDocumentInfo() ?? new List<IMP_MEST>();
            if (documentInfo != null)
            {
                dicMedicineDocumentInfo = Medicines.ToDictionary(o => o.ID, p => documentInfo.Where(o => o.IMP_MEST_CODE == p.TDL_IMP_MEST_CODE).FirstOrDefault() ?? new IMP_MEST());
                dicMaterialDocumentInfo = Materials.ToDictionary(o => o.ID, p => documentInfo.Where(o => o.IMP_MEST_CODE == p.TDL_IMP_MEST_CODE).FirstOrDefault() ?? new IMP_MEST());
            }
        }

        private void GetDiffDocumentPrice()
        {
            var DiffDocumentPrice = new ManagerSql().GetDiffDocumentPrice(ListMediStock.Select(o => o.ID).ToList(), castFilter) ?? new List<Mrs00105RDO>();
            if (DiffDocumentPrice != null)
            {
                dicDiffDocumentPrice = DiffDocumentPrice.GroupBy(o => string.Format("{0}_{1}_{2}", o.MEDI_MATE_ID, o.MEDI_STOCK_ID, o.TYPE)).ToDictionary(p => p.Key, q => q.Sum(s => s.DIFF_DOCUMENT_TOTAL_PRICE));
            }
        }

        private void MapDataFilterToFilter()
        {
            if (this.dicDataFilter.ContainsKey("IS_BLOOD") && this.dicDataFilter["IS_BLOOD"] != null && !string.IsNullOrWhiteSpace(this.dicDataFilter["IS_BLOOD"].ToString()))
            {
                if (this.dicDataFilter["IS_BLOOD"].ToString().ToLower() == "true")
                {
                    castFilter.IS_BLOOD = true;
                }
                if (this.dicDataFilter["IS_BLOOD"].ToString().ToLower() == "false")
                {
                    castFilter.IS_BLOOD = false;
                }
            }
            if (this.dicDataFilter.ContainsKey("PARENT_SERVICE_CODEs") && this.dicDataFilter["PARENT_SERVICE_CODEs"] != null && !string.IsNullOrWhiteSpace(this.dicDataFilter["PARENT_SERVICE_CODEs"].ToString()))
            {
                var services = new HisServiceManager().Get(new HisServiceFilterQuery());
                if (services != null && services.Count > 0)
                {
                    castFilter.EXACT_PARENT_SERVICE_IDs = services.Where(o => string.Format(",{0},", this.dicDataFilter["PARENT_SERVICE_CODEs"].ToString()).Contains(string.Format(",{0},", o.SERVICE_CODE))).Select(p => p.ID).ToList();
                }
            }
        }

        private void GetServiceMetyMaty()
        {
            HisServiceMetyViewFilterQuery serviceMetyFilter = new HisServiceMetyViewFilterQuery();
            if (IsNotNullOrEmpty(castFilter.QUOTA_SERVICE_TYPE_IDs))//Fillter rỗng dẫn đến không lấy được dữ liệu trong khi vẫn phải lấy hết
            {
                serviceMetyFilter.SERVICE_TYPE_IDs = castFilter.QUOTA_SERVICE_TYPE_IDs;
            }
            List<V_HIS_SERVICE_METY> listServiceMety = new HisServiceMetyManager().GetView(serviceMetyFilter);
            if (listServiceMety != null)
            {
                foreach (var item in listServiceMety)
                {
                    if (castFilter.QUOTA_SERVICE_TYPE_IDs != null && !castFilter.QUOTA_SERVICE_TYPE_IDs.Contains(item.SERVICE_TYPE_ID))
                        continue;
                    ServiceMetyMaty rdo = new ServiceMetyMaty();
                    rdo.TYPE = THUOC;
                    rdo.MEDI_MATE_TYPE_ID = item.MEDICINE_TYPE_ID;
                    rdo.QUOTA_AMOUNT = item.EXPEND_AMOUNT;
                    rdo.SERVICE_NAME = item.SERVICE_NAME;
                    listServMetyMaty.Add(rdo);
                }
            }

            HisServiceMatyViewFilterQuery serviceMatyFilter = new HisServiceMatyViewFilterQuery();
            if (IsNotNullOrEmpty(castFilter.QUOTA_SERVICE_TYPE_IDs))//Fillter rỗng dẫn đến không lấy được dữ liệu trong khi vẫn phải lấy hết
            {
                serviceMatyFilter.SERVICE_TYPE_IDs = castFilter.QUOTA_SERVICE_TYPE_IDs;
            }
            List<V_HIS_SERVICE_MATY> listServiceMaty = new HisServiceMatyManager().GetView(serviceMatyFilter);
            if (listServiceMaty != null)
            {
                foreach (var item in listServiceMaty)
                {
                    if (castFilter.QUOTA_SERVICE_TYPE_IDs != null && !castFilter.QUOTA_SERVICE_TYPE_IDs.Contains(item.SERVICE_TYPE_ID))
                        continue;
                    ServiceMetyMaty rdo = new ServiceMetyMaty();
                    rdo.TYPE = VATTU;
                    rdo.MEDI_MATE_TYPE_ID = item.MATERIAL_TYPE_ID;
                    rdo.QUOTA_AMOUNT = item.EXPEND_AMOUNT;
                    rdo.SERVICE_NAME = item.SERVICE_NAME;
                    listServMetyMaty.Add(rdo);
                }
            }
        }

        private void GetBloodAbo()
        {
            this.listBloodAbo = new ManagerSql().GetBloodAbo() ?? new List<HIS_BLOOD_ABO>();
        }

        private void GetSaleProfitCfg()
        {
            SaleProfitCfgs = new ManagerSql().GetSaleProfitCfg();
        }

        private void GetMediMatePaty()
        {
            HisMedicinePatyFilterQuery MedicinePatyfilter = new HisMedicinePatyFilterQuery();
            MedicinePatys = new HisMedicinePatyManager().Get(MedicinePatyfilter);
            HisMaterialPatyFilterQuery MaterialPatyfilter = new HisMaterialPatyFilterQuery();
            MaterialPatys = new HisMaterialPatyManager().Get(MaterialPatyfilter);
        }

        private void GetBidMetyBidMatyAmount()
        {
            try
            {
                HisBidMedicineTypeFilterQuery BidMedicineTypefilter = new HisBidMedicineTypeFilterQuery();
                BidMedicineTypefilter.MEDICINE_TYPE_IDs = castFilter.MEDICINE_TYPE_IDs;
                BidMedicineTypefilter.BID_IDs = castFilter.BID_IDs;
                BidMedicineTypefilter.BID_ID = castFilter.BID_ID;
                var bidMedicineTypes = new HisBidMedicineTypeManager().Get(BidMedicineTypefilter);
                if (bidMedicineTypes != null && bidMedicineTypes.Count > 0)
                {
                    ListBidMety = bidMedicineTypes;
                }

                HisBidMaterialTypeFilterQuery BidMaterialTypefilter = new HisBidMaterialTypeFilterQuery();
                BidMaterialTypefilter.MATERIAL_TYPE_IDs = castFilter.MATERIAL_TYPE_IDs;
                BidMaterialTypefilter.BID_IDs = castFilter.BID_IDs;
                BidMaterialTypefilter.BID_ID = castFilter.BID_ID;
                var bidMaterialTypes = new HisBidMaterialTypeManager().Get(BidMaterialTypefilter);
                if (bidMaterialTypes != null && bidMaterialTypes.Count > 0)
                {
                    ListBidMaty = bidMaterialTypes;
                }

                HisBidBloodTypeFilterQuery BidBloodTypefilter = new HisBidBloodTypeFilterQuery();
                BidBloodTypefilter.BLOOD_TYPE_IDs = castFilter.BLOOD_TYPE_IDs;
                BidBloodTypefilter.BID_IDs = castFilter.BID_IDs;
                BidBloodTypefilter.BID_ID = castFilter.BID_ID;
                var bidBloodTypes = new HisBidBloodTypeManager().Get(BidBloodTypefilter);
                if (bidBloodTypes != null && bidBloodTypes.Count > 0)
                {
                    ListBidBlty = bidBloodTypes;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private void GetBid()
        {
            HisBidFilterQuery bidFilter = new HisBidFilterQuery();
            this.listBid = new HisBidManager().Get(bidFilter);
        }

        private void checkFilterMonth()
        {
            try
            {
                if (castFilter.MONTH != null)
                {
                    castFilter.TIME_FROM = Inventec.Common.DateTime.Get.StartMonth(Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(castFilter.MONTH ?? 0) ?? new DateTime());
                    castFilter.TIME_TO = Inventec.Common.DateTime.Get.EndMonth(Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(castFilter.MONTH ?? 0) ?? new DateTime());
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private void GetExpMest()
        {
            if (castFilter.TAKE_IMP_EXP_MEST == true)
            {
                CommonParam paramGet = new CommonParam();
                HisExpMestFilterQuery expMestFilter = new HisExpMestFilterQuery();
                expMestFilter.MEDI_STOCK_IDs = ListMediStock.Select(o => o.ID).ToList();
                expMestFilter.FINISH_TIME_FROM = castFilter.TIME_FROM;
                expMestFilter.FINISH_TIME_TO = castFilter.TIME_TO;
                expMestFilter.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE;
                var listExpMest = new HisExpMestManager(paramGet).Get(expMestFilter) ?? new List<HIS_EXP_MEST>();
                if (paramGet.HasException)
                    throw new NullReferenceException("Co exception xay ra tai DAOGET trong qua trinh lay du lieu MRS00105");
                listExpMestOn.AddRange(listExpMest);
                if (castFilter.TREATMENT_TYPE_IDs != null)
                {
                    listExpMestOn = listExpMestOn.Where(p => castFilter.TREATMENT_TYPE_IDs.Contains(1) && p.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DPK || (castFilter.TREATMENT_TYPE_IDs.Contains(3) || castFilter.TREATMENT_TYPE_IDs.Contains(4) || castFilter.TREATMENT_TYPE_IDs.Contains(2)) && p.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DDT).ToList();
                }
                HisImpMestFilterQuery impMestFilter = new HisImpMestFilterQuery();
                impMestFilter.MEDI_STOCK_IDs = ListMediStock.Select(o => o.ID).ToList();
                impMestFilter.IMP_TIME_FROM = castFilter.TIME_FROM;
                impMestFilter.IMP_TIME_TO = castFilter.TIME_TO;
                impMestFilter.IMP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__IMPORT;
                var listImpMest = new HisImpMestManager(paramGet).Get(impMestFilter) ?? new List<HIS_IMP_MEST>();
                if (paramGet.HasException)
                    throw new NullReferenceException("Co exception xay ra tai DAOGET trong qua trinh lay du lieu MRS00105");
                listImpMestOn.AddRange(listImpMest);
            }
        }

        private void GetExpReqDepartment()
        {
            try
            {
                listExpReqDepartment = HisDepartmentCFG.DEPARTMENTs.Where(o => listExpMestMedicine.Exists(p => p.EXP_TOTAL_AMOUNT > 0 && p.REQ_DEPARTMENT_ID == o.ID) || listExpMestMaterial.Exists(p => p.EXP_TOTAL_AMOUNT > 0 && p.REQ_DEPARTMENT_ID == o.ID)).ToList();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private void GetAnticipate()
        {
            try
            {
                if (castFilter.TAKE_ANTICIPATE_INFO == true)
                {
                    List<long> mediStockIds = ListMediStock.Select(o => o.ID).ToList();

                    if (mediStockIds != null && mediStockIds.Count > 0)
                    {
                        var AnticipateMetySub = new ManagerSql().GetAnticipateMety(this.castFilter, mediStockIds);
                        if (AnticipateMetySub != null)
                        {
                            AnticipateMetys.AddRange(AnticipateMetySub);
                        }
                        var AnticipateMatySub = new ManagerSql().GetAnticipateMaty(this.castFilter, mediStockIds);
                        if (AnticipateMatySub != null)
                        {
                            AnticipateMatys.AddRange(AnticipateMatySub);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private void GetSupplier()
        {
            try
            {
                HisSupplierFilterQuery Supplierfilter = new HisSupplierFilterQuery();
                Suppliers = new HisSupplierManager().Get(Supplierfilter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private void GetExpMestReason()
        {
            try
            {
                HisExpMestReasonFilterQuery ExpMestReasonfilter = new HisExpMestReasonFilterQuery();
                ExpMestReasons = new HisExpMestReasonManager().Get(ExpMestReasonfilter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private void GetImpSource()
        {
            try
            {
                HisImpSourceFilterQuery ImpSourcefilter = new HisImpSourceFilterQuery();
                ImpSources = new HisImpSourceManager().Get(ImpSourcefilter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private void GetChmsMediStockForImpMest()
        {
            try
            {
                MediMateIdChmsMediStockIds = new ManagerSql().GetChmsMediStockId(ListMediStock.Select(o => o.ID).ToList(), null, castFilter.TIME_FROM ?? 0, castFilter.TIME_TO ?? 0, castFilter.MEDICINE_TYPE_CODEs, castFilter.MATERIAL_TYPE_CODEs, castFilter.BLOOD_TYPE_CODEs, castFilter);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private void GetMediStockMetyMaty()
        {
            try
            {
                if (castFilter.EXP_MEDI_STOCK_IDs != null)
                {
                    listMediStockMety = new ManagerSql().GetMediStockMety(castFilter, ListMediStock.Select(o => o.ID).ToList()) ?? new List<MediStockMetyMaty>();
                    listMediStockMaty = new ManagerSql().GetMediStockMaty(castFilter, ListMediStock.Select(o => o.ID).ToList()) ?? new List<MediStockMetyMaty>();
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private void checkFilterBidCreateTime()
        {
            try
            {
                if (this.castFilter.BID_CREATE_TIME_FROM > 0 || this.castFilter.BID_CREATE_TIME_TO > 0)
                {
                    HisBidFilterQuery bidFilter = new HisBidFilterQuery();
                    bidFilter.CREATE_TIME_FROM = this.castFilter.BID_CREATE_TIME_FROM;
                    bidFilter.CREATE_TIME_TO = this.castFilter.BID_CREATE_TIME_TO;
                    var bids = new HisBidManager().Get(bidFilter) ?? new List<HIS_BID>();
                    this.castFilter.BID_IDs = bids.Select(o => o.ID).ToList();
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private void GetInputEndAmount()
        {
            try
            {
                if (castFilter.TAKE_INPUT_END_AMOUNT == true)
                {
                    InputMedicines = new ManagerSql().GetMediInput(this.castFilter, ListMediStock.Select(o => o.ID).ToList()) ?? new List<V_HIS_IMP_MEST_MEDICINE>();
                    InputMaterials = new ManagerSql().GetMateInput(this.castFilter, ListMediStock.Select(o => o.ID).ToList()) ?? new List<V_HIS_IMP_MEST_MATERIAL>();
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private void GetMediMateBlood()
        {
            try
            {
                List<long> medicineIds = new List<long>();
                if (listMestPeriodMedi != null)
                {
                    medicineIds.AddRange(listMestPeriodMedi.Select(o => o.MEDI_MATE_ID).ToList());
                }
                if (listMestPeriodMediEnd != null)
                {
                    medicineIds.AddRange(listMestPeriodMediEnd.Select(o => o.MEDI_MATE_ID).ToList());
                }
                if (listImpMestMedicine != null)
                {
                    medicineIds.AddRange(listImpMestMedicine.Select(o => o.MEDI_MATE_ID).ToList());
                }
                if (listExpMestMedicine != null)
                {
                    medicineIds.AddRange(listExpMestMedicine.Select(o => o.MEDI_MATE_ID).ToList());
                }

                medicineIds = medicineIds.Distinct().ToList();

                if (medicineIds != null && medicineIds.Count > 0)
                {
                    var skip = 0;
                    while (medicineIds.Count - skip > 0)
                    {
                        var limit = medicineIds.Skip(skip).Take(ManagerConstant.MAX_REQUEST_LENGTH_PARAM).ToList();
                        skip = skip + ManagerConstant.MAX_REQUEST_LENGTH_PARAM;
                        HisMedicineFilterQuery Medicinefilter = new HisMedicineFilterQuery();
                        Medicinefilter.IDs = limit;
                        var MedicineSub = new HisMedicineManager().Get(Medicinefilter);
                        Medicines.AddRange(MedicineSub);
                    }
                }

                List<long> materialIds = new List<long>();
                if (listMestPeriodMateEnd != null)
                {
                    materialIds.AddRange(listMestPeriodMateEnd.Select(o => o.MEDI_MATE_ID).ToList());
                }
                if (listMestPeriodMate != null)
                {
                    materialIds.AddRange(listMestPeriodMate.Select(o => o.MEDI_MATE_ID).ToList());
                }
                if (listImpMestMaterial != null)
                {
                    materialIds.AddRange(listImpMestMaterial.Select(o => o.MEDI_MATE_ID).ToList());
                }
                if (listExpMestMaterial != null)
                {
                    materialIds.AddRange(listExpMestMaterial.Select(o => o.MEDI_MATE_ID).ToList());
                }

                materialIds = materialIds.Distinct().ToList();

                if (materialIds != null && materialIds.Count > 0)
                {
                    var skip = 0;
                    while (materialIds.Count - skip > 0)
                    {
                        var limit = materialIds.Skip(skip).Take(ManagerConstant.MAX_REQUEST_LENGTH_PARAM).ToList();
                        skip = skip + ManagerConstant.MAX_REQUEST_LENGTH_PARAM;
                        HisMaterialFilterQuery Materialfilter = new HisMaterialFilterQuery();
                        Materialfilter.IDs = limit;
                        var MaterialSub = new HisMaterialManager().Get(Materialfilter);
                        Materials.AddRange(MaterialSub);
                    }
                }

                List<long> bloodIds = new List<long>();
                if (listMestPeriodBloodEnd != null)
                {
                    bloodIds.AddRange(listMestPeriodBloodEnd.Select(o => o.MEDI_MATE_ID).ToList());
                }
                if (listMestPeriodBlood != null)
                {
                    bloodIds.AddRange(listMestPeriodBlood.Select(o => o.MEDI_MATE_ID).ToList());
                }
                if (listImpMestBlood != null)
                {
                    bloodIds.AddRange(listImpMestBlood.Select(o => o.MEDI_MATE_ID).ToList());
                }
                if (listExpMestBlood != null)
                {
                    bloodIds.AddRange(listExpMestBlood.Select(o => o.MEDI_MATE_ID).ToList());
                }

                bloodIds = bloodIds.Distinct().ToList();

                if (bloodIds != null && bloodIds.Count > 0)
                {
                    var skip = 0;
                    while (bloodIds.Count - skip > 0)
                    {
                        var limit = bloodIds.Skip(skip).Take(ManagerConstant.MAX_REQUEST_LENGTH_PARAM).ToList();
                        skip = skip + ManagerConstant.MAX_REQUEST_LENGTH_PARAM;
                        HisBloodFilterQuery Bloodfilter = new HisBloodFilterQuery();
                        Bloodfilter.IDs = limit;
                        var BloodSub = new HisBloodManager().Get(Bloodfilter);
                        Bloods.AddRange(BloodSub);
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private void makeRdo()
        {
            //Danh sach loai nhap, loai xuat
            Dictionary<string, long> dicExpMestType = new Dictionary<string, long>();
            Dictionary<string, long> dicImpMestType = new Dictionary<string, long>();
            RDOImpExpMestTypeContext.Define(ref dicImpMestType, ref dicExpMestType);
            //Danh sach loai SL nhap, loai SL xuat
            PropertyInfo[] piAmount = Properties.Get<Mrs00105RDO>();

            foreach (var item in piAmount)
            {
                if (dicImpMestType.ContainsKey(item.Name))
                {
                    if (!dicImpAmountType.ContainsKey(dicImpMestType[item.Name])) dicImpAmountType[dicImpMestType[item.Name]] = item;
                }
                else if (dicExpMestType.ContainsKey(item.Name))
                {
                    if (!dicExpAmountType.ContainsKey(dicExpMestType[item.Name])) dicExpAmountType[dicExpMestType[item.Name]] = item;
                }
            }
        }

        private void GetMediMateBloodPeriod()
        {
            if (castFilter.IS_MEDICINE == true)
            {
                listMestPeriodMedi = new ManagerSql().GetMediPeriod(ListMediStock.Select(o => o.ID).ToList(), castFilter.TIME_FROM ?? 0, castFilter.MEDICINE_TYPE_CODEs, castFilter);
                listMestPeriodMediEnd = new ManagerSql().GetMediPeriod(ListMediStock.Select(o => o.ID).ToList(), (castFilter.TIME_TO ?? 0) + 1, castFilter.MEDICINE_TYPE_CODEs, castFilter);
            }

            if (castFilter.IS_MATERIAL == true || castFilter.IS_CHEMICAL_SUBSTANCE == true)
            {
                listMestPeriodMate = new ManagerSql().GetMatePeriod(ListMediStock.Select(o => o.ID).ToList(), castFilter.TIME_FROM ?? 0, castFilter.MATERIAL_TYPE_CODEs, castFilter);
                listMestPeriodMateEnd = new ManagerSql().GetMatePeriod(ListMediStock.Select(o => o.ID).ToList(), (castFilter.TIME_TO ?? 0) + 1, castFilter.MATERIAL_TYPE_CODEs, castFilter);
            }

            if (castFilter.IS_BLOOD == true)
            {
                listMestPeriodBlood = new ManagerSql().GetBloodPeriod(ListMediStock.Select(o => o.ID).ToList(), castFilter.TIME_FROM ?? 0, castFilter.BLOOD_TYPE_CODEs, castFilter);
                listMestPeriodBloodEnd = new ManagerSql().GetBloodPeriod(ListMediStock.Select(o => o.ID).ToList(), (castFilter.TIME_TO ?? 0) + 1, castFilter.BLOOD_TYPE_CODEs, castFilter);
            }
        }

        private void GetExpMestMediMateBlood()
        {
            if (castFilter.IS_MATERIAL == true || castFilter.IS_CHEMICAL_SUBSTANCE == true)
            {
                listExpMestMaterial = new ManagerSql().GetMateExp(ListMediStock.Select(o => o.ID).ToList(), castFilter);
            }

            if (castFilter.IS_MEDICINE == true)
            {
                listExpMestMedicine = new ManagerSql().GetMediExp(ListMediStock.Select(o => o.ID).ToList(), castFilter);
            }

            if (castFilter.IS_BLOOD == true)
            {
                listExpMestBlood = new ManagerSql().GetBloodExp(ListMediStock.Select(o => o.ID).ToList(), castFilter);
            }
        }

        private void GetImpMestMediMateBlood()
        {
            if (castFilter.IS_MATERIAL == true || castFilter.IS_CHEMICAL_SUBSTANCE == true)
            {
                listImpMestMaterial = new ManagerSql().GetMateImp(ListMediStock.Select(o => o.ID).ToList(), castFilter);
            }

            if (castFilter.IS_MEDICINE == true)
            {
                listImpMestMedicine = new ManagerSql().GetMediImp(ListMediStock.Select(o => o.ID).ToList(), castFilter);
            }

            if (castFilter.IS_BLOOD == true)
            {
                listImpMestBlood = new ManagerSql().GetBloodImp(ListMediStock.Select(o => o.ID).ToList(), castFilter);
            }
        }

        private void GetMedicineTypeMaterialTypeBloodType()
        {
            CommonParam paramGet = new CommonParam();
            var ListMedicineType = castFilter.IS_MEDICINE != null ? new HisMedicineTypeManager(paramGet).GetView(new HisMedicineTypeViewFilterQuery()) : new List<V_HIS_MEDICINE_TYPE>();
            var ListMaterialType = castFilter.IS_MATERIAL != null || castFilter.IS_CHEMICAL_SUBSTANCE != null ? new HisMaterialTypeManager(paramGet).GetView(new HisMaterialTypeViewFilterQuery()) : new List<V_HIS_MATERIAL_TYPE>();
            var ListBloodType = castFilter.IS_BLOOD != null ? new HisBloodTypeManager(paramGet).GetView(new HisBloodTypeViewFilterQuery()) : new List<V_HIS_BLOOD_TYPE>();
            if (IsNotNullOrEmpty(ListMedicineType))
            {
                foreach (var item in ListMedicineType)
                {
                    if (!string.IsNullOrWhiteSpace(this.castFilter.MEDICINE_TYPE_CODEs) && !string.Format(",{0},", this.castFilter.MEDICINE_TYPE_CODEs).Contains(string.Format(",{0},", item.MEDICINE_TYPE_CODE)))
                    {
                        continue;
                    }
                    dicMedicineType[item.ID] = item;
                }
            }

            if (IsNotNullOrEmpty(ListMaterialType))
            {
                foreach (var item in ListMaterialType)
                {
                    if (!string.IsNullOrWhiteSpace(this.castFilter.MATERIAL_TYPE_CODEs) && !string.Format(",{0},", this.castFilter.MATERIAL_TYPE_CODEs).Contains(string.Format(",{0},", item.MATERIAL_TYPE_CODE)))
                    {
                        continue;
                    }
                    dicMaterialType[item.ID] = item;
                }
            }

            if (IsNotNullOrEmpty(ListBloodType))
            {
                foreach (var item in ListBloodType)
                {
                    if (!string.IsNullOrWhiteSpace(this.castFilter.BLOOD_TYPE_CODEs) && !string.Format(",{0},", this.castFilter.BLOOD_TYPE_CODEs).Contains(string.Format(",{0},", item.BLOOD_TYPE_CODE)))
                    {
                        continue;
                    }
                    dicBloodType[item.ID] = item;
                }
            }
        }

        private void GetMediStock()
        {
            try
            {
                if (this.castFilter.MEDI_STOCK_IDs != null)
                {
                    ListMediStock = ListMediStock.Where(o => this.castFilter.MEDI_STOCK_IDs.Contains(o.ID)).ToList();
                }
                if (this.castFilter.MEDI_STOCK_ID != null)
                {
                    ListMediStock = ListMediStock.Where(o => this.castFilter.MEDI_STOCK_ID == o.ID).ToList();
                }
                if (this.castFilter.MEDI_STOCK_CABINET_IDs != null)
                {
                    ListMediStock = ListMediStock.Where(o => this.castFilter.MEDI_STOCK_CABINET_IDs.Contains(o.ID)).ToList();
                }
                if (this.castFilter.MEDI_STOCK_CABINET_ID != null)
                {
                    ListMediStock = ListMediStock.Where(o => this.castFilter.MEDI_STOCK_CABINET_ID == o.ID).ToList();
                }
                if (this.castFilter.IS_CABINET == true)//kiem tra loc chi lay tu truc
                {
                    ListMediStock = ListMediStock.Where(o => o.IS_CABINET == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE).ToList();
                }
                if (!string.IsNullOrWhiteSpace(this.castFilter.MEDI_STOCK_STR_CODEs))
                {
                    List<string> mss = this.castFilter.MEDI_STOCK_STR_CODEs.Split(',').ToList();
                    ListMediStock = ListMediStock.Where(o => mss.Contains(o.MEDI_STOCK_CODE ?? "")).ToList();
                }
                if (this.castFilter.BRANCH_ID != null)//kiem tra loc theo cơ sở
                {
                    var departmentIds = (new HisDepartmentManager().Get(new HisDepartmentFilterQuery() { BRANCH_ID = this.castFilter.BRANCH_ID }) ?? new List<HIS_DEPARTMENT>()).Select(o => o.ID).ToList();
                    ListMediStock = ListMediStock.Where(o => departmentIds.Contains(o.DEPARTMENT_ID)).ToList();
                }
                if (this.castFilter.DEPARTMENT_ID != null)//kiem tra loc theo khoa
                {
                    ListMediStock = ListMediStock.Where(o => o.DEPARTMENT_ID == this.castFilter.DEPARTMENT_ID).ToList();
                }
                if (this.castFilter.DEPARTMENT_IDs != null)//kiem tra loc theo nhieu khoa
                {
                    ListMediStock = ListMediStock.Where(o => this.castFilter.DEPARTMENT_IDs.Contains(o.DEPARTMENT_ID)).ToList();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
