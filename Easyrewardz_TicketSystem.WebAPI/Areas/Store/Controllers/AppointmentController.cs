using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Easyrewardz_TicketSystem.WebAPI.Areas.Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class AppointmentController : ControllerBase
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string _connectioSting;
        private readonly string _radisCacheServerAddress;
        #endregion

        #region Cunstructor
        public AppointmentController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
        }
        #endregion

        #region Custom Methods 
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

                objAppointmentCount = newAppointment.GetAppointmentCountList(new AppointmentServices(_connectioSting), authenticate.TenantId, authenticate.UserMasterID);

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

                int result = newAppointment.updateAppoinment(new AppointmentServices(_connectioSting), appointment, authenticate.TenantId);
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

        [HttpPost]
        [Route("CreateAppointment")]
        public ResponseModel CreateAppointment([FromBody]AppointmentMaster appointmentMaster, bool IsSMS, bool IsLoyalty)
        {
            ResponseModel objResponseModel = new ResponseModel();
            List<AppointmentDetails> appointmentDetails = new List<AppointmentDetails>();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                appointmentMaster.CreatedBy = authenticate.UserMasterID;
                appointmentMaster.TenantID = authenticate.TenantId;
                AppointmentCaller newAppointment = new AppointmentCaller();

                appointmentDetails = newAppointment.CreateAppointment(new AppointmentServices(_connectioSting), appointmentMaster, IsSMS, IsLoyalty);

                statusCode =
              appointmentDetails.Count == 0 ?
                    (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = appointmentDetails;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }


        [HttpPost]
        [Route("CreateNonExistCustAppointment")]
        public ResponseModel CreateNonExistCustAppointment([FromBody]AppointmentMaster appointmentMaster, bool IsSMS, bool IsLoyalty)
        {
            ResponseModel objResponseModel = new ResponseModel();
            List<AppointmentDetails> appointmentDetails = new List<AppointmentDetails>();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                appointmentMaster.CreatedBy = authenticate.UserMasterID;
                appointmentMaster.TenantID = authenticate.TenantId;
                AppointmentCaller newAppointment = new AppointmentCaller();

                appointmentDetails = newAppointment.CreateNonExistCustAppointment(new AppointmentServices(_connectioSting), appointmentMaster, IsSMS, IsLoyalty);

                statusCode =
              appointmentDetails.Count == 0 ?
                    (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = appointmentDetails;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Search Appointment
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SearchAppointment")]
        public ResponseModel SearchAppointment(string searchText, string appointmentDate)
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

                objAppointmentList = newAppointment.SearchAppointment(new AppointmentServices(_connectioSting), authenticate.TenantId, authenticate.UserMasterID, searchText, appointmentDate);
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
        /// Generate OTP
        /// </summary>
        /// <param name="mobileNumber"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GenerateOTP")]
        public ResponseModel GenerateOTP(string mobileNumber)
        {         
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

                int isSent = newAppointment.GenerateOTP(new AppointmentServices(_connectioSting), authenticate.TenantId, authenticate.UserMasterID, mobileNumber);
                statusCode =
                isSent== 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = isSent;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Varify OTP
        /// </summary>
        /// <param name="otp"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("VarifyOTP")]
        public ResponseModel VarifyOTP(int otpID ,string otp)
        {
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

                int isSent = newAppointment.VarifyOTP(new AppointmentServices(_connectioSting), authenticate.TenantId, authenticate.UserMasterID, otpID, otp);
                statusCode =
                isSent == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = isSent;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }


        [HttpPost]
        [Route("GetTimeSlotDetail")]
        public ResponseModel GetTimeSlotDetail(string AppDate)
        {
            List<AlreadyScheduleDetail> alreadyScheduleDetails = new List<AlreadyScheduleDetail>();
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                AppointmentCaller newAppointment = new AppointmentCaller();

                alreadyScheduleDetails = newAppointment.GetTimeSlotDetail(new AppointmentServices(_connectioSting), authenticate.UserMasterID, authenticate.TenantId, AppDate);

                statusCode =
               alreadyScheduleDetails.Count == 0 ?
                    (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);


                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = alreadyScheduleDetails;
            }
            catch (Exception)
            {
                throw;
            }
            return objResponseModel;
        }

        /// <summary>
        /// Update Appointment
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateAppointment")]
        public ResponseModel UpdateAppointment([FromBody]CustomUpdateAppointment appointment)
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
                appointment.TenantID = authenticate.TenantId;
                appointment.UserID = authenticate.UserMasterID;
                appointment.ProgramCode = authenticate.ProgramCode;
                int result = newAppointment.AppoinmentStatus(new AppointmentServices(_connectioSting), appointment);
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
        /// Validate Mobile Number Exist
        /// </summary>
        /// <param name="mobileNumber"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ValidateMobilenoExist")]
        public ResponseModel ValidateMobilenoExist(string mobileNumber)
        {
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

                int isexist = newAppointment.ValidateMobileNo(new AppointmentServices(_connectioSting), authenticate.TenantId, authenticate.UserMasterID, mobileNumber);
                statusCode =
                isexist == 0 ?
                     (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

                statusMessage = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)statusCode);

                objResponseModel.Status = true;
                objResponseModel.StatusCode = statusCode;
                objResponseModel.Message = statusMessage;
                objResponseModel.ResponseData = isexist;

            }
            catch (Exception)
            {
                throw;
            }

            return objResponseModel;
        }

        /// <summary>
        /// Start Appointment Visit
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("StartAppointmentVisit")]
        public ResponseModel StartAppointmentVisit([FromBody]CustomUpdateAppointment appointment)
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
                appointment.TenantID = authenticate.TenantId;
                appointment.UserID = authenticate.UserMasterID;
                appointment.ProgramCode = authenticate.ProgramCode;
                int result = newAppointment.StartVisit(new AppointmentServices(_connectioSting), appointment);
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



        #region TimeSlotMaster CRUD


        /// <summary>
        /// Insert/ Update HSTimeSlotMaster
        /// </summary>
        /// <param name="StoreTimeSlotInsertUpdate"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("InsertUpdateTimeSlotMaster")]
        public ResponseModel InsertUpdateTimeSlotMaster([FromBody]StoreTimeSlotInsertUpdate Slot)
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

                Slot.TenantId = authenticate.TenantId;
                Slot.ProgramCode = authenticate.ProgramCode;
                Slot.CreatedBy = authenticate.UserMasterID;
                Slot.ModifyBy= authenticate.UserMasterID;

                AppointmentCaller newAppointment = new AppointmentCaller();

                ResultCount = newAppointment.InsertUpdateTimeSlotMaster(new AppointmentServices(_connectioSting), Slot);

                statusCode =  ResultCount .Equals(0) ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

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
        /// Delete HSTimeSlotMaster
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

                ResultCount = newAppointment.DeleteTimeSlotMaster(new AppointmentServices(_connectioSting), SlotID,authenticate.TenantId);

                statusCode = ResultCount.Equals(0) ? (int)EnumMaster.StatusCode.RecordNotFound : (int)EnumMaster.StatusCode.Success;

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
        /// <returns></returns>

        [HttpPost]
        [Route("GetStoreTimeSlotMasterList")]
        public ResponseModel GetStoreTimeSlotMasterList()
        {
            ResponseModel objResponseModel = new ResponseModel();
            int statusCode = 0;
            List<StoreTimeSlotMasterModel> TimeSlotList = new List<StoreTimeSlotMasterModel>();
           string statusMessage = "";
            try
            {
                string token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
                Authenticate authenticate = new Authenticate();
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                AppointmentCaller newAppointment = new AppointmentCaller();

                TimeSlotList = newAppointment.GetStoreTimeSlotMasterList(new AppointmentServices(_connectioSting), authenticate.TenantId,authenticate.ProgramCode);

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


        #endregion 

        #endregion
    }
}