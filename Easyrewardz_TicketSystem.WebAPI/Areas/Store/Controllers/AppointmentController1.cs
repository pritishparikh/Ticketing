using System;
using System.Collections.Generic;
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

                int result = newAppointment.UpdateAppoinment(new AppointmentServices(_connectioSting), appointment, authenticate.TenantId);
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


        /// <summary>
        /// Get HSTimeSlotMaster List
        /// </summary>
        /// <param name="SlotID"></param>
        /// <param name="StoreID"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("GetStoreSettingTimeSlot")]
        public ResponseModel GetStoreSettingTimeSlot(int SlotID=0, int StoreID=0)
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

                TimeSlotList = newAppointment.GetStoreSettingTimeSlot(new AppointmentServices(_connectioSting), authenticate.TenantId,authenticate.ProgramCode, SlotID, StoreID);

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