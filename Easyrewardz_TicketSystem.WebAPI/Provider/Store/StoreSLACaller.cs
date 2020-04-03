using Easyrewardz_TicketSystem.Interface.StoreInterface;
using Easyrewardz_TicketSystem.Model.StoreModal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider.Store
{
    public class StoreSLACaller
    {
        #region Variable declaration

        private IStoreSLA _SLA;
        #endregion

        public int InsertStoreSLA(IStoreSLA SLA, StoreSLAModel SLAm)
        {
            _SLA = SLA;

            return _SLA.InsertStoreSLA(SLAm);
        }

        public int UpdateStoreSLA(IStoreSLA SLA, StoreSLAModel SLAm)
        {
            _SLA = SLA;

            return _SLA.UpdateStoreSLA(SLAm);
        }

        //public int UpdateSLA(IStoreSLA SLA, int tenantID, int SLAID, int IssuetypeID, bool isActive, int modifiedBy)
        //{
        //    _SLA = SLA;
        //    return _SLA.UpdateSLA(SLAID, tenantID, IssuetypeID, isActive, modifiedBy);
        //}

        public int DeleteStoreSLA(IStoreSLA SLA, int tenantID, int SLAID)
        {
            _SLA = SLA;
            return _SLA.DeleteStoreSLA(tenantID, SLAID);

        }

        public List<StoreSLAResponseModel> StoreSLAList(IStoreSLA SLA, int TenantID)
        {
            _SLA = SLA;
            return _SLA.StoreSLAList(TenantID);
        }

        public List<FunctionList> BindFunctionList(IStoreSLA SLA, int tenantID, string SearchText)
        {
            _SLA = SLA;
            return _SLA.BindFunctionList(tenantID, SearchText);
        }


        //public List<string> SLABulkUpload(IStoreSLA SLA, int TenantID, int CreatedBy, int SLAFor, DataSet DataSetCSV)
        //{
        //    _SLA = SLA;
        //    return _SLA.BulkUploadSLA(TenantID, CreatedBy, SLAFor, DataSetCSV);
        //}
    }
}
