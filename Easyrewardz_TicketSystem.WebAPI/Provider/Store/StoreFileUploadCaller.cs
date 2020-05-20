using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class StoreFileUploadCaller
    {
        #region Variable declaration

        private IStoreFileUpload _FileUpload;
        #endregion

        /// <summary>
        /// Get File Upload Logs
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        public List<FileUploadLogs> GetFileUploadLogs(IStoreFileUpload FileU, int tenantId, int fileuploadFor)
        {
            _FileUpload = FileU;
            return _FileUpload.GetFileUploadLogs(tenantId, fileuploadFor);
        }
        /// <summary>
        /// Create File Upload Log
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        public int CreateFileUploadLog(IStoreFileUpload FileU, int tenantid, string filename, bool isuploaded, string errorlogfilename,
           string successlogfilename, int createdby, string filetype, string succesFilepath, string errorFilepath, int fileuploadFor)
        {
            _FileUpload = FileU;
            return _FileUpload.CreateFileUploadLog(tenantid, filename, isuploaded, errorlogfilename, successlogfilename, createdby, filetype,
                  succesFilepath, errorFilepath, fileuploadFor);
        }
    }
}
