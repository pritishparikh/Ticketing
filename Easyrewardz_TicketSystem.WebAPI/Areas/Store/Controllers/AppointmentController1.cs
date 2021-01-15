using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Easyrewardz_TicketSystem.WebAPI.Areas.Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public partial class AppointmentController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string _connectioSting;
        private readonly string _radisCacheServerAddress;
        private readonly string rootPath;
        private readonly string BulkUpload;
        private readonly string UploadFiles;
        private readonly string DownloadFile;
        #endregion

        #region Cunstructor
        public AppointmentController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
            rootPath = configuration.GetValue<string>("APIURL");
            BulkUpload = configuration.GetValue<string>("BulkUpload");
            UploadFiles = configuration.GetValue<string>("Uploadfiles");
            DownloadFile = configuration.GetValue<string>("Downloadfile");
        }
        #endregion

        #region Custom Methods

        /// <summary>
        /// Get Appointment List
        /// </summary>
        /// <param name="AppDate"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetAppointmentList")]
        public ResponseModel GetAppointmentList(string AppDate)
        {
            List<AppointmentModel> objAppointmentList = new List<AppointmentModel>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                AppointmentCaller newAppointment = new AppointmentCaller();

                objAppointmentList = newAppointment.GetAppointmentList(new AppointmentServices(_connectioSting), authenticate.TenantId,authenticate.UserMasterID, AppDate);

                statusCode =
                objAppointmentList.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objAppointmentList;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Get Appointment Count
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetAppointmentCount")]
        public ResponseModel GetAppointmentCount()
        {
            List<AppointmentCount> objAppointmentCount = new List<AppointmentCount>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                AppointmentCaller newAppointment = new AppointmentCaller();

                objAppointmentCount = newAppointment.GetAppointmentCountList(new AppointmentServices(_connectioSting), authenticate.TenantId,authenticate.ProgramCode, authenticate.UserMasterID);

                statusCode =
                objAppointmentCount.Count == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = objAppointmentCount;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;



        }

        /// <summary>
        /// Update Appointment Status
        /// </summary>
        /// <param name="appointment"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateAppointmentStatus")]
        public ResponseModel UpdateAppointmentStatus([FromBody]AppointmentCustomer appointment)
        {
            AppointmentCaller newAppointment = new AppointmentCaller();
            ResponseModel objResponseModel = new ResponseModel();
            int StatusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                int result = newAppointment.UpdateAppointmentStatus(new AppointmentServices(_connectioSting), appointment, authenticate.TenantId, authenticate.ProgramCode, authenticate.UserMasterID);
                StatusCode =
                result == 0 ?
                       (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;
                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)StatusCode);
                objResponseModel.Status = true;
                objResponseModel.StatusCode = StatusCode;
                objResponseModel.Message = statusMessage;

            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        /// <summary>
        /// <summary>
        /// Get Appointment Message Tags
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("AppointmentMessageTags")]
        public ResponseModel AppointmentMessageTags()
        {
            List<AppointmentMessageTag> appointmentsTags = new List<AppointmentMessageTag>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                ////Get token (Double encrypted) and get the tenant id 
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                AppointmentCaller newAppointment = new AppointmentCaller();

                appointmentsTags = newAppointment.AppointmentMessageTags(new AppointmentServices(_connectioSting));

                statusCode =
                appointmentsTags.Count > 0 ?  (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.RecordNotFound;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = appointmentsTags;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;



        }

        #region TimeSlotMaster CRUD


        /// <summary>
        /// Insert/ Update HSTimeSlot Setting
        /// </summary>
        /// <param name="StoreTimeSlotInsertUpdate"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("InsertUpdateTimeSlotSetting")]
        public ResponseModel InsertUpdateTimeSlotSetting([FromBody]StoreTimeSlotInsertUpdate Slot)
        {

            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0; int ResultCount = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                Slot.TenantId = authenticate.TenantId;
                Slot.ProgramCode = authenticate.ProgramCode;
                Slot.UserID = authenticate.UserMasterID;
                

                AppointmentCaller newAppointment = new AppointmentCaller();

                ResultCount = newAppointment.InsertUpdateTimeSlotSetting(new AppointmentServices(_connectioSting), Slot);


                statusCode = ResultCount > 0 ? (int)EnumMaster.StatusCode.Success :
                    ResultCount < 0 ? (int)EnumMaster.StatusCode.RecordAlreadyExists : (int)EnumMaster.StatusCode.InternalServerError;


                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = ResultCount;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        /// <summary>
        /// Insert/ Update HSTimeSlot Setting New
        /// </summary>
        /// <param name="StoreTimeSlotInsertUpdate"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateTimeSlotSettingNew")]
        public async Task<ResponseModel> UpdateTimeSlotSettingNew([FromBody]StoreTimeSlotInsertUpdate Slot)
        {

            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0; int ResultCount = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                Slot.TenantId = authenticate.TenantId;
                Slot.ProgramCode = authenticate.ProgramCode;
                Slot.UserID = authenticate.UserMasterID;


                AppointmentCaller newAppointment = new AppointmentCaller();

                ResultCount = await newAppointment.UpdateTimeSlotSettingNew(new AppointmentServices(_connectioSting), Slot);
                statusCode = ResultCount > 0 ? (int)EnumMaster.StatusCode.Success :(int)EnumMaster.StatusCode.InternalServerError;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = ResultCount;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        /// <summary>
        /// Delete Time Slot Master
        /// </summary>
        /// <param name="SlotID"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("DeleteTimeSlotMaster")]
        public ResponseModel DeleteTimeSlotMaster(int SlotID)
        {
            List<AlreadyScheduleDetail> alreadyScheduleDetails = new List<AlreadyScheduleDetail>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0; int ResultCount = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                AppointmentCaller newAppointment = new AppointmentCaller();

                ResultCount = newAppointment.DeleteTimeSlotMaster(new AppointmentServices(_connectioSting), SlotID,authenticate.TenantId,authenticate.ProgramCode);

                statusCode = ResultCount > 0 ? (int)EnumMaster.StatusCode.Success :
                  ResultCount < 0 ? (int)EnumMaster.StatusCode.RecordAlreadyExists : (int)EnumMaster.StatusCode.InternalServerError;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = ResultCount;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        [HttpPost]
        [Route("BulkDeleteTimeSlotMaster")]
        public async Task<ResponseModel> BulkDeleteTimeSlotMaster(string SlotIDs)
        {
            List<AlreadyScheduleDetail> alreadyScheduleDetails = new List<AlreadyScheduleDetail>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0; int ResultCount = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                AppointmentCaller newAppointment = new AppointmentCaller();

                ResultCount = await newAppointment.BulkDeleteTimeSlotMaster(new AppointmentServices(_connectioSting), SlotIDs, authenticate.TenantId, authenticate.ProgramCode);

                statusCode = ResultCount > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.InternalServerError;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = ResultCount;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Get HSTimeSlotMaster List
        /// </summary>
        /// <param name="SlotID"></param>
        /// <param name="StoreID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetStoreSettingTimeSlot")]
        public ResponseModel GetStoreSettingTimeSlot(int SlotID = 0, int StoreID = 0)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            List<StoreTimeSlotSettingModel> TimeSlotList = new List<StoreTimeSlotSettingModel>();
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                AppointmentCaller newAppointment = new AppointmentCaller();

                TimeSlotList = newAppointment.GetStoreSettingTimeSlot(new AppointmentServices(_connectioSting), authenticate.TenantId, authenticate.ProgramCode, SlotID, StoreID);

                statusCode = TimeSlotList.Count.Equals(0) ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = TimeSlotList;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        /// <summary>
        /// Get HSTimeSlotMaster List New
        /// </summary>
        /// <param name="SlotID"></param>
        /// <param name="StoreID"></param>
        /// <param name="Opdays"></param>
        /// <param name="SlotTemplateID"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("GetStoreSettingTimeSlotNew")]
        public async Task<ResponseModel> GetStoreSettingTimeSlotNew(int SlotID=0,  string StoreID = null, string Opdays = null, int SlotTemplateID = 0)
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            List<StoreTimeSlotSettingModel> TimeSlotList = new List<StoreTimeSlotSettingModel>();
           string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]); 
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                AppointmentCaller newAppointment = new AppointmentCaller();

                TimeSlotList = await newAppointment.GetStoreSettingTimeSlotNew(new AppointmentServices(_connectioSting), authenticate.TenantId,authenticate.ProgramCode, SlotID,  StoreID,  Opdays,  SlotTemplateID);

                statusCode = TimeSlotList.Count.Equals(0) ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = TimeSlotList;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }



        /// <summary>
        ///BulkUpload Slot
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("BulkUploadSlot")]
        public ResponseModel BulkUploadSlot(int RoleFor = 6)
        {
            List<TemplateBasedSlots> SlotsList = new List<TemplateBasedSlots>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            string fileName = "";
            string finalAttchment = "";
            string DownloadFilePath = string.Empty;
            string BulkUploadFilesPath = string.Empty;
            bool errorfilesaved = false;
            bool successfilesaved = false;
            string[] filesName = null;
            DataSet dataSetCSV = new DataSet();
            List<string> CSVlist = new List<string>();
            int count = 0;
            try
            {

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                var files = Request.Form.Files;

                if (files.Count > 0)
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        fileName += files[i].FileName.Replace(".", DateTime.Now.ToString("ddmmyyyyhhssfff") + ".") + ",";
                    }
                    finalAttchment = fileName.TrimEnd(',');
                }
                var Keys = Request.Form;

                #region FilePath

                string Folderpath = Directory.GetCurrentDirectory();
                filesName = finalAttchment.Split(",");


                BulkUploadFilesPath = Path.Combine(Folderpath, BulkUpload, UploadFiles, CommonFunction.GetEnumDescription((EnumMaster.FileUpload)RoleFor));
                DownloadFilePath = Path.Combine(Folderpath, BulkUpload, DownloadFile, CommonFunction.GetEnumDescription((EnumMaster.FileUpload)RoleFor));


                if (!Directory.Exists(BulkUploadFilesPath))
                {
                    Directory.CreateDirectory(BulkUploadFilesPath);
                }



                if (files.Count > 0)
                {

                    for (int i = 0; i < files.Count; i++)
                    {
                        using (var ms = new MemoryStream())
                        {
                            files[i].CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            MemoryStream msfile = new MemoryStream(fileBytes);
                            FileStream docFile = new FileStream(Path.Combine(BulkUploadFilesPath, filesName[i]), FileMode.Create, FileAccess.Write);
                            msfile.WriteTo(docFile);
                            docFile.Close();
                            ms.Close();
                            msfile.Close();
                            string s = Convert.ToBase64String(fileBytes);
                            byte[] a = Convert.FromBase64String(s);
                            // act on the Base64 data

                        }
                    }
                }

                #endregion

                AppointmentCaller newAppointment = new AppointmentCaller();
                StoreFileUploadCaller fileU = new StoreFileUploadCaller();
                dataSetCSV = CommonService.csvToDataSet(Path.Combine(BulkUploadFilesPath, filesName[0]));
                CSVlist = newAppointment.BulkUploadSlot(new AppointmentServices(_connectioSting), authenticate.TenantId,authenticate.ProgramCode,
                    authenticate.UserMasterID,  dataSetCSV);

                #region Create Error and Success files and  Insert in FileUploadLog

                string SuccessFileName = "SlotSuccessFile_" + DateTime.Now.ToString("ddmmyyyyhhssfff") + ".csv";
                string ErrorFileName = "SlotErrorFile_" + DateTime.Now.ToString("ddmmyyyyhhssfff") + ".csv";

                string SuccessFileUrl = !string.IsNullOrEmpty(CSVlist[0]) ?
                  rootPath + BulkUpload + "/" + DownloadFile + "/" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)RoleFor) + "/Success/" + SuccessFileName : string.Empty;
                string ErrorFileUrl = !string.IsNullOrEmpty(CSVlist[1]) ?
                    rootPath + BulkUpload + "/" + DownloadFile + "/" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)RoleFor) + "/Error/" + ErrorFileName : string.Empty;

                if (!string.IsNullOrEmpty(CSVlist[0]))
                    successfilesaved = CommonService.SaveFile(Path.Combine(DownloadFilePath, "Success", SuccessFileName), CSVlist[0]);

                if (!string.IsNullOrEmpty(CSVlist[1]))
                    errorfilesaved = CommonService.SaveFile(Path.Combine(DownloadFilePath, "Error", ErrorFileName), CSVlist[1]);

                count = fileU.CreateFileUploadLog(new StoreFileUploadService(_connectioSting), authenticate.TenantId, filesName[0], true,
                                 ErrorFileName, SuccessFileName, authenticate.UserMasterID, "Store_Slot", SuccessFileUrl, ErrorFileUrl, RoleFor);
                #endregion


                statusCode = (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = count;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }


        /// <summary>
        ///BulkUpload Slot
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("BulkUploadSlotNew")]
        public async  Task<ResponseModel> BulkUploadSlotNew(int RoleFor = 6)
        {
            List<TemplateBasedSlots> SlotsList = new List<TemplateBasedSlots>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            string fileName = "";
            string finalAttchment = "";
            string DownloadFilePath = string.Empty;
            string BulkUploadFilesPath = string.Empty;
            bool errorfilesaved = false;
            bool successfilesaved = false;
            string[] filesName = null;
            DataSet dataSetCSV = new DataSet();
            List<string> CSVlist = new List<string>();
            int count = 0;
            try
            {

                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));


                var files = Request.Form.Files;

                if (files.Count > 0)
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        fileName += files[i].FileName.Replace(".", DateTime.Now.ToString("ddmmyyyyhhssfff") + ".") + ",";
                    }
                    finalAttchment = fileName.TrimEnd(',');
                }
                var Keys = Request.Form;

                #region FilePath

                string Folderpath = Directory.GetCurrentDirectory();
                filesName = finalAttchment.Split(",");


                BulkUploadFilesPath = Path.Combine(Folderpath, BulkUpload, UploadFiles, CommonFunction.GetEnumDescription((EnumMaster.FileUpload)RoleFor));
                DownloadFilePath = Path.Combine(Folderpath, BulkUpload, DownloadFile, CommonFunction.GetEnumDescription((EnumMaster.FileUpload)RoleFor));


                if (!Directory.Exists(BulkUploadFilesPath))
                {
                    Directory.CreateDirectory(BulkUploadFilesPath);
                }



                if (files.Count > 0)
                {

                    for (int i = 0; i < files.Count; i++)
                    {
                        using (var ms = new MemoryStream())
                        {
                            files[i].CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            MemoryStream msfile = new MemoryStream(fileBytes);
                            FileStream docFile = new FileStream(Path.Combine(BulkUploadFilesPath, filesName[i]), FileMode.Create, FileAccess.Write);
                            msfile.WriteTo(docFile);
                            docFile.Close();
                            ms.Close();
                            msfile.Close();
                            string s = Convert.ToBase64String(fileBytes);
                            byte[] a = Convert.FromBase64String(s);
                            // act on the Base64 data

                        }
                    }
                }

                #endregion

                AppointmentCaller newAppointment = new AppointmentCaller();
                StoreFileUploadCaller fileU = new StoreFileUploadCaller();
                dataSetCSV = CommonService.csvToDataSet(Path.Combine(BulkUploadFilesPath, filesName[0]));
                CSVlist = await newAppointment.BulkUploadSlotNew(new AppointmentServices(_connectioSting), authenticate.TenantId, authenticate.ProgramCode,
                    authenticate.UserMasterID, dataSetCSV);

                #region Create Error and Success files and  Insert in FileUploadLog

                string SuccessFileName = "SlotSuccessFile_" + DateTime.Now.ToString("ddmmyyyyhhssfff") + ".csv";
                string ErrorFileName = "SlotErrorFile_" + DateTime.Now.ToString("ddmmyyyyhhssfff") + ".csv";

                string SuccessFileUrl = !string.IsNullOrEmpty(CSVlist[0]) ?
                  rootPath + BulkUpload + "/" + DownloadFile + "/" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)RoleFor) + "/Success/" + SuccessFileName : string.Empty;
                string ErrorFileUrl = !string.IsNullOrEmpty(CSVlist[1]) ?
                    rootPath + BulkUpload + "/" + DownloadFile + "/" + CommonFunction.GetEnumDescription((EnumMaster.FileUpload)RoleFor) + "/Error/" + ErrorFileName : string.Empty;

                if (!string.IsNullOrEmpty(CSVlist[0]))
                    successfilesaved = CommonService.SaveFile(Path.Combine(DownloadFilePath, "Success", SuccessFileName), CSVlist[0]);

                if (!string.IsNullOrEmpty(CSVlist[1]))
                    errorfilesaved = CommonService.SaveFile(Path.Combine(DownloadFilePath, "Error", ErrorFileName), CSVlist[1]);

                count = fileU.CreateFileUploadLog(new StoreFileUploadService(_connectioSting), authenticate.TenantId, filesName[0], true,
                                 ErrorFileName, SuccessFileName, authenticate.UserMasterID, "Store_Slot", SuccessFileUrl, ErrorFileUrl, RoleFor);
                #endregion


                statusCode = (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = count;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }


        /// <summary>
        /// Bulk Update Slots
        /// </summary>
        /// <param name="SlotsBulkUpdate"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("BulkUpdateSlots")]
        public async Task<ResponseModel> BulkUpdateSlots([FromBody]SlotsBulkUpdate Slots)
        {

            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0; int ResultCount = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                Slots.TenantId = authenticate.TenantId;
                Slots.ProgramCode = authenticate.ProgramCode;
                Slots.UserID = authenticate.UserMasterID;


                AppointmentCaller newAppointment = new AppointmentCaller();

                ResultCount = await newAppointment.BulkUpdateSlots(new AppointmentServices(_connectioSting), Slots);
                statusCode = ResultCount > 0 ? (int)EnumMaster.StatusCode.Success : (int)EnumMaster.StatusCode.InternalServerError;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = ResultCount;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        #endregion 

        #endregion
    }
}