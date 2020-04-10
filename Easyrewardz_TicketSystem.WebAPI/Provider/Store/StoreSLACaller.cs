using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Interface.StoreInterface;
using Easyrewardz_TicketSystem.Model.StoreModal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider.Store
{
    public class StoreSLACaller
    {
        #region Variable declaration

        private IStoreSLA _SLA;
        private IFileUpload _FileUpload;
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

        public StoreSLAResponseModel GetStoreSLADetail(IStoreSLA SLA, int TenantID, int SLAID)
        {
            _SLA = SLA;
            return _SLA.GetStoreSLADetail(TenantID, SLAID);
        }

        public List<string> StoreSLABulkUpload(IStoreSLA SLA, int TenantID, int CreatedBy, DataSet DataSetCSV)
        {
            _SLA = SLA;
            return _SLA.StoreBulkUploadSLA(TenantID, CreatedBy, DataSetCSV);
        }

        public int CreateFileUploadLog(IFileUpload FileU, int tenantid, string filename, bool isuploaded, string errorlogfilename,
           string successlogfilename, int createdby, string filetype, string succesFilepath, string errorFilepath, int fileuploadFor)
        {
            _FileUpload = FileU;
            return _FileUpload.CreateFileUploadLog(tenantid, filename, isuploaded, errorlogfilename, successlogfilename, createdby, filetype,
                  succesFilepath, errorFilepath, fileuploadFor);
        }
    }
}
