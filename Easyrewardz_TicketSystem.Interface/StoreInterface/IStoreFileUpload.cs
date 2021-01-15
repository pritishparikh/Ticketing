using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IStoreFileUpload
    {
        /// <summary>
        /// Get File Upload Logs
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="fileuploadFor"></param>
        /// <returns></returns>
        List<FileUploadLogs> GetFileUploadLogs(int tenantId, int fileuploadFor);

        /// <summary>
        /// Create File Upload Log
        /// </summary>
        /// <param name="tenantid"></param>
        /// <param name="filename"></param>
        /// <param name="isuploaded"></param>
        /// <param name="errorlogfilename"></param>
        /// <param name="successlogfilename"></param>
        /// <param name="createdby"></param>
        /// <param name="filetype"></param>
        /// <param name="succesFilepath"></param>
        /// <param name="errorFilepath"></param>
        /// <param name="fileuploadFor"></param>
        /// <returns></returns>
        int CreateFileUploadLog(int tenantid, string filename, bool isuploaded, string errorlogfilename, string successlogfilename, int createdby, string filetype,
          string succesFilepath, string errorFilepath, int fileuploadFor);
    }
}
